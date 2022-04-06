using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : MonoBehaviour
{
    public Sprite normalWell;
    public Sprite blessedWell;
    public int wellStaysBlessedTime = 5;

    private bool _isBlessed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BlessWell()
    {
        _isBlessed = true;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = blessedWell;
        StartCoroutine(WellBlessTime());
    }

    IEnumerator WellBlessTime()
    {
        yield return new WaitForSeconds(wellStaysBlessedTime);
        _isBlessed = false;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = normalWell;
    }

    public bool GetBlessedWater()
    {
        if (_isBlessed)
        {
            _isBlessed = false;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = normalWell;
            return true;
        }
        return false;
    }
}
