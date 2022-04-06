using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerDropsFood : MonoBehaviour
{
    public GameObject normalFood;
    public GameObject betterFood;
    public int dropTime = 10;
    public int dropChance = 20;
   
    private GameObject _foodParent;
    private bool _canDrop = true;
    private int type;

    private void Start()
    {
        type = this.GetComponent<VillagerMovement>().villagerType;
    }

    // Update is called once per frame
    void Update()
    {
        if (_canDrop)
        {
            int i = Random.Range(0, dropChance + 1);
            Debug.Log("Checking if to drop something " + i);
            if (i == dropChance)
            {
                GameObject toInstantiate = (type == 0 ? normalFood : betterFood);
                Instantiate(toInstantiate, this.transform.position, Quaternion.identity, _foodParent.transform);
                _canDrop = false;
                Debug.Log("Dropped something, waiting until can drop again");
                StartCoroutine(AllowDropFood());
            }
        }
    }

    public void SetFoodParent(GameObject parent)
    {
        _foodParent = parent;
    }

    IEnumerator AllowDropFood()
    {
        yield return new WaitForSeconds(dropTime);
        _canDrop = true;
        Debug.Log("Can drop again");
    }
}
