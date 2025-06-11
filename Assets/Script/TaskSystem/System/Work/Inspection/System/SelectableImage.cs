using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectableImage : MonoBehaviour
{


    public Image image;
    private bool selected = false;

    private static int maxSeleccion = 3;
    private static int totalSelected = 0;

    public static void SetMaxSeleccion(int cantidad)
    {
        maxSeleccion = cantidad;
        totalSelected = 0;
    }

    public void OnClick()
    {
        if (!selected)
        {
            if (totalSelected >= maxSeleccion)
                return;

            selected = true;
            totalSelected++;
            ChangeVisual(true);
        }
        else
        {
            selected = false;
            totalSelected--;
            ChangeVisual(false);
        }
    }

    public bool IsSelected() => selected;

    public Sprite GetSprite() => image.sprite;

    private void ChangeVisual(bool activo)
    {
       
        GetComponent<Image>().color = activo ? Color.green : Color.grey;
    }


}
