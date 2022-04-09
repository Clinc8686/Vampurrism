using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PriestSpeechBubble : MonoBehaviour
{
    [SerializeField] private Text UIText;
    private float showTime;
    private bool coroutineIsRunning = false;
    private Image speechBubble;
    private void Start()
    {
        speechBubble = transform.parent.gameObject.GetComponent<Image>();
        speechBubble.enabled = false;
        UIText.color = new Color(UIText.color.r, UIText.color.g, UIText.color.b, 0);
    }

    public void AddText(string text)
    {
        UIText.text = text;
    }

    public void StartShowingText(float showTime)
    {
        if (!coroutineIsRunning)
        {
            this.showTime = showTime;
            StartCoroutine("CoroutineShowPriestText");
        }
    }

    public void StopShowingText()
    {
        StopCoroutine("CoroutineShowPriestText");
        coroutineIsRunning = false;
        UIText.color = new Color(UIText.color.r, UIText.color.g, UIText.color.b, 0);
        speechBubble.enabled = false;
    }

    IEnumerator CoroutineShowPriestText()
    {
        coroutineIsRunning = true;
        speechBubble.enabled = true;
        UIText.color = new Color(UIText.color.r, UIText.color.g, UIText.color.b, 1);
        yield return new WaitForSeconds(showTime);
        UIText.color = new Color(UIText.color.r, UIText.color.g, UIText.color.b, 0);
        speechBubble.enabled = false;
        coroutineIsRunning = false;
    }
}