using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WaterKat.Player
{
    [ExecuteAlways]
    public class CameraController : MonoBehaviour
    {
        public Camera PlayerCamera;
        public List<CameraData> CameraDatas;

        [Range(0,2)]
        public float CameraTransition = 0;

        public Vector2 CameraRotation = Vector2.zero;
        public float CameraDistance = 1;

        public Quaternion CameraQuaternion
        {
            get
            {
                return Quaternion.Euler(CameraRotation.y, CameraRotation.x, 0);
            }
        }
        private void Update()
        {
            if (!Input.GetMouseButton(1))
            {
                CameraTransition += 0.1f;
            }
            else
            {
                CameraTransition -= 0.1f;
            }
            CameraTransition = Mathf.Clamp(CameraTransition, 1, 2);

        }
        private void LateUpdate()
        {
            float LocalTransition = 0;

            int StartCamera = Mathf.FloorToInt(CameraTransition);
;
            if (StartCamera == CameraDatas.Count-1)
            {
                LocalTransition = 1f;
                StartCamera = CameraDatas.Count - 2;
            }
            else
            {
                LocalTransition = CameraTransition % 1;
            }

            int EndCamera = StartCamera + 1;

            //Debug.Log("StartCamera" + CameraTemplates[StartCamera].name);
            //Debug.Log("EndCamera " + CameraTemplates[EndCamera].name);
            //Debug.Log("LocalTransition " + LocalTransition);

            CameraRotation.x = Mathf.Repeat(CameraRotation.x + 360f, 720f) - 360f;
            CameraRotation.y = Mathf.Repeat(CameraRotation.y + 360f, 720f) - 360f;

            Vector2 LerpedSensitivity = Vector2.Lerp(CameraDatas[StartCamera].CameraRotationSensitivity, CameraDatas[EndCamera].CameraRotationSensitivity, LocalTransition);

            CameraRotation.x += Input.GetAxis("Mouse X") * LerpedSensitivity.x;
            CameraRotation.y += -Input.GetAxis("Mouse Y") * LerpedSensitivity.y;

            float LerpedDistanceSensitivity = Mathf.Lerp(CameraDatas[StartCamera].CameraDistanceSensitivity, CameraDatas[EndCamera].CameraDistanceSensitivity, LocalTransition);

            CameraDistance += -Input.mouseScrollDelta.y * LerpedDistanceSensitivity;

            Vector2 LerpedXBounds = Vector2.Lerp(CameraDatas[StartCamera].CameraRotationXBounds, CameraDatas[EndCamera].CameraRotationXBounds, LocalTransition);
            Vector2 LerpedYBounds = Vector2.Lerp(CameraDatas[StartCamera].CameraRotationYBounds, CameraDatas[EndCamera].CameraRotationYBounds, LocalTransition);
            Vector2 LerpedZBounds = Vector2.Lerp(CameraDatas[StartCamera].CameraDistanceBounds, CameraDatas[EndCamera].CameraDistanceBounds, LocalTransition);

 //           CameraRotation.x = Mathf.Clamp(CameraRotation.x, LerpedXBounds.x, LerpedXBounds.y);
            CameraRotation.y = Mathf.Clamp(CameraRotation.y, LerpedYBounds.x, LerpedYBounds.y);
            CameraDistance = Mathf.Clamp(CameraDistance, LerpedZBounds.x, LerpedZBounds.y);
                       

            CameraLerp(CameraDatas[StartCamera].TemplateCamera, CameraDatas[EndCamera].TemplateCamera, LocalTransition, PlayerCamera);
            PlayerCamera.transform.localPosition = PlayerCamera.transform.localPosition * CameraDistance;
            PlayerCamera.transform.localPosition = Quaternion.Euler(CameraRotation.y,CameraRotation.x,0) * PlayerCamera.transform.localPosition;
            PlayerCamera.transform.rotation = PlayerCamera.transform.rotation * Quaternion.Euler(CameraRotation.y, CameraRotation.x, 0);


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