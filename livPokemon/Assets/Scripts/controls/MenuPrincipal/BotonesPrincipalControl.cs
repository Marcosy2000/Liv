using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BotonesPrincipalControl : MonoBehaviour
{
    int gameSaved = 0;
    int gameStarted = 0;

    public Button LoadButton;

    //LOADING
    public GameObject loadingScreen;
    public Slider slider;

    //public Animator transition;

    void Awake()
    {
        //transition.SetTrigger("Start");
        //QuestUIManager.uiManager.sceneNumber = 0;

        gameSaved = PlayerPrefs.GetInt("Guardado");                           //SACO EL VALOR DE GAMESTARTED 

        if (gameSaved == 1)
        {
            LoadButton.interactable = true;
        }
        else if (gameSaved == 0)
        {
            LoadButton.interactable = false;
        }
    }

    public void Load()
    {
        //transition.SetTrigger("Start");

        //GUARDO EL VALOR A 1 COMO TRUE
        gameStarted = 0;
        PlayerPrefs.SetInt("Started", gameStarted);

        //HABIAMOS GUARDADO, CARGO LA PARTIDA
        //gameSaved = 1;
        //PlayerPrefs.SetInt("Guardado", gameSaved);

        LoadLevel(1);

        //SceneManager.LoadScene("SampleScene");
    }

    public void start()
    {
        //QuestUIManager.uiManager.startGame = true;
        //transition.SetTrigger("Start");

        //GUARDO EL VALOR A 1 COMO TRUE
        gameStarted = 1;
        PlayerPrefs.SetInt("Started", gameStarted);

        //HABIAMOS GUARDADO, CARGO LA PARTIDA
        gameSaved = 0;
        PlayerPrefs.SetInt("Guardado", gameSaved);


        LoadLevel(1);

        //SceneManager.LoadScene("SampleScene");
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

}
