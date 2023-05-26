using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class Mesh_Controller : MonoBehaviour
{
    [Range(1.5f, 5f)]
    public float radius = 2f;
    [Range(1.5f, 5f)]
    public float deformationStrength = 2f;

    private Mesh mesh;
    private Vector3[] vertices, modifiedVerts;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        modifiedVerts = mesh.vertices;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    // Update is called once per frame
    void RecalculateMesh()
    {
        mesh.vertices = modifiedVerts;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            for (int v = 0; v < modifiedVerts.Length; v++)
            {
                Vector3 distance = modifiedVerts[v] - hit.point;

                float smoothingFactor = 2f;
                float force = (deformationStrength / (1f + hit.point.sqrMagnitude)) * 1000;

                if (distance.sqrMagnitude < radius)
                {
                    if (Input.GetMouseButton(0))
                    {
                        modifiedVerts[v] = modifiedVerts[v] + (Vector3.up * force * 5) / smoothingFactor;
                    }
                    else if (Input.GetMouseButton(1))
                    {
                        modifiedVerts[v] = modifiedVerts[v] + (Vector3.down * force * 5) / smoothingFactor;
                    }
                }
            }
        }
        RecalculateMesh();
    }

}
