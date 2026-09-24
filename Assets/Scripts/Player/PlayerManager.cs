using System;
using UnityEngine;

/// <summary>
/// プレイヤーの全アクションの管理
/// </summary>
public class PlayerManager : MonoBehaviour
{
    // -- 参照
    [SerializeField] private InputReader input;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerCombat combat;
    [SerializeField] private PlayerHealth health;

    [SerializeField] private StateSettingsDatabase stateSettingsDatabase;
    
    // -- PlayerState
    public PlayerState CurrentState { get; private set; } =  PlayerState.Normal;
    private Func<PlayerStateSettings> _stateSetting;
    
    // -- input
    public bool CanMove { get; private  set; } = true;

    // -- 初期化

    private void Awake()
    {
        _stateSetting = () => stateSettingsDatabase.Get(CurrentState);
    }

    private void OnEnable()
    {
        health.Died += OnDied;
    }

    private void OnDisable()
    {
        health.Died -= OnDied;
    }

    // -- メイン
    private void Update()
    {
        // 移動
        Move(input.Move);
        // 射撃
        if (input.Fire)
        {
            Fire();
        }

        if (input.Interact)
        {
            Interact();
        }
    }

    private void Interact()
    {
        if (_stateSetting().canInteract == false)
        {
            return;
        }
    }

    // -- 子の機能
    /// <summary>
    /// 移動を呼ぶメソッド
    /// </summary>
    /// <param name="direction2D">2Dの入力ベクトル</param>
    public void Move(Vector2 direction2D)
    {
        Vector3 direction3D = new Vector3(direction2D.x, 0f, direction2D.y);
        movement.Move(direction3D);
    }
    public void Fire()
    {
        if (_stateSetting().canInteract == false)
        {
            return;
        }
        combat.Fire();
    }
    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
    }
    
    // -- 自分の機能
    public void OnDied()
    {
        ChangeState(PlayerState.Dead);
    }

    public void ChangeState(PlayerState newState)
    {
        CurrentState = newState;
    }
    
    // -- 設定
    /// <summary>
    /// 移動を許可 / 拒否 の設定を更新
    /// </summary>
    public void SetMoveEnabled(bool enabled)
    {
        CanMove = enabled;
    }


}
