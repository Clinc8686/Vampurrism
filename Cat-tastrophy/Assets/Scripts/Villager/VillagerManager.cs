using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillagerManager : MonoBehaviour
{
    public GameObject normalVillagerPrefab;
    public GameObject impfgegnerVillagerPrefab;
    public Text villagerCounter;
    public GameObject villagerParent;
    public GameObject foodParent;
    public int numberNormalVillagers = 5;
    public int numberImpfgegnerVillager = 1;

    private WaitTimes[] _paths;
    private Vector3[] _startingPoints;

    // Start is called before the first frame update
    void Start()
    {
        //Alle Laufpfade holen und die Startpunkte auslesen
        _paths = GetComponentsInChildren<WaitTimes>();
        _startingPoints = new Vector3[_paths.Length];
        for(int i = 0; i < _paths.Length; i++)
        {
            Transform[] pathPoints = _paths[i].GetComponentsInChildren<Transform>();
            _startingPoints[i] = pathPoints[1].transform.position;
        }

        //Villager instantiieren
        for(int i = 0; i < numberNormalVillagers; i++)
        {
            GameObject temp = Instantiate(normalVillagerPrefab, GetRandomStartPoint(), Quaternion.identity, villagerParent.transform);
            temp.GetComponent<VillagerMovement>().SetVillagerManager(this);
            temp.GetComponent<VillagerDropsFood>().SetFoodParent(foodParent);
        }
        for (int i = 0; i < numberImpfgegnerVillager; i++)
        {
            GameObject temp =Instantiate(impfgegnerVillagerPrefab, GetRandomStartPoint(), Quaternion.identity, villagerParent.transform);
            temp.GetComponent<VillagerMovement>().SetVillagerManager(this);
            temp.GetComponent<VillagerDropsFood>().SetFoodParent(foodParent);
        }
        villagerCounter.text = (numberImpfgegnerVillager + numberNormalVillagers).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VillagerDied()
    {

        numberNormalVillagers--;
        villagerCounter.text = (numberImpfgegnerVillager + numberNormalVillagers).ToString();
    }

    public void VampireTurnedBack(Vector3 position)
    {
        int type = Random.Range(0, 2);
        GameObject toInstantiate = (type == 0) ? normalVillagerPrefab : impfgegnerVillagerPrefab;
        GameObject temp = Instantiate(toInstantiate, position, Quaternion.identity, villagerParent.transform);
        temp.GetComponent<VillagerMovement>().SetVillagerManager(this);
        temp.GetComponent<VillagerDropsFood>().SetFoodParent(foodParent);
        //Hier noch den Kurs auf den nächsten Path setzen
    }

    public WaitTimes GetNextPath(Vector3 currentPosition)
    {
        List<int> possiblePaths = new List<int>();
        for(int i = 0; i < _paths.Length; i++)
        {
            if(_startingPoints[i] == currentPosition)
            {
                possiblePaths.Add(i);
            }
        }
        int x = Random.Range(0, possiblePaths.Count);
        Debug.Log("Looking for a new path in: " + possiblePaths.Count + ", paths are: " + _paths.Length);
        return _paths[possiblePaths[x]];
    }

    private Vector3 GetRandomStartPoint()
    {
        int i = Random.Range(0, _startingPoints.Length);
        return _startingPoints[i];
    }
}
