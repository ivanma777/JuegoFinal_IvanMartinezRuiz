//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class InventorySelection : MonoBehaviour
//{
//    [SerializeField] private GameObject imagenUIPrefab;
//    [SerializeField] private Transform imagenesUIContainer;
//    [SerializeField] private Button confirmarSeleccionButton;
//    private List<Sprite> seleccionadas = new();
//    private List<SelectableImage> botonesActivos = new();

//    private List<Sprite> objetivosCorrectos = new(); // estas deben venir del InspectionTaskSystem

//    public System.Action<bool> OnConfirmarSeleccion;

//    public void MostrarInventario(List<Sprite> imagenesObjetos, List<Sprite> objetivos)
//    {
//        Limpiar();

//        objetivosCorrectos = objetivos;

//        foreach (Sprite sprite in imagenesObjetos)
//        {
//            GameObject go = Instantiate(imagenUIPrefab, imagenesUIContainer);
//            SelectableImage selectable = go.GetComponent<SelectableImage>();
//            selectable.Init(sprite, this);
//            botonesActivos.Add(selectable);
//        }

//        gameObject.SetActive(true);
//        confirmarSeleccionButton.onClick.AddListener(ConfirmarSeleccion);
//    }

//    public void AñadirSeleccion(Sprite sprite, SelectableImage boton)
//    {
//        if (!seleccionadas.Contains(sprite))
//        {
//            if (seleccionadas.Count < 3)
//            {
//                seleccionadas.Add(sprite);
//            }
//            else
//            {
//                boton.ResetVisual(); // deselecciona visualmente si pasa del límite
//            }
//        }
//    }

//    public void EliminarSeleccion(Sprite sprite, SelectableImage boton)
//    {
//        if (seleccionadas.Contains(sprite))
//            seleccionadas.Remove(sprite);
//    }

//    private void ConfirmarSeleccion()
//    {
//        bool acierto = objetivosCorrectos.TrueForAll(sprite => seleccionadas.Contains(sprite));

//        OnConfirmarSeleccion?.Invoke(acierto);

//        Limpiar();
//        gameObject.SetActive(false);
//    }

//    private void Limpiar()
//    {
//        foreach (Transform child in imagenesUIContainer)
//            Destroy(child.gameObject);

//        seleccionadas.Clear();
//        botonesActivos.Clear();
//        confirmarSeleccionButton.onClick.RemoveAllListeners();
//    }
//}
