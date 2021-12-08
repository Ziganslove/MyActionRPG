using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public Sprite[] items;

    private bool active = false;
    private int[] itemsCount = { 0, 0, 0};
    private GameObject panel;
    private Image[] images;
    private Button[] buttons;
    private GameObject[] itemsCountUI;
    private float lastCall;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        Sort();

        player = GameObject.FindObjectOfType<Player>();
        panel = GameObject.Find("Panel");
        itemsCountUI = GameObject.FindGameObjectsWithTag("ItemsCount");
        foreach (GameObject item in itemsCountUI)
        {
            item.SetActive(false);
        }

        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryFilling();
        }
        if (panel.activeSelf)
        {
            buttons[0].onClick.AddListener(() => { OnClick(1); });
            buttons[1].onClick.AddListener(() => { OnClick(2); });
            buttons[2].onClick.AddListener(() => { OnClick(3); });
        }
    }

    void Sort()
    {
        Sprite minSprite;
        int indexMin = 1;
        int position = 1;
        bool end;
        do
        {
            end = true;
            minSprite = items[position];
            for (int i = position + 1; i < items.Length; i++)
            {
                if (minSprite.name[0] > items[i].name[0])
                {
                    
                    minSprite = items[i];
                    end = false;
                    indexMin = i;
                }
            }
            if (end)
            {
                break;
            }
            items[indexMin] = items[position];
            items[position] = minSprite;
            position++;
        } while (!end);
    }

    void InventoryFilling()
    {
        panel.SetActive(!active);
        active = !active;
        if (active)
        {
            images = GetComponentsInChildren<Image>();
            buttons = GetComponentsInChildren<Button>();
            int j = 1;
            for (int i = 1; i < 4; i++)
            {
                while (j < 4)
                {
                    if (items[j].name == "Meat" && itemsCount[0] != 0)
                    {
                        images[i].sprite = items[j];
                        itemsCountUI[i - 1].SetActive(true);
                        itemsCountUI[i - 1].GetComponent<Text>().text = itemsCount[0].ToString();
                        j++;
                        break;
                    }
                    else if (items[j].name == "First Aid" && itemsCount[1] != 0)
                    {
                        images[i].sprite = items[j];
                        itemsCountUI[i - 1].SetActive(true);
                        itemsCountUI[i - 1].GetComponent<Text>().text = itemsCount[1].ToString();
                        j++;
                        break;
                    }
                    else if (items[j].name == "Gem" && itemsCount[2] != 0)
                    {
                        images[i].sprite = items[j];
                        itemsCountUI[i - 1].SetActive(true);
                        itemsCountUI[i - 1].GetComponent<Text>().text = itemsCount[2].ToString();
                        j++;
                        break;
                    }
                    j++;
                }
            }
        }
    }

    void OnClick(int id)
    {
        if (Time.time - lastCall > 0.1f)
        {
            if (images[id].sprite.name == "Meat")
            {
                player.GetDamage(-25);
                itemsCount[0]--;
                itemsCountUI[id - 1].GetComponent<Text>().text = itemsCount[0].ToString();
                if (itemsCount[0] <= 0)
                {
                    images[id].sprite = items[0];
                    itemsCountUI[id - 1].SetActive(false);
                }
            }
            else if (images[id].sprite.name == "First Aid")
            {
                player.GetDamage(-100);
                itemsCount[1]--;
                itemsCountUI[id - 1].GetComponent<Text>().text = itemsCount[1].ToString();
                if (itemsCount[1] <= 0)
                {
                    images[id].sprite = items[0];
                    itemsCountUI[id - 1].SetActive(false);
                }
            }
            else if (images[id].sprite.name == "Gem")
            {
                player.LevelUp();
                ScoreManager.playerLvl++;
                itemsCount[2]--;
                itemsCountUI[id - 1].GetComponent<Text>().text = itemsCount[2].ToString();
                if (itemsCount[2] <= 0)
                {
                    images[id].sprite = items[0];
                    itemsCountUI[id - 1].SetActive(false);
                }
            }
            lastCall = Time.time;
        }
    }

    public void GiveItem(int itemID)
    {
        if (itemID == 1)
        {
            itemsCount[0]++;
        }
        else if (itemID == 2)
        {
            itemsCount[1]++;
        }
        else if (itemID == 3)
        {
            itemsCount[2]++;
        }
    }
}