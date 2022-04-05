using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerMovement : MonoBehaviour
{
    public float speed = 3f;
    public WaitTimes times;
    public VillagerManager manager;

    private Transform[] _path;
    private int[] _waitTimes;
    private Vector3 _nextPoint;
    private int _pathIndex;
    private bool _waiting = false;

    // Start is called before the first frame update
    void Start()
    {
        //Call Manager to get first path
        SetUpNewPath();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _nextPoint, speed * Time.deltaTime);    
        if(this.transform.position == _nextPoint && !_waiting)
        {
            NextPoint();
        }
    }

    private void NextPoint()
    {
        Debug.Log("Next Point " + _pathIndex);
        _pathIndex++;
        if( _pathIndex >= _path.Length)
        {
            SetUpNewPath();
        }
        else
        {
            Debug.Log("Starting coroutine");
            _waiting = true;
            StartCoroutine(WaitingAtPoint());
        }
    }

    IEnumerator WaitingAtPoint()
    {
        Debug.Log("Waiting for " + _waitTimes[_pathIndex-1]);
        yield return new WaitForSeconds(_waitTimes[_pathIndex-1]);
        _nextPoint = _path[_pathIndex].transform.position;
        _waiting = false;
        Debug.Log("Finished Waiting");
    }

    private void SetUpNewPath()
    {
        times = manager.GetNextPath(this.transform.position);
        _waitTimes = times.waitTimes;
        _path = times.gameObject.GetComponentsInChildren<Transform>();
        _pathIndex = 1;
        _nextPoint = _path[_pathIndex].transform.position;
    }
}
