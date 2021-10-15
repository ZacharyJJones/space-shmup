using UnityEngine;

public class Player2DController : MonoBehaviour
{
    // Editor Fields
    [Header("Control")]
    public PlayerControlMode Mode;
    public Vector2 MinRestriction;
    public Vector2 MaxRestriction;

    [Header("Speed Settings")]
    public float MoveSpeed;

    [Range(0,1)]
    public float SlowMoMult;

    // Runtime Fields
    private Camera _mainCam;


    private void Awake()
    {
        _mainCam = Camera.main;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Mode == PlayerControlMode.Keyboard)
                Mode = PlayerControlMode.Mouse;
            else
                Mode = PlayerControlMode.Keyboard;
        }

        var localPos = transform.localPosition;
        float moveSpeed = MoveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift))
            moveSpeed *= SlowMoMult;

        if      (Mode == PlayerControlMode.Keyboard) _keyboardMovement(localPos, moveSpeed);
        else if (Mode == PlayerControlMode.Mouse) _mouseControlMovement(localPos, moveSpeed);
    }


    private void _mouseControlMovement(Vector3 localPos, float moveSpeed)
    {
        Vector2 mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        var movementVector = localPos - (Vector3)mousePos;

        Vector3 desiredPos;
        if (movementVector.magnitude <= moveSpeed)
            desiredPos = mousePos;
        else
            desiredPos = localPos + (movementVector.normalized * moveSpeed);

        transform.localPosition = _restrictPlayerPosition(desiredPos);
    }

    private void _keyboardMovement(Vector3 localPos, float moveSpeed)
    {
        //float horiz = Input.GetAxisRaw("Horizontal");
        //float vert = Input.GetAxisRaw("Vertical");
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        localPos += new Vector3(horiz, vert) * moveSpeed;

        transform.localPosition = _restrictPlayerPosition(localPos);
    }

    private Vector3 _restrictPlayerPosition(Vector3 desiredPosition)
    {
        Vector3 ret = new Vector3(
            Mathf.Clamp(desiredPosition.x, MinRestriction.x, MaxRestriction.x),
            Mathf.Clamp(desiredPosition.y, MinRestriction.y, MaxRestriction.y),
            desiredPosition.z);

        return ret;
    }
}
