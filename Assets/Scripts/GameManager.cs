using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
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

    [Header("Beds control")]
    public List<Transform> bedsPoints;
    public List<Transform> bedsAttendedPoints;

    [Header("AI points to move")]
    public Transform exitPoint;
    public Transform operatingPoint;
    public Transform treatmentPoint;
    public Transform xRayPoint;

    [HideInInspector]
    public List<bool> freeBeds;

    [HideInInspector]
    public List<GameObject> medicalObjs;
    [HideInInspector]
    public bool playerActionInprogress = false;

    //WIN/LOSE & LEVELS CONTROL
    int _patientsInLevel = 0;
    int prestige = 100;
    private const int MAXIMUM_LEVELS = 5;
    private int levelsCompleted = 0;

    public delegate void HUDToggleDelegate(GameObject o);
    public static event HUDToggleDelegate OnActivateHUD, OnDeactivateHUD;

    #endregion

    #region BASE
    private void Awake()
    {
        freeBeds = new List<bool>() { false, false, false };
    }

    private void Update()
    {
        CountDown();

        if (Input.GetButtonDown("Notes"))
        {
            menuNotes.SetActive(!menuNotes.activeSelf);
        }
    }
    #endregion

    #region Level CountDown
    private void CountDown()
    {
        if (timeLevel > 0f)
        {
            timeLevel -= Time.deltaTime;
            float minutes = (float)Math.Truncate(timeLevel / 60f);
            timerLevelTexts[0].text = minutes.ToString("00");
            timerLevelTexts[1].text = (timeLevel - (minutes * 60f)).ToString("00");
        }
        else
        {
            if (countTimer)
            {
                countTimer = false;
                timerLevelTexts[0].text = "00";
                timerLevelTexts[1].text = "00"; 
                Lose();
            }
        }

    }
    #endregion

    #region Focus in Actuators

    public void ActivateHUD(GameObject o)
    {
        if (OnActivateHUD != null)
        {
            OnActivateHUD.Invoke(o);
        }
    }

    public void DeactivateHUD(GameObject o)
    {
        if (OnDeactivateHUD != null)
        {
            OnDeactivateHUD.Invoke(o);
        }
    }

    #endregion

    #region Patholigies in Level

    public void RegisterPatient()
    {
        _patientsInLevel++;
    }

    //Enfermedades a curar (Va restando)
    public void PatientCured()
    {
        _patientsInLevel = _patientsInLevel - 1;

        if (_patientsInLevel <= 0)
        {
            WinLevel();
        }
    }

    public void PatientDied()
    {
        prestige -= 25;

        if (prestige <= 0)
        {
            Lose();
        }
    }

    public void WrongAction()
    {
        prestige -= 10;

        if (prestige <= 0)
        {
            Lose();
        }
    }
    #endregion

    #region Game State
    //Nivel superado
    private void WinLevel()
    {
        levelsCompleted = levelsCompleted + 1;

        if (levelsCompleted < MAXIMUM_LEVELS)
        {
            print("Level Complete!");
            MenuVisibility(menuWinLevel, true);
            menuNotes.SetActive(false);
            Pause();
        }
        else
        {
            WinGame();
        }
    }

    //Has perdido
    private void Lose()
    {
        print("You Lose!");
        menuNotes.SetActive(false);
        MenuVisibility(menuLose, true);
        Pause();
    }

    //Te has pasado el juego
    private void WinGame()
    {
        print("YOU WIN THE GAME! Thanks for playing");
        MenuVisibility(menuWinGame, true);
        menuNotes.SetActive(false);
        Pause();
    }
    #endregion

    #region Menus
    //Visibilidad de los menus del nivel
    private void MenuVisibility(GameObject menu, bool v)
    {
        menu.SetActive(v);
    }
    #endregion

    #region Resume/Pause
    //Pausa
    private void Pause()
    {
        Time.timeScale = 0f;
    }

    //Reanudar
    private void Resume()
    {
        Time.timeScale = 1f;
    }
    #endregion

    #region Navigation Menu Click

    public void RetryLevel() {
        Resume();
        SceneManager.LoadScene("SampleScene");
    }

    public void ReturnMenu() {
        SceneManager.LoadScene("Menu");
    }

    #endregion
}
