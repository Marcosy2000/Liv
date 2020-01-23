using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alcalde : MonoBehaviour
{
    public GameObject target;

    public GameObject olla;

    bool ollaEntregada = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Interactable()
    {
        for (int i = 0; i < QuestManager.questManager.questList.Count; i++)
        {
            /*//prueba para spawnear la olla
            if (QuestManager.questManager.questList[i].id == 0 && QuestManager.questManager.questList[i].progress == Quest.QuestProgress.COMPLETE && !ollaEntregada)
            {
                Vector3 pos = new Vector3(transform.position.x, 2.5f, transform.position.z);
                GameObject clone = Instantiate(olla, pos, Quaternion.identity) as GameObject;
                clone.SetActive(true);
                ollaEntregada = true;
            }*/

            /*if (QuestManager.questManager.questList[i].id == 4 && QuestManager.questManager.questList[i].progress == Quest.QuestProgress.ACCEPTED)
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
            }*/
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Interactable();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

        }
    }

}
