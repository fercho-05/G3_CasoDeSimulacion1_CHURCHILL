using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    [SerializeField]
    Vector3 offset;

    [SerializeField]
    float damping;

    [SerializeField]
    Transform target;

    Vector3 _velocity = Vector2.zero;

    void LateUpdate()
    {
        Vector3 position = target.position + offset;
        position.z = transform.position.z;  

        transform.position = Vector3.SmoothDamp(transform.position, position, ref _velocity, damping);
    }
}
