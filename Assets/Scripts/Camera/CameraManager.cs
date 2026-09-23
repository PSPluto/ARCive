using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private float smoothTime = 0.5f;
    private Vector3 _currentVelocity;
    

    private Vector3　_targetCameraPos;
    private RaycastHit _hit;

    private void Update()
    {
        // // プレイヤーからのレイ。間に障害物があれば
        // Ray ray = new Ray(playerTransform.position, ((playerTransform.position + cameraOffset) - playerTransform.position).normalized);
        // _targetCameraPos = Physics.Raycast(ray, out _hit, cameraOffset.magnitude)
        //     ? _hit.point
        //     : playerTransform.position + cameraOffset;
        _targetCameraPos = playerTransform.position + cameraOffset;
        // ref : 変数の参照を渡す修飾子
        // デフォだと値のコピーが渡されるが、refつければ、元の変数自体がそのまま使われる。（関数中での変更がそとでも反映されるみたいなかんじ）
        this.transform.position = Vector3.SmoothDamp(this.transform.position, _targetCameraPos, ref _currentVelocity, smoothTime);
    }
}
