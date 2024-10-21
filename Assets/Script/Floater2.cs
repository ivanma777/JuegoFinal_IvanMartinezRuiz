using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater2 : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [SerializeField]float  depthBefSub;

    [SerializeField]float displacementAmt;

    [SerializeField] int floater;

    [SerializeField] float waterDrags;

    [SerializeField] float waterAngularDrags;

    [SerializeField] NVWaterShaders water;


    NVWaterShaders Search;
    NVWaterShaders SearchResult;

    private void FixedUpdate()
    {
        rb.AddForceAtPosition(Physics.gravity / floater, transform.position, ForceMode.Acceleration);
      
    }


}
