using System;
using UnityEngine;

public class BrakeEffect : MonoBehaviour
{
    [SerializeField] private GameObject EffectPrefab;
    
    private Joint joint;
    private void OnEnable()
    {
        joint = GetComponent<Joint>();
    }

    private void OnJointBreak(float breakForce)
    {
        var anchor = joint.anchor + this.transform.position;
        var axis =joint.axis;
        var inst = Instantiate(EffectPrefab, anchor,Quaternion.LookRotation(axis), joint.connectedBody.transform );

        inst.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
    }
}
