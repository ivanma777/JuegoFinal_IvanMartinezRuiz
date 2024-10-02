using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Vector3 W;
    [SerializeField] Vector3 S;
    [SerializeField] Vector3 A;
    [SerializeField] Vector3 D;
    [SerializeField] Vector3 Spaces;
    
    
    [SerializeField] float velocidad;
    [SerializeField] float velocidad1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {

            transform.Translate(velocidad * Time.deltaTime * (W).normalized, Space.World);

        }
        if (Input.GetKey(KeyCode.S))
        {

            transform.Translate(velocidad * Time.deltaTime * (S).normalized, Space.World);

        }
        if (Input.GetKey(KeyCode.A))
        {

            transform.Translate(velocidad * Time.deltaTime * (A).normalized, Space.World);

        }
        if (Input.GetKey(KeyCode.D))
        {

            transform.Translate(velocidad * Time.deltaTime * (D).normalized, Space.World);

        }
        if (Input.GetKey(KeyCode.Space))
        {

            transform.Translate(velocidad1 * Time.deltaTime * (Spaces).normalized, Space.World);

        }

    }
}
