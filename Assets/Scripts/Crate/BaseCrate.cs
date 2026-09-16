using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCrate : MonoBehaviour
{
    public virtual Action[] OnInteract(PlayerController player)
    {
        return new Action[]
        {
            () => {player.animation},
            () => {},
            () => {}
            
        };
    }
    

}