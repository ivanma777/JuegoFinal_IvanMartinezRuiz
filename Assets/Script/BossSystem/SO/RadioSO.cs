using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Radio/Radio Event")]
public class RadioSO : ScriptableObject
{
    [Header("Respuestas por nivel de confianza")]
    public List<RadioResponseSO> highAnswer;
    public List<RadioResponseSO> midAnswer;
    public List<RadioResponseSO> lowAnswer;

    public AudioClip audioClip;
    public float midAnswerThreshold = 40f;
    public float highAnswerThreshold = 70f;

    /// <summary>
    /// Devuelve una respuesta aleatoria según el nivel de confianza.
    /// </summary>
    public RadioResponseSO AnswerByTrust(int nivelConfianza)
    {
        if (nivelConfianza >= highAnswerThreshold && highAnswer.Count > 0)
        {
            return highAnswer[Random.Range(0, highAnswer.Count)];
        }
        else if (nivelConfianza >= midAnswerThreshold && midAnswer.Count > 0)
        {
            return midAnswer[Random.Range(0, midAnswer.Count)];
        }
        else if (lowAnswer.Count > 0)
        {
            return lowAnswer[Random.Range(0, lowAnswer.Count)];
        }
        else
        {
            Debug.LogWarning("No hay respuestas definidas para el nivel de confianza.");
            return null;
        }
    }
}
