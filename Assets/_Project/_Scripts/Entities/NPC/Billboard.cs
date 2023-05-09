using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    public class Billboard : MonoBehaviour
    {
        public bool UseStaticBillBoard = true;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void LateUpdate()
        {
            if (!UseStaticBillBoard)
            {
                transform.LookAt(_camera.transform);
            }
            else
            {
                transform.rotation = _camera.transform.rotation;
            }

            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        }
    }
}
