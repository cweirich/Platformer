using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{ 
    private float initialPos;
    private int direction = 1;
    public float speed;
    public int range;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float pos = transform.position.y;
        float newPos;
        float acceleration;

        if (direction < 0)
            acceleration = 2;
        else
            acceleration = 1;

        newPos = pos + speed * Time.deltaTime * direction * acceleration;

        if (initialPos <= newPos && newPos <= initialPos + range)
            transform.position = new Vector3(transform.position.x, newPos, transform.position.z);
        else
            direction *= -1;
    }
}
