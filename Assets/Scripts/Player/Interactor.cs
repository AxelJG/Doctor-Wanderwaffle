using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform grabPoint;
    GameObject _observedObject;
    ActuatorObject _grabbedActuator;
    Patient _targetPatient;
    bool _isGrabbingSomething;

    public GameObject progressBar;
    ProgressBarAction _progressBarAction;

    public GameObject workingParticleEffect;

    void Awake()
    {
        _progressBarAction = progressBar.GetComponent<ProgressBarAction>();
    }

    void Start()
    {
        InvokeRepeating("ScanForObjects", 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //Boton espacio
            if (_observedObject != null)
            {
                float loadingTime = .25f;

                switch (_observedObject.tag)
                {
                    case "Grabbable":
                        switch (_observedObject.GetComponent<ActuatorObject>().actuatorData.grabbingTime)
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

                        ActivateProgressBar(loadingTime, true, false); //Activamos barra de progreso para acción
                        break;

                    case "Patient":
                        if (!_isGrabbingSomething)
                        {
                            break;
                        }

                        _targetPatient = _observedObject.GetComponent<Patient>();

                        switch (_grabbedActuator.actuatorData.performanceTime)
                        {
                            case Actuator.loadingTimeOptions.verySlow:
                                loadingTime = 2.5f;
                                break;

                            case Actuator.loadingTimeOptions.slow:
                                loadingTime = 2f;
                                break;

                            case Actuator.loadingTimeOptions.medium:
                                loadingTime = 1.5f;
                                break;

                            default:
                                loadingTime = 1f;
                                break;
                        }

                        workingParticleEffect.SetActive(true);
                        ActivateProgressBar(loadingTime, false, true); //Activamos barra de progreso para acción
                        break;
                }
            }
            else if (_isGrabbingSomething)
            {
                DropObject(); //Lanzar objeto
            }
        }
    }

    void ScanForObjects()
    {
        RaycastHit hit;

        if (Physics.Raycast(grabPoint.position, grabPoint.TransformDirection(Vector3.forward), out hit, 1f))
        {
            if (hit.transform.tag.Equals("Grabbable") || hit.transform.tag.Equals("Patient"))
            {
                GameManager.Instance.FocusObject(hit.transform.gameObject);
                _observedObject = hit.transform.gameObject;
            }

            return;
        }

        GameManager.Instance.UnfocusObjects();
        _observedObject = null;
    }

    public void GrabObject()
    {
        if (_isGrabbingSomething)
        {
            Debug.LogFormat("Dropped object: {0}", _grabbedActuator);
            _grabbedActuator.Drop();
        }

        _isGrabbingSomething = true;

        _grabbedActuator = _observedObject.GetComponent<ActuatorObject>();
        Debug.LogFormat("Grabbed object: {0}", _grabbedActuator);
        _grabbedActuator.Grab(grabPoint);

    }

    void DropObject()
    {
        _isGrabbingSomething = false;

        _grabbedActuator.Drop();
        _grabbedActuator = null;
    }

    void ResetObject()
    {
        _isGrabbingSomething = false;

        _grabbedActuator.ForceReset();
        _grabbedActuator = null;
    }

    public void ActivateProgressBar(float time, bool grabObject, bool performAction)
    {
        if (grabObject && performAction)
        {
            Debug.LogWarning("Cannot activate progressbar in both modes at the same time", this);
        }

        GameManager.Instance.playerActionInprogress = true;

        _progressBarAction.grabObject = grabObject;
        _progressBarAction.performAction = performAction;
        _progressBarAction.loadingTime = time;

        progressBar.SetActive(true);
    }

    public void PerformAction()
    {
        Debug.LogFormat(this, "Action performed on patient {0}", _targetPatient);
        _targetPatient.ReceiveActuator(_grabbedActuator.actuatorData);

        workingParticleEffect.SetActive(false);

        ResetObject();
    }
}
