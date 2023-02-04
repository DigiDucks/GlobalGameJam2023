using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private bool RotateTowardMouse;

    private Camera cam; //cam for targeting

    [Header("Input")]
    [HideInInspector]
    [SerializeField] private Vector2 inputVector; // WASD input
    [HideInInspector]
    [SerializeField] private Vector3 mousePos; //world space mouse pos

    [Header("Player Scripts")]
    private PlayerController PC;


    //fetchs
    private void Start()
    {
        cam = Camera.main;
        PC = GetComponent<PlayerController>();
    }


    //update loops
    void Update()
    {
        InputVars();
        Targeting();
    }


    //determine targeting
    private void Targeting()
    {
        Vector3 targetVector = new Vector3(inputVector.x, 0, inputVector.y);
        Vector3 movementVector = MoveTowardTarget(targetVector);//null ref?

        //if we using rotate bool
        if (!RotateTowardMouse)
        {
            RotateTowardMovementVector(movementVector);
        }
        if (RotateTowardMouse)
        {
            RotateFromMouseVector();
        }
    }


    //player input
    private void InputVars()
    {
        //fetch axis var
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        inputVector = new Vector2(h, v);

        //fetch mouse position
        mousePos = Input.mousePosition;
    }


    //rotate player body to look at mouse pos
    private void RotateFromMouseVector()
    {
        Ray ray = cam.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }


    //move player towards 
    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        //calc speed
        var speed = PC.currentSpeed * Time.deltaTime;

        //find target
        targetVector = Quaternion.Euler(0, cam.gameObject.transform.rotation.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }


    //if moving, update rotation to move dir
    private void RotateTowardMovementVector(Vector3 movementDirection)
    {
        if (movementDirection.magnitude == 0)
        {
            return;
        }
        var rotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, PC.rotationSpeed * Time.deltaTime);
    }
}
