using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// following the mesh generation tutorial from brackyes
// https://www.youtube.com/watch?v=eJEpeUH1EMg

public class MeshGenerator : MonoBehaviour
{
    // define our mesh
    Mesh mesh;

    // lists for verticies and triangles
    Vector3[] vertices;
    int[] triangles;

    // length and width for mesh gen
    public int x = 150;
    public int y = 150;

    void Start()
    {
        // we only call this on start
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        GenerateMesh(x,y);
        UpdateMesh();

    }

    void Update()
    {
    }

    void GenerateMesh(int xSize, int zSize)
    {
        // set vertices size
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        // populate vertices using xSize and zSize
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                vertices[i] = new Vector3(x, 0, z);
                i++;
            }
        }

        // set triangles size
        triangles = new int[x * y * 6];
        int v = 0;
        int t = 0;
        // populate triangles using offsets and xSize and ySize
        for (int j = 0; j < zSize; j++)
        {
            for (int i = 0; i < xSize; i++)
            {
                // using offset we are able to setup all triangles between vertices
                triangles[t + 0] = v + 0;
                triangles[t + 1] = v + xSize + 1;
                triangles[t + 2] = v + 1;
                triangles[t + 3] = v + 1;
                triangles[t + 4] = v + xSize + 1;
                triangles[t + 5] = v + xSize + 2;

                v++;
                t += 6;
            }
            v++;
        }
    }

    // update our mesh with new vertices and triangles
    void UpdateMesh() 
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}
