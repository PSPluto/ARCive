using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// アイテムの静的データを格納する
public class ItemDataBase : MonoBehaviour
{
    // シングルトン化 {
    public static ItemDataBase Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // }
    
    // 宣言 ｛
    public readonly Dictionary<string, ItemData> ItemDataDictionary = new Dictionary<string, ItemData>();
    
    // }
}
