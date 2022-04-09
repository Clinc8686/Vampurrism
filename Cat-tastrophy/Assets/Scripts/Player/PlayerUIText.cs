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
        coroutineIsRunning = true;
        speechBubble.enabled = true;
        UIText.color = new Color(UIText.color.r, UIText.color.g, UIText.color.b, 1);
        Debug.Log("start");
        yield return new WaitForSeconds(showTime);
        Debug.Log("stop");
        UIText.color = new Color(UIText.color.r, UIText.color.g, UIText.color.b, 0);
        speechBubble.enabled = false;
        coroutineIsRunning = false;
    }
}
