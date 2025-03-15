using UnityEngine;
using UnityEngine.InputSystem;

public class ArcDrawer : MonoBehaviour
{
    public WheelRotateController rotatingWheel;  // La roue qui tourne
    public float radius = 1.4f;
    public int segments = 30;
    public Color fillColor = Color.red;
    public Material splitMaterial;  // Matériau personnalisé pour les arcs
    public InputActionReference pressArcAction;

    public float startAngle;
    public float endAngle;
    private bool isDrawing = false;
    private GameObject currentArcObject;
    public GameObject parent;

    int count = 0;

    void OnEnable()
    {
        pressArcAction.action.started += StartDrawing;
        pressArcAction.action.canceled += StopDrawing;
        pressArcAction.action.Enable();
    }

    void OnDisable()
    {
        pressArcAction.action.started -= StartDrawing;
        pressArcAction.action.canceled -= StopDrawing;
        pressArcAction.action.Disable();
    }

    void StartDrawing(InputAction.CallbackContext context)
    {
        currentArcObject = new GameObject("ArcSegment");
        currentArcObject.transform.SetParent(transform);
        currentArcObject.transform.localPosition = Vector3.zero - Vector3.forward * 0.02f * count;
        //currentArcObject.transform.rotation = Quaternion.Euler(0, 0, 90);

        MeshFilter meshFilter = currentArcObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = currentArcObject.AddComponent<MeshRenderer>();

        // Appliquer le matériau personnalisé (ou un fallback)
        if (splitMaterial != null)
        {
            meshRenderer.material = splitMaterial;
        }
        else
        {
            meshRenderer.material = new Material(Shader.Find("Unlit/Color"));
            meshRenderer.material.color = fillColor;
        }

        meshFilter.mesh = new Mesh();

        startAngle = 90;
        endAngle = startAngle;
        isDrawing = true;

        count++;
    }

    void StopDrawing(InputAction.CallbackContext context)
    {
        isDrawing = false;
        currentArcObject.transform.SetParent(parent.transform);
    }

    void Update()
    {
        if (isDrawing && currentArcObject != null)
        {
            endAngle -= rotatingWheel.RotationSpeed * Time.deltaTime; //+rotatingWheel.eulerAngles.z;
            GenerateArcMesh(currentArcObject.GetComponent<MeshFilter>().mesh);
        }
    }

    void GenerateArcMesh(Mesh mesh)
    {
        mesh.Clear();

        int verticesCount = segments + 2;
        Vector3[] vertices = new Vector3[verticesCount];
        int[] triangles = new int[segments * 3];
        Color[] colors = new Color[verticesCount];

        vertices[0] = Vector3.zero;
        colors[0] = fillColor;

        float angleStep = (endAngle - startAngle) / segments;

        for (int i = 0; i <= segments; i++)
        {
            float angle = startAngle + angleStep * i;
            float radian = angle * Mathf.Deg2Rad;
            vertices[i + 1] = new Vector3(Mathf.Cos(radian) * radius, Mathf.Sin(radian) * radius, 0);
            colors[i + 1] = fillColor;

            if (i < segments)
            {
                int startIndex = i * 3;
                triangles[startIndex] = 0;
                triangles[startIndex + 1] = i + 1;
                triangles[startIndex + 2] = i + 2;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.RecalculateNormals();
    }

    public void SelectNote(int value, Color color)
    {
        fillColor = color;
    }
}