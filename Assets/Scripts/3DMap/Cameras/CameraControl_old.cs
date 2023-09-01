using UnityEngine;
using UnityEngine.InputSystem;

public class CaomeraControl_old : MonoBehaviour
{
    //Related to the input system. Pulling action at every frame.
    private CameraControlActions cameraActions;
    private InputAction movement;

    //Rotation is outdated:230818 
    //private InputAction rotation;

    //Main camera that we will use
    [SerializeField]
    private Camera mainCam;

    private Transform cameraTransform;

    //maxSpeed : maximum speed of movement
    //speed : variable to store current speed
    //acceleration : value for speed of movement to incerase over time
    //damping : smoothness of movement. The larger the value, the smoother the movement will be.

    //Involved with horizontal movement, wasd.
    //maxspeed of movement
    [SerializeField]
    private float maxSpeed = 15f;
    //current speed of movement
    private float speed = 0f;
    [SerializeField]
    private float acceleration = 10f;
    //used in decelation
    [SerializeField]
    private float damping = 5f;

    /*
    //Involved with rotational movement, qe.
    //Rotation is outdated:230818 
    [SerializeField]
    private float rot_acceleration = 130f;
    [SerializeField]
    private float rot_maxSpeed = 70f;
    private float rot_speed = 0f;
    [SerializeField]
    private float rot_damping = 100f;
    */

    //Related to zoom in and out, mouse wheel.
    //how close and far the camera will go (orthographic size of the camera = zoom)
    /*//orthographic
    [SerializeField]
    private float minZoom = 3f;
    [SerializeField]
    private float maxZoom = 10f;
    */

    //perspective
    //limit of camera for zooming closer to the ground
    [SerializeField]
    private float minZoom = 15f;
    //limit of camera for zooming further away from the ground
    [SerializeField]
    private float maxZoom = 50f;
    //zoom speed
    [SerializeField]
    private float zoom_damping = 20f;
    //debug : using limitation for zooms
    [SerializeField]
    private bool useZoomLimit = true;

    //value set in various functions 
    //used to update the position of the camera base object.
    //target vector refers to inputted vector, (from wasd)which contains direction+scalar
    private Vector3 targetVector;
    //Rotation is outdated:230818 
    //targetRotaiton refers to clock or counterclockwise of the rotation that the camera might rotate to.
    //private float targetRotation;
    //targetZoom refers to possible zoom that the camera might reach.
    private float targetZoom;

    //bounding box of camera movement.
    private Rect camLimit;

    //padding value to limit space of cam movement
    [SerializeField]
    private float camPad = 3f;

    //debug
    private Rect camRect;

    private void Awake()
    {
        cameraActions = new CameraControlActions();
    }

    private void OnEnable()
    {
        cameraTransform = this.mainCam.transform;
        targetZoom = mainCam.fieldOfView;
        cameraTransform.LookAt(this.transform);
        SetCamera_xyLimit(-100, 100, -100, 100, camPad, camPad);
        movement = cameraActions.Camera.Movement;
        //Rotation is outdated:230818 
        //rotation = cameraActions.Camera.RotateCamera;
        cameraActions.Camera.ZoomCamera.performed += zoomCamera;
        cameraActions.Camera.Enable();
    }


    private void OnDisable()
    {
        cameraActions.Disable();
        cameraActions.Camera.ZoomCamera.performed -= zoomCamera;
    }

    private void Update()
    {
        GetKeyboardMovement();
    }
    private void LateUpdate()
    {
        UpdateBasePos();
        UpdateCameraSize();
        camRect = CustomMath.NearPlaneDimensions(mainCam);
        //Rotation is outdated:230818 
        //UpdateBaseRotation();
    }

    private void SetCamera_xyLimit(float minX, float maxX, float minY, float maxY, float camPadx, float camPady)
    {
        this.camLimit.xMin = minX + camPadx;
        this.camLimit.xMax = maxX - camPadx;
        this.camLimit.yMin = minY + camPady;
        this.camLimit.yMax = maxY - camPady;
    }

    /// <summary>
    /// Returns camera relative direction to move the camera, combined with key inputs +
    /// rotation direction, which is represented in manner of 1f or -1f =>
    /// Save this to targetDirection and targetRotaiton
    /// </summary>
    private void GetKeyboardMovement()
    {
        Vector3 inputValue = movement.ReadValue<Vector2>().x * GetCameraRight() + movement.ReadValue<Vector2>().y * GetCameraUp();
        //Rotation is outdated:230818
        //float rotDir = rotation.ReadValue<float>();

        inputValue = inputValue.normalized;

        targetVector = inputValue;
        //Rotation is outdated:230818 
        //targetRotation = rotDir;
    }

    /// <summary>
    /// Getting x and y for RED axis of the camera. Used for HORIZONTAL movement.
    /// </summary>
    /// <returns>Vector3, with z being zeroed out</returns>
    private Vector3 GetCameraRight()
    {
        //getting RED axis of the camera
        Vector3 right = cameraTransform.right;
        right.z = 0f;
        return right;
    }

    /// <summary>
    /// Getting x and y for BLUE axis of the camera. Used for HORIZONTAL movement.
    /// </summary>
    /// <returns>Vector3, with z being zeroed out</returns>
    private Vector3 GetCameraForward()
    {
        //getting x of BLUE axis of the camera
        Vector3 forward = cameraTransform.forward;
        forward.z = 0f;
        return forward;
    }

    /// <summary>
    /// Getting x and y for Green axis of the camera. Used for HORIZONTAL movement.
    /// </summary>
    /// <returns>Vector3, with z being zeroed out</returns>
    private Vector3 GetCameraUp()
    {
        //getting x of BLUE axis of the camera
        Vector3 up = cameraTransform.up;
        up.z = 0f;
        return up;
    }

    /// <summary>
    /// This function is used for horizontal movement of the camera.
    /// This function features acceleration and deacceleration of camera movement via wasd.
    /// Used in either Update() function OR LateUpdate() function
    /// </summary>
    private void UpdateBasePos()
    {
        //This temporary vector will be added to current movement vector to make smooth movement towards certain direction.
        float tempSpeed;
        //Checking to see if we have to do acceleration or deacceleration for damping.
        if (targetVector.sqrMagnitude > 0.1f)
        {
            //accel until it reaches the maxspeed.
            tempSpeed = (Time.deltaTime * acceleration);
            speed = Mathf.Clamp((speed + tempSpeed), 0f, maxSpeed);
        }
        else
        {
            //decel when there is no keyboard input of wasd.
            tempSpeed = (Time.deltaTime * damping);
            speed = Mathf.Clamp((speed - tempSpeed), 0f, maxSpeed);
        }

        transform.position += (speed * Time.deltaTime) * targetVector;

        if (!camLimit.Contains(new Vector2(transform.position.x, transform.position.y)))
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, camLimit.xMin, camLimit.xMax), Mathf.Clamp(transform.position.y, camLimit.yMin, camLimit.yMax), 0f);
        }
    }

    /*
    //Rotation is outdated:230818 
    /// <summary>
    /// This function is used for rotation of the camera via q and e keys.
    /// This function features acceleration of rotation camera until it reaches certain amount of speed,
    /// and it also features smooth deacceleration of rotation camera when there's no buttons being pushed.
    /// Used in either Update() function OR LateUpdate() function
    /// </summary> 
    private void UpdateBaseRotation()
    {
        //temporary speed that will be added to current speed
        float tempSpeed;
        //Check if there is any keyboard input of q or e
        //rot_speed = direction of rotation * rot_acceleration * time.deltaTIme
        if (targetRotation != 0f)
        {
            //accel the rotation speed until it reaches the rotation max speed.
            //accel happens only until it reaches the max speed.
            tempSpeed = (targetRotation * rot_acceleration * Time.deltaTime);
            if(Mathf.Abs(rot_speed + tempSpeed) <= rot_maxSpeed)
            {
                rot_speed += tempSpeed;
            }
            else
            {
                rot_speed = Mathf.Sign(rot_speed) * rot_maxSpeed;
            }
        }
        
        else
        {
            //deaccel when there is no keyboard input.
            tempSpeed = Mathf.Sign(rot_speed) * (rot_damping * Time.deltaTime);

            if(Mathf.Abs(tempSpeed) <= Mathf.Abs(rot_speed))
            {
                rot_speed -= tempSpeed;
            }
            else
            {
                rot_speed = 0f;
            }
        }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, (rot_speed * Time.deltaTime) + transform.rotation.eulerAngles.y, 0f);
    }
    */

    /// <summary>
    /// This function is called when zoom event actually happens, which means it does not do the zoom itself.
    /// UpdateCameraPosition function does the trick every frame.
    /// </summary>
    /// <param name="inputVal"></param>
    private void zoomCamera(InputAction.CallbackContext inputVal)
    {
        float value;
        value = -inputVal.ReadValue<Vector2>().y * 50f / 1000f;

        if (Mathf.Abs(value) > 0.1f)
        {
            if (useZoomLimit)
            {
                targetZoom = Mathf.Clamp(mainCam.fieldOfView + value, minZoom, maxZoom);

            }
            else
            {
                targetZoom = mainCam.fieldOfView + value;
            }
        }

        cameraTransform.LookAt(this.transform);
    }

    private void UpdateCameraSize()
    {
        foreach (Camera cams in this.transform.GetComponentsInChildren<Camera>())
        {
            cams.fieldOfView = Mathf.Lerp(cams.fieldOfView, targetZoom, Time.deltaTime * zoom_damping);
        }
    }
}
