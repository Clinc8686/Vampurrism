using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int _regeneratedHealth = 1;

    public void OnPickUpFood()
    {
        Destroy(this.gameObject);
    }

    public int GetRegenHealth()
    {
        return _regeneratedHealth;
    }
}
