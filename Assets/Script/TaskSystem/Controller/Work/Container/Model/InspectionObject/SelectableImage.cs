using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectableImage : MonoBehaviour
{
    //public Image image; 
    //private bool selected = false; 

    //public void ToggleSelect()
    //{

    //    selected = !selected;


    //    GetComponent<Image>().color = selected ? Color.green : Color.grey;
    //}

    //public bool IsSelected() => selected; 

    //public Sprite GetSprite() => image.sprite; 


    public Image image;
    private bool selected = false;

    private static int maxSeleccion = 3;
    private static int totalSeleccionadas = 0;

    public static void SetMaxSeleccion(int cantidad)
    {
        maxSeleccion = cantidad;
        totalSeleccionadas = 0;
    }

    public void OnClick()
    {
        if (!selected)
        {
            if (totalSeleccionadas >= maxSeleccion)
                return;

            selected = true;
            totalSeleccionadas++;
            CambiarVisual(true);
        }
        else
        {
            selected = false;
            totalSeleccionadas--;
            CambiarVisual(false);
        }
    }

    public bool IsSelected() => selected;

    public Sprite GetSprite() => image.sprite;

    private void CambiarVisual(bool activo)
    {
        // Cambia visualmente la selección: puedes usar un borde, color o escala
        GetComponent<Image>().color = activo ? Color.green : Color.grey;
    }


}
