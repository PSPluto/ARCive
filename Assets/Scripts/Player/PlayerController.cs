using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("参照")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerAnimater animator;
    [Header("移動設定")]
    [SerializeField] private float maxMoveSpeed = 5.0f;
    [SerializeField] private float rotationSpeed = 10.0f;
    [SerializeField] private float acceleration = 10.0f;
    private Vector3 _moveVec;

    void FixedUpdate()
    {
        Vector2 movement = InputManager.Instance.Move;
        Vector3 targetDirection = new Vector3(movement.x, 0f, movement.y).normalized;
        Vector3 targetMoveVec = targetDirection * maxMoveSpeed;
        
        // 移動
        _moveVec = Vector3.MoveTowards(_moveVec, targetMoveVec, acceleration * Time.fixedDeltaTime);
        
        Vector3 nextPosition = rb.position + _moveVec * Time.fixedDeltaTime;
        rb.MovePosition(nextPosition);

        // アニメーション
        animator.AssignMoveSpeed(_moveVec.magnitude);
        
        // 回転
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion nextRotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(nextRotation);
        }
    }
}