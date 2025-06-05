using System.Collections.Generic;
using UnityEngine;

using System.Collections;

using UnityEngine.UI;
using System.Linq;

public class InspectionTaskSystem : MonoBehaviour
{
    [Header("Eventos")]
    [SerializeField] private InspectionTaskEvent taskEvent;
    [SerializeField] private VoidEvent taskCompletedEvent;
    [SerializeField] private TrustEvent trustEvent;
    [SerializeField] private MentalHealthEvent mentalHealthEvent;

    [Header("Configuración general")]
    [SerializeField] private InspectionSO taskData;
    [SerializeField] private Transform container;
    [SerializeField] private List<Transform> spawnPoints;

    public GameObject panelInspeccion; // Asignar en el Inspector
    private bool panelAbierto = false;


    [Header("UI")]
    
    [SerializeField] private GameObject imagenUIprefab;
    [SerializeField] private Transform imagenesUIContainer;
    [SerializeField] private Button confirmarSeleccionButton;

    private List<GameObject> objetosGenerados = new();
    private List<InspectableItemSO> itemsInstanciados = new();
    private List<Sprite> imagenesCorrectas = new();

    private Queue<GameObject> pool = new();
    private List<GameObject> activos = new();

    private bool taskActive = false;
    private float remainingTime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            panelAbierto = !panelAbierto;
            panelInspeccion.SetActive(panelAbierto);
        }
    }



    private void OnEnable()
    {
        taskEvent.Register(IniciarInspeccion);
        confirmarSeleccionButton.onClick.AddListener(ConfirmarSeleccion);
    }

    private void OnDisable()
    {
        taskEvent.UnRegister(IniciarInspeccion);
        confirmarSeleccionButton.onClick.RemoveListener(ConfirmarSeleccion);
    }

    private void IniciarInspeccion(InspectionSO data)
    {
        if (data == null || container == null)
        {
            Debug.LogError("[InspectionTaskSystem] Faltan referencias necesarias.");
            return;
        }

        CanvasManager.Instance.ShowTask(data);

        remainingTime = data.timeLimit;
        taskActive = true;
        StartCoroutine(TaskTimer());

        LimpiarObjetos();

        // Instanciar objetos en spawn points
        for (int i = 0; i < data.objectsIn; i++)
        {
            InspectableItemSO prefab = data.possibleItems[Random.Range(0, data.possibleItems.Count)];
            GameObject go = pool.Count > 0 ? pool.Dequeue() : Instantiate(prefab.prefab, container);
            go.SetActive(true);

            

            itemsInstanciados.Add(prefab);

            // Posicionamiento
            if (spawnPoints.Count > 0)
            {
                Transform spawn = spawnPoints[i % spawnPoints.Count];
                go.transform.position = spawn.position;
                go.transform.rotation = spawn.rotation;
            }
            else
            {
                go.transform.localPosition = Vector3.zero;
            }

            go.transform.SetParent(container);
            objetosGenerados.Add(go);
            activos.Add(go);
        }

        // Obtener 3 sprites de los objetos generados como correctos
        //imagenesCorrectas.Clear();
        //for (int i = 0; i < Mathf.Min(3, objetosGenerados.Count); i++)
        //{
        //    SpriteRenderer sr = objetosGenerados[i].GetComponentInChildren<SpriteRenderer>();
        //    if (sr != null)
        //        imagenesCorrectas.Add(sr.sprite);
        //}
        imagenesCorrectas.Clear();
        HashSet<Sprite> spritesUnicos = new();

        foreach (var itemSO in itemsInstanciados)
        {
            //SpriteRenderer sr = go.GetComponentInChildren<SpriteRenderer>();
            //if (sr != null)
            //    spritesUnicos.Add(sr.sprite);
            if (itemSO.referenceImage != null)
                spritesUnicos.Add(itemSO.referenceImage);
            else {
                Debug.Log("ERROR NO SE COGE");
                    }
        }

        imagenesCorrectas = spritesUnicos.ToList();
        SelectableImage.SetMaxSeleccion(imagenesCorrectas.Count);





        // Mostrar imágenes en UI, incluyendo distractores si hay más
        List<Sprite> imagenesMostrar = new List<Sprite>(data.referenceImages);
        Shuffle(imagenesMostrar);

        foreach (Sprite sprite in imagenesMostrar)
        {
            GameObject imgGO = Instantiate(imagenUIprefab, imagenesUIContainer);
            SelectableImage selectable = imgGO.GetComponentInChildren<SelectableImage>();
            selectable.image.sprite = sprite;
        }

        confirmarSeleccionButton.gameObject.SetActive(true);
    }

    private void ConfirmarSeleccion()
    {
        if (!taskActive) return;

        List<Sprite> seleccionadas = new();
        foreach (Transform child in imagenesUIContainer)
        {
            SelectableImage selectable = child.GetComponent<SelectableImage>();
            if (selectable != null && selectable.IsSelected())
            {
                seleccionadas.Add(selectable.GetSprite());
            }
        }

        // Verifica si se seleccionaron exactamente 3 imágenes
        //if (seleccionadas.Count != 3)
        //{
        //    Debug.Log("Debes seleccionar exactamente 3 imágenes.");
        //    return;
        //}
        if (seleccionadas.Count != imagenesCorrectas.Count)
        {
            Debug.Log("Debes seleccionar exactamente " + imagenesCorrectas.Count + " imágenes.");
            return;
        }



        //int aciertos = 0;
        //foreach (var sprite in seleccionadas)
        //{
        //    if (imagenesCorrectas.Contains(sprite))
        //        aciertos++;
        //}

        //bool exito = aciertos == 3;


        bool exito = imagenesCorrectas.All(correcta => seleccionadas.Contains(correcta));


        TaskCompleted(exito);
    }

    private IEnumerator TaskTimer()
    {
        CanvasManager.Instance.StartTimer(remainingTime);
        while (remainingTime > 0f && taskActive)
        {
            remainingTime -= Time.deltaTime;
            CanvasManager.Instance.UpdateTimer(remainingTime);
            yield return null;
        }

        if (taskActive)
        {
            TaskCompleted(false);
        }
    }

    private void TaskCompleted(bool success)
    {
        CanvasManager.Instance.StopTimer();
        StopAllCoroutines();
        taskActive = false;

        trustEvent.Raise(success ? 10 : -10);
        mentalHealthEvent.Raise(-5);
        CanvasManager.Instance.ShowResult(success, taskData);

        LimpiarObjetos();
        confirmarSeleccionButton.gameObject.SetActive(false);
        Debug.Log("[InspectionTaskSystem] Tarea completada. Éxito: " + success);
        taskCompletedEvent.Raise(new Void());


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
        imagenesCorrectas.Clear();

        foreach (Transform child in imagenesUIContainer)
        {
            Destroy(child.gameObject);
        }
    }

    // Mezcla una lista (Fisher-Yates)
    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
