using System.Collections;
using UnityEngine;

public class CameraManager : Manager
{
    [SerializeField] Vector2 mouseSensitivity;
    [SerializeField] Range panAxisRange;

    [SerializeField] Transform playerBody;
    [SerializeField] Transform lookAtHolder;
    Transform cameraObj => Camera.main.transform;

    private Quaternion initialRotation;
    private Vector2 rotation;
    
    void LateUpdate()
    {
        HandleCamera(inputHandler.MouseInput);
    }

    void HandleCamera(Vector2 mouseInput)
    {
        Vector2 mouseVelocity = Vector2.Scale(mouseInput, mouseSensitivity) / 100f;

        // Handle camera rotation
        PerformCameraRotation(mouseVelocity);
    }

    void PerformCameraRotation(Vector2 mouseInput)
    {
        rotation.x -= mouseInput.y;
        rotation.x = Mathf.Clamp(rotation.x, panAxisRange.min, panAxisRange.max);
        rotation.y += mouseInput.x;

        Quaternion xQuaternion = Quaternion.AngleAxis(rotation.x, Vector3.right);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotation.y, Vector3.up);

        lookAtHolder.rotation = initialRotation * yQuaternion * xQuaternion;
        cameraObj.rotation = lookAtHolder.rotation;

        playerBody.localRotation = Quaternion.RotateTowards(playerBody.localRotation, yQuaternion, 10f);
    }

    void OnEnable()
    {
        initialRotation = lookAtHolder.rotation;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}