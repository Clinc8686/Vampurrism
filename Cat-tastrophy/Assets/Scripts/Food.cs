using UnityEngine;

public class Food : MonoBehaviour
{
    public int _regeneratedHealth = 1;
    public GameObject interactIndicator;

    public void OnPickUpFood()
    {
        Destroy(this.gameObject);
    }

    public int GetRegenHealth()
    {
        return _regeneratedHealth;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
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
