using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanmakU;


[RequireComponent(typeof(DanmakuCollider))]
public class PlayerHitByDanmaku : MonoBehaviour
{
    // needs a reference to player data of some kind to tell it that damage was taken.

    public Player Player;
    public DanmakuCollider DanmakuCollider;

    void Awake()
    {
        DanmakuCollider = GetComponent<DanmakuCollider>();
        DanmakuCollider.OnDanmakuCollision += _onDanmakuCollision;
    }

    void OnDestroy()
    {
        // do i even need to worry about this?
        DanmakuCollider.OnDanmakuCollision -= _onDanmakuCollision;
    }


    void _onDanmakuCollision(DanmakuCollisionList list)
    {
        // send damage to player reference.
        Player.TakeDamage(1);

        // grant a period of invulnerability afterwards.
        

        // for now, just destroy them
        for (int i = 0; i < list.Count; i++)
        {
            list[i].Danmaku.Destroy();
        }
    }
}
