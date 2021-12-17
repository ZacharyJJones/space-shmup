using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseMovement : Player2DMovement
{
    private Camera _mainCam;
    protected override void Start()
    {
        _mainCam = Camera.main;
    }

    // do the actual movement here
    private void FixedUpdate()
    {
        var localPosition = transform.localPosition;
        float moveSpeed = MoveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift))
            moveSpeed *= SlowMoMult;

        Vector2 mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        var movementVector = localPosition - (Vector3)mousePos;

        Vector3 desiredPosition;
        if (movementVector.magnitude <= moveSpeed)
            desiredPosition = mousePos;
        else
            desiredPosition = localPosition - (movementVector.normalized * moveSpeed);

        transform.localPosition = Utils.ConstrainWithinCollider(desiredPosition, MovementArea);
    }
}
