using System;
using DefaultNamespace;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private StateChangeRelay stateChangeRelay;
    // Rayのスタート位置
    [SerializeField] private Transform rayStartPos;
    // Rayの向きベクトルのyオフセット
    [SerializeField] private float rayDirectionYOffset = -0.5f;
    
    // ターゲットオブジェクトを保持する変数
    private GameObject _targetObj;
    private TargetObjectData  _targetObjectData;

    

    public void Interact(PlayerInventory  playerInventory)
    { 
        if (_targetObj == null) return;
        if (_targetObjectData.PlayerStateChangeRequester !=  null)
        {
            // いれた
            _targetObjectData.PlayerStateChangeRequester.SetStateChangeRelay(stateChangeRelay);
        }
        
        _targetObjectData.Interaction.Interact(playerInventory);
    }

    private void Update()
    {
        if(FindInteractableObject(out _targetObj))
        { 
            // もし値を更新すべきだと判定されたら値の更新   
            _targetObjectData = GenInteractionTargetObjectData(_targetObj);
        }
        _targetObjectData.MaterialHighlight?.HighlightThisFrame();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="targetInteractableObject">結果のゲームオブジェクトが入る</param>
    /// <returns>値に更新をかけるべきか</returns>
    private bool FindInteractableObject( out GameObject targetInteractableObject )
    {
        Ray ray = new Ray(rayStartPos.position, (transform.forward + new Vector3(0, rayDirectionYOffset, 0)).normalized);
        Debug.DrawLine(rayStartPos.position, rayStartPos.position + (transform.forward + new Vector3(0, rayDirectionYOffset, 0)).normalized * 3f, Color.red);

        if (Physics.Raycast(ray, out var hit, 3f))
        {
            // もし 直前に変数に上がっていたオブジェクトと同じであれば、即リターン
            if (hit.collider.gameObject == _targetObj)
            {
                // Rayに当たったオブジェクトが以前通ったものと同じだった場合。
                targetInteractableObject = hit.collider.gameObject;
                return false;
            };
            
            if (hit.collider.gameObject.TryGetComponent<IInteraction>(out var _))
            {
                // Rayに当たったオブジェクトが以前のオブジェクトとは違い、かつIInteractionを持っていた場合
                targetInteractableObject = hit.collider.gameObject;
                return true;
            }
        }
        // Rayに何も当たらなかった、または IInteractionを持っていなかった場合
        targetInteractableObject = null;
        return true;
    }

    private TargetObjectData GenInteractionTargetObjectData(GameObject target)
    {
        if (target == null)
        {
            return new TargetObjectData(null, null,null);
        }
        return new TargetObjectData(
            target.GetComponent<IInteraction>(),
            target.GetComponent<IPlayerStateChangeRequester>(),
            target.GetComponent<MaterialHighlight>());
    }
}

public struct TargetObjectData
{
    public IInteraction Interaction;
    public IPlayerStateChangeRequester PlayerStateChangeRequester;
    public MaterialHighlight MaterialHighlight;


    public TargetObjectData(IInteraction interaction, IPlayerStateChangeRequester playerStateChangeRequester, MaterialHighlight materialHighlight)
    {
        Interaction = interaction ?? null;
        PlayerStateChangeRequester = playerStateChangeRequester ?? null;
        MaterialHighlight = materialHighlight ?? null;
    }
}
