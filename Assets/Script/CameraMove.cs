using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float viewRadius;
    [SerializeField] private float viewAngle;

    [SerializeField] private LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;

    [SerializeField] private Vector3 position;

    public List<Transform> visibleTargets = new List<Transform>();

    private void Start()
    {
        StartCoroutine("FindTargetsWithDeLay", 0.2f);
    }

    //IEnumerator FindTargetsWithDelay(float delay)
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds (delay);

    //    }

    //}

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float disToTarget = Vector3.Distance(transform.forward, target.position);

                if (Physics.Raycast(transform.position, dirToTarget, disToTarget, obstacleMask)){ 
                visibleTargets.Add(target);
                }
                   

            }      
        }



            
    }
}


    


