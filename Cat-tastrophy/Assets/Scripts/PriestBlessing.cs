using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PriestBlessing : MonoBehaviour
{
    [SerializeField] private PlayerUIText priestSpeechBubble;
    public int blessingTime = 5;
    public int blessingCooldown = 100;
    public GameObject blessingBar;
    public Text blessingTextObject;
    public RectTransform blessingBarRect;
    public string coolDownText = "Am Beten...";
    public string blessingText = "Am Weihen...";
    public Well well;
    public GameObject interactIndicator;

    private float _coolDownInterval;
    private float _blessInterval;
    private float _blessBarRectWidth;
    private float _blessBarReduction;
    private int _blessingInProgress = 0;
    private bool _canBless = true;

    // Start is called before the first frame update
    void Start()
    {
        _blessInterval = (float)blessingTime / 100.0f;
        _coolDownInterval = (float)blessingCooldown / 100.0f;
        _blessBarRectWidth = blessingBarRect.rect.width;
        _blessBarReduction = _blessBarRectWidth / 100.0f;
    }

    // Update is called once per frame
    //Checks whether the priest is blessing or cooling down and
    //starts the corresponding coroutine
    void Update()
    {
        Vector3 priestPos = Camera.main.WorldToScreenPoint(this.transform.position);
        blessingBar.transform.position = new Vector3(priestPos.x, priestPos.y);
        if (_blessingInProgress == 2)
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

    public void SetCanBless(bool x)
    {
        _canBless = x;
    }

    //Sets the UI for blessing active and starts the blessing
    public bool BlessWell()
    {
        if(_blessingInProgress == 0 && _canBless)
        {
            Debug.Log("Starting to bless..." + _blessInterval);
            gameObject.GetComponent<Animator>().SetTrigger("Blessing");
            interactIndicator.SetActive(false);
            _blessingInProgress = 1;
            blessingBar.SetActive(true);
            blessingTextObject.text = blessingText;
            StartCoroutine(Blessing());
            return true;
        }
        return false;
    }

    //Lowers the progress bar in certain intervalls
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

    //Raises the progress bar in certain intervalls
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

    //Blesses the well and starts the cooling down process
    private void FinishedBlessing()
    {
        Debug.Log("Finished blessing, starting cooldown");
        well.BlessWell();
        blessingTextObject.text = coolDownText;
        _blessingInProgress = 3;
        StartCoroutine(CoolDownBlessing());
        priestSpeechBubble.AddText("The well contains now holy water.");
        priestSpeechBubble.StartShowingText(5.0f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && _blessingInProgress == 0)
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
