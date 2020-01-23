using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager questManager;

    public List<Quest> questList = new List<Quest>();                               //Lista maestra de misiones
    public List<Quest> currentQuestList = new List<Quest>();                        //lista de misiones actual

    public string Objective;
    public bool itemCorrecto;                                                       //cuando el item cogido coincide con el de la currentList
    public bool talking = false;                                                    //decir cuando el player esta frente al npc para no lanzar el objeto

    public bool DestroyObHead = false;

    void Awake()
    {
        if (questManager == null)
        {
            questManager = this;
        }
        else if (questManager !=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void QuestRequest(QuestObject NPCQuestObject)
    {
        //MISION DISPONIBLE
        if (NPCQuestObject.availableQuestIDs.Count > 0)
        {
            for (int i = 0; i < questList.Count; i++)
            {
                for (int j = 0; j< NPCQuestObject.availableQuestIDs.Count; j++)
                {
                    if (questList[i].id == NPCQuestObject.availableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.AVAILABLE)
                    {
                        QuestUIManager.uiManager.questAvailable = true;
                        QuestUIManager.uiManager.availableQuests.Add(questList[i]);
                    }
                }
            }
        }

        //ACTIVAR MISION
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            for (int j = 0; j < NPCQuestObject.receivableQuestIDs.Count; j++)
            {
                if ((currentQuestList[i].id == NPCQuestObject.receivableQuestIDs[j]) && (currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED || currentQuestList[i].progress == Quest.QuestProgress.COMPLETE))
                {
                    QuestUIManager.uiManager.questRunning = true;
                    QuestUIManager.uiManager.activeQuests.Add(currentQuestList[i]);
                }
            }
        }
    }


    //ACEPTAR MISION
    public void AcceptQuest(int questID)
    {
        for (int i =0; i < questList.Count; i++)
        {
            if(questList[i].id == questID && (questList[i].progress == Quest.QuestProgress.AVAILABLE || questList[i].progress == Quest.QuestProgress.ACCEPTED))
            {
                currentQuestList.Add(questList[i]);
                questList[i].progress = Quest.QuestProgress.ACCEPTED;

                for (int j = 0; j < currentQuestList.Count; j++)
                {
                    if (currentQuestList[j].id == 0 && currentQuestList[j].progress == Quest.QuestProgress.ACCEPTED)
                    {
                        Interfaz.monedas = 10;
                       AddQuestItem("Hablar con la cocinera", 1);
                    }
                    if (currentQuestList[j].id == 1 && currentQuestList[j].progress == Quest.QuestProgress.ACCEPTED)
                    {
                        currentQuestList.Add(questList[12]);
                        questList[12].progress = Quest.QuestProgress.ACCEPTED;
                        AddQuestItem("pescador", 1);
                    }
                    if (currentQuestList[j].id == 2 && currentQuestList[j].progress == Quest.QuestProgress.ACCEPTED)
                    {
                        currentQuestList.Add(questList[11]);
                        questList[11].progress = Quest.QuestProgress.ACCEPTED;

                        AddQuestItem("herrero", 1);
                    }
                    if (currentQuestList[j].id == 3 && currentQuestList[j].progress == Quest.QuestProgress.ACCEPTED && Interfaz.monedas >= 10)
                    {
                        AddQuestItem("none", 1); 
                    }
                    if (currentQuestList[j].id == 4 && currentQuestList[j].progress == Quest.QuestProgress.ACCEPTED)
                    {
                        AddQuestItem("herrero", 1);
                    }
                    if (currentQuestList[j].id == 5 && currentQuestList[j].progress == Quest.QuestProgress.ACCEPTED && Interfaz.monedas >= 10)
                    {
                        AddQuestItem("consigue oro", 1);
                    }
                    if (currentQuestList[j].id == 6 && currentQuestList[j].progress == Quest.QuestProgress.ACCEPTED)
                    {
                        AddQuestItem("habla con el alcalde", 1);
                    }
                    if (currentQuestList[j].id == 7 && currentQuestList[j].progress == Quest.QuestProgress.ACCEPTED)
                    {
                        AddQuestItem("Habla con el boticario", 1);
                    }
                    if (currentQuestList[j].id == 15 && currentQuestList[j].progress == Quest.QuestProgress.ACCEPTED)
                    {
                        AddQuestItem("devuelve la olla", 1);
                    }
                    if (currentQuestList[j].id == 16 && currentQuestList[j].progress == Quest.QuestProgress.ACCEPTED)
                    {
                        AddQuestItem("medicina", 1);
                    }
                }
            }
        }
    }

    //ENTREGAR EL OBJETO DE LA MISION
    public void deliverItemQuest (int questID)
    {
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            if (currentQuestList[i].id == 3 && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED && Interfaz.monedas >= 10)
            {
                //Si tengo 10 monedas el herrero me vende el hacha
                itemCorrecto = true;
                Objective = "none";
                Interfaz.withAxe = true;
            }
            if (currentQuestList[i].id == 5 && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED && Interfaz.monedas >= 10)
            {
                //Si tengo 10 monedas el herrero me arregla la olla
                itemCorrecto = true;
                Objective = "consigue oro";
                Interfaz.monedas -= 10;
            }

            //ENTREGO EL OBJETO
            if (currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED && itemCorrecto)
            {
                itemCorrecto = false;               
                AddQuestItem(Objective, 1);

                //DESTRUIR OBJETO ENCIMA DE LA CABEZA
                DestroyObHead = true;
            }

        }
    }

    //COMPLETAR LA MISION
    public void CompleteQuest(int questID)
    {
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            if (currentQuestList[i].id == questID && currentQuestList[i].progress == Quest.QuestProgress.COMPLETE)
            {
                currentQuestList[i].progress = Quest.QuestProgress.DONE;
                    

                if (currentQuestList[i].id == 3 && currentQuestList[i].progress == Quest.QuestProgress.DONE)  //PAGO AL HERRERO 10 MONEDAS POR SU HACHA
                {
                    Interfaz.monedas -= 10;
                    Interfaz.withAxe = true;
                }

                if (currentQuestList[i].id == 20 && currentQuestList[i].progress == Quest.QuestProgress.DONE)
                {
                    Interfaz.monedas = Interfaz.monedas + 10;
                }

                currentQuestList.Remove(currentQuestList[i]);
            }
        }

        //check for chain quests
        CheckChainQuest(questID);
    }


    //COMPROBAR MISION ENCADENADA  si has completado la misión, la siguiente la pasa a disponible
    void CheckChainQuest (int questID)
    {
        int tempID = 0;
        for(int i = 0; i < questList.Count; i++)
        {
            if(questList[i].id ==questID && questList[i].nextQuest > 0)
            {
                tempID = questList[i].nextQuest;
            }
        }

        if (tempID > 0)                                                             
        {
            for(int i = 0; i< questList.Count; i++)
            {
                if ( questList[i].id == tempID && questList[i].progress == Quest.QuestProgress.NOT_AVAILABLE)
                {
                    questList[i].progress = Quest.QuestProgress.AVAILABLE;
                }
            }
        }
    }

    //Add Items
    public void AddQuestItem(string questObjective, int itemAmount)
    {
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            if (currentQuestList[i].questObjective == questObjective && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
            {
                currentQuestList[i].questObjectiveCount += itemAmount;
            }

            if (currentQuestList[i].questObjectiveCount >= currentQuestList[i].questObjectiveRequirement && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
            {
               currentQuestList[i].progress = Quest.QuestProgress.COMPLETE;
            }
        }
    }

    //Bools
    public bool RequestAvailableQuest(int questID)
    {
        for (int i =0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.AVAILABLE)
            {
                return true;
            }
        }
        return false;
    }

    public bool RequestAcceptedQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.ACCEPTED)
            {
                return true;
            }
        }
        return false;
    }

    public bool RequestCompleteQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.COMPLETE)
            {
                return true;
            }
        }
        return false;
    }

    //Bools 2
    public bool CheckAvailableQuests(QuestObject NPCQuestObject)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            for  (int j = 0; j< NPCQuestObject.availableQuestIDs.Count; j++)
            {
                if(questList[i].id == NPCQuestObject.availableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.AVAILABLE)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckAcceptedQuests(QuestObject NPCQuestObject)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < NPCQuestObject.receivableQuestIDs.Count; j++)
            {
                if (questList[i].id == NPCQuestObject.receivableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.ACCEPTED)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckCompletedQuests(QuestObject NPCQuestObject)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < NPCQuestObject.receivableQuestIDs.Count; j++)
            {
                if (questList[i].id == NPCQuestObject.receivableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.COMPLETE)
                {
                    return true;
                }
            }
        }
        return false;
    }

    //SHOW QUEST LOG
    public void ShowQuestLog(int questID)
    {
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            if (currentQuestList[i].id == questID)
            {
                QuestUIManager.uiManager.ShowQuestLog(currentQuestList[i]);
            }
        }
    }

}
