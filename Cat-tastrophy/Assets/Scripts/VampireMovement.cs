using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireMovement : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject PlayerTarget;
    public float VampireSpeed=2f;


    void Start()
    {
        PlayerTarget = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()  
    {
        transform.LookAt(PlayerTarget.transform.position);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.eulerAngles.x*-1)); // roate z
        transform.Translate(VampireSpeed * Time.deltaTime,0.0f, 0.0f );
    }
}
