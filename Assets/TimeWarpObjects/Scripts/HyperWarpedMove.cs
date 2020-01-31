using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperWarpedMove : MonoBehaviour
{
    public float localValue = 0;

    public float Speed = 50;

    public float OriginalTimeScale;
    public float WarpedTimeScale
    {
        get
        {
            return Mathf.Pow(OriginalTimeScale/Time.timeScale,2);
        }
    }

    private void Start()
    {
        OriginalTimeScale = Time.timeScale;
    }

    void Update()
    {
        transform.position = Quaternion.Euler(0,localValue,0)*new Vector3(0, 0, 10);
        localValue += Time.deltaTime * Speed * WarpedTimeScale;
    }
}
