using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerDropsFood : MonoBehaviour
{
    public GameObject food;
    public int dropTime = 10;
    public int dropChance = 20;
    public GameObject foodParent;

    private bool _canDrop = true;

    // Update is called once per frame
    void Update()
    {
        if (_canDrop)
        {
            int i = Random.Range(0, dropChance + 1);
            Debug.Log("Checking if to drop something " + i);
            if (i == dropChance)
            {
                Instantiate(food, this.transform.position, Quaternion.identity, foodParent.transform);
                _canDrop = false;
                Debug.Log("Dropped something, waiting until can drop again");
                StartCoroutine(AllowDropFood());
            }
        }
    }

    IEnumerator AllowDropFood()
    {
        yield return new WaitForSeconds(dropTime);
        _canDrop = true;
        Debug.Log("Can drop again");
    }
}
