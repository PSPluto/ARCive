using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 50f;
    [SerializeField] private Rigidbody rb;
    
    /// <summary>
    /// 引数の値をもとに移動するメソッド
    /// </summary>
    /// <param name="direction">入力方向</param>
    public void Move(Vector3 direction)
    {
        float linearVelocityY  = rb.velocity.y;
        rb.linearVelocity = direction * moveSpeed +  new Vector3(0, linearVelocityY, 0);
    }
}
