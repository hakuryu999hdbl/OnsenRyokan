using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Camera))]
public sealed class PanoramaCamera : MonoBehaviour
{
    [SerializeField, Min(0f)] private float keyboardSpeed = 90f;
    [SerializeField, Min(0f)] private float dragSensitivity = 0.2f;
    [SerializeField, Range(0f, 89f)] private float pitchLimit = 85f;

    private Quaternion initialRotation;
    private Vector2 previousMousePosition;
    private float yaw;
    private float pitch;

    private void Awake()
    {
        initialRotation = transform.localRotation;
    }

    private void Update()
    {
        float horizontal = (Input.GetKey(KeyCode.RightArrow) ? 1f : 0f)
            - (Input.GetKey(KeyCode.LeftArrow) ? 1f : 0f);
        float vertical = (Input.GetKey(KeyCode.UpArrow) ? 1f : 0f)
            - (Input.GetKey(KeyCode.DownArrow) ? 1f : 0f);
        Vector2 turn = new Vector2(horizontal, vertical) * (keyboardSpeed * Time.unscaledDeltaTime);

        Vector2 mousePosition = Input.mousePosition;
        if (Input.GetMouseButton(1) && !Input.GetMouseButtonDown(1))
            turn += (mousePosition - previousMousePosition) * dragSensitivity;

        previousMousePosition = mousePosition;
        yaw = Mathf.Repeat(yaw + turn.x, 360f);
        pitch = Mathf.Clamp(pitch - turn.y, -pitchLimit, pitchLimit);
        transform.localRotation = initialRotation * Quaternion.Euler(pitch, yaw, 0f);
    }
}
