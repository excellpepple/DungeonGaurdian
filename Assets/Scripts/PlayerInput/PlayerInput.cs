using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TopDownController))]
public class PlayerInput : MonoBehaviour
{

    TopDownController controller;

    [SerializeField] private CombatController combatController;

    public enum CameraDirection { x, z }
    public CameraDirection cameraDirection = CameraDirection.x;
    public float cameraHeight = 20f;
    public float cameraDistance = 7f;
    

    //Mouse cursor Camera offset effect
    Vector2 playerPosOnScreen;
    Vector2 cursorPosition;
    Vector2 offsetVector;


    public Camera playerCamera;
    public GameObject targetIndicatorPrefab;

    GameObject targetObject;
    //Plane that represents imaginary floor that will be used to calculate Aim target position
    Plane surfacePlane = new Plane();
    private bool isTopDownAttached;
    private void Awake()
    {
        // controller = GetComponent<TopDownController>();
        isTopDownAttached = TryGetComponent<TopDownController>(out controller); //this is more fail proof
        //Instantiate aim target prefab
        if (targetIndicatorPrefab)
        {
            targetObject = Instantiate(targetIndicatorPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        }

        //Hide the cursor
        //Cursor.visible = false;
    }
    // Start is called before the first frame update
   

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Input.GetMouseButtonDown(0) && isTopDownAttached)
        {
            combatController.MeleeAttack();
        }
        if (Input.GetMouseButtonDown(1) && isTopDownAttached)
        {
            combatController.RangedAttack();
        }

        Vector3 cameraOffset = Vector3.zero;
        if (cameraDirection == CameraDirection.x)
        {
            cameraOffset = new Vector3(cameraDistance, cameraHeight, 0);
        }
        else if (cameraDirection == CameraDirection.z)
        {
            cameraOffset = new Vector3(0, cameraHeight, cameraDistance);
        }

        Vector3 targetVelocity = Vector3.zero;

        // Calculate how fast we should be moving
        if (cameraDirection == CameraDirection.x)
        {
            targetVelocity = new Vector3(Input.GetAxis("Vertical") * (cameraDistance >= 0 ? -1 : 1), 0, Input.GetAxis("Horizontal") * (cameraDistance >= 0 ? 1 : -1));
        }
        else if (cameraDirection == CameraDirection.z)
        {
            targetVelocity = new Vector3(Input.GetAxis("Horizontal") * (cameraDistance >= 0 ? -1 : 1), 0, Input.GetAxis("Vertical") * (cameraDistance >= 0 ? -1 : 1));
        }

        //Mouse cursor offset effect
        playerPosOnScreen = playerCamera.WorldToViewportPoint(transform.position);
        cursorPosition = playerCamera.ScreenToViewportPoint(Input.mousePosition);
        offsetVector = cursorPosition - playerPosOnScreen;

        //Camera follow
        playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, transform.position + cameraOffset, Time.deltaTime * 7.4f);
        playerCamera.transform.LookAt(transform.position + new Vector3(-offsetVector.y * 2, 0, offsetVector.x * 2));

        //Aim target position and rotation
        targetObject.transform.position = GetAimTargetPos();
        targetObject.transform.LookAt(new Vector3(transform.position.x, targetObject.transform.position.y, transform.position.z));

        //Player rotation
        transform.LookAt(new Vector3(targetObject.transform.position.x, transform.position.y, targetObject.transform.position.z));

        controller.Move(targetVelocity);
    }

    Vector3 GetAimTargetPos()
    {
        //Update surface plane
        surfacePlane.SetNormalAndPosition(Vector3.up, transform.position);

        //Create a ray from the Mouse click position
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        //Initialise the enter variable
        float enter = 0.0f;

        if (surfacePlane.Raycast(ray, out enter))
        {
            //Get the point that is clicked
            Vector3 hitPoint = ray.GetPoint(enter);

            //Move your cube GameObject to the point where you clicked
            return hitPoint;
        }

        //No raycast hit, hide the aim target by moving it far away
        return new Vector3(-5000, -5000, -5000);
    }

   
}
