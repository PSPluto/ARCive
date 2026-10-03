using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "new ItemInstanceFactoryDatabase", menuName = "ItemInstanceFactoryDatabase", order = 0)]
    public class ItemInstanceFactoryDatabase : ScriptableObject
    {
        //TODO : Key:ID、Value:重み(レアリティの)を渡すとnew 式が入ったラムダが帰るメソッドを持ったSO が入るDBがここに
    }
}