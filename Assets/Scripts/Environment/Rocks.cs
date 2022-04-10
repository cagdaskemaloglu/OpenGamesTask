using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : MonoBehaviour
{
    private Player player;
    public bool giveRock = false;
    [SerializeField] private float rate = 2f;
    private float rockRate = 0;
    public GameObject rock;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (giveRock)
        {
            RockGenerator();
        }
    }

    void RockGenerator()
    {
        rockRate += Time.deltaTime;

        if (rockRate >= rate)
        {
            GameObject rockClone = Instantiate(rock, transform.position + new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(0.2f, 0.4f), Random.Range(-0.3f, 0.3f)), transform.rotation);
            rockClone.GetComponent<Rigidbody>().AddForce(Vector3.up * 60 + Vector3.right * 45 * (Random.Range(0, 2) * 2 - 1) );
            rockRate = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<Player>().movementDir == Vector3.zero)
        {
            player.isDig = true;
            giveRock = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<Player>().movementDir == Vector3.zero)
        {
            player.isDig = true;
            giveRock = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player.isDig = false;
            giveRock = false;
        }
    }
}
