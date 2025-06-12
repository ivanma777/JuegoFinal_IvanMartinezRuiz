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
        //UpdateAmbientLight();
        UpdateSkyboxExposure();
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

    void UpdateSkyboxExposure()
    {
        float targetExposure;

        if (hour < 6 || hour > 18)
        {
            // De 18 a 6 baja progresivamente
            float t = hour < 6 ? hour / 6f : (24 - hour) / 6f; // t entre 0 y 1
            targetExposure = Mathf.Lerp(0.3f, 1.3f, t);
        }
        else
        {
            // De 6 a 18 sube progresivamente
            float t = (hour - 6f) / 12f; // t entre 0 y 1
            targetExposure = Mathf.Lerp(0.3f, 1.3f, t);
        }

        RenderSettings.skybox.SetFloat("_Exposure", targetExposure);
    }

}
