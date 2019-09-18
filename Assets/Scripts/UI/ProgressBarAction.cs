using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ProgressBarAction : MonoBehaviour
{
    public float timeMax = 1f;
    public UnityEvent loaded;

    private float currTime = 0f;
    private Slider slider;
    

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        Progression();
    }

    private void Progression() {
        if (currTime < timeMax) {
            currTime += Time.deltaTime;
            slider.value = currTime / timeMax;
        } else {
            gameObject.SetActive(false);
        }
    }

    private void ResetProgressBar() {
        slider.value = 0f;
        currTime = 0f;
    }

    private void OnEnable() {
        transform.LookAt(Camera.main.transform);
    }

    private void OnDisable() {
        ResetProgressBar();
        loaded.Invoke();
        GameManager.Instance.playerActionInprogress = false;
    }
}
