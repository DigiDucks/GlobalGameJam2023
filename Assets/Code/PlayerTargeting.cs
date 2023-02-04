using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    //vars
    [Header("Aim")]
    public LayerMask debugGroundMask;
    public LayerMask groundMask;//mask of collidables
    public bool ignoreHeight;//targeting ignore height

    [Header("Gizmos")]
    [SerializeField] private Transform projectileSpawnTransform;
    [SerializeField] private bool gizmo_cameraRay = false;
    [SerializeField] private bool gizmo_ground = false;
    [SerializeField] private bool gizmo_target = false;
    [SerializeField] private bool gizmo_ignoredHeightTarget = false;

    private Camera mainCamera;


    //fetchs
    private void Start()
    {
        mainCamera = Camera.main;
    }


    //calculate aim position in world space - CALLED BY PLAYER ABILITIES SCRIPT TO GET TARGET POSITION
    public Vector3 Aim()
    {
        //fetch the mouse position, store it in a vector3, and make a bool for success
        var (success, position) = FetchMousePos();

        if (success)
        {
            //calculate the direction
            var direction = position - transform.position;

            //ignore the height difference
            if (ignoreHeight)
            {
                position.y = 0;
            }

            //set transform to look in the direction
            //transform.forward = direction; //if we want to turn the body to face the target
        }
        return position;
    }


    //find mouse position in world space
    private (bool success, Vector3 position) FetchMousePos()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            //raycast hit, return true / position
            return (success: true, position: hitInfo.point);
        }
        else
        {
            //raycast miss, return false / zero
            return (success: false, position: Vector3.zero);
        }
    }

    #region Gizmos QOL
    //draw targeting gizmos if enabled
    private void OnDrawGizmos()
    {
        //wierd safty check, idk why it works
        if (Application.isPlaying == false)
        {
            return;
        }

        //fetch the mouse position, store it in an open var
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        //if we hit something, not on mask
        if (Physics.Raycast(ray, out var hitInfo, float.MaxValue, debugGroundMask))
        {
            //gizmo camera
            if (gizmo_cameraRay)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(ray.origin, hitInfo.point);
                Gizmos.DrawWireSphere(ray.origin, 0.5f);
            }

            //set variables for gizmo
            var hitPosition = hitInfo.point;
            var hitGroundHeight = Vector3.Scale(hitInfo.point, new Vector3(1, 0, 1)); ;
            var hitPositionIngoredHeight = new Vector3(hitInfo.point.x, projectileSpawnTransform.position.y, hitInfo.point.z);

            //gizmo ground
            if (gizmo_ground)
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawWireSphere(hitGroundHeight, 0.5f);
                Gizmos.DrawLine(hitGroundHeight, hitPosition);
            }

            //gizmo target
            if (gizmo_target)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(hitInfo.point, 0.5f);
                Gizmos.DrawLine(projectileSpawnTransform.position, hitPosition);
            }

            //gizmo target with ingored height
            if (gizmo_ignoredHeightTarget)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(hitPositionIngoredHeight, 0.5f);
                Gizmos.DrawLine(projectileSpawnTransform.position, hitPositionIngoredHeight);
            }
        }
    }


    //toggle different gizmo settings
    //1 ray, 2 ground, 3 target, 4 target with ignored height, 5 toggle between ignore height and not ignore height
    private void GizmoSettings()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gizmo_cameraRay = !gizmo_cameraRay;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gizmo_ground = !gizmo_ground;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gizmo_target = !gizmo_target;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            gizmo_ignoredHeightTarget = !gizmo_ignoredHeightTarget;
        }
    }
    #endregion
}
