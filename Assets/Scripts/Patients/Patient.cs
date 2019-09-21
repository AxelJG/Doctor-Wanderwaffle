using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : MonoBehaviour
{
    public Pathology pathology;
    int _pathologyStep = 0;

    public void ReceiveActuator(Actuator actuator)
    {
        if (actuator.id == pathology.actuators[_pathologyStep].id)
        {
            _pathologyStep++;

            if (_pathologyStep >= pathology.actuators.Count)
            {
                Debug.LogFormat(this, "Patient {0} is cured! Good job!", name);
                _pathologyStep = 0;
                return;
            }

            Debug.Log("Seems that will do...");
        }
        else
        {
            Debug.Log("Dude, how are you even a doctor?");
        }
    }
}
