using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTutorial : MonoBehaviour
{
    int contadorCanvas = 0;

    public void siguiente()
    {
        contadorCanvas++;

        GameObject mandoControles = QuestUIManager.uiManager.TutorialController.transform.GetChild(0).gameObject;
        GameObject Historia = QuestUIManager.uiManager.TutorialController.transform.GetChild(1).gameObject;
        GameObject Minijuegos = QuestUIManager.uiManager.TutorialController.transform.GetChild(2).gameObject;

        if (contadorCanvas == 1)
        {
            mandoControles.SetActive(false);
            Historia.SetActive(true);
        }
        if (contadorCanvas == 2)
        {
            Historia.SetActive(false);
            Minijuegos.SetActive(true);
        }
        if (contadorCanvas == 3)
        {
            Minijuegos.SetActive(false);
            QuestUIManager.uiManager.startGame = false;
            QuestUIManager.uiManager.TutorialController.SetActive(false);
        }

    }

    public void Abandonar()
    {
        QuestUIManager.uiManager.TutorialController.SetActive(false);
        QuestUIManager.uiManager.startGame = false;

    }



}
