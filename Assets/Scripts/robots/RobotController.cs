using UnityEngine;

public class RobotController : MonoBehaviour
{
    void Start()
    {
        MeshCollider[] childParts = GetComponentsInChildren<MeshCollider>();

        for (int i = 0; i < childParts.Length; i++)
        {
            for (int j = i + 1; j < childParts.Length; j++)
            {
                Physics.IgnoreCollision(childParts[i], childParts[j]);
            }
        }
    }
}
