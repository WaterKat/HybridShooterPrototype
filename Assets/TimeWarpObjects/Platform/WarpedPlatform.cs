using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterKat.TimeKeeping;

public class WarpedPlatform : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;

    public GameObject Platform;

    public float Transition = 0;

    public float PlatformSpeed = 1;
    public float PlatformWaitTime = 1;

    public float TransitionAmount;
    public float Goal;

    public int SwitchCase = 0; //0,2 Waiting 1,3 moving forward,backward;

    public Ticker WaitTicker = new Ticker() {};

    public float OriginalTimeScale;

    private float CurrentTimeScale
    {
        get
        {
            return Time.deltaTime * (OriginalTimeScale / Time.timeScale);
        }
    }

    private void Start()
    {
        OriginalTimeScale = Time.timeScale;

        WaitTicker.MaxTick = PlatformWaitTime;
        Goal = (PointB.position - PointA.position).magnitude;
        TransitionAmount = (PlatformSpeed/Goal);
    }

    private void Update()
    {
        switch (SwitchCase)
        {
            case 0:
                if (WaitTicker.TryTick(CurrentTimeScale))
                {
                    SwitchCase++;
                }
                break;
            case 1:
                Transition = Mathf.Clamp01(Transition+TransitionAmount*CurrentTimeScale);
                if (Transition == 1)
                {
                    SwitchCase++;
                }
                break;
            case 2:
                if (WaitTicker.TryTick(CurrentTimeScale))
                {
                    SwitchCase++;
                }
                break;
            case 3:
                Transition = Mathf.Clamp01(Transition - TransitionAmount*CurrentTimeScale);
                if (Transition == 0)
                {
                    SwitchCase = 0;
                }
                break;
        }

        Platform.transform.position = Vector3.Lerp(PointA.position, PointB.position, Transition);
    }

}
