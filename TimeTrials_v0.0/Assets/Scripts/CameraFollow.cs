using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 10f;
    public BoxCollider2D targetCollider;
    
    private void Start()
    {
        // Find the target object with the specified name
        GameObject targetObject = GameObject.Find("Car");

        // Get the BoxCollider component from the target object
        targetCollider = targetObject.GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        Vector3 targetPos = new Vector3(targetCollider.bounds.center.x, targetCollider.bounds.center.y, -10f);
        Vector3 currentPos = new Vector3(transform.position.x, transform.position.y, -10f);
        Vector3 newPos = Vector3.Lerp(currentPos, targetPos, FollowSpeed * Time.deltaTime);
        transform.position = newPos;
    }
}