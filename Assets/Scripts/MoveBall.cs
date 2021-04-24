using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    public float speed = 2.0f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //move in z direction every frame (once a second)
            rb.AddForce(Vector3.forward * Time.deltaTime * speed, ForceMode.Impulse);
        }
    }
}
