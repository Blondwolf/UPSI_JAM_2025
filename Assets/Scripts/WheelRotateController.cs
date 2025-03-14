using UnityEngine;

public class WheelRotateController : MonoBehaviour
{
    public float rotationSpeed = 100f;

    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.back);
    }
}
