using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interfaz : MonoBehaviour
{
    public static bool withAxe = false;
    public static int monedas;

    //PERSONAJE LIV
    public GameObject personaje;
    public GameObject meshPlayer;

    float jugadorposicionX;
    float jugadorposicionY;
    float jugadorposicionZ;

    float jugadorrotacionX;
    float jugadorrotacionY;
    float jugadorrotacionZ;

    CharacterController controller;
    
    //CAMARA LIV
    float camaraPosicionX;
    float camaraPosicionY;
    float camaraPosicionZ;

    public GameObject Camara;

    //CONTROL
    public int gameSaved = 0;
    int gameStarted = 0;
    int gameLoaded = 0;
    int SizeCurrentListSaved;

    //VALORES VARIOS
    int monedasSave;

    public int[] IDListMissions;


    public void Start()
    {
        QuestUIManager.uiManager.sceneNumber = 1;

        //START  SACO EL VALOR DE GAMESTARTED
        gameStarted = PlayerPrefs.GetInt("Started");

        if (gameStarted == 1)
        {
            PlayerPrefs.DeleteAll();
            QuestUIManager.uiManager.startGame = true;

            controller = personaje.GetComponent<CharacterController>();

            controller.enabled = false;

            //VALORES VARIOS
            monedas = 0;
            monedas = monedasSave;
            monedasSave = PlayerPrefs.GetInt("monedas");

            //CAMARA LIV
            camaraPosicionX = 0;
            camaraPosicionY = 9.0f;
            camaraPosicionZ = -9.5f;

            Vector3 posicionCamara = new Vector3(camaraPosicionX, camaraPosicionY, camaraPosicionZ);
            Camara.transform.position = posicionCamara;

            //POSICION PERSONAJE
            jugadorposicionX = 0;
            jugadorposicionY = 0;
            jugadorposicionZ = 0;
            Vector3 posicionPersonaje = new Vector3(jugadorposicionX, jugadorposicionY, jugadorposicionZ);
            personaje.transform.position = posicionPersonaje;

            //ROTACION PERSONAJE
            meshPlayer.GetComponentInParent<PlayerControls>().angulo = 180;

            controller.enabled = true;

            //HEMOS EMPEZADO PARTIDA, BORRO LA PARTIDA GUARDADA
            gameSaved = 0;
            PlayerPrefs.SetInt("Guardado", gameSaved);

            //GUARDO EL VALOR A 0 FALSO, YA HE CARGADO EL START
            gameStarted = 0;
            PlayerPrefs.SetInt("Started", gameStarted);
        }

        //LOAD
        gameSaved = PlayerPrefs.GetInt("Guardado");
        //print("gameSaved: " + gameSaved);

        if (gameSaved == 1)
        {
            QuestManager.questManager.currentQuestList.Clear();


            IDListMissions = new int[QuestManager.questManager.questList.Count];

            //MISIONES
            for (int i = 0; i < QuestManager.questManager.questList.Count; i++)
            {
                IDListMissions[i] = PlayerPrefs.GetInt("IDQuestListMisiones" + i);
                print("Hueco: " + IDListMissions[i] + ". ID de la mision: " + PlayerPrefs.GetInt("IDQuestListMisiones" + i));

                if (IDListMissions[i] == 0)      //NOT_AVAILABLE
                {
                    QuestManager.questManager.questList[i].progress = Quest.QuestProgress.NOT_AVAILABLE;
                }
                if (IDListMissions[i] == 1)      //AVAILABLE
                {
                    QuestManager.questManager.questList[i].progress = Quest.QuestProgress.AVAILABLE;
                }
                if (IDListMissions[i] == 2)      //ACCEPTED
                {
                    QuestManager.questManager.questList[i].progress = Quest.QuestProgress.ACCEPTED;

                    if (QuestManager.questManager.questList[i].progress == Quest.QuestProgress.ACCEPTED)
                    {
                        print("Mision " + i + ": " + 2 + " acceptada");
                        QuestManager.questManager.currentQuestList.Add(QuestManager.questManager.questList[i]);
                    }
                }
                if (IDListMissions[i] == 3)      //COMPLETE
                {
                    QuestManager.questManager.questList[i].progress = Quest.QuestProgress.COMPLETE;

                    if (QuestManager.questManager.questList[i].progress == Quest.QuestProgress.COMPLETE)
                    {
                        print("Mision " + i + ": " + 3 + " Completada");
                        QuestManager.questManager.currentQuestList.Add(QuestManager.questManager.questList[i]);
                    }
                }
                if (IDListMissions[i] == 4)      //DONE
                {
                    QuestManager.questManager.questList[i].progress = Quest.QuestProgress.DONE;
                }
            }

            controller = personaje.GetComponent<CharacterController>();

            controller.enabled = false;

            //MONEDAS
            monedasSave = PlayerPrefs.GetInt("monedas");
            Interfaz.monedas = monedasSave;

            //CAMARA LIV
            camaraPosicionX = PlayerPrefs.GetFloat("camaraPosicionX");
            camaraPosicionY = PlayerPrefs.GetFloat("camaraPosicionY");
            camaraPosicionZ = PlayerPrefs.GetFloat("camaraPosicionZ");

            Vector3 posicionCamara = new Vector3(camaraPosicionX, camaraPosicionY, camaraPosicionZ);
            Camara.transform.position = posicionCamara;

            //POSICION PERSONAJE
            jugadorposicionX = PlayerPrefs.GetFloat("jugadorposicionX");
            jugadorposicionY = PlayerPrefs.GetFloat("jugadorposicionY");
            jugadorposicionZ = PlayerPrefs.GetFloat("jugadorposicionZ");
            Vector3 posicionPersonaje = new Vector3(jugadorposicionX, jugadorposicionY, jugadorposicionZ);
            personaje.transform.position = posicionPersonaje;

            //ROTACION PERSONAJE
            meshPlayer.GetComponentInParent<PlayerControls>().angulo = PlayerPrefs.GetFloat("jugadorAngulo");

            controller.enabled = true;
        }

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Interfaz.monedas++;
        }

    }


}
