using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private Player player;
    private bool giveWood = false;
    [SerializeField] private float rate = 3f;
    private float woodRate = 0;
    public GameObject logs;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();


    }

    // Update is called once per frame
    void Update()
    {
        if (giveWood)
        {
            //shake it
            WoodGenerator();
        }
    }

    void WoodGenerator()
    {
        woodRate += Time.deltaTime;

        if (woodRate >= rate)
        {
            Debug.Log("Wood!!");
            GameObject logsClone = Instantiate(logs, transform.position + new Vector3 (Random.Range(-0.3f,0.3f), Random.Range(.2f, 0.4f), Random.Range(-0.3f, 0.3f)), transform.rotation);
            logsClone.GetComponent<Rigidbody>().AddForce(Vector3.up * 50 + Vector3.right * 40 * (Random.Range(0, 2) * 2 - 1));
            woodRate = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<Player>().movementDir == Vector3.zero)
        {
            player.isChop = true;
            giveWood = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<Player>().movementDir == Vector3.zero)
        {
            player.isChop = true;
            giveWood = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player.isChop = false;
            giveWood = false;
        }
    }
}
