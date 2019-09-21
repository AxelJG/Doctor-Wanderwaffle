using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ActuatorObject : MonoBehaviour
{
    Vector3 _initalPos;
    Quaternion _initalRotation;
    Rigidbody _rigidbody;

    [HideInInspector]
    public Material material;

    public Actuator actuatorData;

    void Awake()
    {
        _initalPos = this.transform.position;
        _initalRotation = this.transform.rotation;
        material = GetComponent<Renderer>().material;
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        GameManager.Instance.medicalObjs.Add(this.gameObject);
    }

    public void Drop()
    {
        transform.parent = null;
        _rigidbody.isKinematic = false;
        StartCoroutine(ResetActuator(.5f));
    }

    public void Grab(Transform t)
    {
        _rigidbody.isKinematic = true;

        transform.parent = t;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    IEnumerator ResetActuator(float time)
    {
        yield return new WaitForSeconds(time);

        ForceReset();

        yield break;
    }

    public void ForceReset()
    {
        this.transform.parent = null;
        this.transform.position = _initalPos;
        this.transform.rotation = _initalRotation;
    }
}
