using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Rayのスタート位置
    [SerializeField] private Transform rayStartPos;
    // Rayの向きベクトルのyオフセット
    [SerializeField] private float rayDirectionYOffset = -0.5f;
    
    // ターゲットオブジェクトを保持する変数
    private IInteraction _targetObject;

    

    public PlayerState? Interact()
    {   
        Debug.Log("インタラクト");
        return _targetObject?.RequestState;
    }

    private void Update()
    {
        _targetObject = FindInteractableObject();
    }

    private IInteraction FindInteractableObject()
    {
        Ray ray = new Ray(rayStartPos.position, (transform.forward + new Vector3(0, rayDirectionYOffset, 0)).normalized);
        Debug.DrawLine(rayStartPos.position, rayStartPos.position + (transform.forward + new Vector3(0, rayDirectionYOffset, 0)).normalized * 3f, Color.red);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3f))
        {
            Debug.unityLogger.Log($"インタラクト可能なオブジェクトを見つけました{hit.collider.gameObject.name}");
            IInteraction hitInteraction = hit.collider.GetComponent<IInteraction>();
            return hitInteraction;
        }
        return null;
    }
    
    
}
