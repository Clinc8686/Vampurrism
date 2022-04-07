using UnityEngine;
using UnityEngine.UI;

public class VampireManager : MonoBehaviour
{
    public GameObject Vampireprefab;
    public Text Vampirecounter;
    public GameObject villagerParent;
    public int numberVampires = 5;

    private WaitTimes[] _paths;
    private Vector3[] _startingPoints;

    // Start is called before the first frame update
    void Start()
    {
        //Alle Laufpfade holen und die Startpunkte auslesen
        _paths = GetComponentsInChildren<WaitTimes>();
        _startingPoints = new Vector3[_paths.Length];
        for (int i = 0; i < _paths.Length; i++)
        {
            Transform[] pathPoints = _paths[i].GetComponentsInChildren<Transform>();
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

    private Vector3 GetRandomStartPoint()
    {
        int i = Random.Range(0, _startingPoints.Length);
        return _startingPoints[i];
    }

    public void Addvampire(int numberOfVampires)
    {
        for (int i = 0; i < numberOfVampires; i++)
        {
            GameObject temp = Instantiate(Vampireprefab, new Vector3(0, 0, 0), Quaternion.identity, villagerParent.transform);
            
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