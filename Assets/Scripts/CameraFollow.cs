using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public bool follow;

    Vector3 offset;

    void Awake()
    {
        offset = transform.position - target.transform.position;
    }


    void Update()
    {
        if (follow)
        {
            transform.position = target.transform.position + offset;
                
        }
    }

}
