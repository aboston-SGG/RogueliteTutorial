using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraHeight = 12f;

    // When this object enters a trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Camera Trigger"))
        {
            Camera.main.transform.position = other.transform.position + Vector3.up * cameraHeight;
        }
    }
}
