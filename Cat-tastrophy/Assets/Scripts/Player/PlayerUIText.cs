using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUIText : MonoBehaviour
{
    [SerializeField] private Text UIText;
    private float showTime;
    private bool coroutineIsRunning = false;
    [SerializeField] private Image speechBubble;
    private void Start()
    {
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
            coroutineIsRunning = true;
            this.showTime = showTime;
            StartCoroutine("CoroutineShowText");
        }
    }

    public void StopShowingText()
    {
        StopCoroutine("CoroutineShowText");
        coroutineIsRunning = false;
        speechBubble.enabled = false;
        UIText.color = new Color(UIText.color.r, UIText.color.g, UIText.color.b, 0);
    }

    IEnumerator CoroutineShowText()
    {
        
        speechBubble.enabled = true;
        UIText.color = new Color(UIText.color.r, UIText.color.g, UIText.color.b, 1);
        yield return new WaitForSeconds(showTime);
        UIText.color = new Color(UIText.color.r, UIText.color.g, UIText.color.b, 0);
        speechBubble.enabled = false;
        coroutineIsRunning = false;
    }
}
