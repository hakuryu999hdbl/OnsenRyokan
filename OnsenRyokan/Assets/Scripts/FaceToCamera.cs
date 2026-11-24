using UnityEngine;

[DisallowMultipleComponent]
public sealed class FaceToCamera : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;

    private Quaternion originalRotation;

    private void OnEnable()
    {
        originalRotation = transform.rotation;

        if (targetCamera == null)
            targetCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (targetCamera == null)
            return;

        Vector3 forward = Vector3.ProjectOnPlane(targetCamera.transform.forward, Vector3.up);
        if (forward.sqrMagnitude > 0.000001f)
            transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
    }

    private void OnDisable()
    {
        transform.rotation = originalRotation;
    }
}
