using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaterKat.Player
{
    public class AerialSlowMo : MonoBehaviour
    {
        public float Transition = 0;
        public float TransitionSpeed = 0.075f;

        public float TimeScaleA = 1;
        public float TimeScaleB = 0.25f;

        public Player ActivePlayer;

        float OriginalTimeScale;
        float OriginalFixedDeltaTime;

        void Start()
        {
            OriginalTimeScale = Time.timeScale;
            OriginalFixedDeltaTime = Time.fixedDeltaTime;
        }

        public Camera CameraA;
        public Camera CameraB;
        void Update()
        {
            if (Transition == 1)
            {
                CameraA.gameObject.SetActive(true);
                CameraB.gameObject.SetActive(true);
            }
            else
            {
                CameraA.gameObject.SetActive(false);
                CameraB.gameObject.SetActive(false);
            }

            if (Input.GetMouseButtonDown(1) && !ActivePlayer.CheckIfGrounded())
            {
                Transition += TransitionSpeed;
            }
            if (Input.GetMouseButton(1) && !ActivePlayer.CheckIfGrounded()&&(Transition>0))
            {
                Transition += TransitionSpeed;
            }
            else
            {
                Transition += -TransitionSpeed;
            }
            Transition = Mathf.Clamp01(Transition);

            float LerpedScale = Mathf.Lerp(TimeScaleA, TimeScaleB, Transition);
            Time.timeScale = OriginalTimeScale * LerpedScale;
            Time.fixedDeltaTime = OriginalFixedDeltaTime * LerpedScale;
        }
    }
}