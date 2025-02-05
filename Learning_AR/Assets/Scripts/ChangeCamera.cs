using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ChangeCamera : MonoBehaviour
{
    [SerializeField] ARCameraManager cameraManager;
    CameraFacingDirection currentFacing;

    private void OnEnable()
    {
        currentFacing = cameraManager.currentFacingDirection;
    }

    public void OnPressChangeCamera()
    {
        currentFacing = currentFacing == CameraFacingDirection.World ?
                        CameraFacingDirection.User :
                        CameraFacingDirection.World;

        cameraManager.requestedFacingDirection = currentFacing;
    }
}
