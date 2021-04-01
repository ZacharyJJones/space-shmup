using UnityEngine;

public class Player2DController : MonoBehaviour
{
    [Header("Control")]
    public PlayerControlMode Mode;
    public Vector2 MinRestriction;
    public Vector2 MaxRestriction;

    [Header("Speed Settings")]
    public float MoveSpeed;

    [Range(0,1)]
    public float SlowMoMult;


    // used for mouse position
    private Camera _mainCam;



    void Awake()
    {
        _mainCam = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // looks weird, but switches the input mode from keyboard to mouse, or vice versa.
            Mode = (Mode == PlayerControlMode.Keyboard) 
                ? PlayerControlMode.Mouse
                : PlayerControlMode.Keyboard;
        }

        var localPos = transform.localPosition;
        bool shiftKeyPressed = Input.GetKey(KeyCode.LeftShift);
        float mult = MoveSpeed * Time.deltaTime * ((shiftKeyPressed) ? SlowMoMult : 1f);

        if      (Mode == PlayerControlMode.Keyboard) _keyboardMovement(localPos, mult);
        else if (Mode == PlayerControlMode.Mouse) _mouseControlMovement(localPos, mult);
    }


    private void _mouseControlMovement(Vector3 localPos, float mult)
    {
        Vector2 mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);


        // vector for direction trying to go.
        var heading = localPos - (Vector3)mousePos;



        // mult is the max distance travellable this frame
        Vector3 desiredPos;
        if (heading.magnitude <= mult)
        {
            desiredPos = mousePos;
        }
        else
        {
            var headingNorm = heading.normalized;

            headingNorm *= mult;

            desiredPos = localPos + headingNorm;
        }


        transform.localPosition = _restrictPlayerPosition(desiredPos);
    }

    private void _keyboardMovement(Vector3 localPos, float mult)
    {
        //float horiz = Input.GetAxisRaw("Horizontal");
        //float vert = Input.GetAxisRaw("Vertical");
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        localPos += (new Vector3(horiz, vert) * mult);

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
