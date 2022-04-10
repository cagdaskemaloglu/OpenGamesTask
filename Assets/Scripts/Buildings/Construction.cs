using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Construction : MonoBehaviour
{
    public int wood = 15;
    public int stone = 10;
    public int gold = 5;

    public Text maxWood, maxStone, maxGold;
    public Text currentWood, currentStone, currentGold;
    private int curWood, curStone, curGold;

    public GameObject buildingPrefab;
    private bool doOnce;

    // Start is called before the first frame update
    void Start()
    {
        maxWood.text = wood.ToString() + "/";
        maxStone.text = stone.ToString() + "/";
        maxGold.text = gold.ToString() + "/";

        curWood = wood;
        curStone = stone;
        curGold = gold;
    }

    // Update is called once per frame
    void Update()
    {
        currentWood.text = (curWood - wood).ToString();
        currentStone.text = (curStone - stone).ToString();
        currentGold.text = (curGold-gold).ToString();


        Construct();
    }

    void Construct()
    {
        if (wood == 0 && stone == 0 && gold == 0 &&!doOnce)
        {
            StartCoroutine(InstantiateBuilding(3));
            Debug.Log("bina inşa ediliyor!!");
            doOnce = true;
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().building = this.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().toConstruct = true;
            other.GetComponent<Player>().building = this.gameObject;
            //Debug.Log("constructta girdi!!");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().toConstruct = false;
            //Debug.Log("constructtan çıktı");
        }
    }

    IEnumerator InstantiateBuilding(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(buildingPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.Euler(0,90,0));
        gameObject.SetActive(false);
    }
}
