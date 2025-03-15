using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ArcDrawerTest : MonoBehaviour
{
    public float radius = 2f;
    public float startAngle = 0f;
    public float endAngle = 90f;
    public int segments = 30;
    public Color fillColor = Color.red;

    private Mesh mesh;
    private MeshFilter meshFilter;

    void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        mesh = new Mesh();
        meshFilter.mesh = mesh;
    }

    void Update()
    {
        GenerateArcMesh();
    }

    void GenerateArcMesh()
    {
        mesh.Clear();

        int verticesCount = segments + 2; // Centre + (segments + 1) points
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