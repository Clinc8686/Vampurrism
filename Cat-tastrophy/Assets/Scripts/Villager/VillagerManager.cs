using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerManager : MonoBehaviour
{
    private WaitTimes[] _paths;

    // Start is called before the first frame update
    void Start()
    {
        _paths = GetComponentsInChildren<WaitTimes>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public WaitTimes GetNextPath(Vector3 currentPosition)
    {
        return _paths[0];
    }
}
