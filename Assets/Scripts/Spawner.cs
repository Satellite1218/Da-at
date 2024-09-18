using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    public Transform[] spawnPoints;
    public Transform[] groundPoints;
    public GameObject[] fly_prefabs;
    public GameObject[] ground_prefabs;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        spawnPoints = FilterPoints(spawnPoints);
        groundPoints = FilterPoints(groundPoints);
    }

    void Update()
    {
        //테스트 하려고 만들어둔거임
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fly_Spawn();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Ground_Spawn();
        }
    }

    public void Fly_Spawn()
    {
        if (fly_prefabs.Length == 0 || spawnPoints.Length == 0) return;
        Debug.Log("flyload");

        int randomPrefabIndex = Random.Range(0, fly_prefabs.Length);
        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);

        GameObject prefabToSpawn = fly_prefabs[randomPrefabIndex];
        Transform spawnPoint = spawnPoints[randomSpawnPointIndex];

        Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
    }

    public void Ground_Spawn()
    {
        if (ground_prefabs.Length == 0 || groundPoints.Length == 0) return;
        Debug.Log("groundload");

        int randomPrefabIndex = Random.Range(0, ground_prefabs.Length);
        int randomSpawnPointIndex = Random.Range(0, groundPoints.Length);

        GameObject prefabToSpawn = ground_prefabs[randomPrefabIndex];
        Transform groundPoint = groundPoints[randomSpawnPointIndex];

        Instantiate(prefabToSpawn, groundPoint.position, groundPoint.rotation);
    }

    private Transform[] FilterPoints(Transform[] points)
    {
        List<Transform> filteredPoints = new List<Transform>();
        foreach (Transform point in points)
        {
            if (point != this.transform)
            {
                filteredPoints.Add(point);
            }
        }
        return filteredPoints.ToArray();
    }
}
