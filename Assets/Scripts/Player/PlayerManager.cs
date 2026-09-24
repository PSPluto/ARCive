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
    
    // -- PlayerState
    public PlayerState currentState { get; private set; } =  PlayerState.Normal;
    
    // -- input
    public bool CanMove { get; private  set; } = true;

    // -- 初期化
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
    }
    
    // -- 子の機能
    /// <summary>
    /// 移動を呼ぶメソッド
    /// </summary>
    /// <param name="direction2D">2Dの入力ベクトル</param>
    public void Move(Vector2 direction2D)
    {
        if (CanMove) return;
        
        Vector3 direction3D = new Vector3(direction2D.x, 0f, direction2D.y);
        movement.Move(direction3D);
    }
    public void Fire()
    {
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
        CanMove = false;
    }

    public void ChangeState(PlayerState newState)
    {
        currentState = newState;
    }
    
    // -- 設定
    /// <summary>
    /// 移動を許可 / 拒否 の設定を更新
    /// </summary>
    /// <param name="enabled">true = 許可、false = 拒否</param>
    public void SetMoveEnabled(bool enabled)
    {
        CanMove = enabled;
    }


}
