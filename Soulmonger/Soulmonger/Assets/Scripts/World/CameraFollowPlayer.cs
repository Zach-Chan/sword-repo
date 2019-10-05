using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.3f;
    public Vector3 offset;

    public float posY;

    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if(target!=null)
        {


        Vector3 targetPosition = target.TransformPoint(new Vector3(0,posY,-10));
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);

        // Vector3 smoothedPosition = Vector2.SmoothDamp(transform.position,desiredPosition,ref velocity,smoothSpeed);
        // transform.position = smoothedPosition;
        // transform.LookAt(target);
        }
    }

}
