using System;
using UnityEngine;

public class Player2DMovement : MonoBehaviour
{
    // Due to issues with using a key to switch movement control modes, I am removing that feature for the time being.
    // -- Future plan is to have a option in "settings" menu which will be read by this script on unpause/start...
    // ... which sets up the appropriate script for use.

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
        // var mouseMovement = gameObject.AddComponent<PlayerMouseMovement>();
        // mouseMovement.ImportSettings(this);
        Destroy(this);
    }

    protected void ImportSettings(Player2DMovement old)
    {
        MovementArea = old.MovementArea;
        MoveSpeed = old.MoveSpeed;
        SlowMoMult = old.SlowMoMult;
    }
}
