using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    public Transform[] spawnPoints;   // Inspector���� �Ҵ��ؾ� ��
    public Transform[] groundPoints;  // Inspector���� �Ҵ��ؾ� ��
    public GameObject[] fly_prefabs;  // Inspector���� �Ҵ��ؾ� ��
    public GameObject[] ground_prefabs; // Inspector���� �Ҵ��ؾ� ��

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
        // �׽�Ʈ�� ���� �ڵ�
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
        // fly_prefabs �Ǵ� spawnPoints�� null�̰ų� �迭�� ������� ��� ����
        if (fly_prefabs == null || fly_prefabs.Length == 0 || spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Fly ������ �Ǵ� ���� ������ �������� �ʾҽ��ϴ�.");
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
        // ground_prefabs �Ǵ� groundPoints�� null�̰ų� �迭�� ������� ��� ����
        if (ground_prefabs == null || ground_prefabs.Length == 0 || groundPoints == null || groundPoints.Length == 0)
        {
            Debug.LogError("Ground ������ �Ǵ� ���� ������ �������� �ʾҽ��ϴ�.");
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
