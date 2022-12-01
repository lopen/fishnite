using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// following the mesh generation tutorial from brackyes
// https://www.youtube.com/watch?v=eJEpeUH1EMg

public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    // length and width for mesh gen
    public int x = 10;
    public int y = 10;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        GenerateMesh(x,y);
        UpdateMesh();
    }

    void Update()
    {
        
    }

    void GenerateMesh(int x, int y)
    {
        vertices = new Vector3[(x + 1) * (y + 1)];

        // populate vertices using x and y
        int vert = 0;
        for (int i = 0; i <= x; i++)
        {
            for (int j = 0; j <= y; j++)
            {
                vertices[vert] = new Vector3(i, 0, j);
                vert++;
            }
        }

        triangles = new int[x * y];
        for (int i = 0; i < x; i++)
        {
            
        }
    }

    void UpdateMesh() 
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}
