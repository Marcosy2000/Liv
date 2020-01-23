using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QButtonScript : MonoBehaviour
{
    public int questID;
    public Text questTitle;
    public int test;

    //BOTONES CONTROL
    public Button LoadButton;

    //SHOW ALL INFOS
    public void ShowAllInfos()
    {
        QuestUIManager.uiManager.ShowSelectedQuest(questID);

        //ACCEPT BUTTON
        if (QuestManager.questManager.RequestAvailableQuest(questID))
        {
            QuestUIManager.uiManager.acceptButton.SetActive(true);
            QuestUIManager.uiManager.acceptButtonScript.questID = questID;
        }
        else
        {
            QuestUIManager.uiManager.acceptButton.SetActive(false);
        }

        //DELIVER BUTTON
        if (QuestManager.questManager.RequestAcceptedQuest(questID))
        {

            QuestUIManager.uiManager.deliverButton.SetActive(true);
            QuestUIManager.uiManager.deliverButtonScript.questID = questID;
        }
        else
        {
            QuestUIManager.uiManager.deliverButton.SetActive(false);
        }

        //COMPLETE BUTTON
        if (QuestManager.questManager.RequestCompleteQuest(questID))
        {

            QuestUIManager.uiManager.completeButton.SetActive(true);
            QuestUIManager.uiManager.completeButtonScript.questID = questID;
        }
        else
        {
            QuestUIManager.uiManager.completeButton.SetActive(false);
        }

    }

    public void AcceptQuest()
    {
        QuestManager.questManager.AcceptQuest(questID);
        QuestUIManager.uiManager.HideQuestPanel();

        //UPDATE ALL NPC
        QuestObject[] currentQuestGuys = FindObjectsOfType(typeof(QuestObject)) as QuestObject[];

        foreach (QuestObject obj in currentQuestGuys)
        {
            obj.SetQuestMaker();
        }
        QuestUIManager.uiManager.acceptButton.SetActive(false);

        QuestManager.questManager.SizeCurrentList++;
    }

     public void deliverItemQuest()
     {
         QuestManager.questManager.deliverItemQuest(questID);
         QuestUIManager.uiManager.HideQuestPanel();

         //UPDATE ALL NPC
         QuestObject[] currentQuestGuys = FindObjectsOfType(typeof(QuestObject)) as QuestObject[];

         foreach (QuestObject obj in currentQuestGuys)
         {
             obj.SetQuestMaker();
         }

         QuestUIManager.uiManager.deliverButton.SetActive(false);
    }

    public void CompleteQuest()
     {
         QuestManager.questManager.CompleteQuest(questID);
         QuestUIManager.uiManager.HideQuestPanel();

         //UPDATE ALL NPC
         QuestObject[] currentQuestGuys = FindObjectsOfType(typeof(QuestObject)) as QuestObject[];

         foreach (QuestObject obj in currentQuestGuys)
         {
             obj.SetQuestMaker();
         }
         QuestUIManager.uiManager.completeButton.SetActive(false);
    }



     public void ClosePanel()
     {
         QuestUIManager.uiManager.acceptButton.SetActive(false);
         QuestUIManager.uiManager.deliverButton.SetActive(false);
         QuestUIManager.uiManager.completeButton.SetActive(false);
         QuestUIManager.uiManager.HideQuestPanel();
     }


 }
