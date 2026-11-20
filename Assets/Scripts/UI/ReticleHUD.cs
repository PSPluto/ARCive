
using System;
using UnityEngine;

public class ReticleHUD : UnityEngine.MonoBehaviour
{
    [SerializeField] private TargetProvider targetProvider;
    [SerializeField] private Camera _camera;
    [SerializeField,Range(0.01f , 1f)] private float time = 0.1f;

    private void Update()
    {
        transform.position = Vector3.Lerp(this.transform.position, targetProvider.TargetData.Item1, time);
        transform.rotation = _camera.transform.rotation;
    }
}