using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class navigation : MonoBehaviour
{

    public CanvasGroup menu;
    public CanvasGroup instrucciones;
    public CanvasGroup controles;
    public Button start;
    public Button inst;
    public Button contr;
    public Button backInst;
    public Button backContr;

    // Start is called before the first frame update
    void Start()
    {
        menu.blocksRaycasts = true;
        menu.alpha = 1;
        
    }

    // Update is called once per frame
    void Update()
    {

        
        


    }

    
}
