
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneDisplay : MonoBehaviour
{
    public Image spriteImage, baseImage;

    void Start()
    {
        GameManager.OnActivateHUD += TryActivateHUD;
        GameManager.OnDeactivateHUD += TryDeactivateHUD;
    }

    void OnDestroy()
    {
        GameManager.OnActivateHUD -= TryActivateHUD;
        GameManager.OnDeactivateHUD -= TryDeactivateHUD;
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);
        /* transform.rotation = Quaternion.Euler(Vector3.zero); */
    }

    void TryActivateHUD(GameObject o)
    {
        if (GameObject.ReferenceEquals(o, transform.parent.gameObject))
        {
            spriteImage.enabled = true;
            baseImage.enabled = true;
        }
    }

    void TryDeactivateHUD(GameObject o)
    {
        if (GameObject.ReferenceEquals(o, transform.parent.gameObject))
        {
            spriteImage.enabled = false;
            baseImage.enabled = false;
        }
    }

}
