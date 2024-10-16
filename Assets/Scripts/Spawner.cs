using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 프리팹과 그에 대응하는 거리를 저장하는 구조체
[System.Serializable]
public struct PrefabWithDistance
{
    public GameObject prefab;  // 추가할 프리팹
    public float spawnDistance;  // 이 프리팹이 추가될 거리
}

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    public Transform[] spawnPoints;   // Inspector에서 할당해야 함
    public Transform[] groundPoints;  // Inspector에서 할당해야 함
    public GameObject[] fly_prefabs;  // Inspector에서 할당해야 함
    public GameObject[] ground_prefabs; // Inspector에서 할당해야 함
    public Distance distanceScript;   // 거리 계산 스크립트 참조

    public List<PrefabWithDistance> additionalFlyPrefabs;  // 추가할 fly 프리팹과 거리 리스트
    public List<PrefabWithDistance> additionalGroundPrefabs;  // 추가할 ground 프리팹과 거리 리스트

    private List<int> addedFlyPrefabIndexes = new List<int>();  // 추가된 fly 프리팹의 인덱스 저장
    private List<int> addedGroundPrefabIndexes = new List<int>();  // 추가된 ground 프리팹의 인덱스 저장

    private float lastCheckedDistance = 0f; // 마지막으로 체크한 거리

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
        // 추가 프리팹들의 스폰 거리를 20배로 조정
        MultiplySpawnDistance(additionalFlyPrefabs, 10);
        MultiplySpawnDistance(additionalGroundPrefabs, 10);

        spawnPoints = FilterPoints(spawnPoints);
        groundPoints = FilterPoints(groundPoints);
    }

    void Update()
    {
        if (distanceScript != null)
        {
            float currentDistance = Mathf.Abs(distanceScript.totalDistance);

            // currentDistance가 25를 처음 넘을 때 한 번만 호출
            if (currentDistance > 25f && lastCheckedDistance <= 25f)
            {
                lastCheckedDistance = currentDistance;

                // Fly_Spawn 4번 호출
                for (int i = 0; i < 4; i++)
                {
                    Fly_Spawn();
                }

                // Ground_Spawn 1번 호출
                Ground_Spawn();
            }

            // fly 프리팹 추가 처리
            for (int i = 0; i < additionalFlyPrefabs.Count; i++)
            {
                if (currentDistance >= additionalFlyPrefabs[i].spawnDistance && !addedFlyPrefabIndexes.Contains(i))
                {
                    AddFlyPrefab(additionalFlyPrefabs[i].prefab);
                    addedFlyPrefabIndexes.Add(i);  // 중복 추가 방지
                }
            }

            // ground 프리팹 추가 처리
            for (int i = 0; i < additionalGroundPrefabs.Count; i++)
            {
                if (currentDistance >= additionalGroundPrefabs[i].spawnDistance && !addedGroundPrefabIndexes.Contains(i))
                {
                    AddGroundPrefab(additionalGroundPrefabs[i].prefab);
                    addedGroundPrefabIndexes.Add(i);  // 중복 추가 방지
                }
            }
        }
    }

    // 스폰 거리를 20배로 늘리는 함수
    private void MultiplySpawnDistance(List<PrefabWithDistance> prefabs, float multiplier)
    {
        for (int i = 0; i < prefabs.Count; i++)
        {
            // PrefabWithDistance 구조체를 임시 변수로 가져온 후 수정
            PrefabWithDistance tempPrefab = prefabs[i];
            tempPrefab.spawnDistance *= multiplier;

            // 수정된 구조체를 다시 리스트에 넣어줌
            prefabs[i] = tempPrefab;
        }
    }

    public void Fly_Spawn()
    {
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

        // 프리팹의 기본 y축 값을 가져옴
        float prefabY = prefabToSpawn.transform.position.y;

        // groundPoint의 x축 값과 프리팹의 기본 y축 값을 사용하여 스폰
        Vector3 spawnPosition = new Vector3(groundPoint.position.x, prefabY, groundPoint.position.z);

        Instantiate(prefabToSpawn, spawnPosition, groundPoint.rotation);
    }


    private void AddFlyPrefab(GameObject prefab)
    {
        List<GameObject> flyPrefabsList = new List<GameObject>(fly_prefabs);
        flyPrefabsList.Add(prefab);
        fly_prefabs = flyPrefabsList.ToArray();  // 배열을 다시 설정
        Debug.Log("추가된 Fly 프리팹: " + prefab.name);
    }

    private void AddGroundPrefab(GameObject prefab)
    {
        List<GameObject> groundPrefabsList = new List<GameObject>(ground_prefabs);
        groundPrefabsList.Add(prefab);
        ground_prefabs = groundPrefabsList.ToArray();  // 배열을 다시 설정
        Debug.Log("추가된 Ground 프리팹: " + prefab.name);
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
