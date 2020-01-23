using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class granjeroController : MonoBehaviour
{
    public int gallinasCount = 6;
    bool StartMision;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "gallina")
        {
            gallinasCount++;

            QuestManager.questManager.questList[19].questObjectiveCount = gallinasCount;

            if (QuestManager.questManager.questList[19].questObjectiveCount >= QuestManager.questManager.questList[19].questObjectiveRequirement && QuestManager.questManager.questList[19].progress == Quest.QuestProgress.ACCEPTED)
            {
                print("Mision de gallinas a done");
                QuestManager.questManager.questList[19].progress = Quest.QuestProgress.COMPLETE;

            }

            if (QuestManager.questManager.questList[19].progress == Quest.QuestProgress.AVAILABLE && gallinasCount>=6)
            {
                print("Mision de gallinas a not_available");

                QuestManager.questManager.questList[19].progress = Quest.QuestProgress.NOT_AVAILABLE;
            }

            //UPDATE ALL NPC
            QuestObject[] currentQuestGuys = FindObjectsOfType(typeof(QuestObject)) as QuestObject[];

            foreach (QuestObject obj in currentQuestGuys)
            {
                obj.SetQuestMaker();
            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "gallina")
        {
            gallinasCount--;

            QuestManager.questManager.questList[19].questObjectiveCount = gallinasCount;

            //LA MISION ESTARA DISPONIBLE AHORA
            if (QuestManager.questManager.questList[19].progress == Quest.QuestProgress.NOT_AVAILABLE || QuestManager.questManager.questList[19].progress == Quest.QuestProgress.DONE)
            {
                QuestManager.questManager.questList[19].progress = Quest.QuestProgress.AVAILABLE;
            }

            //SI LA MISION ESTA COMPLETA PERO SALE UNA GALLINA VUELVE A ACEPTADA
            if (QuestManager.questManager.questList[19].progress == Quest.QuestProgress.COMPLETE)
            {
                QuestManager.questManager.questList[19].progress = Quest.QuestProgress.ACCEPTED;

            }

            if (QuestManager.questManager.questList[19].progress == Quest.QuestProgress.AVAILABLE)
            {
                StartMision = false;
            }

            //UPDATE ALL NPC
            QuestObject[] currentQuestGuys = FindObjectsOfType(typeof(QuestObject)) as QuestObject[];

            foreach (QuestObject obj in currentQuestGuys)
            {
                obj.SetQuestMaker();
            }

        }
    }

}
