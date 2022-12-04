using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class MeshDeformer : MonoBehaviour
{
    public bool recalculateNormals;
    public bool collisionDetection;

    Mesh mesh;
    MeshCollider meshCollider;
    List<Vector3> vertices;
    List<Vector3> initVertices;
    List<Vector3> initNormals;


    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        meshCollider = GetComponent<MeshCollider>();
        vertices = mesh.vertices.ToList();
        initVertices = mesh.vertices.ToList();
        initNormals = mesh.normals.ToList();
    }


    public void DeformWithInitNormal(Vector3 point, float radius, float stepRadius, float strength, float stepStrength, Vector3 hitNormal, bool down)
    {
        for (int i = 0; i < vertices.Count; i++)
        {
            if (down && Vector3.Distance(vertices[i], initVertices[i]) < 0.1f) { continue; }

            Vector3 vi = transform.TransformPoint(vertices[i]);
            float distance = Vector3.Distance(point, vi);
            float s = strength;
            for (float r = 0.0f; r < radius; r += stepRadius)
            {
                if (distance < r)
                {
                    //mesh.normals[i] // initNormals[i]
                    Vector3 rotatedNormal = transform.TransformDirection(mesh.normals[i]);
                    Vector3 deformation = rotatedNormal * s;
                    vertices[i] = transform.InverseTransformPoint(vi + deformation);
                    break;
                }
                s -= stepStrength;
            }
        }
        if (recalculateNormals)
            mesh.RecalculateNormals();

        if (collisionDetection)
        {
            meshCollider.sharedMesh = null;
            meshCollider.sharedMesh = mesh;
        }
        mesh.SetVertices(vertices);
    }

}
