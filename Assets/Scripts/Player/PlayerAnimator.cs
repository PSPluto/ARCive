using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    // キャッシュ
    private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
    private static readonly int InSearch = Animator.StringToHash("InSearch");
    private static readonly int Itemtype = Animator.StringToHash("Itemtype");

    //参照
    [Header("参照")]
    [SerializeField] private Animator playerAnimator;
    
    // メイン
    public void AssignMoveSpeed(float speed)
    {
        playerAnimator.SetFloat(MoveSpeed,speed);
    }

    public void AssignInSearch(bool isInSearch)
    {
        playerAnimator.SetBool(InSearch,isInSearch);
    }

    public void AssignItemType(int itemType)
    {
        playerAnimator.SetFloat(Itemtype, itemType);
    }
}
