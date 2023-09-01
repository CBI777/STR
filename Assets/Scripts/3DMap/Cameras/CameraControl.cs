using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    //Related to the input system. Pulling action at every frame.
    private CameraControlActions cameraActions;
    //Related to input action - movement
    private InputAction movement;

    //Value that is multiplied onto the movement to change the speed
    [SerializeField]
    private float speed = 25f;
    //Our camera is not touched in moving. Thus, this actaully is the transform of the object that this script is attached,
    //meaning it is the transform of the object that cinemachine is 'following'
    private Transform cameraTransform;

    //============================================================================================================//
    //We achieve zooming 'effect' by changing the offset value of cinemachine camera
    [SerializeField]
    private CinemachineVirtualCamera cineCamera;
    //Transposer contains the offset value that we need to change
    private CinemachineTransposer cineTransposer;

    //Full zoom(closest to the look at object):
    [SerializeField]
    private float yMinZoom = -10f;
    [SerializeField]
    private float zMinZoom = -60f;

    //Full zoomout(most far from look at object
    [SerializeField]
    private float yMaxZoom = 0f;
    [SerializeField]
    private float zMaxZoom = -20f;

    //This value refers to zoom speed difference between y and z
    private float zoomRatio;
    //This value refers to amount of zoom with every input.
    Vector3 zoomDir;

    //The target value that zoom needs to go.
    [SerializeField]
    private Vector3 zoomTarget;

    //zoom speed max and min
    private float maxZoomSpeed = 50f;
    private float minZoomSpeed = 10f;

    //debug : using limitation for zooms or not
    [SerializeField]
    private bool useZoomLimit = true;

    //============================================================================================================//

    private void Awake()
    {
        cameraTransform = this.transform;
        cameraActions = new CameraControlActions();
        cineTransposer = this.cineCamera.GetCinemachineComponent<CinemachineTransposer>();
        //initialize zoomtarget
        zoomTarget = this.cineTransposer.m_FollowOffset;
        zoomRatio = (zMaxZoom - zMinZoom) / (yMaxZoom - yMinZoom);
        zoomDir = new Vector3(0f, -1f, zoomRatio);
        zoomDir = zoomDir.normalized;

    }
    private void OnEnable()
    {
        //Enabling the input map for camera movement
        movement = cameraActions.Camera.Movement;
        //Adding Function ZoomCameraPerformed into the zoomcamera performed. 
        cameraActions.Camera.ZoomCamera.performed += ZoomCameraPerformed;
        cameraActions.Camera.Enable();
    }
    private void OnDisable()
    {
        cameraActions.Disable();
        cameraActions.Camera.ZoomCamera.performed -= ZoomCameraPerformed;
    }
    private void Update()
    {
        GetKeyboardMovement();
    }

    /// <summary>
    /// This Function is called in Upadate(). It is getting the keyboard input and changing the camera's position
    /// //(=transform of the gameobject that the camera is looking at)
    /// </summary>
    private void GetKeyboardMovement()
    {
        Vector3 inputValue = movement.ReadValue<Vector2>().x * GetCameraRight() + movement.ReadValue<Vector2>().y * GetCameraUp();
        //Rotation is outdated:230818
        //float rotDir = rotation.ReadValue<float>();

        inputValue = inputValue.normalized;

        this.transform.position += inputValue * speed * Time.deltaTime;
        //Rotation is outdated:230818 
        //targetRotation = rotDir;
    }

    //The reason why we zero out z is to make the camera move along the plain = map
    //No matter how rotated the transform is = related to camera rotation
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
    /// Getting x and y for Green axis of the camera. Used for VERTICAL movement.
    /// </summary>
    /// <returns>Vector3, with z being zeroed out</returns>
    private Vector3 GetCameraUp()
    {
        //getting x of GREEN axis of the camera
        Vector3 up = cameraTransform.up;
        up.z = 0f;
        return up;
    }


    /// <summary>
    /// This function is called when zoom event actually happens.
    /// It calculates ratio of y and z changes and changes the target, so that UpdateCameraZoom can do the necessary tricks.
    /// </summary>
    /// <param name="inputVal"></param>
    private void ZoomCameraPerformed(InputAction.CallbackContext inputVal)
    {
        //When wheel action is performed, y and z takes portion of the value.
        float value;
        //value is pos if you wheel up, neg if you wheel down.
        //we are going to zoom in on wheel up.
        value = inputVal.ReadValue<Vector2>().y;
        
        if (Mathf.Abs(value) > 0.0f)
        {
            zoomTarget += zoomDir * value;
            if (useZoomLimit)
            {
                zoomTarget = new Vector3(zoomTarget.x,
                    Mathf.Clamp(zoomTarget.y, yMinZoom, yMaxZoom),
                    Mathf.Clamp(zoomTarget.z, zMinZoom, zMaxZoom));
            }
            this.cineTransposer.m_FollowOffset= Vector3.Lerp(this.cineTransposer.m_FollowOffset, 
                zoomTarget, Time.deltaTime * calcZoomSpeed(this.cineTransposer.m_FollowOffset.z));

        }
    }

    /// <summary>
    /// Calculate zoom speed based on current zoom
    /// </summary>
    /// <param name="curZoom">current zoom of z(z's change is more dramatic)</param>
    /// <returns></returns>
    private float calcZoomSpeed(float curZoom)
    {
        float temp = Mathf.Clamp(3600f*3f / Mathf.Pow(curZoom, 2) + 5f,
            minZoomSpeed, maxZoomSpeed);
        Debug.Log(temp);

        return temp;
    }
}
