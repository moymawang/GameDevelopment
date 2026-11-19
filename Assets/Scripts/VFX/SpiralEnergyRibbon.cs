
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SpiralEnergyRibbon : MonoBehaviour
{
    [Header("Ribbon Shape")]
    public float radius = 0.75f;
    public float height = 2.2f;
    public float turns = 3.5f;
    public float ribbonWidth = 0.16f;
    [Range(32, 256)]
    public int segments = 180;

    [Header("Animation")]
    public float rotationSpeed = 100f;

    [Header("Color")]
    public Color bottomColor = new Color(1f, 0.25f, 0.75f, 1f);
    public Color topColor = new Color(0.3f, 0.8f, 1f, 1f);

    private void Start()
    {
        BuildRibbon();
    }

    private void Update()
    {
        transform.Rotate(
            Vector3.up,
            rotationSpeed * Time.deltaTime,
            Space.Self
        );
    }

    private void BuildRibbon()
    {
        Mesh mesh = new Mesh();
        mesh.name = "Spiral Energy Ribbon";

        Vector3[] vertices = new Vector3[(segments + 1) * 2];
        Vector2[] uvs = new Vector2[vertices.Length];
        Color[] colors = new Color[vertices.Length];
        int[] triangles = new int[segments * 6];

        for (int i = 0; i <= segments; i++)
        {
            float t = (float)i / segments;

            float angle = t * turns * Mathf.PI * 2f;

            Vector3 center = new Vector3(
                Mathf.Cos(angle) * radius,
                t * height,
                Mathf.Sin(angle) * radius
            );

            Vector3 tangent = new Vector3(
                -Mathf.Sin(angle) * radius * turns * Mathf.PI * 2f,
                height,
                Mathf.Cos(angle) * radius * turns * Mathf.PI * 2f
            ).normalized;

            Vector3 radial = new Vector3(
                Mathf.Cos(angle), 0f, Mathf.Sin(angle)
            );

            Vector3 side = Vector3.Cross(tangent, radial).normalized;

            float halfWidth = ribbonWidth * 0.5f;

            int index = i * 2;

            vertices[index] = center - side * halfWidth;
            vertices[index + 1] = center + side * halfWidth;

            uvs[index] = new Vector2(0f, t);
            uvs[index + 1] = new Vector2(1f, t);

            float fade = Mathf.Clamp01(
                Mathf.Min(t * 8f, (1f - t) * 8f)
            );

            Color color = Color.Lerp(bottomColor, topColor, t);
            color.a = fade;

            colors[index] = color;
            colors[index + 1] = color;

            if (i < segments)
            {
                int tri = i * 6;

                triangles[tri] = index;
                triangles[tri + 1] = index + 2;
                triangles[tri + 2] = index + 1;

                triangles[tri + 3] = index + 1;
                triangles[tri + 4] = index + 2;
                triangles[tri + 5] = index + 3;
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.colors = colors;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        GetComponent<MeshFilter>().mesh = mesh;
    }
}
