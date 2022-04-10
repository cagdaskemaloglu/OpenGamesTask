using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikeman : MonoBehaviour
{
    public bool isHappy;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHappy)
        {
            anim.SetBool("isHappy", true);
            isHappy = false;
        }
        else
        {
            anim.SetBool("isHappy", false);
        }
    }
}
