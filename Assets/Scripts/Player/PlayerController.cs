using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("参照")]
    [SerializeField] private CharacterController playerController;
    [SerializeField] private PlayerAnimater animator;
    [Header("移動設定")]
    [SerializeField] private float maxMoveSpeed = 5.0f;
    [SerializeField] private float rotationSpeed = 10.0f;
    [SerializeField] private float acceleration = 10.0f; // 加速・減速の滑らかさ
    [Header("データ")] 
    private Vector3 _moveVec;

    void Update()
    {
        Vector2 movement = InputManager.Instance.Move;
        Vector3 targetDirection = new Vector3(movement.x, 0f, movement.y).normalized;
        Vector3 targetMoveVec = targetDirection * maxMoveSpeed;
        
        // 移動ベクトル補間
        _moveVec = Vector3.MoveTowards(_moveVec, targetMoveVec, acceleration * Time.deltaTime);
        playerController.Move(_moveVec * Time.deltaTime);

        // アニメーション
        animator.AssignMoveSpeed(_moveVec.magnitude);
        
        // 回転
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}