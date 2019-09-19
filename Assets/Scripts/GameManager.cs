using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Material focusedMaterial;
    [HideInInspector]
    public List<GameObject> medicalObjs;
    [HideInInspector]
    public bool playerActionInprogress = false;

    //Resaltar objeto interactuable cuando estamos delante
    public void FocusObject(GameObject obj) {
        obj.GetComponent<Renderer>().material = focusedMaterial;
    }

    //Quitamos efecto resaltado cuando nos alejamos
    public void DefocusObjects() {
        foreach (GameObject mObj in medicalObjs) {
            Material m = mObj.GetComponent<MedicalObject>().material;
            mObj.GetComponent<Renderer>().material = m;
        }
    }

}
