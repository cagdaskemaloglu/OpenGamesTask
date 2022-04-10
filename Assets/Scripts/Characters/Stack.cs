using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [SerializeField] private GameObject currentBox;
    [SerializeField] private Transform parent;
    [SerializeField] public int stackCount = 1;
    private Vector3 newPos;

    private Player player;

    public int wood = 0;
    public int stone = 0;
    public int gold = 0;

    public List<GameObject> pickObjects = new List<GameObject>();

    public bool militaryPay;

    // Start is called before the first frame update
    void Start()
    {
        currentBox = this.gameObject;
        parent = currentBox.transform;
        player = gameObject.transform.parent.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        if (stackCount == 1)
        {
            currentBox = this.gameObject;
        }

        if (stackCount > 1)
        {
            player.isCarry = true;
        }
        else
        {
            player.isCarry = false;
        }

        UnStack(); 
        MilitaryPay();
    }
    void StackObject(GameObject stackObj)
    {
        stackObj.GetComponent<BoxCollider>().enabled = false;
        stackObj.transform.parent = parent;
        newPos = currentBox.transform.localPosition;
        newPos.y += .3f;
        stackObj.transform.localPosition = newPos;
        stackObj.transform.localRotation = Quaternion.Euler(90, 0, 0);
        //newBox.name = "Box (" + stackCount + ")";
        pickObjects.Add(stackObj);
        stackCount++;
        currentBox = stackObj;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wood")
        {
            StackObject(other.gameObject);
            other.GetComponent<Rigidbody>().isKinematic = true;
            wood++;
        }
        if (other.tag == "Stone")
        {
            StackObject(other.gameObject);
            other.GetComponent<Rigidbody>().isKinematic = true;
            stone++;
        }
        if (other.tag == "Gold")
        {
            StackObject(other.gameObject);
            other.GetComponent<Rigidbody>().isKinematic = true;
            gold++;
        }
    }

    void UnStack()
    {
        if (player.toConstruct && pickObjects.Count > 0)
        {
            int activeIndex = pickObjects.Count -1;
            GameObject currentObj = pickObjects[activeIndex];

            if (currentObj.CompareTag("Wood") && player.building.GetComponent<Construction>().wood > 0)
            {
                currentObj.transform.parent = null;
                currentObj.tag = "Construct";
                currentObj.transform.position = player.building.transform.position + new Vector3(0, 0.2f, 0);
                currentObj.GetComponent<Rigidbody>().isKinematic = false;
                currentObj.GetComponent<BoxCollider>().enabled = true;
                currentObj.transform.SetParent(player.building.transform);

                pickObjects.Remove(currentObj);
                wood--;
                player.building.GetComponent<Construction>().wood--;
                stackCount--;
            }
            else if(currentObj.CompareTag("Wood") && player.building.GetComponent<Construction>().wood == 0)
            {
                //currentObj.tag = "Waste";
                activeIndex -= 1;
                
                return;
            }

            if(currentObj.CompareTag("Stone")&& player.building.GetComponent<Construction>().stone > 0)
            {
                currentObj.tag = "Construct";
                currentObj.transform.parent = null;
                currentObj.transform.position = player.building.transform.position + new Vector3(0, 0.2f, 0);
                currentObj.GetComponent<Rigidbody>().isKinematic = false;
                currentObj.GetComponent<BoxCollider>().enabled = true;
                currentObj.transform.SetParent(player.building.transform);

                pickObjects.Remove(currentObj);
                stone--;
                player.building.GetComponent<Construction>().stone--;
                stackCount--;
            }
            else if(currentObj.CompareTag("Stone")&& player.building.GetComponent<Construction>().stone == 0)
            {
                //currentObj.tag = "Waste";
                activeIndex -= 1;
                return;
            }

            if (currentObj.CompareTag("Gold") && player.building.GetComponent<Construction>().gold > 0)
            {
                currentObj.tag = "Construct";
                currentObj.transform.parent = null;
                currentObj.transform.position = player.building.transform.position + new Vector3(0, 0.2f, 0);
                currentObj.GetComponent<Rigidbody>().isKinematic = false;
                currentObj.GetComponent<BoxCollider>().enabled = true;
                currentObj.transform.SetParent(player.building.transform);

                pickObjects.Remove(currentObj);
                gold--;
                player.building.GetComponent<Construction>().gold--;
                stackCount--;
            }
            else if (currentObj.CompareTag("Gold") && player.building.GetComponent<Construction>().gold == 0)
            {
                //currentObj.tag = "Waste";
                activeIndex -= 1;
                return;
            }

            

            if (pickObjects.Count - 1 < 1)
            {
                currentBox = this.gameObject;
            }
            else
            {
                currentBox = pickObjects[pickObjects.Count - 1];
            }


        }
    }

    void MilitaryPay()
    {
        if (militaryPay)
        {
            int activeIndex = pickObjects.Count - 1;
            GameObject currentObj = pickObjects[activeIndex];

            if (currentObj.CompareTag("Wood") && player.building.GetComponent<Military>().needWood > 0)
            {
                currentObj.transform.parent = null;
                currentObj.tag = "Construct";

                currentObj.transform.position = player.building.transform.position + new Vector3(0, 0.2f, 0);
                //currentObj.GetComponent<Rigidbody>().isKinematic = false;
                currentObj.transform.SetParent(player.building.transform);

                pickObjects.Remove(currentObj);
                wood--;
                player.building.GetComponent<Military>().needWood--;
                stackCount--;
            }
            else if (currentObj.CompareTag("Wood") && player.building.GetComponent<Military>().needWood == 0)
            {
                //currentObj.tag = "Waste";
                activeIndex -= 1;

                return;
            }

            if (currentObj.CompareTag("Stone") && player.building.GetComponent<Military>().needStone > 0)
            {
                currentObj.tag = "Construct";

                currentObj.transform.parent = null;

                currentObj.transform.position = player.building.transform.position + new Vector3(0, 0.2f, 0);
                //currentObj.GetComponent<Rigidbody>().isKinematic = false;
                currentObj.transform.SetParent(player.building.transform);


                pickObjects.Remove(currentObj);
                stone--;
                player.building.GetComponent<Military>().needStone--;
                stackCount--;
            }
            else if (currentObj.CompareTag("Stone") && player.building.GetComponent<Military>().needStone == 0)
            {
                //currentObj.tag = "Waste";
                activeIndex -= 1;
                return;
            }

            if (currentObj.CompareTag("Gold") && player.building.GetComponent<Military>().needGold > 0)
            {
                currentObj.tag = "Construct";

                currentObj.transform.parent = null;

                currentObj.transform.position = player.building.transform.position + new Vector3(0, 0.2f, 0);
                //currentObj.GetComponent<Rigidbody>().isKinematic = false;
                currentObj.transform.SetParent(player.building.transform);


                pickObjects.Remove(currentObj);
                gold--;
                player.building.GetComponent<Military>().needGold--;
                stackCount--;
            }
            else if (currentObj.CompareTag("Gold") && player.building.GetComponent<Military>().needGold == 0)
            {
                //currentObj.tag = "Waste";
                activeIndex -= 1;
                return;
            }

            
            if(pickObjects.Count -1 < 1)
            {
                currentBox = this.gameObject;
            }
            else
            {
                currentBox = pickObjects[pickObjects.Count - 1];
            }

        }
    }

}
