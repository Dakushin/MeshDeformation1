using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Plane : MonoBehaviour
{
    public int res;
    int size;
    public int taille;
    private Mesh m;
    private Vector3[] vertices;
    private int[] triangles;
    // Start is called before the first frame update
    void Start()
    {
        res = res - 1;
        m = new Mesh();
        GetComponent<MeshFilter>().mesh = m;

        vertices = new Vector3[(res +1) * (res +1)];
        int i = 0;
        for(int z = 0; z <= res; z++)
        {
            for (int x = 0; x <= res; x++)
            {
                vertices[i] = new Vector3(x * taille / res, 0, z * taille / res );
                i++;
            }
        }

        //triangles = new int[3 * (int)(2 * Mathf.Pow(res, 2))];
        triangles = new int[6 * res * res];
        int vert = 0;
        int tris = 0;
        
        Debug.Log(vertices.Length);
        Debug.Log(triangles.Length);
        for (int z = 0; z < res ; z++)
        {
            for (int x = 0; x < res ; x++)
            {
                triangles[tris + 0] = vert;
                triangles[tris + 1] = vert + res + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + res + 1;
                triangles[tris + 5] = vert + res + 2;


                vert++;
                tris += 6;
            }
            vert++;
        }

        m.vertices = vertices;
        m.triangles = triangles;

    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
