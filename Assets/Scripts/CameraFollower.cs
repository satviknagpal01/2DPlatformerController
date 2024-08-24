using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.25f;
    public Vector2 horizontalClamp;
    public Vector2 vertcialClamp;

    private Vector3 offset;
    private Vector3 refVelocity = Vector3.zero;

    private void Awake()
    {
        offset = transform.position;
    }
    private void FixedUpdate()
    {
        Vector3 targetPos = target.position + offset;
        targetPos.x = Mathf.Clamp(targetPos.x, horizontalClamp.x, horizontalClamp.y);
        targetPos.y = Mathf.Clamp(targetPos.y, vertcialClamp.x, vertcialClamp.y);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref refVelocity, smoothTime);
    }
}
