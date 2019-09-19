using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grab : MonoBehaviour
{
    public Transform grabPoint;
    public GameObject progressBar;

    private GameObject objInHands;
    private ProgressBarAction progressBarAction;

    private void Start() {
        progressBarAction = progressBar.GetComponent<ProgressBarAction>();
    }

    // Update is called once per frame
    void Update()
    {
        ObjDetection();

        if (Input.GetButtonDown("Jump")) { //Boton espacio
            if (ObjDetection() != null && objInHands == null) {
                ActivateProgressBar(); //Activamos barra de progreso para acción
            } else if (objInHands != null) {
                ObjThrown(); //Lanzar objeto
            }
        }
    }

    private GameObject ObjDetection() {
        RaycastHit hit;

        if(Physics.Raycast(grabPoint.position, grabPoint.TransformDirection(Vector3.forward), out hit, 1f) && !objInHands) {
            if (hit.transform.tag.Equals("Grabbable")) {
                GameManager.Instance.FocusObject(hit.transform.gameObject);
                return hit.transform.gameObject;
            }
        }
        GameManager.Instance.DefocusObjects();
        return null;
    }

    public void ObjGrabbed() {
        objInHands = ObjDetection();
        objInHands.GetComponent<Rigidbody>().isKinematic = true;
        objInHands.transform.parent = grabPoint;
        objInHands.transform.localPosition = Vector3.zero;
        objInHands.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private void ObjThrown() {
        objInHands.GetComponent<Rigidbody>().isKinematic = false;
        objInHands.transform.parent = null;
        objInHands = null;
    }

    public void ActivateProgressBar() {
        GameManager.Instance.playerActionInprogress = true;

        progressBarAction.timeMax = 0.5f;
        progressBar.SetActive(true);
        progressBarAction.loaded.RemoveAllListeners();
        progressBarAction.loaded.AddListener(ObjGrabbed);
    }
}
