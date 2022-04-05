using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireMovement : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject PlayerTarget;
    public float VampireSpeed=2f;
    private Vector3 targetPos;
    private Vector3 thisPos;
    private float angle;
    public float offset;

    void Start()
    {
        PlayerTarget = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()  
    {
        targetPos = PlayerTarget.transform.position;
        thisPos = transform.position;
        targetPos.x = targetPos.x - thisPos.x;
        targetPos.y = targetPos.y - thisPos.y;
        angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));


        transform.Translate(VampireSpeed * Time.deltaTime,0.0f, 0.0f );

    }
}
