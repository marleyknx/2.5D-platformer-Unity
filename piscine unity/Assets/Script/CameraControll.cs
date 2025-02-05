using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{

    public Transform pivotTarget;
    private Transform target;
    public float decalZ, decalY;
    private float posX, posY,posZ;

     private Vector3 targetTransform;
    private Vector3 smoothVelocity;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(pivotTarget);
        
        targetTransform = new Vector3(target.position.x, target.position.y + decalY, target.position.z + decalZ);


        transform.position = Vector3.SmoothDamp(transform.position, targetTransform, ref smoothVelocity, 0.3f);
        

    }
}
