using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll2 : MonoBehaviour
{
    private float speed = -3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0);
        if (transform.position.x < -9.31)
        {
            transform.position = new Vector3(10f, transform.position.y);
        }
    }
}
