
using System;
using UnityEngine;

public class ReticleHUD : UnityEngine.MonoBehaviour
{
    [SerializeField] private TargetProvider targetProvider;

    private void Update()
    {
        transform.position = Vector3.Lerp(this.transform.position, targetProvider.TargetData.Item1, 0.075f);
    }
}