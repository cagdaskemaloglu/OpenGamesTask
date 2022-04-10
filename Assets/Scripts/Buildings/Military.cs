using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Military : MonoBehaviour
{
    private Stack stack;
    private Player player;

    public GameObject archerPrefab;

    [SerializeField] private int wood =3;
    [SerializeField] private int stone = 0;
    [SerializeField] private int gold = 2;

    public int needWood;
    public int needStone;
    public int needGold;

    public int generatedSoldier;
    private Vector3 offset = new Vector3(1, 0, 0);

    [SerializeField] private int buildingType = 1; // for archery or pikeman
    private GameManager gameManager;

    public Text woodText, stoneText, goldText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        stack = GameObject.Find("Player").transform.GetChild(3).gameObject.GetComponent<Stack>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        needWood = wood;
        needGold = gold;
        needStone = stone;
    }

    // Update is called once per frame
    void Update()
    {
        if(needWood==0 && needStone==0 && needGold==0)
        {
            GenerateSoldier();
        }

        woodText.text = wood.ToString() + "/" + (wood -  needWood).ToString();
        stoneText.text = stone.ToString() + "/" + (stone-needStone).ToString();
        goldText.text = gold.ToString() + "/" + (gold-needGold).ToString();
    }

    void GenerateSoldier()
    {
        
        GameObject archerClone = Instantiate(archerPrefab, transform.position + offset, transform.rotation);
        offset += new Vector3(.5f, 0, 0);
        
        

        if (buildingType == 1)
        {
            gameManager.generatedPikeman++;

            Pikeman[] pikemans = (Pikeman[])GameObject.FindObjectsOfType(typeof(Pikeman));
            foreach (Pikeman pikeman in pikemans)
            {
                pikeman.GetComponent<Pikeman>().isHappy = true;
            }
        }
        else if (buildingType == 2)
        {
            gameManager.generatedArcher++;

            Archer[] archers = (Archer[])GameObject.FindObjectsOfType(typeof(Archer));
            foreach (Archer archer in archers)
            {
                archer.GetComponent<Archer>().isHappy = true;
            }
        }
        

        needWood = wood;
        needStone = stone;
        needGold = gold;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            stack.militaryPay = true;
            player.building = this.gameObject;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            stack.militaryPay = true;
            player.building = this.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            stack.militaryPay = false;
            player.building = this.gameObject;
        }
    }
}
