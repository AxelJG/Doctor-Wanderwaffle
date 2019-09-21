using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ProgressBarAction : MonoBehaviour
{
    [HideInInspector]
    public float loadingTime = 1f;
    [HideInInspector]
    public bool grabObject, performAction;
    public UnityEvent onObjectReadyToGrab, onActionPerformed;

    float _curTime = 0f;
    Slider _slider;


    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        Progression();
    }

    private void Progression()
    {
        if (_curTime < loadingTime)
        {
            _curTime += Time.deltaTime;
            _slider.value = _curTime / loadingTime;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void ResetProgressBar()
    {
        _slider.value = 0f;
        _curTime = 0f;
    }

    private void OnEnable()
    {
        transform.LookAt(Camera.main.transform);
    }

    private void OnDisable()
    {
        ResetProgressBar();

        if (grabObject && onObjectReadyToGrab != null)
        {
            Debug.LogFormat("Object ready to grab!");
            onObjectReadyToGrab.Invoke();
        }

        if (performAction && onActionPerformed != null)
        {
            Debug.LogFormat("Performing action on patient...");
            onActionPerformed.Invoke();
        }

        grabObject = false;
        performAction = false;
        GameManager.Instance.playerActionInprogress = false;
    }
}
