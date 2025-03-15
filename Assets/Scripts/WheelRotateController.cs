using System;
using UnityEngine;

public class WheelRotateController : MonoBehaviour
{
    public int bpm = 120;
    public int beatsNumber = 32;

    private float rotationSpeed = 360f; // 1 revolution = 1 seconde with 360 speed
    public float RotationSpeed => rotationSpeed;

    private void Update()
    {
        rotationSpeed = 360 / CalculateDuration(bpm, beatsNumber);

        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.back);
    }

    public static float CalculateDuration(int bpm, int beats)
    {
        return (beats * 60.0f) / bpm;
    }
}
