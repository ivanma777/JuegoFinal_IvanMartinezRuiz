using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightsFollow : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float smoothSpeed = 10f;
    [SerializeField] private Vector3 offset = new Vector3(0, -0.2f, 0.5f);

    void Update()
    {
        if (cameraTransform == null) return;

        Vector3 targetPosition = cameraTransform.position + cameraTransform.forward * offset.z + cameraTransform.up * offset.y + cameraTransform.right * offset.x;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);

        Quaternion targetRotation = Quaternion.LookRotation(cameraTransform.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
    }
}
