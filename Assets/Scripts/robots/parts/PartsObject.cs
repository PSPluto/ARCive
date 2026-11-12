using System;
using UnityEngine;

namespace robots.parts
{
    public class PartsObject : MonoBehaviour
    {
        private Joint joint;
        private void OnEnable()
        {
            joint = GetComponent<Joint>();
        }

        private void OnJointBreak(float breakForce)
        {
            // Physics.GetIgnoreCollision();
        }
    }
}