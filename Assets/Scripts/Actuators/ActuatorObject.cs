using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActuatorObject : MonoBehaviour
{
    Vector3 _initalPos;
    Quaternion _initalRotation;

    [HideInInspector]
    public Material material;

    public Actuator actuatorData;

    void Awake()
    {
        _initalPos = this.transform.position;
        _initalRotation = this.transform.rotation;
        material = GetComponent<Renderer>().material;
    }

    void Start()
    {
        GameManager.Instance.medicalObjs.Add(this.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        /* if (collision.transform.tag.Equals("Floor"))
        {
            this.transform.position = _initalPos;
            this.transform.rotation = _initalRotation;
        } */
    }

    public void DelayedReset(float time)
    {
        StartCoroutine(ResetActuator(time));
    }

    IEnumerator ResetActuator(float time)
    {
        yield return new WaitForSeconds(time);

        this.transform.position = _initalPos;
        this.transform.rotation = _initalRotation;

        yield break;
    }
}
