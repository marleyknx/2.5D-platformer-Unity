using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    [SerializeField] float floorOffsetY;
  
    
    [HideInInspector]
    public Rigidbody rb;
   
  
    
    Vector3 raycastFloorPos;
    Vector3 floorMovement;
    protected Vector3 gravity;
    Vector3 CombinedRaycast;
    public CapsuleCollider coll;
   public bool isgrounded  { get; private set; }
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();
        isgrounded = true;
    }

   

    private void FixedUpdate()
    {
        // if not grounded , increase down force
        if (FloorRaycasts(0, 0, 0.6f) == Vector3.zero)
        {
            gravity += Vector3.up * Physics.gravity.y * Time.fixedDeltaTime;
            isgrounded = false;
        }

        // find the Y position via raycasts
        floorMovement = new Vector3(rb.position.x, FindFloor().y + floorOffsetY, rb.position.z);

        // only stick to floor when grounded
        if (FloorRaycasts(0, 0, 0.6f) != Vector3.zero && floorMovement != rb.position)
        {
            // move the rigidbody to the floor
            rb.MovePosition(floorMovement);
            gravity.y = 0;
            isgrounded = true;

        }
    }
    #region Grounded
    Vector3 FindFloor()
    {
        // width of raycasts around the centre of your character
        float raycastWidth = 0.25f;
        // check floor on 5 raycasts   , get the average when not Vector3.zero  
        int floorAverage = 1;

        CombinedRaycast = FloorRaycasts(0, 0, 1.6f);
        floorAverage += (getFloorAverage(raycastWidth, 0) + getFloorAverage(-raycastWidth, 0) + getFloorAverage(0, raycastWidth) + getFloorAverage(0, -raycastWidth));

        return CombinedRaycast / floorAverage;
    }

    // only add to average floor position if its not Vector3.zero
    int getFloorAverage(float offsetx, float offsetz)
    {

        if (FloorRaycasts(offsetx, offsetz, 1.6f) != Vector3.zero)
        {
            CombinedRaycast += FloorRaycasts(offsetx, offsetz, 1.6f);
            return 1;
        }
        else { return 0; }
    }

    Vector3 FloorRaycasts(float offsetx, float offsetz, float raycastLength)
    {
        RaycastHit hit;
        // move raycast
        raycastFloorPos = transform.TransformPoint(0 + offsetx, 0 + 0.5f, 0 + offsetz);

        Debug.DrawRay(raycastFloorPos, Vector3.down, Color.magenta);
        if (Physics.Raycast(raycastFloorPos, -Vector3.up, out hit, raycastLength))
        {
            return hit.point;

        }
        else return Vector3.zero;
    }

    #endregion

  
}
