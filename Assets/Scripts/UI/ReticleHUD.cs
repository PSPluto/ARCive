
using System;
using UnityEngine;

public class ReticleHUD : UnityEngine.MonoBehaviour
{
    [SerializeField] private TargetProvider targetProvider;
    [SerializeField] private Camera _camera;

    private void Update()
    {
        transform.position = Vector3.Lerp(this.transform.position, targetProvider.TargetData.Item1, 0.075f);
        transform.rotation = _camera.transform.rotation;
    }
}