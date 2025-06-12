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
        UpdateAmbientLight();

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

    void UpdateAmbientLight()
    {
        float targetAmbientIntensity;

        if (hour >= 6f && hour <= 18f)
        {
          
            float t = Mathf.InverseLerp(6f, 18f, hour);
            targetAmbientIntensity = Mathf.Lerp(0.1f, 1f, t);
        }
        else
        {
            
            float nightHour = hour < 6f ? hour + 24f : hour;
            float t = Mathf.InverseLerp(18f, 30f, nightHour);
            targetAmbientIntensity = Mathf.Lerp(1f, 0.1f, t);
        }

       
        RenderSettings.ambientIntensity = Mathf.Lerp(RenderSettings.ambientIntensity, targetAmbientIntensity, Time.deltaTime * 2f);
    }


}
