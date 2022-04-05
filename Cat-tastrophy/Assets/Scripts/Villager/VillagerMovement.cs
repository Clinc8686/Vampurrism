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
    private bool _moving = true;

    // Start is called before the first frame update
    void Start()
    {
        //Call Manager to get first path
        SetUpNewPath();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_moving) { return; }
        transform.position = Vector2.MoveTowards(transform.position, _nextPoint, speed * Time.deltaTime);    
        if(this.transform.position == _nextPoint && !_waiting)
        {
            NextPoint();
        }
    }
    
    public void SetMoving(bool m)
    {
        _moving = m;
    }

    private void NextPoint()
    {
        _pathIndex++;
        if( _pathIndex >= _path.Length)
        {
            SetUpNewPath();
        }
        else
        {
            _waiting = true;
            StartCoroutine(WaitingAtPoint());
        }
    }

    IEnumerator WaitingAtPoint()
    {
        yield return new WaitForSeconds(_waitTimes[_pathIndex-1]);
        _nextPoint = _path[_pathIndex].transform.position;
        _waiting = false;
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
