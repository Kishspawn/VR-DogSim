using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMgr : MonoBehaviour
{
    public static AIMgr inst;
    public float speed = 2f;
    public float rotateSpeed = 2;
    public NavMeshAgent agent;
    public Transform player;
    public float theDistance = 5f;
    public GameObject ball;
    private float timer = 15f;
    private bool timerOn = false; 
    Animator anim;
    void Awake()
    {
        inst = this;
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //what I want is if click button to call dog, dog comes. Like yelling for it 
        if (Vector3.Distance(transform.position, player.position) >= theDistance)
        {
            speed = 2f;
            anim.SetBool("isMoving", true);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            var rotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
        }
        else
        {
            speed = 0f;
            anim.SetBool("isMoving", false);
        }
       /* if(speed == 0f)
        {
           
            timerOn = true;
            InvokeRepeating("BegToPlayTimer", 0.0f, 0.01667f);
        }
        else if(speed > 0)
        {
            
            timerOn = false;
            anim.SetBool("isMoving", true);
           // anim.SetBool("isBored", false);

        }*/
    }
    void fetch()
    {
        //If I throw ball
        //then dog go towards ball
        //dog reaches ball and picks it up
        //dog walks towards me
        //dog drops ball infront of me
    }
    //Virus timer
    void BegToPlayTimer()
    {
        if (timerOn == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0) //0 minutes terminates
            {
                anim.SetBool("isBored", true);
                
            }
            //anim.SetBool("isBored", false);
        }

    }
}
