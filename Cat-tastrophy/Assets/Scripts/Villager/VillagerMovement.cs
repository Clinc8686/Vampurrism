using System.Collections;
using UnityEngine;

public class VillagerMovement : MonoBehaviour
{
    public float speed = 3f;
    public WaitTimes times;
    //Type 0 = normal Villager, 1 = Impfgegner
    public int villagerType = 0;

    private VillagerManager _manager;
    private Transform[] _path;
    private int[] _waitTimes;
    private Vector3 _nextPoint;
    private int _pathIndex;
    private bool _waiting = false;
    private bool _moving = true;
    private bool _direction = false;
    private Vector2 _position, _lastPosition;

    //Majas variables:
    public bool collidedWithVampire= false;
    public  int secondstowait = 5;
    private float catpause;


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
        _lastPosition = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, _nextPoint, speed * Time.deltaTime); 
        _position = transform.position;
        _direction = (_position.x - _lastPosition.x > 0) ? true : false;
        this.GetComponent<SpriteRenderer>().flipX = _direction;
        if (this.transform.position == _nextPoint && !_waiting)
        {
            NextPoint();
        }
    }
    
    public void SetMoving(bool m)
    {
        _moving = m;
    }

    public void SetVillagerManager(VillagerManager manager)
    {
        _manager = manager;
    }

    private void NextPoint()
    {

        // Majas Code:
        if (collidedWithVampire == true && secondstowait <= catpause) //prepare the countdown, something collided.
        {
            Debug.Log("colllidedwithVampire=true");
            catpause = 0;
            secondstowait = 5;
        }

        if (secondstowait >= catpause && collidedWithVampire == true) //do the countdown
        {
            catpause += Time.deltaTime;
        }

        if (secondstowait <= catpause && collidedWithVampire == true) // after the countdown
        {
            collidedWithVampire = false;
        }
        //untill here

        if(collidedWithVampire == false)
        {
            _pathIndex++;
            if( _pathIndex >= _path.Length)
            {
                SetUpNewPath();
            }
            else
            {
                _waiting = true;
                if(_waitTimes[_pathIndex-1] > 0)
                {
                    this.GetComponent<Animator>().SetBool("Walking", false);
                }
                StartCoroutine(WaitingAtPoint());
            }

        }

    }



    IEnumerator WaitingAtPoint()
    {
        yield return new WaitForSeconds(_waitTimes[_pathIndex-1]);
        _nextPoint = _path[_pathIndex].transform.position;
        _waiting = false;
        this.GetComponent<Animator>().SetBool("Walking", true);
    }

    private void SetUpNewPath()
    {
        times = _manager.GetNextPath(this.transform.position);
        _waitTimes = times.waitTimes;
        _path = times.gameObject.GetComponentsInChildren<Transform>();
        _pathIndex = 1;
        _nextPoint = _path[_pathIndex].transform.position;
    }
}
