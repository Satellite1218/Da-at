using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    public Transform[] spawnPoints;   // Inspector에서 할당해야 함
    public Transform[] groundPoints;  // Inspector에서 할당해야 함
    public GameObject[] fly_prefabs;  // Inspector에서 할당해야 함
    public GameObject[] ground_prefabs; // Inspector에서 할당해야 함

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

        Fly_Spawn();
        Ground_Spawn();
    }

    void Update()
    {
        // 테스트를 위한 코드
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
        // fly_prefabs 또는 spawnPoints가 null이거나 배열이 비어있을 경우 리턴
        if (fly_prefabs == null || fly_prefabs.Length == 0 || spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Fly 프리팹 또는 스폰 지점이 설정되지 않았습니다.");
            return;
        }

        Debug.Log("flyload");

        int randomPrefabIndex = Random.Range(0, fly_prefabs.Length);
        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);

        GameObject prefabToSpawn = fly_prefabs[randomPrefabIndex];
        Transform spawnPoint = spawnPoints[randomSpawnPointIndex];

        Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
    }

    public void Ground_Spawn()
    {
        // ground_prefabs 또는 groundPoints가 null이거나 배열이 비어있을 경우 리턴
        if (ground_prefabs == null || ground_prefabs.Length == 0 || groundPoints == null || groundPoints.Length == 0)
        {
            Debug.LogError("Ground 프리팹 또는 스폰 지점이 설정되지 않았습니다.");
            return;
        }

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
