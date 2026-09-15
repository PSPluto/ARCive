using UnityEngine;

public class PlayerAnimater : MonoBehaviour
{
    // キャッシュ
    private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
    //参照
    [Header("参照")]
    [SerializeField] private Animator playerAnimator;
    
    // メイン
    public void AssignMoveSpeed(float speed)
    {
        playerAnimator.SetFloat(MoveSpeed,speed);
    }
}
