using System;
using UnityEngine;

public class PlayerInteractManager : MonoBehaviour
{
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float distanceOffset;
    private Collider[] _overlapColliders;

    
    private void Update()
    {
        CheckCrate();
    }

    private void CheckCrate()
    {
        Physics.OverlapSphereNonAlloc((transform.forward * distanceOffset) + offset, radius, _overlapColliders);
        foreach (var collider in _overlapColliders)
        {
            //受け渡し処理
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((transform.forward * distanceOffset) + offset, radius );
    }
}
