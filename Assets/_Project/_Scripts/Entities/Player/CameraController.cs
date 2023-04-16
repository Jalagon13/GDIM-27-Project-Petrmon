using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    public class CameraController : MonoBehaviour
    {
        public Transform player;
        private float mouseX, mouseY;
        public float mouseSence;
        public float xRotation;

        void update()
        {
            mouseX = Input.GetAxis("Mouse X") * mouseSence * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * mouseSence * Time.deltaTime;

            xRotation -= mouseY;

            player.Rotate(Vector3.up * mouseX);
            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        }
    }
}
