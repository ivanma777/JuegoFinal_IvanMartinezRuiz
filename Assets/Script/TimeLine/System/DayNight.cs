using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    [Range(0.0f, 24f)] public float hour = 12;
    [SerializeField] private Transform sun;

    [SerializeField] private float dayDurationInMin;

    private float Xsun;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        hour += Time.deltaTime * (24 / (60 * dayDurationInMin));

        if (hour >= 24)
        {
            hour = 0;

        }

        RotacionSol();
    }

    void RotacionSol()
    {
        Xsun = 15 * hour;

        sun.localEulerAngles = new Vector3(Xsun, 0, 0);

        if (hour < 6 || hour > 18)
        {
            sun.GetComponent <Light>().intensity = 0;

        }
        else
        {
            sun.GetComponent<Light>().intensity = 1f;
        }

    }
}
