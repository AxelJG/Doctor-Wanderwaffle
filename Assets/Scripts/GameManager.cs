using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    #region VARIABLES
    public Material focusedMaterial;

    [Header("UI Level Menus")]
    public GameObject menuWinLevel;
    public GameObject menuLose;
    public GameObject menuWinGame;
    public GameObject menuNotes;

    [Header("Timer")]
    public List<Text> timerLevelTexts;
    public float timeLevel = 60f;
    private bool countTimer = true;

    [HideInInspector]
    public List<GameObject> medicalObjs;
    [HideInInspector]
    public bool playerActionInprogress = false;  

    //WIN/LOSE & LEVELS CONTROL
    [HideInInspector]
    public int pathologiesToCure = 1;
    private const int MAXIMUM_LEVELS = 5;
    private int levelsCompleted = 0;
    #endregion

    #region BASE
    private void Update() {
        CountDown();

        if (Input.GetButtonDown("Notes")) {
            menuNotes.SetActive(!menuNotes.activeSelf);
        }
    }
    #endregion

    #region Level CountDown
    private void CountDown() {
        if(timeLevel > 0f) {
            timeLevel -= Time.deltaTime;
            timerLevelTexts[0].text = Mathf.Floor((timeLevel * 100) / 100.0f).ToString("00"); ;
            timerLevelTexts[1].text = Mathf.Round((float)((timeLevel - Math.Truncate(timeLevel)) * 100)).ToString("00");
        } else {
            if (countTimer) {
                countTimer = false;
                timerLevelTexts[0].text = "00";
                timerLevelTexts[1].text = "00";
                Lose();
            }
        }

    }
    #endregion

    #region Focus in Actuators
    //Resaltar objeto interactuable cuando estamos delante
    public void FocusObject(GameObject obj) {
        obj.GetComponent<Renderer>().material = focusedMaterial;
    }

    //Quitamos efecto resaltado cuando nos alejamos
    public void UnfocusObjects() {
        foreach (GameObject mObj in medicalObjs) {
            Material m = mObj.GetComponent<ActuatorObject>().material;
            mObj.GetComponent<Renderer>().material = m;
        }
    }
    #endregion

    #region Patholigies in Level
    //Enfermedades a curar (Va restando)
    public void PathologiesDiscount() {
        pathologiesToCure = pathologiesToCure - 1;

        if (pathologiesToCure <= 0) {
            WinLevel();
        }
    }
    #endregion

    #region Game State
    //Nivel superado
    private void WinLevel() {
        levelsCompleted = levelsCompleted + 1;
     
        if(levelsCompleted < MAXIMUM_LEVELS) {
            print("Level Complete!");
            MenuVisibility(menuWinLevel, true);
            menuNotes.SetActive(false);
            Pause();
        } else {
            WinGame();
        }
    }

    //Has perdido
    private void Lose() {
        print("You Lose!");
        menuNotes.SetActive(false);
        MenuVisibility(menuLose, true);
        Pause();
    }

    //Te has pasado el juego
    private void WinGame() {
        print("YOU WIN THE GAME! Thanks for playing");
        MenuVisibility(menuWinGame, true);
        menuNotes.SetActive(false);
        Pause();
    }
    #endregion

    #region Menus
    //Visibilidad de los menus del nivel
    private void MenuVisibility(GameObject menu, bool v) {
        menu.SetActive(v);
    }
    #endregion

    #region Resume/Pause
    //Pausa
    private void Pause() {
        Time.timeScale = 0f;
    }

    //Reanudar
    private void Resume() {
        Time.timeScale = 1f;
    }
    #endregion
}
