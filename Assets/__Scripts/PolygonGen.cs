using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonGen : MonoBehaviour
{
    [Header("Polygon Settings")]
    public string label = "Joe";
    public GameObject Joe;
    [Range(3, 18)]
    public int vertexAmount = 3;
    [Range(1.0f, 10.0f)]
    public float radius = 1;

    [Range(0, 120)]
    public float phase = 0;
    public bool withAHole = false;
    [Range(1, 100f)]
    public float innerRadiusFraction = 80f;
    public GameObject parent;
    public Material material;

    [Header("Collider Settings")]
    public bool hasCollider = false;
    
    
    private GameObject go;
    private MeshFilter meshFilter;
    private PolygonCollider2D coll;
    private void Start()
    {
        if (Joe != null)
        {
            go = Instantiate<GameObject>(Joe);
        }
        else
        {
            go = new GameObject(label);
        }

        if (parent != null)
        {
            go.transform.SetParent(parent.transform);
        }
        go.transform.localPosition = Vector3.zero;
        
        MeshRenderer meshRenderer = go.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial =
            (material == null) ? new Material(Shader.Find("Standard")) : material;
        
        meshFilter = go.AddComponent<MeshFilter>();
        
        if(hasCollider)
            coll = go.AddComponent<PolygonCollider2D>();

       
    }


    private void SolidPolyGen()
    {
        
        Mesh mesh = new Mesh();
        Vector3[] verts = new Vector3[vertexAmount];
        Vector2[] uv = new Vector2[vertexAmount];
        int[] tris = new int[3 * (vertexAmount - 2)];
        Vector3[] normals = new Vector3[vertexAmount];
        
        
        float x, y;
        float angleStep = 360.0f / vertexAmount;
        for (int i = 0; i < vertexAmount; i++)
        {
            x = Mathf.Cos(((i * angleStep + phase) * Mathf.Deg2Rad));
            y = Mathf.Sin(((i * angleStep + phase) * Mathf.Deg2Rad));

            verts[vertexAmount - i - 1] = new Vector3(x, y) * radius;
            uv[vertexAmount - i - 1] = new Vector3(x, y);
            normals[i] = -Vector3.forward;
        }

        for (int i = 0; i < vertexAmount - 2; i++)
        {
            tris[i * 3] = i + 2;
            tris[i * 3 + 1] = 0;
            tris[i * 3 + 2] = i + 1;
        }
        
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.normals = normals;
        mesh.uv = uv;

        meshFilter.mesh = mesh;
    }

    private void HoledPolyGen()
    {
         
        Mesh mesh = new Mesh();
        Vector3[] verts = new Vector3[vertexAmount * 2];
        Vector2[] uv = new Vector2[vertexAmount * 2];
        int[] tris = new int[vertexAmount * 6];
        Vector3[] normals = new Vector3[vertexAmount * 2];
        
        
        float x, y;
        float angleStep = 360.0f / vertexAmount;
        
        for (int i = 0; i < vertexAmount; i++)
        {
            x = Mathf.Cos((i * angleStep + phase)  * Mathf.Deg2Rad);
            y = Mathf.Sin((i * angleStep + phase) * Mathf.Deg2Rad);

            verts[vertexAmount - i - 1] = new Vector3(x, y) * radius;
            uv[vertexAmount - i - 1] = new Vector3(x, y);
            normals[i] = -Vector3.forward;
            
            verts[vertexAmount * 2 - i - 1] = new Vector3(x, y) * (radius * innerRadiusFraction / 100f);
            uv[vertexAmount * 2- i - 1] = new Vector3(x, y) * (innerRadiusFraction / 100);
            normals[vertexAmount + i] = -Vector3.forward;
        }

        for (int i = 0; i < vertexAmount; i++)
        {
            tris[i * 3] = i;
            tris[i * 3 + 1] = (i + 1) % vertexAmount;
            tris[i * 3 + 2] = i + vertexAmount;

            tris[vertexAmount * 3 + i * 3] = i;
            tris[vertexAmount * 3 + i * 3 + 1] = i + vertexAmount;
            tris[vertexAmount * 3 + i * 3 + 2] = (i + vertexAmount - 1) % vertexAmount + vertexAmount;

        }
        
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.normals = normals;
        mesh.uv = uv;

        meshFilter.mesh = mesh;
    }

    private void AddCollider()
    {

        Vector2[] verts = new Vector2[vertexAmount];
        float x, y;
        float angleStep = 360.0f / vertexAmount;
        for (int i = 0; i < vertexAmount; i++)
        {
            x = Mathf.Cos(((i * angleStep + phase) * Mathf.Deg2Rad));
            y = Mathf.Sin(((i * angleStep + phase) * Mathf.Deg2Rad));

            verts[vertexAmount - i - 1] = new Vector2(x, y) * radius;
        }

        coll.points = verts;
    }
        
    
    private void Update()
    {
        if (!withAHole)
        {
            SolidPolyGen();
        }
        else
        {
            HoledPolyGen();
        }

        if (hasCollider)
        {
            AddCollider();
        }
        
    }
}
