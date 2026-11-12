using System;
using UnityEngine;

public class BrakeEffect : MonoBehaviour
{
    [SerializeField] private GameObject[] EffectPrefabs;
    
    private Joint joint;
    private void OnEnable()
    {
        joint = GetComponent<Joint>();
    }

    private void OnJointBreak(float breakForce)
    {
        var anchor = joint.anchor + this.transform.position;
        var axis =joint.axis;
        foreach (var effectPref in EffectPrefabs)
        {
            var inst = Instantiate(effectPref, anchor,Quaternion.LookRotation(axis), joint.connectedBody.transform );
            inst.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }
    }
}
