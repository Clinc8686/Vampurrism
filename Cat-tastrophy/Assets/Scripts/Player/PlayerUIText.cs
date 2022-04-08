using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUIText : MonoBehaviour
{
    [SerializeField] private Text UIText;
    private bool blink;
    private bool coroutineIsRunning = false;
    private Image speechBubble;
    private void Start()
    {
        speechBubble = transform.parent.gameObject.GetComponent<SpriteRenderer>().GetComponent<Image>();
        speechBubble.enabled = false;
        UIText.color = new Color(UIText.color.r, UIText.color.g, UIText.color.b, 0);
    }

    public void addText(string text, bool blink)
    {
        this.blink = blink;
        UIText.text = text;
    }

    public void startShowingText()
    {
        if (!coroutineIsRunning)
        {
            UIText.color = new Color(UIText.color.r, UIText.color.g, UIText.color.b, 1);
            speechBubble.enabled = true;
            //StopCoroutine("coroutineShowText");
            //StartCoroutine("coroutineShowText");
        }
    }

    public void stopShowingText()
    {
        speechBubble.enabled = false;
        UIText.color = new Color(UIText.color.r, UIText.color.g, UIText.color.b, 0);
        //StopCoroutine("coroutineShowText");
    }

    IEnumerator coroutineShowText()
    {
        coroutineIsRunning = true;
        if (blink)
        {
            while (true)
            {
                switch (UIText.color.a.ToString())
                {
                    case "0":
                        UIText.color = new Color(UIText.color.r, UIText.color.g, UIText.color.b, 1);
                        yield return new WaitForSeconds(1.0f);
                        break;
                    case "1":
                        UIText.color = new Color(UIText.color.r, UIText.color.g, UIText.color.b, 0);
                        yield return new WaitForSeconds(1.0f);
                        break;
                }
            }
        }
        else
        {
            UIText.color = new Color(UIText.color.r, UIText.color.g, UIText.color.b, 1);
        }

        coroutineIsRunning = false;
    }
}
