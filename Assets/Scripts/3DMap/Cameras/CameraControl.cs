using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Cinemachine;
using System;

public class CameraControl : MonoBehaviour
{
    //Related to the input system. Pulling action at every frame.
    private CameraControlActions cameraActions;
    //Related to input action - movement
    private InputAction movement;

    //Value that is multiplied onto the movement to change the speed
    [SerializeField]
    private float speed = 25f;
    //Value that is multiplied onto the zoom to change the speed of zoom
    [SerializeField]
    private float zoomSpeed = 0.5f;
    //Our camera is not touched when moving. Thus, this actaully is the transform of the object that this script is attached,
    //meaning it is the transform of the object that cinemachine is 'following'
    private Transform cameraTransform;

    //Debug : serialize field to see the value
    //storing zoom limit : 0 is smallest, 1 is largest
    [SerializeField]
    private float[] zoomLimit = new float[2];
    //minX, maxX, minY, maxY - decides the boundaries of camera movement
    [SerializeField]
    private Rect camLimit;
    //decides CURRENT boundary of camera movement.
    //This value is calculated by combining fov and offset of the camera.
    //so, this actually IS the one that limits camera movement.
    [SerializeField]
    private Rect curLimit;

    //Gameobject that has cinemachine camera attached to itself
    [SerializeField]
    private GameObject cineCam;

    //The actual cinemachineCam that we are going to touch
    //we are going to touch the offset of the camera for zoom
    //plus, we will use it in start to get the fov value.
    private CinemachineVirtualCamera virtualCam;
    //fov value that we will use when doing zoom
    float cineCamFov;
    //Current Zoom value
    float curZoom;

    //======Event Related======//
    [SerializeField]
    private JSON_CameraManager_Init_SO cameraInitEvent;
    //=========================//


    private void Awake()
    {
        cameraTransform = this.transform;
        cameraActions = new CameraControlActions();
        //initialize zoomtarget
    }
    private void Start()
    {
        this.virtualCam = cineCam.GetComponent<CinemachineVirtualCamera>();
        cineCamFov = this.virtualCam.m_Lens.FieldOfView;
    }
    private void OnEnable()
    {
        //Enabling the input map for camera movement
        movement = cameraActions.Camera.Movement;
        cameraActions.Camera.Enable();
        cameraActions.Camera.Zoom.performed += ZoomCamera;
        cameraInitEvent.jsonCameraInitEvent.AddListener(GetInitInfo);
    }
    private void OnDisable()
    {
        cameraActions.Disable();
        cameraActions.Camera.Zoom.performed -= ZoomCamera;
        cameraInitEvent.jsonCameraInitEvent.RemoveListener(GetInitInfo);
    }
    private void LateUpdate()
    {
        GetKeyboardMovement();
        //we have to change the actual zoom of the camera,
        //Aaand the boundary of movement since change in zoom DOES effect the boundary.
        SetZoom();
        SetRealBoundary();
    }

    public void GetInitInfo(CameraConfigInfo cf)
    {
        //Basically, camLimit is a list of float with size of 4.
        //It consists of floats, in order of : xMin, yMax, xMax, yMin.
        //we fully fill in the rect - camlimit - according to these four values
        camLimit.xMin = cf.camLimit[0];
        camLimit.yMax = cf.camLimit[1];
        camLimit.xMax = cf.camLimit[2];
        camLimit.yMin = cf.camLimit[3];

        this.transform.position = new Vector3(cf.initCamPos[0], cf.initCamPos[1], cf.initCamPos[2]);
        zoomLimit[0] = cf.camZoomLimit;
        zoomLimit[1] = CustomMath.GetCameraViewDistance(camLimit.width, cineCamFov) * -1f;
        //since camera offset HAS TO BE -, we multiply -1f
        this.curZoom = (zoomLimit[0] + zoomLimit[1]) / 2;
        SetRealBoundary();
        SetZoom();
    }

    /// <summary>
    /// This Function is called in Upadate(). It is getting the keyboard input and changing the camera's position
    /// //(=transform of the gameobject that the camera is looking at)
    /// </summary>
    private void GetKeyboardMovement()
    {
        Vector3 inputValue = movement.ReadValue<Vector2>().x * GetCameraRight() + movement.ReadValue<Vector2>().y * GetCameraUp();

        inputValue = inputValue.normalized;

        //this.transform.position += inputValue * speed * Time.deltaTime;

        //because camlimit is in shape of rectangle, we can do :
        if(inputValue.magnitude > 0.1f)
        {
            this.transform.position =
                CustomMath.ClampVector((this.transform.position + inputValue * speed * Time.deltaTime),
                curLimit);
        }
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
    /// Setting up the real boundary of the camera to limit its the movement.
    /// </summary>
    private void SetRealBoundary()
    {
        Vector2 tempFrustum = CustomMath.CameraViewWH(curZoom, cineCamFov);
        tempFrustum.x /= 2;
        tempFrustum.y /= 2;
        curLimit.xMin = camLimit.xMin + tempFrustum.x;
        curLimit.xMax = camLimit.xMax - tempFrustum.x;
        curLimit.yMax = camLimit.yMax - tempFrustum.y;
        curLimit.yMin = camLimit.yMin + tempFrustum.y;

        this.transform.position = CustomMath.ClampVector(this.transform.position, curLimit);
    }
    /// <summary>
    /// Setting zoom : z offset of virtual camera to curzoom
    /// Thus, this is where the 'Acutal Zoom' takes place.
    /// </summary>
    private void SetZoom()
    {
        this.virtualCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z
            = curZoom;
    }
    private void ZoomCamera(InputAction.CallbackContext inputValue)
    {
        float tempValue = Mathf.Sign(-inputValue.ReadValue<Vector2>().y);
        //check if input is not zero or not
        if(Mathf.Abs(tempValue) > 0.1f)
        {
            tempValue = tempValue*zoomSpeed;
            Debug.Log(tempValue);
            if ((curZoom + tempValue) >= zoomLimit[0])
            {
                curZoom = zoomLimit[0];
            }
            else if((curZoom + tempValue) <= zoomLimit[1])
            {
                curZoom = zoomLimit[1];
            }
            else
            {
                curZoom = curZoom + tempValue;
            }
        }
    }
}
