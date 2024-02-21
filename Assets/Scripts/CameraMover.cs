using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [Tooltip("Reference To Camera")]
    [SerializeField]
    private Camera cameraMove;

    [Tooltip("List of transforms to move the camera to")]
    [SerializeField]
    private List<Transform> placements;

    [Tooltip("List of times to move the camera to the corresponding transforms")]
    [SerializeField]
    private List<float> times;

    private void Start()
    {
        StartCoroutine(MoveCameraAfterDelays());
    }

    private IEnumerator MoveCameraAfterDelays()
    {
        for (int i = 0; i < placements.Count; i++)
        {
            yield return new WaitForSeconds(times[i]);
            MoveCameraToTransform(placements[i]);
        }
    }

    private void MoveCameraToTransform(Transform targetTransform)
    {
        cameraMove.transform.position = targetTransform.position;
        cameraMove.transform.rotation = targetTransform.rotation;
    }
}
