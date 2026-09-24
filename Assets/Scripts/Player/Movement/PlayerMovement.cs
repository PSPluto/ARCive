using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody rb;
    
    /// <summary>
    /// 引数の値をもとに移動するメソッド
    /// </summary>
    /// <param name="direction">入力方向</param>
    public void Move(Vector3 direction)
    {
        rb.linearVelocity = direction * moveSpeed;
    }
}
