using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodController : MonoBehaviour
{
    public int itemId = 0;

    private Text text;
    private PlayerInventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.FindGameObjectWithTag("PressButton").GetComponent<Text>();
        inventory = GameObject.FindObjectOfType<PlayerInventory>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Player>()) {
            text.enabled = true;
            if (Input.GetKeyDown(KeyCode.F))
            {
                inventory.GiveItem(itemId);
                text.enabled = false;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            text.enabled = false;
        }
    }
}
