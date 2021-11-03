using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float speedGamePerSecond = 0.2f;
    
    void Update()
    {
        speed += speedGamePerSecond * Time.deltaTime; //разгон

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
