using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    private void Update()
    {
        if (Input.GetKey(KeyCode.W)) transform.position += transform.forward * speed * Time.deltaTime; 
        if (Input.GetKey(KeyCode.S)) transform.position -= transform.forward * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.D)) transform.position += transform.right * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A)) transform.position -= transform.right * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space)) transform.position += transform.up * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftControl)) transform.position -= transform.up * speed * Time.deltaTime;
    }
}
