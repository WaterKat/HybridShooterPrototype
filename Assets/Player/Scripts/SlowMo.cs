using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterKat.TimeWarping;

namespace WaterKat.Player
{
    public class SlowMo : MonoBehaviour
    {
        public float Transition = 0;
        public float TransitionSpeed = 0.075f;

        public Player ActivePlayer;

        public Camera CameraA;
        public Camera CameraB;

        public bool SlowMoToggle = false;

        private void Awake()
        {
            ActivePlayer.InputActionMap.Player.SlowMo.started += ctx => Toggle();
        }

        public void Toggle()
        {
            SlowMoToggle = !SlowMoToggle;
        }

        void Update()
        {
            /*
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
            */

            if (SlowMoToggle)
            {
                Transition += TransitionSpeed;
            }
            else
            {
                Transition += -TransitionSpeed;
            }

            CameraA.gameObject.SetActive(SlowMoToggle);
            CameraB.gameObject.SetActive(SlowMoToggle);
            Transition = Mathf.Clamp01(Transition);
            
            

            float LerpedTimeScale = Mathf.Lerp(TimeWarp.DefaultTimeScale,TimeWarp.SlowedTimeScale, Transition);
            Time.timeScale = LerpedTimeScale;
            Time.fixedDeltaTime = TimeWarp.DefaultFixedTimeStep * LerpedTimeScale;
        }
    }
}