using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private TargetProvider targetProvider;
    [SerializeField] private Rigidbody myRigidBody;

    public void UpdateAim()
    {
        var direction = Vector3.Scale(targetProvider.TargetData.Item1, new Vector3(1, 0, 1)) - Vector3.Scale(transform.position, new Vector3(1, 0, 1));
        myRigidBody.MoveRotation(Quaternion.LookRotation(direction));
    }
}
