using System;
using DefaultNamespace;
using UnityEngine;

/// <summary>
/// プレイヤーの全アクションの管理
/// </summary>
public class Player : MonoBehaviour
{
    // ==================== 参照 ====================
    [SerializeField] private InputReader input;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerCombat combat;
    [SerializeField] private PlayerHealth health;
    [SerializeField] private PlayerInteraction interact;
    [SerializeField] private StateSettingsDatabase stateSettingsDatabase;
    [SerializeField] private PlayerAim aim;
    [SerializeField] private PlayerInventory inventory;

    // ==================== State ====================
    public PlayerState currentState { get; private set; } = PlayerState.Normal;

    // ==================== 初期化 ====================
    private void OnEnable()
    {
        health.Died += OnDied;
    }

    private void OnDisable()
    {
        health.Died -= OnDied;
    }

    // ==================== メインループ ====================
    private void Update()
    {
        // 移動
        Move(input.Move);
        // 射撃
        if (input.Fire)
        {
            Fire();
        }
        // インタラクト
        if (input.Interact)
        {
            Interact();
        }

        UpdateAim();
    }

    // ==================== 子の機能呼び出し ====================

    /// <summary>
    /// インタラクト
    /// </summary>
    private void Interact()
    {
        if (currentStateSettings.canInteract == false)
        {
            return;
        }
        // インタラクトを実行
        interact.Interact(inventory);
    }

    /// <summary>
    /// 移動を呼ぶメソッド
    /// </summary>
    /// <param name="direction2D">2Dの入力ベクトル</param>
    public void Move(Vector2 direction2D)
    {
        if (currentStateSettings.canMove == false)
        {
            return;
        }
        Vector3 direction3D = new Vector3(direction2D.x, 0f, direction2D.y);
        movement.Move(direction3D);
    }
    
    private void UpdateAim()
    {
        if(currentStateSettings.canUpdateAim == false) return;
        aim.UpdateAim();
    }

    /// <summary>
    /// 銃の射撃
    /// </summary>
    public void Fire()
    {
        if (currentStateSettings.canFire == false)
        {
            return;
        }

        combat.Fire();
    }
    
    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="damage">ダメージ量</param>
    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
    }

    // ==================== 自分の機能 ====================
    public void OnDied()
    {
        ChangeState(PlayerState.Dead);
    }

    public void ChangeState(PlayerState? newState)
    {
        if (newState == null) return; 
        currentState = (PlayerState)newState;
    }

    private PlayerStateSettings currentStateSettings
    {
        get => stateSettingsDatabase.Get(currentState);
    }
    // ==================== 設定 ====================
}