using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    private Player player;
    private Stack stack;
    private bool disCharge;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        stack = GameObject.Find("Player").transform.GetChild(3).gameObject.GetComponent<Stack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (disCharge)
        {
            UnStack();
        }
        
    }

    void UnStack()
    {
        for (int i = 0; i < stack.pickObjects.Count; i++)
        {
            GameObject currentObj = stack.pickObjects[i];
            currentObj.transform.parent = null;
            currentObj.transform.position = transform.position + new Vector3(0, 0.2f, 0);
            Destroy(currentObj.GetComponent<Rigidbody>());
            currentObj.GetComponent<BoxCollider>().enabled = false;
            currentObj.transform.SetParent(transform);
        }
        stack.stackCount = 1;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            disCharge = true;
            //Debug.Log("constructta girdi!!");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            disCharge = false;            
        }
    }

}
