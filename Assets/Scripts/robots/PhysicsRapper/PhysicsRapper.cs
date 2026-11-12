using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace robots.PhysicsRapper
{
    public static class PhysicsRapper
    {
        public static HashSet<IgnoreCollisionPairData> IgnoreCollisionPairs { get; private set; } = new HashSet<IgnoreCollisionPairData>();
        
        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="one">１つめ</param>
        /// <param name="other">２つめ</param>
        public static void AddIgnoreCollision(Collider one, Collider other)
        { 
            // IgnoreCollisionPairDataに イコールメソッドを実装する形で進める
            IgnoreCollisionPairs.Add(new IgnoreCollisionPairData(one, other));
            Physics.IgnoreCollision(one, other);
        }

        public static void RemoveIgnoreCollision(Collider one, Collider other)
        {
            IgnoreCollisionPairs.Remove(new IgnoreCollisionPairData(one, other));
        }
    }

    public struct IgnoreCollisionPairData
    {
        public HashSet<Collider> colliderPair ;

        /// <summary>
        /// ペアのオブジェクト
        /// </summary>
        /// <param name="one"></param>
        /// <param name="other"></param>
        public IgnoreCollisionPairData(Collider one,  Collider other)
        {
            colliderPair = new HashSet<Collider>
            {
                one,
                other
            };
        }
    }
}