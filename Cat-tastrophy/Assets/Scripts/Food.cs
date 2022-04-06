using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int _regeneratedHealth = 1;

    public int OnPickUpFood()
    {
        Destroy(this.gameObject, 1);
        return _regeneratedHealth;
    }
}
