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
}
