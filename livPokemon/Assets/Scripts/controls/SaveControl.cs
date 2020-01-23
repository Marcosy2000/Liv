using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveControl : MonoBehaviour
{
    //LOADING
    public GameObject loadingScreen;
    public Slider slider;

    //PERSONAJE LIV
    float jugadorposicionX;
    float jugadorposicionY;
    float jugadorposicionZ;

    float jugadorrotacionX;
    float jugadorrotacionY;
    float jugadorrotacionZ;

    public GameObject personaje;
    public GameObject meshPlayer;

    CharacterController controller;

    //CAMARA LIV
    public GameObject Camara;

    //VALORES VARIOS
    int monedasSave;
    public int[] IDSave;
    int SizeDone;
    string QuestProgress;

    //CONTROL
    public int gameSaved = 0;
    int gameLoaded = 0;
    int gameStarted = 0;
    int SizeCurrentList = 0;
    int SizeCurrentListSaved = 0;
    int sceneIndex;

    //BOTONES CONTROL
    public Button LoadButton;



    public void Awake()
    {
        gameSaved = PlayerPrefs.GetInt("Guardado");

        if (gameSaved == 0)
        {
            print("Deshabilitando");
            LoadButton.interactable = false;
        }
        else
        {
            print("Habilitando");
            LoadButton.interactable = true;
        }
    }
    public void Start ()
    {
        //gameSaved = PlayerPrefs.GetInt("Guardado");

        //print("gameSaved: " + gameSaved);

        /*if (gameSaved ==0)
        {
            print("Deshabilitando");
            LoadButton.interactable = false;
        }
        else
        {
            print("Habilitando");
            LoadButton.interactable = true;
        }*/

    }

    public void Update()
    {
        //print("gameSaved: " + gameSaved);
        //print("gameStarted: " + gameStarted);

        //gameSaved = PlayerPrefs.GetInt("Guardado");
        //print("gameSaved: " + gameSaved);

        print("gameSaved: " + gameSaved);

    }
    public void Cargar()
    {
        QuestUIManager.uiManager.VolverMenuGame();
        QuestUIManager.uiManager.ConfirmationPanel();
    }

    public void Si()
    {
        LoadLevel(1);

        QuestUIManager.uiManager.ConfirmationPanel();
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
            float progress = Mathf.Clamp01(operation.progress/.9f);

            slider.value = progress;

            yield return null;
        }
    }

    public void No()
    {
        QuestUIManager.uiManager.ConfirmationPanel();

        QuestUIManager.uiManager.VolverMenuGame();
    }

    public void Guardar()
    {
        controller = personaje.GetComponent<CharacterController>();

        controller.enabled = false;

        //VALORES VARIOS
        PlayerPrefs.SetInt("monedas", Interfaz.monedas);

        //MISIONES
        PlayerPrefs.SetInt("SizeCurrentListSaved", QuestManager.questManager.SizeCurrentList);

        for (int i = 0; i < QuestManager.questManager.questList.Count; i++)
        {
            if (QuestManager.questManager.questList[i].progress == Quest.QuestProgress.DONE)
            {
                PlayerPrefs.SetInt("IDQuestListMisiones" + i, 4);
            }
            if (QuestManager.questManager.questList[i].progress == Quest.QuestProgress.AVAILABLE)
            {
                PlayerPrefs.SetInt("IDQuestListMisiones" + i, 1);
            }
            if (QuestManager.questManager.questList[i].progress == Quest.QuestProgress.NOT_AVAILABLE)
            {
                PlayerPrefs.SetInt("IDQuestListMisiones" + i, 0);
            }
            if (QuestManager.questManager.questList[i].progress == Quest.QuestProgress.COMPLETE)
            {
                PlayerPrefs.SetInt("IDQuestListMisiones" + i, 3);
            }
            if (QuestManager.questManager.questList[i].progress == Quest.QuestProgress.ACCEPTED)
            {
                PlayerPrefs.SetInt("IDQuestListMisiones" + i, 2);
            }
        }

        //CAMARA LIV
        Camara = GameObject.Find("MainCamera");
        PlayerPrefs.SetFloat("camaraPosicionX", Camara.transform.position.x);
        PlayerPrefs.SetFloat("camaraPosicionY", Camara.transform.position.y);
        PlayerPrefs.SetFloat("camaraPosicionZ", Camara.transform.position.z);

        //POSICION PERSONAJE
        PlayerPrefs.SetFloat("jugadorposicionX", personaje.transform.position.x);
        PlayerPrefs.SetFloat("jugadorposicionY", personaje.transform.position.y);
        PlayerPrefs.SetFloat("jugadorposicionZ", personaje.transform.position.z);

        //ROTACION PERSONAJE
        PlayerPrefs.SetFloat("jugadorAngulo", meshPlayer.GetComponentInParent<PlayerControls>().angulo);

        controller.enabled = true;

        gameSaved = 1;
        PlayerPrefs.SetInt("Guardado", gameSaved);

        QuestUIManager.uiManager.VolverMenuGame();

        //PONGO EN TRUE EL BOTON DE CARGAR
        LoadButton.interactable = true;
        
    }
}
