using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaterKat.Player
{
    public class CameraController : MonoBehaviour
    {
        public List<Camera> CameraTemplates;

        [Range(0,2)]
        public float CameraTransition = 0;

        private void Update()
        {
            float LocalTransition = 0;

            int StartCamera = Mathf.FloorToInt(CameraTransition);
;
            if (StartCamera == CameraTemplates.Count-1)
            {
                LocalTransition = 1f;
                StartCamera = CameraTemplates.Count - 2;
            }

            int EndCamera = StartCamera + 1;
        }

        public void CameraLerp(Camera CameraA,Camera CameraB, float input, Camera TargetCamera)
        {
            TargetCamera.transform.position = Vector3.Lerp(CameraA.transform.position, CameraB.transform.position, input);
            TargetCamera.transform.rotation = Quaternion.Lerp(CameraA.transform.rotation, CameraB.transform.rotation, input);

            TargetCamera.fieldOfView = Mathf.Lerp(CameraA.fieldOfView, CameraB.fieldOfView, input);
            TargetCamera.nearClipPlane = Mathf.Lerp(CameraA.nearClipPlane, CameraB.nearClipPlane, input);
            TargetCamera.farClipPlane = Mathf.Lerp(CameraA.farClipPlane, CameraB.farClipPlane, input);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            //Gizmos.DrawRay(new Ray(transform.position,transform.forward*5));
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5);
        }
    }
}