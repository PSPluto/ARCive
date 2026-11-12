using System;
using UnityEditor;
using UnityEngine;

public class TensionView : MonoBehaviour
{
    [SerializeField] private float textPosOffset;
    private Joint joint;
    private void Start()
    {
        joint =  this.gameObject.GetComponent<Joint>();
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            return;
        }
        #if UNITY_EDITOR
                // オブジェクトの少し上にテキストを表示
                Vector3 textPos = transform.position + Vector3.up * textPosOffset;
                Handles.Label(textPos, $"Force:{joint.currentForce.magnitude :F1}/{joint.breakForce :f1} Torque: {joint.currentTorque.magnitude :F1}/{joint.breakTorque :f1}");
        #endif
    }
}
