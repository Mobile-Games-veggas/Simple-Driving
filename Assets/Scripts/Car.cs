using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float speedGamePerSecond = 0.2f;
    [SerializeField] private float turnSpeed = 200f;
    
    private int steerValue;

    void Update()
    {
        speed += speedGamePerSecond * Time.deltaTime; //разгон

        transform.Rotate(0f,steerValue * turnSpeed * Time.deltaTime, 0f); // поворот

        transform.Translate(Vector3.forward * speed * Time.deltaTime); // вперед перемещение
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene(0); // если проиграл, то сцена 0
        }    
    }

    public void Steer(int value)
    {
        steerValue = value; // калибровка управления
    }
}
