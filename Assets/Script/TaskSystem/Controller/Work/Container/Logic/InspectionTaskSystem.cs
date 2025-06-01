using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionTaskSystem : MonoBehaviour
{
    [SerializeField] private InspectionTaskEvent taskEvent;
    [SerializeField] private VoidEvent taskCompletedEvent;
    [SerializeField] private Transform container;
    [SerializeField] private GameObject imagenUIprefab;
    [SerializeField] private Transform imagenesUIContainer;
    [SerializeField] private TrustEvent trustEvent;
    [SerializeField] private MentalHealthEvent mentalHealthEvent;

    private List<GameObject> objetosGenerados = new();
    private List<GameObject> objetivos = new();

    private Queue<GameObject> pool = new();
    private List<GameObject> activos = new();

    private bool taskActive = false;

    private void OnEnable() => taskEvent.Register(IniciarInspeccion);
    private void OnDisable() => taskEvent.UnRegister(IniciarInspeccion);

    private void IniciarInspeccion(InspectionSO data)
    {
        LimpiarObjetos();
        taskActive = true;

        // Generar objetos aleatorios
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
            go.transform.localPosition = Vector3.zero;
            objetosGenerados.Add(go);
            activos.Add(go);
        }

        // Definir objetivos de inspección (por ejemplo, los 3 primeros)
        objetivos = new List<GameObject>(objetosGenerados.GetRange(0, 3));

        // Mostrar imágenes UI
        foreach (var sprite in data.referenceImages)
        {
            var img = Instantiate(imagenUIprefab, imagenesUIContainer);
            img.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
        }

        Debug.Log("[InspectionTaskSystem] Inspección iniciada.");
    }

    public void RevisarObjeto(GameObject obj)
    {
        if (!taskActive) return;

        if (objetivos.Contains(obj))
        {
            objetivos.Remove(obj);
            Debug.Log(" Correcto. Objeto inspeccionado.");
            trustEvent.Raise(5);
            mentalHealthEvent.Raise(-5);
        }
        else
        {
            Debug.Log("Error. Este objeto no está en la lista.");
            trustEvent.Raise(-10);
        }

        if (objetivos.Count == 0)
        {
            taskActive = false;
            Debug.Log("[InspectionTaskSystem] Todos los objetivos inspeccionados.");
            taskCompletedEvent.Raise(new Void()); // Notifica al EventSystem que se completó
        }
    }

    private void LimpiarObjetos()
    {
        foreach (var go in activos)
        {
            go.SetActive(false);
            pool.Enqueue(go);
        }
        activos.Clear();
        objetosGenerados.Clear();
        objetivos.Clear();

        foreach (Transform child in imagenesUIContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
