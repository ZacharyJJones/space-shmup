using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardMovement : Player2DMovement
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var mouseMovement = gameObject.AddComponent<PlayerMouseMovement>();
            mouseMovement.ImportSettings(this);
            Destroy(this);
        }
    }

    private void FixedUpdate()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        var localPosition = transform.localPosition;

        float moveSpeed = MoveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift))
            moveSpeed *= SlowMoMult;

        var desiredPosition = localPosition + new Vector3(horiz, vert) * moveSpeed;
        transform.localPosition = Utils.ConstrainWithinCollider(desiredPosition, MovementArea);
    }
}
