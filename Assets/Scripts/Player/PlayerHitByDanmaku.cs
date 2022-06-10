using System;
using System.ComponentModel;
using UnityEngine;
using DanmakU;

public class PlayerHitByDanmaku : MonoBehaviour
{
    // Editor Fields
    public Player Player;
    public DanmakuCollider DanmakuCollider;
    public DanmakuCollider DanmakuShieldCollider;

    [Category("On-Hit Shield")]
    public float ShieldTime;

    public float EndShieldScale;

    private bool _shieldActive;
    private float _startingShieldScale;


    private void Awake()
    {
        DanmakuCollider.OnDanmakuCollision += _onDanmakuCollision;
        DanmakuShieldCollider.OnDanmakuCollision += _onDanmakuShieldCollision;
    }

    private void Start()
    {
        _startingShieldScale = DanmakuShieldCollider.transform.localScale.x;
        _shieldActive = false;
    }

    private void OnDestroy()
    {
        DanmakuCollider.OnDanmakuCollision -= _onDanmakuCollision;
        DanmakuShieldCollider.OnDanmakuCollision -= _onDanmakuShieldCollision;
    }

    private void _onDanmakuCollision(DanmakuCollisionList list)
    {
        if (_shieldActive)
            return;

        // send damage to player reference.
        Player.TakeDamage(1);

        // Destroy the Danmaku in question
        _destroyCollidingDanmaku(list);

        // Scale shield, to get rid of other bullets on-screen.
        // -- scaling shield will fit with the intended shockwave visual effect best.
        _shieldActive = true;
        StartCoroutine(Utils.DoOverTime(
            ShieldTime,
            (t) =>
            {
                float scale = Mathf.Lerp(_startingShieldScale, EndShieldScale, t);
                _scaleDanmakuShield(scale);
            },
            () =>
            {
                _scaleDanmakuShield(_startingShieldScale);
                _shieldActive = false;
            }
        ));
    }

    private void _onDanmakuShieldCollision(DanmakuCollisionList list) => _destroyCollidingDanmaku(list);

    private void _scaleDanmakuShield(float scaleVal)
    {
        DanmakuShieldCollider.transform.localScale = new Vector3(scaleVal, scaleVal, 1);
    }

    private void _destroyCollidingDanmaku(DanmakuCollisionList list)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            list[i].Danmaku.Destroy();
        }
    }


}
