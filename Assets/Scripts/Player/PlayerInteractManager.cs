using UnityEngine;

public class PlayerInteractManager : MonoBehaviour
{
    [Header("判定設定")]
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float distanceOffset;

    [Header("参照")]
    [SerializeField] private PlayerAnimator playerAnimator;

    private Collider[] _overlapColliders = new Collider[10];

    private void Update()
    {
        CheckCrate();
    }

    private void CheckCrate()
    {
        Vector3 center = transform.TransformPoint(Vector3.forward * distanceOffset + offset);

        int count = Physics.OverlapSphereNonAlloc(
            center,
            radius,
            _overlapColliders
        );

        for (int i = 0; i < count; i++)
        {
            if (_overlapColliders[i] != null && _overlapColliders[i].TryGetComponent(out BaseCrate targetObj))
            {
                if (InputManager.Instance.Interact)
                {
                    targetObj.OnInteract(this, playerAnimator);
                }
                targetObj.isHighlight = true;
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 center = transform.TransformPoint(Vector3.forward * distanceOffset + offset);

        Gizmos.DrawWireSphere(center, radius);
    }
}