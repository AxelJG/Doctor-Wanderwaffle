using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public Transform grabPoint;
    public GameObject progressBar;

    GameObject _grabbedObject;
    ActuatorObject _observedActuator;
    ProgressBarAction _progressBarAction;
    bool _isGrabbingSomething;

    private void Awake()
    {
        _progressBarAction = progressBar.GetComponent<ProgressBarAction>();
    }

    // Update is called once per frame
    void Update()
    {
        ObjDetection();

        if (Input.GetButtonDown("Jump"))
        { //Boton espacio
            if (ObjDetection() != null)
            {
                if (_isGrabbingSomething)
                {
                    DropObject();
                }

                float loadingTime = .25f;

                switch (_observedActuator.actuatorData.loadingTime)
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

                ActivateProgressBar(loadingTime); //Activamos barra de progreso para acción
            }
            else if (_grabbedObject != null)
            {
                DropObject(); //Lanzar objeto
            }
        }
    }

    GameObject ObjDetection()
    {
        RaycastHit hit;

        if (Physics.Raycast(grabPoint.position, grabPoint.TransformDirection(Vector3.forward), out hit, 1f))
        {
            if (hit.transform.tag.Equals("Grabbable"))
            {
                GameManager.Instance.FocusObject(hit.transform.gameObject);
                _observedActuator = hit.transform.gameObject.GetComponent<ActuatorObject>();

                return hit.transform.gameObject;
            }
        }

        GameManager.Instance.UnfocusObjects();
        return null;
    }

    public void GrabObject()
    {
        _grabbedObject = ObjDetection();

        if (_grabbedObject == null)
        {
            return;
        }

        _isGrabbingSomething = true;

        _grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
        _grabbedObject.transform.parent = grabPoint;
        _grabbedObject.transform.localPosition = Vector3.zero;
        _grabbedObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    void DropObject()
    {
        _grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
        _grabbedObject.GetComponent<ActuatorObject>().DelayedReset(.5f);

        _isGrabbingSomething = false;
        _grabbedObject.transform.parent = null;
        _grabbedObject = null;
    }

    public void ActivateProgressBar(float time)
    {
        GameManager.Instance.playerActionInprogress = true;

        _progressBarAction.timeMax = time;
        _progressBarAction.loaded.RemoveAllListeners();
        _progressBarAction.loaded.AddListener(GrabObject);

        progressBar.SetActive(true);
    }
}
