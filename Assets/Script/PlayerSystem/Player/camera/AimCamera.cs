using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Rango rango;

    [SerializeField] private WorkManager workManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rango.EstaEnRango)
        {

            Vector3 targetOrientation = target.position - transform.position;
            Debug.DrawRay(transform.position, targetOrientation, Color.green);

            Quaternion targetOrientationQuaternion = Quaternion.LookRotation(targetOrientation);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetOrientationQuaternion, Time.deltaTime);

            workManager.EstaTrabajando = true;

        }
        
        
    }
}
