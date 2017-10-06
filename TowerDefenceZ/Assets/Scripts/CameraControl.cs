using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera cam;

    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    public Vector2 limitX;
    public Vector2 limitZ;
    public Vector2 limitY = new Vector2(10, 80);

    public float scrollSpeed = 5f;

    void Update()
    {
        //WASD或鼠标靠近边缘移动镜头
        Vector3 pos = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos += transform.forward * panSpeed * Time.deltaTime;
        }
        else if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            pos -= transform.forward * panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            pos -= transform.right * panSpeed * Time.deltaTime;
        }
        else if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos += transform.right * panSpeed * Time.deltaTime;
        }


        //滚轮控制y
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        pos += cam.transform.forward * scroll * scrollSpeed * 1000 *  Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, limitX.x, limitX.y);
        pos.z = Mathf.Clamp(pos.z, limitZ.x, limitZ.y);
        pos.y = Mathf.Clamp(pos.y, limitY.x, limitY.y);

        //transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
        transform.position = pos;

    }
}
