using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionTaskSystem : MonoBehaviour
{
    [SerializeField] private InspectionTaskEvent taskEvent;
    [SerializeField] private Transform container;
    [SerializeField] private GameObject imagenUIprefab;
    [SerializeField] private Transform imagenesUIContainer;
    [SerializeField] private TrustEvent trustEvent;

    private List<GameObject> objetosGenerados = new();
    private List<GameObject> objetivos = new();

    private Queue<GameObject> pool = new();
    private List<GameObject> activos = new();

    private void OnEnable() => taskEvent.Register(IniciarInspeccion);
    private void OnDisable() => taskEvent.UnRegister(IniciarInspeccion);

    private void IniciarInspeccion(InspectionSO data)
    {
        LimpiarObjetos();

        // Elegir objetos aleatorios
        for (int i = 0; i < data.objectsIn; i++)
        {
            var prefab = data.possibleItems[Random.Range(0, data.possibleItems.Count)];

            GameObject go;
            if (pool.Count > 0)
            {
                go = pool.Dequeue();
                go.SetActive(true);
            }
            else
            {
                go = Instantiate(prefab, container);
            }

            go.transform.SetParent(container);
            go.transform.localPosition = Vector3.zero; // O ajusta posici�n si quieres m�s variedad
            objetosGenerados.Add(go);
        }

        // Seleccionar objetivos de la lista generada (por ejemplo, los 3 primeros)
        objetivos = new List<GameObject>(objetosGenerados.GetRange(0, 3));

        // Mostrar im�genes relacionadas
        for (int i = 0; i < data.referenceImages.Length; i++)
        {
            var img = Instantiate(imagenUIprefab, imagenesUIContainer);
            img.GetComponent<UnityEngine.UI.Image>().sprite = data.referenceImages[i];
        }

        Debug.Log("Inspecci�n iniciada. Revisa el contenedor.");
    }

    private void LimpiarObjetos()
    {
        foreach (var go in activos)
        {
            go.SetActive(false);
            pool.Enqueue(go);
        }
        activos.Clear();

        // Limpieza de im�genes UI
        foreach (Transform child in imagenesUIContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void RevisarObjeto(GameObject obj)
    {
        if (objetivos.Contains(obj))
        {
            objetivos.Remove(obj);
            Debug.Log("Correcto. Objeto inspeccionado.");

            // Aumentar confianza +10
            trustEvent.Raise(10);
        }
        else
        {
            Debug.Log("Error. Este objeto no est� en la lista.");
            trustEvent.Raise(-10);
        }
    }
}
