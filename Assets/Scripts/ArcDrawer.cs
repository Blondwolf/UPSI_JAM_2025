using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ArcDrawer : MonoBehaviour
{
    public Transform rotatingWheel;  // La roue qui tourne
    public float radius = 2f;
    public int segments = 30;
    public Color fillColor = Color.red;
    public InputActionReference pressArcAction;  // Référence à l'input action

    private Mesh mesh;
    public float startAngle;
    public float endAngle;
    private bool isDrawing = false;

    void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

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
        startAngle = rotatingWheel.eulerAngles.z;
        endAngle = startAngle;
        isDrawing = true;
    }

    void StopDrawing(InputAction.CallbackContext context)
    {
        isDrawing = false;
    }

    void Update()
    {
        if (isDrawing)
        {
            endAngle = rotatingWheel.eulerAngles.z;
            GenerateArcMesh();
        }
    }

    void GenerateArcMesh()
    {
        mesh.Clear();

        int verticesCount = segments + 2;
        Vector3[] vertices = new Vector3[verticesCount];
        int[] triangles = new int[segments * 3];
        Color[] colors = new Color[verticesCount];

        // Centre du cercle
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
}