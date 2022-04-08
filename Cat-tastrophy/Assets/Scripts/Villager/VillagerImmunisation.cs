using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VillagerImmunisation : MonoBehaviour
{
    public GameObject immuneBar;
    public GameObject immuneArrow;
    public int immuneBarOffset = 37;
    public float immunityFailingRate = 1;
    public GameObject notImmuneText;
    public int rejectImmunityTime = 5;

    private int _immune = 0;
    private Vector3 _villagerScreenPos;
    private int _currentImmunity = 100;
    private bool _decreasedImmunity = false;
    private bool _notImmune = false;


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
        if (_notImmune)
        {
            _villagerScreenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            notImmuneText.transform.position = new Vector3(_villagerScreenPos.x, _villagerScreenPos.y + immuneBarOffset);
        }
    }

    public void SetGeimpft()
    {
        Debug.Log("Impfstatus: " + _immune);
        if(_immune == 0)
        {
            _immune = 1;
            tag = "immune";
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.tag == "Vaccine")
        {
            int type = this.gameObject.GetComponent<VillagerMovement>().villagerType;
            if(type == 1)
            {
                Vector2 collisionAngle = new Vector2(go.transform.position.x -gameObject.transform.position.x, go.transform.position.y - gameObject.transform.position.y);
                float degrees = Mathf.Atan2(collisionAngle.y, collisionAngle.x);
                if(!gameObject.GetComponent<VillagerMovement>().GetDirection() && ((degrees > 1.8f && degrees < 4f) || (degrees < -1.5f && degrees > -4f)))
                {
                    StartCoroutine(AgainstImmunity());
                }
                else if (gameObject.GetComponent<VillagerMovement>().GetDirection() && (degrees < 1.8f && degrees > -1.5f))
                {
                    StartCoroutine(AgainstImmunity());
                }
                Debug.Log("Collision Angle: " + degrees + ", direction: " + gameObject.GetComponent<VillagerMovement>().GetDirection());
            }
            SetGeimpft();
            Destroy(collision.gameObject);
        }
    }

    IEnumerator AgainstImmunity()
    {
        _notImmune = true;
        _immune = 10;
        notImmuneText.SetActive(true);
        yield return new WaitForSeconds(rejectImmunityTime);
        _notImmune = false;
        notImmuneText.SetActive(false);
        _immune = 0;
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
        _currentImmunity = 100;
        tag = "Villager";
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
