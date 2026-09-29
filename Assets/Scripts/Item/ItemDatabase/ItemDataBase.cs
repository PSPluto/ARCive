using System;
using Unity.VisualScripting;
using UnityEngine;

// アイテムの静的データを格納する
public class ItemDataBase : MonoBehaviour
{
    public static ItemDataBase Instance;
    private void Awake()
    {
        Instance = this;
    }

    public ItemData[]  itemDataAllay;
}
