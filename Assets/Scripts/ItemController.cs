using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    public enum ItemType 
    {
        penetration,
        allDestroy,
        heal,
    }

    [SerializeField]
    GameObject[] itemBase = null;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ItemSpawn(ItemType.penetration);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ItemSpawn(ItemType.allDestroy);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            ItemSpawn(ItemType.heal);
        }


    }


    public void ItemSpawn(ItemType type)
    {
        GameObject item = Instantiate(itemBase[(int)type], itemBase[(int)type].transform.position, Quaternion.identity);
        item.GetComponent<Item>().itemType = type;

    }

}
