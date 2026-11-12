using UnityEngine;

public class PartsBreaker : MonoBehaviour
{
    [ContextMenu("破断の実行")]
    public void Break()
    {
        var joint = GetComponent<Joint>();
        joint.breakForce = 0f;
        joint.breakTorque = 0f;
    }
}
