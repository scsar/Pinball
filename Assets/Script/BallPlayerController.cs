using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class BallPlayerController : MonoBehaviour
{
    [SerializeField] private int FPower = 15;
    [SerializeField] private float waitTime = 1.0f;
    [SerializeField] private float fallingspeed = 0.05f;


    private IItem item;
    private GameObject curitem;
    private float timer;
    private float itemTimer;
    private float h;
    private float v;
    private Vector3 dir = Vector3.zero;

    public bool forceset
    {
        get 
        {
            return forcecheck;
        }
        set
        {
            forcecheck = value;
            if (!forcecheck)
                timer = 0f;
        }
    }
    private bool forcecheck = false;

    private bool itemcheck = false;

    private AudioSource audio;

    private Rigidbody playerRigidbody;
    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        timer = 0f;
        itemTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, playerRigidbody.velocity.y, playerRigidbody.velocity.z - fallingspeed);

        if (forcecheck)
        {
            timer += Time.deltaTime;
            if (timer > waitTime)
            {
                timer = 0f;
                forcecheck = false;
            }
        } 

        if (itemcheck)
        {
            itemTimer += Time.deltaTime;
            if (itemTimer >= 10f)
            {
                FPower -= 5;
                itemTimer = 0f;
                itemcheck = false;
            }
        }

        if (dir != Vector3.zero && !forcecheck)
        {
            Force();
        }

        if (Input.GetButtonDown("UseItem") && curitem != null)
        {
            curitem.GetComponent<IItem>().use(gameObject);
            curitem = null;
        }
    }

    void FixedUpdate()
    {
        InputKey();
    }

    void Force()
    {
        forcecheck = true;   
        Vector3 force = Vector3.zero; 

        if (dir.x < 0 || dir.z < 0) 
            FPower = -FPower;


        if (dir.x != 0)
            force.x = FPower;
        else
        {
            //force.y = FPower;
            force.z = FPower;
        }
            
        playerRigidbody.AddForce(force, ForceMode.VelocityChange); 

        
        if (FPower < 0) 
            FPower = -FPower;
    }

    void InputKey()
    {
        h = Input.GetAxis("Horizontal"); //left and right
        v = Input.GetAxis("Vertical");   //up and down
        dir.x = h;
        dir.z = v;
        dir.Normalize();
    }

    void OnTriggerEnter(Collider other)
    {
        item = null;
        item = other.GetComponent<IItem>();
        other.GetComponent<AudioSource>().Play();
        if (item != null)
        {
            StartCoroutine(ItemDisAble(other.gameObject));
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (GameManager.Instance.isPlayedSet)
        {
            audio.Play();
        }
        if (collision.gameObject.CompareTag("Score1"))
        {
            GameManager.Instance.scoreSet = 50;
        }
        else if (collision.gameObject.CompareTag("Score2"))
        {
            GameManager.Instance.scoreSet = 100;
        }
    }

    IEnumerator ItemDisAble(GameObject other)
    {
        GameObject tempitem = item.Guse(gameObject);
        yield return new WaitForSeconds(0.1f);
        other.gameObject.SetActive(false);
        curitem = tempitem;
    }
    
}
