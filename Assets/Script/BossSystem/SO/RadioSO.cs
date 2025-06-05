using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Radio/Radio Event")]
public class RadioSO : ScriptableObject
{
    [Header("Respuestas por nivel de confianza")]
    public List<RadioResponseSO> respuestasAltas;
    public List<RadioResponseSO> respuestasMedias;
    public List<RadioResponseSO> respuestasBajas;

    public AudioClip audioClip;
    public float confianzaMediaThreshold = 40f;
    public float confianzaAltaThreshold = 70f;

    /// <summary>
    /// Devuelve una respuesta aleatoria según el nivel de confianza.
    /// </summary>
    public RadioResponseSO ObtenerRespuestaPorConfianza(int nivelConfianza)
    {
        if (nivelConfianza >= confianzaAltaThreshold && respuestasAltas.Count > 0)
        {
            return respuestasAltas[Random.Range(0, respuestasAltas.Count)];
        }
        else if (nivelConfianza >= confianzaMediaThreshold && respuestasMedias.Count > 0)
        {
            return respuestasMedias[Random.Range(0, respuestasMedias.Count)];
        }
        else if (respuestasBajas.Count > 0)
        {
            return respuestasBajas[Random.Range(0, respuestasBajas.Count)];
        }
        else
        {
            Debug.LogWarning("No hay respuestas definidas para el nivel de confianza.");
            return null;
        }
    }
}
