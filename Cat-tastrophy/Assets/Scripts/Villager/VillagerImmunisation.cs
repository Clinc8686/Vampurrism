using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillagerImmunisation : MonoBehaviour
{
    public GameObject immuneBar;
    public GameObject immuneArrow;
    public int immuneBarOffset = 37;
    public int immunityFailingRate = 1;

    private int _immune = 0;
    private Vector3 _villagerScreenPos;
    private int _currentImmunity = 100;
    private bool _decreasedImmunity = false;


    // Update is called once per frame
    void Update()
    {
        if (_currentImmunity <= 0)
        {
            ImmunityFailed();
        }
        if (_immune == 1)
        {
            GotGeimpft();
        }
        if (_immune == 2)
        {
            _villagerScreenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            immuneBar.transform.position = new Vector3(_villagerScreenPos.x, _villagerScreenPos.y + immuneBarOffset);

            if (!_decreasedImmunity)
            {
                _decreasedImmunity = true;
                StartCoroutine(FailingImmunity());
            }
        }
    }

    public void SetGeimpft()
    {
        if(_immune == 0)
        {
            _immune = 1;
        }
    }

    public int GetImpfStatus()
    {
        return _immune;
    }

    private void ImmunityFailed()
    {
        immuneBar.SetActive(false);
        immuneArrow.SetActive(false);
        _immune = 0;
    }

    private void GotGeimpft()
    {
        immuneBar.SetActive(true);
        immuneArrow.SetActive(true);
        _currentImmunity = 100;
        _immune = 2;
        SetBarSize();
    }

    IEnumerator FailingImmunity()
    {
        yield return new WaitForSeconds(immunityFailingRate);
        _currentImmunity -= 1;
        SetBarSize();
        _decreasedImmunity = false;
    }

    private void SetBarSize()
    {
        Image[] iBars = immuneBar.GetComponentsInChildren<Image>();
        RectTransform temp = iBars[2].gameObject.GetComponent<RectTransform>();
        temp.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _currentImmunity);
    }
}
