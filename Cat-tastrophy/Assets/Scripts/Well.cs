using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : MonoBehaviour
{
    public Sprite normalWell;
    public Sprite blessedWell;
    public int wellStaysBlessedTime = 5;

    private bool _isBlessed = false;
    private bool _isFull = true;

    //Bless the well and start the coroutine to unbless it after a certain time
    public void BlessWell()
    {
        if(!_isFull) { return; }
        _isBlessed = true;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = blessedWell;
        StartCoroutine(WellBlessTime());
    }

    //After a certain time the well automatically unblesses itself
    IEnumerator WellBlessTime()
    {
        yield return new WaitForSeconds(wellStaysBlessedTime);
        _isBlessed = false;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = normalWell;
    }

    //Returns true if the well is blessed and unblesses it
    public bool GetBlessedWater()
    {
        if (_isBlessed && _isFull)
        {
            _isBlessed = false;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = normalWell;
            return true;
        }
        return false;
    }

    public void SetIsFull(bool x)
    {
        _isFull = x;
    }
}
