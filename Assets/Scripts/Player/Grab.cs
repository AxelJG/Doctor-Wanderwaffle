using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public Transform grabPoint;
    private GameObject objInHands;
    
    // Update is called once per frame
    void Update()
    {
        ObjDetection();

        if (Input.GetButtonDown("Jump")) { //Boton espacio
            if (ObjDetection() != null && objInHands == null) {
                ObjGrabbed(); //Coger objeto
            }else if (objInHands != null) {
                ObjThrown(); //Lanzar objeto
            }
        }
    }


    private GameObject ObjDetection() {
        RaycastHit hit;

        if(Physics.Raycast(grabPoint.position, grabPoint.TransformDirection(Vector3.forward), out hit, 1f)) {
            if (hit.transform.tag.Equals("Grabbable")) {
                GameManager.Instance.FocusObject(hit.transform.gameObject);
                return hit.transform.gameObject;
            }
        }
        GameManager.Instance.DefocusObjects();
        return null;
    }

    private void ObjGrabbed() {
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
}
