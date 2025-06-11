using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkManager : MonoBehaviour
{
    [SerializeField] private DayNight dayNightManager;

    [SerializeField] private Work workControll;
    

    [SerializeField] private float workHour;
    [SerializeField] private float restHour;

    private bool timeToWork;
    private bool timeToRest;

    private bool estaTrabajando;

    private bool noEstaTrabajando;
    

    public int Confianza;

    public bool EstaTrabajando { get => estaTrabajando; set => estaTrabajando = value; }


    // Update is called once per frame
    void Update()
    {
        WorkFlow();

        
    }

    void WorkFlow()
    {
        if (dayNightManager.hour >= workHour && dayNightManager.hour <= restHour )
        {
            timeToWork = true;
            timeToRest = false;

           
        }
        else
        {
            timeToRest = true;
            timeToWork = false;
           

        }

    }

    //void ControlladorConfianza()
    //{
    //    if (timeToWork && EstaTrabajando )
    //    {



    //    }
    //    else if (timeToRest)
    //    {


    //    }

    //}




}
