using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : MonoBehaviour
{
    public Pathology pathology;
    int _pathologyStep = 0;
    float lifetime = 0;
    [HideInInspector]
    public bool isCured;

    public AudioSource fartAudioSource, OKAudioSource, OhNoAudioSource, WowAudioSource;
    public SceneDisplay display;
    public ParticleSystem goodJobPS, badJobPS;
    public GameManager gameManager;


    void Awake()
    {
        gameManager = Camera.main.GetComponent<GameManager>();
        lifetime = pathology.timeOfLife;

        display.sprite = pathology.sprite;
    }

    void Start()
    {
        gameManager.RegisterPatient();
    }

    public void ReceiveActuator(Actuator actuator)
    {
        if (actuator.id == pathology.actuators[_pathologyStep].id)
        {
            _pathologyStep++;

            if (_pathologyStep >= pathology.actuators.Count)
            {
                isCured = true;
                Debug.LogFormat(this, "Patient {0} is cured! Good job!", name);
                _pathologyStep = 0;
                
                goodJobPS.Play();
                WowAudioSource.Play();
                gameManager.PatientCured();
                Leave();
                
                return;
            }

            goodJobPS.Play();
            OKAudioSource.Play();
            Debug.Log("Seems that will do...");
        }
        else
        {
            Debug.Log("Dude, how are you even a doctor?");
            badJobPS.Play();
            fartAudioSource.Play();
            gameManager.WrongAction();
        }
    }

    void Leave()
    {

    }

    void CheckIfDead()
    {
        if (!isCured)
        {
            Debug.LogFormat("Patient {0} died you fool!", name);
            OhNoAudioSource.Play();
        }
    }
}
