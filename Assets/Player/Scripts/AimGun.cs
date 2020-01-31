using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WaterKat.Player
{
    public class AimGun : MonoBehaviour
    {
        public GameObject Gun;

        public GameObject GunTemplate;

        public CameraController PlayerCameraController;

        private void Update()
        {
            Gun.transform.localPosition = PlayerCameraController.CameraQuaternion * GunTemplate.transform.localPosition;
            Gun.transform.rotation = PlayerCameraController.CameraQuaternion;
        }
    }
}