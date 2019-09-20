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
            Debug.Log("Nice job! That was the correct actuator");
        }
        else
        {
            Debug.Log("Dude, how are you even a doctor?");
        }
    }
}
