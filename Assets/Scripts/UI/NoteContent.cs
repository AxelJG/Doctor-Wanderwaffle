using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteContent : MonoBehaviour
{
    public Pathology pathology;
    public Text pathologyNameText, difficultyText;
    public Image AspectImage;
    public Transform curesTransform;

    // Start is called before the first frame update
    void Start()
    {
        LoadPathologyNote();
    }
    private void LoadPathologyNote() {
        pathologyNameText.text = pathology.namePathology;
        string difficultyResult = pathology.difficulty.ToString();

        if (difficultyResult.Equals("very_easy")) {
            difficultyResult = "very easy";
        }else if (difficultyResult.Equals("very_hard")) {
            difficultyResult = "very hard";
        }
        difficultyText.text = difficultyResult;

        AspectImage.sprite = pathology.sprite;

        foreach (Transform cure in curesTransform) {
            for (int i = 0; i < pathology.actuators.Count; i++) {
                cure.GetComponent<Image>().sprite = pathology.actuators[i].sprite;
            }
        }
    }
}
