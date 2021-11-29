using System;
using UnityEngine;

public class Player2DMovement : MonoBehaviour
{
    // Intended as a subclass for other forms of player movement.
    // Point is to put this on the player prefab, and then load settings or prefs...
    // ... to get the preferred option. For now, just default into keyboard control.

    // Editor Fields
    [Header("Control")]
    public BoxCollider2D MovementArea;
    [Header("Speed Settings")]
    public float MoveSpeed;
    [Range(0,1)]
    public float SlowMoMult;

    protected virtual void Start()
    {
        var keyboardMovement = gameObject.AddComponent<PlayerKeyboardMovement>();
        keyboardMovement.ImportSettings(this);
        Destroy(this);
    }

    public void ImportSettings(Player2DMovement old)
    {
        MovementArea = old.MovementArea;
        MoveSpeed = old.MoveSpeed;
        SlowMoMult = old.SlowMoMult;
    }
}
