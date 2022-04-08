using UnityEngine;
using UnityEngine.UI;

public class VampireManager : MonoBehaviour
{
    public GameObject Vampireprefab;
    public Text Vampirecounter;
    public GameObject villagerParent;
    public int numberVampires = 5;

    private GameObject[] spawnpoints;
    private Vector3[] _startingPoints;
    private Vector3 thisStartingpoint;

    // Start is called before the first frame update
    void Start()
    {
        //Alle Laufpfade holen und die Startpunkte auslesen
        spawnpoints = GameObject.FindGameObjectsWithTag("VampireStartingPoint") ;
        _startingPoints = new Vector3[spawnpoints.Length];
        for (int i = 0; i < spawnpoints.Length; i++)
        {
            Transform[] pathPoints = spawnpoints[i].GetComponentsInChildren<Transform>();
            _startingPoints[i] = pathPoints[1].transform.position;
        }


        //Villager instantiieren
        Addvampire(numberVampires);

        Vampirecounter.text = (numberVampires).ToString(); 
    }

    public void VampireDied()
    {
        numberVampires--;
        Vampirecounter.text = (numberVampires).ToString();
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
            GameObject temp = Instantiate(Vampireprefab, thisStartingpoint, Quaternion.identity, villagerParent.transform);
            
        }
    }
    public void Addvampire(int numberOfVampires,Vector3 position)
    {
        for (int i = 0; i < numberOfVampires; i++)
        {
            GameObject temp = Instantiate(Vampireprefab, position, Quaternion.identity, villagerParent.transform);
            numberVampires++;
            Vampirecounter.text = (numberVampires).ToString();
            Debug.Log(numberVampires);
        }
    }
}