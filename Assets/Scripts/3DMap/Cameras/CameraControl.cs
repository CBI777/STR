using UnityEngine;
using System.Collections.Generic;
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

    //Debug : serialize field to see the value
    //storing zoom limit : not used for now.
    /*[SerializeField]
    private float[] zoomLimit = new float[2];*/
    //Value used for setting up bounds of polygon collider2d
    [SerializeField]
    private List<Vector2> poly_camLimit = new List<Vector2>();


    //polygon collider 2d to confine the movement of cinemachine.
    [SerializeField]
    private PolygonCollider2D polyCollider;
    //The actual confiner of cinemachine that we are going to touch
    [SerializeField]
    private CinemachineConfiner2D confiner;

    private void Awake()
    {
        cameraTransform = this.transform;
        cameraActions = new CameraControlActions();
        //initialize zoomtarget
    }
    private void OnEnable()
    {
        //Enabling the input map for camera movement
        movement = cameraActions.Camera.Movement;
        cameraActions.Camera.Enable();
    }
    private void OnDisable()
    {
        cameraActions.Disable();
    }
    private void LateUpdate()
    {
        GetKeyboardMovement();
    }

    public void GetInitInfo(CameraConfigInfo cf)
    {
        //Basically, camLimit is a list of float with size of 8.
        //It consists of 4 pairs, each represents x,y of cam limit collider.
        //each pairs are in order of : -+, --, +-, ++ OR 2nd->3rd->4th->1st quadrant
        for (int i = 0; i<(cf.camLimit.Count/2); i++)
        {
            this.poly_camLimit.Add(new Vector2(cf.camLimit[2*i], cf.camLimit[2*i + 1]));
        }
        this.transform.position = new Vector3(cf.initCamPos[0], cf.initCamPos[1], cf.initCamPos[2]);

        SetPolyCollider();
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
        this.transform.position = CustomMath.ClampVector((this.transform.position + inputValue*speed*Time.deltaTime),
            poly_camLimit[0].x, poly_camLimit[2].x, poly_camLimit[1].y, poly_camLimit[0].y);
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
    /// Setting up the polygon collider 2d to limit the movement of camera.
    /// </summary>
    /// <param name="tolerance">tolerance of camera being close to the border</param>
    private void SetPolyCollider(float tolerance = 0.05f)
    {
        
        this.polyCollider.pathCount = 1;
        for(int i = 0; i < this.poly_camLimit.Count; i++)
        {
            polyCollider.SetPath(0, poly_camLimit.ToArray());
        }

        confiner.InvalidateCache();
    }
}
