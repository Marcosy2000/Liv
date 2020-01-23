using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QLogButtonScript : MonoBehaviour
{
    public int questID;
    public Text questTitle;
    //public GameObject ListaMisiones;

    //LOADING
    public GameObject loadingScreen;
    public Slider slider;

    public void ShowAllInfos()
    {
        //QuestUIManager.uiManager.ShowQuestLog(questID);
        QuestManager.questManager.ShowQuestLog(questID);

    }

    public void Misiones()
    {
        QuestUIManager.uiManager.VolverMenuGame();
        //ListaMisiones.SetActive(true);
        QuestUIManager.uiManager.ListaMisiones(); 
    }

    public void GoMenu()
    {
        LoadLevel(0);

        QuestUIManager.uiManager.VolverMenuGame();

        QuestUIManager.uiManager.sceneNumber = 0;


        //SceneManager.LoadScene("Menu");

        print("Cambio de escena al menu");
    }


    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            Debug.Log(progress);
            yield return null;
        }
    }


    public void Ajustes()
    {
        QuestUIManager.uiManager.VolverMenuGame();
        QuestUIManager.uiManager.AjustesPanel();
    }

    public void Volver()
    {
        QuestUIManager.uiManager.VolverMenuGame();
    }

    public void CloseAjustes()
    {

        QuestUIManager.uiManager.AjustesPanel();

        QuestUIManager.uiManager.VolverMenuGame();
    }

    public void ClosePanel()
    {
        QuestUIManager.uiManager.HideQuestLogPanel();

        QuestUIManager.uiManager.VolverMenuGame();
    }


}
