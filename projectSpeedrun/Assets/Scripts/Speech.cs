using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Speech : MonoBehaviour
{
    public GameObject note;
    public void DisplayText()
    {
        note.transform.DOScale(1, 0.5f);
    }
    public void HideText()
    {
        note.transform.DOScale(0, 0.5f);
    }

    public void DisplaySpeechImage()
    {
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 0f);
    }
    public void HideSpeechImage()
    {
        gameObject.GetComponent<RectTransform>().localScale = Vector3.zero;
    }


}
