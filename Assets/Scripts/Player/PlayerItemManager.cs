using System;
using UnityEngine;

public class PlayerItemManager : MonoBehaviour
{
    public Itemdata itemdata;
    public PlayerAnimator animator;
    public PlayerController controller;

    private void Update()
    {
        animator.AssignItemType((int)itemdata.itemAnimType);
    }
}
