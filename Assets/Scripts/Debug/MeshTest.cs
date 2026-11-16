using UnityEngine;

public class MeshTest : MonoBehaviour
{
    [ContextMenu("Dump Mesh Vertices")]
    void DumpVertices()
    {
        MeshFilter filter = GetComponent<MeshFilter>();

        if (filter == null || filter.sharedMesh == null)
        {
            Debug.LogError("Mesh not found");
            return;
        }

        Mesh mesh = filter.sharedMesh;

        Debug.Log($"Mesh: {mesh.name}");
        Debug.Log($"Vertex Count: {mesh.vertexCount}");
        Vector3 size = mesh.bounds.size;

        Debug.Log($"Bounds: ({size.x:F8}, {size.y:F8}, {size.z:F8})");

        for (int i = 0; i < mesh.vertexCount; i++)
        {
            Vector3 v = mesh.vertices[i];

            Debug.Log(
                $"Vertex {i}: ({v.x:F8}, {v.y:F8}, {v.z:F8})"
            );
        }

        Debug.Log("Triangles:");

        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            Debug.Log(
                $"{mesh.triangles[i]}, " +
                $"{mesh.triangles[i + 1]}, " +
                $"{mesh.triangles[i + 2]}"
            );
        }
    }
}
