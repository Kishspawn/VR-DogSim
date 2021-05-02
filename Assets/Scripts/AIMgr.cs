using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR;

public class AIMgr : MonoBehaviour
{
    public static AIMgr inst;

    public bool whistling = false;

    public float speed = 0f;
    public float rotateSpeed = 2;
    public NavMeshAgent agent;
    public Transform player;
    public float theDistance = 5f;
    public GameObject ball;
    private float timer = 5f;
    private bool timerOn = false;
    Animator anim;
    public GameObject bowl;
    bool isFood = false;

    public Transform dog;
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
        if (SteamVR_Actions._default.Whistle.GetState(SteamVR_Input_Sources.Any))
        {
            //Debug.Log("Whistle??");
            CancelInvoke("GetAttention");
            CancelInvoke("GoToBowl");
            Follow();
        }
    }

    //Fetch Functions
    public void playerBall()
    {
        //Player picks up ball
        InvokeRepeating("Follow", 0.0f, 0.01667f);
    }
    public void Fetch()
    {
        //Begins Fetch
       // CancelInvoke("Follow");
        InvokeRepeating("GetAttention", 0.0f, 0.01667f);
    }

    public void GetAttention()
     {
            //Dog Goes to ball
            if (Vector3.Distance(transform.position, ball.transform.position) >= 2)
            {
                speed = 3f;
                anim.SetBool("isMoving", true);
                transform.position = Vector3.MoveTowards(transform.position, ball.transform.position, speed * Time.deltaTime);
                var rotation = Quaternion.LookRotation(ball.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
            }
            else
            {

                speed = 0f;
                anim.SetBool("isMoving", false);
                PickUpBall();

            }
     }

    public void PickUpBall()
    {
        //Dog picks up ball
        CancelInvoke("GetAttention");
        Debug.Log("PickUp");
        ball.transform.SetParent(dog);
        //ball.transform.localPosition = Vector3.zero;
        InvokeRepeating("Follow", 0.0f, 0.01667f);

    }
    public void PutDownBall()
    {
        //Dog puts down ball
        Debug.Log("PutDown");
        ball.transform.SetParent(null);
        CancelInvoke("Follow");
    }

    //Used for Fetch and Whistle
    public void Follow()
    {
        //Dog Goes to person
        Debug.Log("Follow");

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
            PutDownBall();

        }

    }

    //Petting functions

    public void PetDog()
    {
        //Person pets dog
        if(anim.GetBool("isMoving") == false && anim.GetBool("isEat") == false)
        {
            anim.SetBool("isPet", true);
        }


    }
    public void StopPetDog()
    {
        //Person stops petting dog
        anim.SetBool("isPet", false);
    }


    //Food bowl functions
    public void EatBowl()
    {
        //Begins the dog eating fod
        InvokeRepeating("GoToBowl", 0.0f, 0.01667f);
    }
    public void GoToBowl()
    {
        //Dog goes to bowl
        if (Vector3.Distance(transform.position, bowl.transform.position) >= 1)
        {
            speed = 2f;
            anim.SetBool("isMoving", true);
            transform.position = Vector3.MoveTowards(transform.position, bowl.transform.position, speed * Time.deltaTime);
            var rotation = Quaternion.LookRotation(bowl.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
        }
        else
        {
            speed = 0f;
            anim.SetBool("isMoving", false);
            //Dog eats food
            anim.SetBool("isEat", true);
            timerOn = true;
            InvokeRepeating("myTimer", 0.0f, 0.01667f);
        }
    }

    public void EndEat()
    {
        //Dog finishes food
        CancelInvoke("myTimer");
        anim.SetBool("isEat", false);
        Bowl.inst.food.SetActive(false);
        timer = 5f;
    }

    //Timer
    void myTimer()
    {
        //Timer to give a little time for animation
        Debug.Log("timer");
        CancelInvoke("GoToBowl");
        if (timerOn == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0) //0 minutes terminates
            {

                EndEat();
               // timerOn = false;
            }
            
        }

    }
}
