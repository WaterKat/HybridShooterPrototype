using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraData : MonoBehaviour
{
    public Camera TemplateCamera;

    public Vector2 CameraSensitivity = new Vector2(1, 1);

    public Vector2 CameraRotationXBounds = new Vector2(0, 0);
    public Vector2 CameraRotationYBounds = new Vector2(0, 0);
    public Vector2 CameraDistanceBounds = new Vector2(0, 0);
}
