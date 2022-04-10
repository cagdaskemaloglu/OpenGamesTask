using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;
    private FixedJoystick joystick;

    //---Actions
    public bool isChop = false;
    public bool isDig = false;
    public bool isCarry = false;

    //---Movement
    public Vector3 movementDir;
    [SerializeField] private float speed = .8f;
    [SerializeField] private float rotSpeed;

    public bool toConstruct = false;
    public GameObject building;

    private Stack stack;
    public GameObject stackObj;

    public bool disCharge = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        joystick = FindObjectOfType<FixedJoystick>();
        stack = gameObject.transform.GetChild(3).gameObject.GetComponent<Stack>();
    }

    // Update is called once per frame
    void Update()
    {
        Chopping();
        Digging();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        movementDir = new Vector3(joystick.Horizontal * speed, rb.velocity.y, joystick.Vertical * speed);

        rb.velocity = movementDir;

        if (movementDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotSpeed * Time.deltaTime);
        }

        if (Mathf.Abs(rb.velocity.x) > 0.1f || Mathf.Abs( rb.velocity.z) > 0.1f)
        {
            anim.SetBool("isWalk", true);
        }
        else
        {
            anim.SetBool("isWalk", false);
        }

        if(isCarry && (Mathf.Abs(rb.velocity.x) > 0.1f || Mathf.Abs(rb.velocity.z) > 0.1f))
        {
            anim.SetBool("isCarry", true);
        }
        else
        {
            anim.SetBool("isCarry", false);
        }



        if (isCarry)
        {
            speed = .5f;
        }
        else
        {
            speed = .8f;
        }

     
    }


    void Chopping()
    {
        if (isChop)
        {
            anim.SetBool("isChop", true);
            gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            anim.SetBool("isChop", false);
            gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
    }

    void Digging()
    {
        if (isDig)
        {
            anim.SetBool("isDig", true);
            gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            anim.SetBool("isDig", false);
            gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(false);
        }
    }

    /*
    public void FindObjectwithTag(string _tag)
    {
        Transform parent = stackObj.transform;
        GetChildObject(parent, _tag);
    }

    public void GetChildObject(Transform parent, string _tag)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == _tag)
            {
                actors.Add(child.gameObject);
            }
            if (child.childCount > 0)
            {
                GetChildObject(child, _tag);
            }
        }
    }
    */
}
