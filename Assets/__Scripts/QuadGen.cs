using UnityEngine;

public class QuadGen : MonoBehaviour
{
    public float width = 1;
    public float height = 1;

    public void Start()
    {
        GameObject go = new GameObject("Mesh");
        go.transform.position = Vector3.zero;
        
        MeshRenderer meshRenderer = go.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));

        MeshFilter meshFilter = go.AddComponent<MeshFilter>();

        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(0, 0, 0),
            new Vector3(width, 0, 0),
            new Vector3(0, height, 0),
            new Vector3(width, height, 0)
        };

        int[] tris = new int[6]
        {
            // lower left triangle
            0, 2, 1,
            // upper right triangle
            1, 2, 3
        };

        Vector3[] normals = new Vector3[4]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };

        Vector2[] uv = new Vector2[4]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };

        mesh.vertices = vertices;
        mesh.triangles = tris;
        mesh.normals = normals;
        mesh.uv = uv;

        meshFilter.mesh = mesh;
    }
}