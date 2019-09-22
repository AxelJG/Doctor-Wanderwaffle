using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class navigation : MonoBehaviour
{

    public CanvasGroup menu;
    public CanvasGroup instrucciones;
    public CanvasGroup controles;
    
    public void OnClickStart() {
        SceneManager.LoadScene("SampleScene");
    }
    
    public void OnClickInstructions() {
        menu.alpha = 0f;
        menu.blocksRaycasts = false;

        instrucciones.alpha = 1f;
        instrucciones.blocksRaycasts = true;
    }

    public void OnClickControles() {
        menu.alpha = 0f;
        menu.blocksRaycasts = false;

        controles.alpha = 1f;
        controles.blocksRaycasts = true;
    }

    public void OnClickReturnMenu() {
        controles.alpha = 0f;
        controles.blocksRaycasts = false;

        instrucciones.alpha = 0f;
        instrucciones.blocksRaycasts = false;

        menu.alpha = 1f;
        menu.blocksRaycasts = true;


    }
}
