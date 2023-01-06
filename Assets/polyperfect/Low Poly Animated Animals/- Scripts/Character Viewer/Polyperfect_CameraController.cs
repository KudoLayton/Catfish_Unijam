using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Polyperfect.Animals
{
    public class Polyperfect_CameraController : MonoBehaviour
    {
        public Transform target, podium;
        public float panSpeed;
        public float zoomSpeed;
        public Vector2 ZoomDistanceMinMax;
        Vector2 clickedPosition, mousePos;
        public float threshold = 0.2f;
        public float zoomAmount = 0;

        Camera mainCam;
        bool canControl, pressedLastFrame;

        // Use this for initialization
        void Start()
        {
            mainCam = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            mousePos = Input.mousePosition;

            if (Input.GetMouseButtonDown(0))
            {
                clickedPosition = mousePos;
            }

            if (Input.GetMouseButtonDown(0))
            {
                canControl = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                canControl = false;
            }

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                mainCam.fieldOfView += -Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
                mainCam.fieldOfView = Mathf.Clamp(mainCam.fieldOfView, ZoomDistanceMinMax.x, ZoomDistanceMinMax.y);
            }

            else
            {
                canControl = false;
            }

            if (canControl)
            {
                var dragDirection = clickedPosition - mousePos;

                if (dragDirection.magnitude > threshold)
                    if (mousePos.x > clickedPosition.x)
                        podium.RotateAround(target.position, -target.transform.up, Time.deltaTime * dragDirection.magnitude);
                    else
                        podium.RotateAround(target.position, target.transform.up, Time.deltaTime * dragDirection.magnitude);
            }
        }
    }
}
