using UnityEngine;
using UnityEngine.UI;

public class VampireManager : MonoBehaviour
{
    public GameObject[] Vampireprefabs;
    public Text Vampirecounter;
    public GameObject villagerParent;
    public int numberVampires = 20;
    public int maxNumberVampires = 20;

    private GameObject[] spawnpoints;
    private Vector3[] _startingPoints;
    private Vector3 thisStartingpoint;
    private int _numberVampiresKilled = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Alle Laufpfade holen und die Startpunkte auslesen
        spawnpoints = GameObject.FindGameObjectsWithTag("VampireStartingPoint") ;
        _startingPoints = new Vector3[spawnpoints.Length];
        for (int i = 0; i < spawnpoints.Length; i++)
        {
            Vector3 meow = spawnpoints[i].GetComponent<Transform>().position;
          //  Transform[] pathPoints = spawnpoints[i].GetComponentsInChildren<Transform>();
          //  Transform pathpoint = pathPoints[i];
            _startingPoints[i] = meow;
        }


        //Villager instantiieren
        Addvampire(numberVampires);

        Vampirecounter.text = (_numberVampiresKilled).ToString(); 
    }


    public void VampireDied()
    {
        numberVampires--;
        _numberVampiresKilled++;
        Vampirecounter.text = (_numberVampiresKilled).ToString();
        Addvampire(1);
    }

    public int GetNumberVampiresKilled()
    {
        return _numberVampiresKilled;
    }

    private void GetRandomStartPoint()
    {
        int i = Random.Range(0, _startingPoints.Length);
        thisStartingpoint= _startingPoints[i];
    }

    public void Addvampire(int numberOfVampires)
    {
        for (int i = 0; i < numberOfVampires; i++)
        {
            GetRandomStartPoint();
            int x = Random.Range(0, 30);
            x = x > 24 ? 2 : x;
            x = x > 19 ? 1 : x;
            x = x > 2 ? 0 : x;
            GameObject temp = Instantiate(Vampireprefabs[x], thisStartingpoint, Quaternion.identity, villagerParent.transform);
            
        }
    }
    public void Addvampire(int numberOfVampires,Vector3 position)
    {
        for (int i = 0; i < numberOfVampires; i++)
        {
            int x = Random.Range(0, 30);
            x = x > 24 ? 2 : x;
            x = x > 19 ? 1 : x;
            x = x > 2 ? 0 : x;
            GameObject temp = Instantiate(Vampireprefabs[x], position, Quaternion.identity, villagerParent.transform);
            numberVampires++;
            Vampirecounter.text = (_numberVampiresKilled).ToString();
        }
    }
}