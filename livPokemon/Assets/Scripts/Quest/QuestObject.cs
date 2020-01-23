using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestObject : MonoBehaviour
{
    private bool inTrigger = false;

    public List<int> availableQuestIDs = new List<int>();
    public List<int> receivableQuestIDs = new List<int>();

    public GameObject questMarker;
    public Image theImage;

    public Sprite questAvailableSprite;
    public Sprite questReceivableSprite;

    public Transform target;
    //public float smooth;

    //public GameObject anguila;
    //public GameObject olla;

    void Start()
    {
        SetQuestMaker();
    }

    public void SetQuestMaker()
    {
        if (QuestManager.questManager.CheckCompletedQuests(this))
        {
            //Debug.Log("Completada");

            questMarker.SetActive(true);
            theImage.sprite = questReceivableSprite;
            theImage.color = Color.yellow;
        }
        else if (QuestManager.questManager.CheckAvailableQuests(this))
        {
            //Debug.Log("disponible");

            questMarker.SetActive(true);
            theImage.sprite = questAvailableSprite;
            theImage.color = Color.yellow;
        }
        else if (QuestManager.questManager.CheckAcceptedQuests(this))
        {
            //Debug.Log("aceptada");

            questMarker.SetActive(true);
            theImage.sprite = questReceivableSprite;
            theImage.color = Color.gray;
        }
        else
        {
            questMarker.SetActive(false);
            //Debug.Log("Done, debes desaparecer");

        }
    }

    void Update()
    {
        //print("Objetivo: " + QuestManager.questManager.Objective);
        //print("Monedas: " + Interfaz.monedas);


       /* if (inTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            if (!QuestUIManager.uiManager.questPanelActive)
            {
                //quest ui manager
                QuestUIManager.uiManager.CheckQuests(this);
                QuestManager.questManager.QuestRequest(this);

                for (int i = 0; i < QuestManager.questManager.questList.Count; i++)
                {
                    //prueba para spawnear la olla
                    if (QuestManager.questManager.questList[i].id == 0 && QuestManager.questManager.questList[i].progress == Quest.QuestProgress.COMPLETE)
                    {
                        Vector3 pos = new Vector3(transform.position.x, 2, transform.position.z);
                        GameObject clone = Instantiate(olla, pos, Quaternion.identity) as GameObject;
                        clone.SetActive(true);
                    }

                    if (QuestManager.questManager.questList[i].id == 4 && QuestManager.questManager.questList[i].progress == Quest.QuestProgress.ACCEPTED)
                    {
                        Vector3 pos = new Vector3(transform.position.x, 2, transform.position.z);
                        GameObject clone = Instantiate(olla, pos, Quaternion.identity) as GameObject;
                        clone.SetActive(true);
                    }
                    //SE AUTOENTREGAN, LO USO PARA LAS MISIONES DE CHARLAR CON OTROS NPCS
                    if (QuestManager.questManager.currentQuestList[i].id == 0 && QuestManager.questManager.currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
                    {
                        //QuestManager.questManager.Objective = "Hablar con la cocinera";
                        QuestManager.questManager.AddQuestItem("Hablar con la cocinera", 1);
                    }
                    if (QuestManager.questManager.currentQuestList[i].id == 1 && QuestManager.questManager.currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
                    {
                        QuestManager.questManager.AddQuestItem("pescador", 1);
                    }
                    if ((QuestManager.questManager.currentQuestList[i].id == 2 || QuestManager.questManager.currentQuestList[i].id == 4) && QuestManager.questManager.currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
                    {
                        QuestManager.questManager.AddQuestItem("herrero", 1);
                    }
                    if (QuestManager.questManager.currentQuestList[i].id == 3 && QuestManager.questManager.currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED && Interfaz.monedas >= 10)
                    {
                        QuestManager.questManager.AddQuestItem("none", 1);
                    }
                    if (QuestManager.questManager.currentQuestList[i].id == 6 && QuestManager.questManager.currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
                    {
                        QuestManager.questManager.AddQuestItem("habla con el alcalde", 1);
                    }
                    if (QuestManager.questManager.currentQuestList[i].id == 7 && QuestManager.questManager.currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
                    {
                        QuestManager.questManager.AddQuestItem("Habla con el boticario", 1);
                    }
                    if (QuestManager.questManager.currentQuestList[i].id == 15 && QuestManager.questManager.currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
                    {
                        QuestManager.questManager.AddQuestItem("devuelve la olla", 1);
                    }
                    if (QuestManager.questManager.currentQuestList[i].id == 16 && QuestManager.questManager.currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
                    {
                        QuestManager.questManager.AddQuestItem("medicina", 1);
                    }
                }
            }

            //LOOKING PLAYER
            if (QuestManager.questManager.talking)
            {
                Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
                transform.LookAt(targetPosition);
            }

        }*/



        /*if (!QuestUIManager.uiManager.questPanelActive && inTriggerItem)
        {
            //QuestManager.questManager.AddQuestItem(Objective, 1);
            //animacion de subir objeto a la cabeza
            // bool de tengo en la cabeza algo

            Debug.Log("tengo hacha?: " + Interfaz.withAxe);
            Debug.Log("estoy en el arbol? " + gameObject.tag == "arbol");


            for (int i = 0; i < QuestManager.questManager.currentQuestList.Count; i++)
            {
                if (QuestManager.questManager.currentQuestList[i].id == 3 && QuestManager.questManager.currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED && gameObject.tag == "herrero" && Interfaz.monedas >= 10)
                {
                    //Si tengo 10 monedas el herrero me arreglará la olla
                    Interfaz.monedas -= 10;
                    QuestManager.questManager.AddQuestItem("none", 1);

                }
                if (QuestManager.questManager.currentQuestList[i].id == 5 && QuestManager.questManager.currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED && gameObject.tag == "herrero" && Interfaz.monedas >= 10)
                {
                    //Si tengo 10 monedas el herrero me arreglará la olla
                    Interfaz.monedas -= 10;
                    QuestManager.questManager.AddQuestItem("consigue oro", 1);

                }
                if (Interfaz.withAxe && gameObject.tag == "arbol")
                {
                    Debug.Log("estoy talando");
                    //La madera se puede coger
                    QuestManager.questManager.AddQuestItem("madera", 1);
                }

            }


        }*/

        if (Input.GetKeyDown(KeyCode.M))
        {
            //UPDATE ALL NPC
            QuestObject[] currentQuestGuys = FindObjectsOfType(typeof(QuestObject)) as QuestObject[];

            foreach (QuestObject obj in currentQuestGuys)
            {
                obj.SetQuestMaker();
            }
        }
    }

    void Interactable()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            //LOOKING PLAYER
            if (QuestManager.questManager.talking)
            {
                Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
                transform.LookAt(targetPosition);
            }

            if (!QuestUIManager.uiManager.questPanelActive)
            {
                //quest ui manager
                QuestUIManager.uiManager.CheckQuests(this);
                QuestManager.questManager.QuestRequest(this);
            }

            //DIALOGO
            if (!QuestUIManager.uiManager.startedConvers)
            {
                QuestUIManager.uiManager.StartDialogue(this);

                QuestUIManager.uiManager.startedConvers = true;
            }
            else if (QuestUIManager.uiManager.startedConvers)
            {
                QuestUIManager.uiManager.DisplayNextSentence(this);
            }

        }

    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            inTrigger = true;
            //QuestManager.questManager.talking = true;


            Interactable();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "item" || other.tag == "arbol")
        {
            //inTriggerItem = true;
            //onHead = GetComponent<ItemObject>();
            //Objective = onHead.objective;

            /*for (int i = 0; i < QuestManager.questManager.currentQuestList.Count; i++)
            {
                if (QuestManager.questManager.Objective == QuestManager.questManager.currentQuestList[i].questObjective)
                {
                    print("esto es justo lo que busco");

                }
                else
                {
                    print("busco otro objeto crack");

                }
            }*/
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inTrigger = false;
            QuestUIManager.uiManager.HideQuestPanel();
            //Objective = "none";
            //QuestManager.questManager.talking = false;

            QuestUIManager.uiManager.startedConvers = false;
            QuestUIManager.uiManager.StopAllCoroutines();
        }

    }
}
