using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectContainer : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;

    private bool EventContainer;

    void Update()
    {
        if(EventContainer)
        {
            int randomIndex = Random.Range(0, objects.Length);
           

            Instantiate(objects[randomIndex], transform.position, Quaternion.identity);

            EventContainer = false;
        }

        if(Input.GetKey(KeyCode.P))
        {
            EventContainer = true;

        }
        



    }
}
