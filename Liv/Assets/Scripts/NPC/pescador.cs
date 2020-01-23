using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pescador : MonoBehaviour
{
    bool inTrigger = false;
    public GameObject anguilas;

    void aPescar()
    {
        /*if (QuestManager.questManager.Objective == "anguilas")
        {
            Vector3 pos = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z - 1);
            GameObject clone = Instantiate(disparo3, pos, Quaternion.identity) as GameObject;
            clone.SetActive(true);
        }*/
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Interfaz.monedas >= 10)
            {
                //SceneManager.LoadScene("OtherScene");
                for (int i = 0; i < QuestManager.questManager.questList.Count; i++)
                {
                    if (QuestManager.questManager.questList[i].id == 12 && QuestManager.questManager.questList[i].progress == Quest.QuestProgress.DONE)
                    {
                        //Interfaz.monedas -= 10;
                        QuestManager.questManager.talking = false;
                        SceneManager.LoadScene("OtherScene");
                    }
                    else
                    {
                        print("La barca está rota");
                    }

                }

                //print("Te cambio de escena para pescar");
                //anguilas.SetActive(true);
            }
            else
            {
                //print("No tienes ni un duro");
            }
        }

    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            aPescar();
            print("puedes pescar");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

        }
    }
}
