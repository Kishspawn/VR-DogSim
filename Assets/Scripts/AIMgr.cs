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
    private float timer = 15f;
    private bool timerOn = false; 
    Animator anim;

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
            Follow();
         }
    }
    public void GetAttention()
    {
        //Debug.Log(Vector3.Distance(transform.position, ball.transform.position));
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
    public void Fetch()
    {
        //Debug.Log("GetAttention");
        InvokeRepeating("GetAttention", 0.0f, 0.01667f);
    }
    public void PickUpBall()
    {
        CancelInvoke("GetAttention");
        Debug.Log("PickUp");
        ball.transform.SetParent(dog);
        //ball.transform.localPosition = Vector3.zero;
        InvokeRepeating("Follow", 0.0f, 0.01667f);
        
    }
    public void PutDownBall()
    {
        Debug.Log("PutDown");
        ball.transform.SetParent(null);
        CancelInvoke("Follow");
    }
    public void Follow()
    {
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
    public void PetDog()
    {
        anim.SetBool("isPet", true);
    }
    public void StopPetDog()
    {
        anim.SetBool("isPet", false);
    }
    //Timer
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
