using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public Transform grabPoint;
    public GameObject progressBar;

    GameObject _observedObject;
    ActuatorObject _grabbedActuator;
    ProgressBarAction _progressBarAction;
    bool _isGrabbingSomething;

    void Awake()
    {
        _progressBarAction = progressBar.GetComponent<ProgressBarAction>();
    }

    void Start()
    {
        InvokeRepeating("ObjDetection", 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        { 
            //Boton espacio
            if (_observedObject != null)
            {
                if (_isGrabbingSomething)
                {
                    DropObject();
                }

                float loadingTime = .25f;

                switch (_observedObject.tag)
                {
                    case "Grabbable":
                        switch (_grabbedActuator.actuatorData.loadingTime)
                        {
                            case Actuator.loadingTimeOptions.verySlow:
                                loadingTime = 1.25f;
                                break;

                            case Actuator.loadingTimeOptions.slow:
                                loadingTime = 1f;
                                break;

                            case Actuator.loadingTimeOptions.medium:
                                loadingTime = .50f;
                                break;

                            default:
                                loadingTime = .25f;
                                break;
                        }
                        break;

                    case "Patient":
                        Patient patient = _observedObject.GetComponent<Patient>();
                        patient.ReceiveActuator(_grabbedActuator.actuatorData);
                        
                        break;
                }

                ActivateProgressBar(loadingTime); //Activamos barra de progreso para acción
            }
            else if (_isGrabbingSomething)
            {
                DropObject(); //Lanzar objeto
            }
        }
    }

    void ObjDetection()
    {
        RaycastHit hit;

        if (Physics.Raycast(grabPoint.position, grabPoint.TransformDirection(Vector3.forward), out hit, 1f))
        {
            if (hit.transform.tag.Equals("Grabbable") || hit.transform.tag.Equals("Patient"))
            {
                GameManager.Instance.FocusObject(hit.transform.gameObject);
                _grabbedActuator = hit.transform.gameObject.GetComponent<ActuatorObject>();

                _observedObject = hit.transform.gameObject;
                return;
            }
        }

        GameManager.Instance.UnfocusObjects();
        _observedObject =  null;
    }

    public void GrabObject()
    {
        if (_observedObject == null)
        {
            return;
        }

        _isGrabbingSomething = true;

        _grabbedActuator.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        _grabbedActuator.gameObject.transform.parent = grabPoint;
        _grabbedActuator.gameObject.transform.localPosition = Vector3.zero;
        _grabbedActuator.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    void DropObject()
    {
        _isGrabbingSomething = false;

        _grabbedActuator.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        _grabbedActuator.gameObject.GetComponent<ActuatorObject>().DelayedReset(.5f);
        _grabbedActuator.gameObject.transform.parent = null;
        _grabbedActuator = null;
    }

    public void ActivateProgressBar(float time)
    {
        GameManager.Instance.playerActionInprogress = true;

        _progressBarAction.grabObject = true;
        _progressBarAction.loadingTime = time;

        progressBar.SetActive(true);
    }
}
