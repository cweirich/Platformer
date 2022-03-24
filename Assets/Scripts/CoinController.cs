using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public float rotationSpeed = 10;

    // Update is called once per frame
    void Update()
    {
        float angle = rotationSpeed * Time.deltaTime;

        transform.Rotate(angle * Vector3.up, Space.World);
    }
}
