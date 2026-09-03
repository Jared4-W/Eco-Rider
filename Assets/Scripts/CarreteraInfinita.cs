/*using System.Collections;
using UnityEngine;

public class CarreteraInfinita : MonoBehaviour
{
    [Header("Prefabs en orden")]
    [Tooltip("Ciudad(0-9), Bosque(10-19), Playa(20-29), Industria(30-39), Montaña(40-49)")]
    [SerializeField]
    private GameObject[] sectionsPrefabs;

    [Header("Configuración")]
    [SerializeField]
    private int visibleSections = 10;

    [SerializeField]
    private float sectionLength = 200f;

    [SerializeField]
    private float updateInterval = 0.1f;

    private GameObject[] sectionsPool;
    private GameObject[] activeSections;

    private Transform playerTransform;

    private int nextSectionIndex = 0;

    private WaitForSeconds waitDelay;

    private void Awake()
    {
        waitDelay = new WaitForSeconds(updateInterval);
    }

    private void Start()
    {
        GameObject player =
            GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError(
                "No existe ningún GameObject con Tag 'Player'"
            );

            enabled = false;
            return;
        }

        playerTransform = player.transform;

        if (sectionsPrefabs.Length == 0)
        {
            Debug.LogError(
                "No hay prefabs asignados en sectionsPrefabs."
            );

            enabled = false;
            return;
        }

        visibleSections =
            Mathf.Min(
                visibleSections,
                sectionsPrefabs.Length
            );

        CreatePool();

        SpawnInitialSections();

        StartCoroutine(UpdateSectionsCoroutine());
    }

    private void CreatePool()
    {
        int totalPrefabs = sectionsPrefabs.Length;

        sectionsPool = new GameObject[totalPrefabs];

        for (int i = 0; i < totalPrefabs; i++)
        {
            GameObject section =
                Instantiate(sectionsPrefabs[i]);

            section.SetActive(false);

            sectionsPool[i] = section;
        }

        Debug.Log(
            $"Pool creado con {totalPrefabs} secciones."
        );
    }

    private void SpawnInitialSections()
    {
        activeSections =
            new GameObject[visibleSections];

        for (int i = 0; i < visibleSections; i++)
        {
            GameObject section =
                GetNextSection();

            Vector3 pos =
                section.transform.position;

            pos.z = i * sectionLength;

            section.transform.position = pos;

            section.SetActive(true);

            activeSections[i] = section;
        }
    }

    private IEnumerator UpdateSectionsCoroutine()
    {
        while (true)
        {
            UpdateSectionPositions();

            yield return waitDelay;
        }
    }

    private void UpdateSectionPositions()
    {
        float playerZ =
            playerTransform.position.z;

        for (int i = 0; i < activeSections.Length; i++)
        {
            GameObject section =
                activeSections[i];

            if (
                section.transform.position.z <
                playerZ - sectionLength
            )
            {
                MoveSectionForward(i);
            }
        }
    }

    private void MoveSectionForward(int sectionIndex)
    {
        GameObject oldSection =
            activeSections[sectionIndex];

        Vector3 oldPosition =
            oldSection.transform.position;

        oldSection.SetActive(false);

        GameObject nextSection =
            GetNextSection();

        float newZ =
            oldPosition.z +
            (sectionLength * visibleSections);

        Vector3 pos =
            nextSection.transform.position;

        pos.z = newZ;

        nextSection.transform.position = pos;

        nextSection.SetActive(true);

        activeSections[sectionIndex] =
            nextSection;
    }

    private GameObject GetNextSection()
    {
        GameObject section =
            sectionsPool[nextSectionIndex];

        nextSectionIndex++;

        if (nextSectionIndex >= sectionsPool.Length)
        {
            nextSectionIndex = 0;
        }

        return section;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (visibleSections < 3)
            visibleSections = 3;

        if (sectionLength < 1f)
            sectionLength = 1f;

        if (updateInterval < 0.02f)
            updateInterval = 0.02f;
    }
#endif
}
*//*
using System.Collections;
using UnityEngine;

public class CarreteraInfinita : MonoBehaviour
{
    [SerializeField]
    GameObject[] sectionsPrefabs; // Array para variedad visual

    GameObject[] sectionsPool = new GameObject[20]; // Reserva de objetos

    GameObject[] sections = new GameObject[10]; // Secciones visibles en pantalla

    Transform playerCarTransform;

    WaitForSeconds waitFor100ms = new WaitForSeconds(0.1f); // Optimización de memoria

    const float sectionLenght = 200; // Longitud de cada pieza

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   // Localiza al jugador mediante el tag "Player" 
        playerCarTransform = GameObject.FindGameObjectWithTag("Player").transform;

        int prefabIndex = 0;

        //CREAR POOL PARA CARRETERA INFINITA
        for(int i = 0; i < sectionsPool.Length; i++)
        {
            sectionsPool[i] = Instantiate(sectionsPrefabs[prefabIndex]);
            sectionsPool[i].SetActive(false);

            prefabIndex++;
            // Reinicia el índice si hay menos prefabs que el tamaño del pool
            if (prefabIndex > sectionsPrefabs.Length - 1)
                prefabIndex = 0;
        }

        // Generar las primeras 10 secciones visibles al inicio
        for (int i = 0; i < sections.Length; i++)
        {
            GameObject randomSection = GetRandomSectionFromPool();

            // Posiciona las secciones una tras otra en el eje Z
            randomSection.transform.position = new Vector3(sectionsPool[i].transform.position.x, 0, i * sectionLenght);
            randomSection.SetActive(true);

            sections[i] = randomSection;
        }
        // Inicia la rutina de actualización periódica
        StartCoroutine(UpdateLessOftenCO());
    }
    // Corrutina para evitar el uso excesivo de Update() en cada frame
    IEnumerator UpdateLessOftenCO()
    {
        while (true)
        {
            UpdateSectionPositions();
            yield return waitFor100ms;
        }
    }
    // Comprueba si el jugador pasó una sección para reutilizarla
    void UpdateSectionPositions()
    {
        for(int i = 0; i < sections.Length; i++)
        {
            // Verifica si la sección ha quedado atrás del coche
            if (sections[i].transform.position.z - playerCarTransform.position.z < -sectionLenght)
            {
                //
                Vector3 lastSectionPosition = sections[i].transform.position;
                sections[i].SetActive(false);

                // Obtiene una nueva sección aleatoria del pool y la coloca adelante
                sections[i] = GetRandomSectionFromPool();

                // La nueva posición es la anterior más la longitud total de la carretera visible
                sections[i].transform.position = new Vector3(lastSectionPosition.x, 0, lastSectionPosition.z + sectionLenght * sections.Length);
                sections[i].SetActive(true);
            }
        }
    }

    // Lógica para buscar un objeto disponible (inactivo) en el pool
    GameObject GetRandomSectionFromPool()
    {
        int randomIndex = Random.Range(0, sectionsPool.Length);

        bool isNewSectionFound = false;

        while(!isNewSectionFound)
        {
            // Si el objeto no está activo, puede ser reutilizado
            if (!sectionsPool[randomIndex].activeInHierarchy)
                isNewSectionFound = true;
            else
            {
                // Si está ocupado, busca en el siguiente índice del array
                randomIndex++;

                if (randomIndex > sectionsPool.Length - 1)
                    randomIndex = 0;
            }
        }
        return sectionsPool[randomIndex];
    }
}*/

using System.Collections;
using UnityEngine;

public class CarreteraInfinita : MonoBehaviour
{
    [SerializeField]
    GameObject[] sectionsPrefabs;

    GameObject[] sectionsPool = new GameObject[31];
    GameObject[] sections = new GameObject[3];

    Transform playerCarTransform;

    WaitForSeconds waitFor100ms = new WaitForSeconds(0.1f);

    const float sectionLenght = 200;

    int currentPrefabIndex = -1;

    void Start()
    {
        playerCarTransform = GameObject.FindGameObjectWithTag("Player").transform;

        int prefabIndex = 0;

        // CREAR POOL
        for (int i = 0; i < sectionsPool.Length; i++)
        {
            sectionsPool[i] = Instantiate(sectionsPrefabs[prefabIndex]);
            sectionsPool[i].SetActive(false);

            prefabIndex++;

            if (prefabIndex > sectionsPrefabs.Length - 1)
                prefabIndex = 0;
        }

        // GENERAR SECCIONES INICIALES (SIN CAMBIAR POSICIONES ORIGINALES)
        for (int i = 0; i < sections.Length; i++)
        {
            GameObject section = GetNextSectionFromPool();

            section.transform.position = new Vector3(
                sectionsPool[i].transform.position.x,
                0,
                i * sectionLenght
            );

            section.SetActive(true);
            sections[i] = section;
        }

        StartCoroutine(UpdateLessOftenCO());
    }

    IEnumerator UpdateLessOftenCO()
    {
        while (true)
        {
            UpdateSectionPositions();
            yield return waitFor100ms;
        }
    }

    void UpdateSectionPositions()
    {
        for (int i = 0; i < sections.Length; i++)
        {
            if (sections[i].transform.position.z - playerCarTransform.position.z < -sectionLenght)
            {
                Vector3 lastSectionPosition = sections[i].transform.position;

                sections[i].SetActive(false);

                sections[i] = GetNextSectionFromPool();

                sections[i].transform.position = new Vector3(
                    lastSectionPosition.x,
                    0,
                    lastSectionPosition.z + sectionLenght * sections.Length
                );

                // INTEGRACIÓN DE AUTOS Y MONEDAS
                RoadSection rs = sections[i].GetComponent<RoadSection>();
                if (rs != null)
                {
                    rs.ResetSection();
                }

                sections[i].SetActive(true);
            }
        }
    }

    GameObject GetNextSectionFromPool()
    {
        bool isNewSectionFound = false;

        while (!isNewSectionFound)
        {
            currentPrefabIndex++;

            if (currentPrefabIndex >= sectionsPool.Length)
                currentPrefabIndex = 0;

            if (!sectionsPool[currentPrefabIndex].activeInHierarchy)
                isNewSectionFound = true;
        }

        return sectionsPool[currentPrefabIndex];
    }
}