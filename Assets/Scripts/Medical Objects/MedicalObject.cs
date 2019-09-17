using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalObject : MonoBehaviour
{
    private Vector3 initalPos;
    private Quaternion initalRotation;
    [HideInInspector]
    public Material material;

    private void Start() {
        initalPos = this.transform.position;
        initalRotation = this.transform.rotation;
        material = GetComponent<Renderer>().material;

        GameManager.Instance.medicalObjs.Add(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag.Equals("Floor")) {
            this.transform.position = initalPos;
            this.transform.rotation = initalRotation;
        }
    }
}
