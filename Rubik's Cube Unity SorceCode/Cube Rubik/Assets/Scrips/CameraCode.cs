using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCode : MonoBehaviour
{
    public GameObject target;

    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            transform.RotateAround(target.transform.position, transform.up, Input.GetAxis("Mouse X") * 2);
            transform.RotateAround(target.transform.position, transform.right, Input.GetAxis("Mouse Y") * -2);
        }


        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * -10;
        fov = Mathf.Clamp(fov, 40, 80);
        Camera.main.fieldOfView = fov;
    }
}
