using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    [Range(0.0f, 24f)] public float hora = 12;
    [SerializeField] private Transform sol;

    [SerializeField] private float duracionDiaEnMin;

    private float solX;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        hora += Time.deltaTime * (24 / (60 * duracionDiaEnMin));

        if (hora >= 24)
        {
            hora = 0;

        }

        RotacionSol();
    }

    void RotacionSol()
    {
        solX = 15 * hora;

        sol.localEulerAngles = new Vector3(solX, 0, 0);

        if (hora < 6 || hora > 18)
        {
            sol.GetComponent <Light>().intensity = 0;

        }
        else
        {
            sol.GetComponent<Light>().intensity = 1f;
        }

    }
}
