using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    public float anglePerStep = 36f;
    public float rotationSpeed = 180f;

    private bool isRotating = false;

    private Quaternion targetRotation;
}
