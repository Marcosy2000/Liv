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

            //QuestManager.questManager.AddQuestItem("meter las gallinas", 1);


            /*if (QuestManager.questManager.questList[19].progress == Quest.QuestProgress.ACCEPTED)
            {
                QuestManager.questManager.questList[19].questObjectiveCount = gallinasCount;
            }*/

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

            //QuestManager.questManager.AddQuestItem("meter las gallinas", -1);

            gallinasCount--;

            QuestManager.questManager.questList[19].questObjectiveCount = gallinasCount;

            /*if (gallinasCount < 6 && !StartMision)
            {
                QuestManager.questManager.questList[19].questObjectiveCount = gallinasCount;
                StartMision = true;
            }
            if (QuestManager.questManager.questList[19].progress == Quest.QuestProgress.DONE)
            {
                StartMision = false;
            }
            */


            /*if (QuestManager.questManager.questList[19].questObjectiveCount == QuestManager.questManager.questList[19].questObjectiveRequirement && QuestManager.questManager.questList[19].progress == Quest.QuestProgress.ACCEPTED)
            {
            QuestManager.questManager.questList[19].progress = Quest.QuestProgress.COMPLETE
            }*/


            //LA MISION ESTARA DISPONIBLE AHORA
            if (QuestManager.questManager.questList[19].progress == Quest.QuestProgress.NOT_AVAILABLE || QuestManager.questManager.questList[19].progress == Quest.QuestProgress.DONE)
            {
                QuestManager.questManager.questList[19].progress = Quest.QuestProgress.AVAILABLE;
                //QuestManager.questManager.questList[i].questObjectiveCount = 5;

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

            //QuestManager.questManager.AddQuestItem("meter las gallinas", -1);


            //UPDATE ALL NPC
            QuestObject[] currentQuestGuys = FindObjectsOfType(typeof(QuestObject)) as QuestObject[];

            foreach (QuestObject obj in currentQuestGuys)
            {
                obj.SetQuestMaker();
            }

        }
    }

}
