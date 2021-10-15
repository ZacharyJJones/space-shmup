using UnityEngine;
using DanmakU;

[RequireComponent(typeof(DanmakuCollider))]
public class PlayerHitByDanmaku : MonoBehaviour
{
    // Editor Fields
    public Player Player;
    public DanmakuCollider DanmakuCollider;

    private void Awake()
    {
        DanmakuCollider.OnDanmakuCollision += _onDanmakuCollision;
    }

    private void OnDestroy()
    {
        DanmakuCollider.OnDanmakuCollision -= _onDanmakuCollision;
    }

    private void _onDanmakuCollision(DanmakuCollisionList list)
    {
        // send damage to player reference.
        Player.TakeDamage(1);

        // grant a period of invulnerability afterwards.
        // Disable collider temporarily?

        // Destroy the Danmaku in questioon
        for (int i = 0; i < list.Count; i++)
        {
            list[i].Danmaku.Destroy();
        }
    }
}
