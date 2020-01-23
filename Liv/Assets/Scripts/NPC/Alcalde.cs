using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alcalde : MonoBehaviour
{
    public GameObject target;

    public GameObject olla;

    bool ollaEntregada = false;

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
            */
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
