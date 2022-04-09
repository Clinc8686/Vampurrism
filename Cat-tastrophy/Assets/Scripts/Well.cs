using System.Collections;
using UnityEngine;

public class Well : MonoBehaviour
{
    public Sprite normalWell;
    public Sprite blessedWell;
    public int wellStaysBlessedTime = 5;
    public ParticleSystem SparcleParticles;
    public GameObject interactIndicator;

    private bool _isBlessed = false;
    private bool _isFull = true;

    //Bless the well and start the coroutine to unbless it after a certain time
    public void BlessWell()
    {
        if(!_isFull) { return; }
        _isBlessed = true;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = blessedWell;
        SparcleParticles.gameObject.SetActive(true); // maja he
        SparcleParticles.Play();                     // maja ho
        StartCoroutine(WellBlessTime());
        transform.GetComponent<AudioSource>().Play();
    }

    //After a certain time the well automatically unblesses itself
    IEnumerator WellBlessTime()
    {
        yield return new WaitForSeconds(wellStaysBlessedTime);
        _isBlessed = false;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = normalWell;
        SparcleParticles.Stop();                      // maja da
        SparcleParticles.gameObject.SetActive(false); // maja ah-ah
    }

    //Returns true if the well is blessed and unblesses it
    public bool GetBlessedWater()
    {
        if (_isBlessed && _isFull)
        {
            _isBlessed = false;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = normalWell;
            interactIndicator.SetActive(false);
            return true;
        }
        return false;
    }

    public void SetIsFull(bool x)
    {
        _isFull = x;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && _isBlessed)
        {
            interactIndicator.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactIndicator.SetActive(false);
        }
    }
}
