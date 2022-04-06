using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PriestBlessing : MonoBehaviour
{
    public int blessingTime = 5;
    public int blessingCooldown = 100;
    public GameObject blessingBar;
    public Text blessingTextObject;
    public RectTransform blessingBarRect;
    public string coolDownText = "Am Beten...";
    public string blessingText = "Am Weihen...";
    public Well well;

    private float _coolDownInterval;
    private float _blessInterval;
    private float _blessBarRectWidth;
    private float _blessBarReduction;
    private int _blessingInProgress = 0;

    // Start is called before the first frame update
    void Start()
    {
        _blessInterval = (float)blessingTime / 100.0f;
        _coolDownInterval = (float)blessingCooldown / 100.0f;
        _blessBarRectWidth = blessingBarRect.rect.width;
        _blessBarReduction = _blessBarRectWidth / 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(_blessingInProgress == 2)
        {
            _blessingInProgress = 1;
            StartCoroutine(Blessing());
        }
        if(_blessingInProgress == 4)
        {
            _blessingInProgress = 3;
            StartCoroutine(CoolDownBlessing());
        }
    }

    public void BlessWell()
    {
        if(_blessingInProgress == 0)
        {
            Debug.Log("Starting to bless..." + _blessInterval);
            _blessingInProgress = 1;
            blessingBar.SetActive(true);
            blessingTextObject.text = blessingText;
            StartCoroutine(Blessing());
        }
    }

    IEnumerator Blessing()
    {
        yield return new WaitForSeconds(_blessInterval);
        _blessingInProgress = 2;
        float temp = blessingBarRect.rect.width;
        if(temp <= 0)
        {
            FinishedBlessing();
        }
        blessingBarRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, temp - _blessBarReduction);
    }

    IEnumerator CoolDownBlessing()
    {
        yield return new WaitForSeconds(_coolDownInterval);
        _blessingInProgress = 4;
        float temp = blessingBarRect.rect.width;
        if (temp >= _blessBarRectWidth)
        {
            _blessingInProgress = 0;
            blessingBar.SetActive(false);
        }
        blessingBarRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, temp + _blessBarReduction);
    }

    private void FinishedBlessing()
    {
        Debug.Log("Finished blessing, starting cooldown");
        well.BlessWell();
        blessingTextObject.text = coolDownText;
        _blessingInProgress = 3;
        StartCoroutine(CoolDownBlessing());
    }
}
