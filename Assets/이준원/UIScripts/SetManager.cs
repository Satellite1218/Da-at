using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetManager : MonoBehaviour
{
    #region Singleton
    public static SetManager instance;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion


    public int Gold;

    public GameObject InventoryPanel;
    public GameObject ShelterPanel;


    void Start()
    {
        Gold = 0;
        InventoryPanel = GameObject.Find("UICanvas");

        StartCoroutine(LoadSceneAndFindShelterPanel("AnimalShelter"));
    }

    IEnumerator LoadSceneAndFindShelterPanel(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        ShelterPanel = GameObject.Find("ShelterCanvas");
        ShelterPanel.SetActive(false);
        SceneManager.LoadScene("GroundMain");
    }
    public void LoadShelter()
    {
        InventoryPanel.SetActive(false);
        ShelterPanel.SetActive(true);
        SceneManager.LoadScene("AnimalShelter");
    }

    public void LoadMain()
    {
        ShelterPanel.SetActive(false);
        InventoryPanel.SetActive(true);
        SceneManager.LoadScene("GroundMain");
    }
}
