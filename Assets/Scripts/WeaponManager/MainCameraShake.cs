using UnityEngine;
using System;
using System.Collections;

public class MainCameraShake : MonoBehaviour
{
    Transform cameraTransform;
    public static float Angle;
    public static Action<int> onShake;

    void Start()
    {
        cameraTransform = GetComponent<Transform>();
    }

    void OnEnable()
    {
        onShake += Shake;
    }

    void OnDisable()
    {
        onShake -= Shake;
    }

    void Shake(int angle)
    {
        StartCoroutine(SetAngle(angle));
    }

    IEnumerator SetAngle(int angle)
    {
        yield return new WaitForSeconds(0.2f);
        Angle += angle * Time.deltaTime;
        yield return new WaitForSeconds(0.2f);
        Angle = 0f;
    }
}
