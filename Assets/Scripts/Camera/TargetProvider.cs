using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetProvider : MonoBehaviour
{
    private Camera _camera;
    public Tuple<Vector3, Collider> TargetData{get ; private set;}

    private void Start()
    {
        _camera = this.GetComponent<Camera>();
    }

    private void Update()
    {
        TargetData = TryGetMouseRaycastHit();
    }

    private Tuple<Vector3, Collider> TryGetMouseRaycastHit()
    {
        Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out var hit))
        {
            return new (hit.point, hit.collider);
        }

        return new (Vector3.zero, null);
    }
}
