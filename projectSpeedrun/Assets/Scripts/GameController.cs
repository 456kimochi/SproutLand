using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private Tilemap farmland;
    [SerializeField] private Tilemap plantland;

    [SerializeField] private Tile hiddenDirt;
    [SerializeField] private Tile dirt;
    [SerializeField] private Tile chooseDirtIcon;
    [SerializeField] private RuleTile farmlandDirt;

    public Vector3Int prePos;
    private bool chosenDirt = false;
    private bool chosenLand = false;

    public TMPro.TMP_Text moneyText;

    public GameObject shop;
    public GameObject seedsShop;
    public GameObject seedsShopSlot;

    public GameObject itemsShop;
    public GameObject itemsShopSlot;

    public GameObject sellShop;
    public GameObject sellShopSlot;

    public CurrentItem currentItem;
    public GameObject Inventory_UI;

    public GameObject bridgeNotification;
    public GameObject oKButton;
    public FenceToNextIsland fenceToNextIsland;
    private bool isbridgeNotificationUIOpen = false;

    void Start()
    {
        foreach (var dirt in farmland.cellBounds.allPositionsWithin)
        {
            TileBase tile = farmland.GetTile(dirt);
            if (tile != null && tile.name == "Dirt") 
            {
                farmland.SetTile(dirt, hiddenDirt);
            }
            
        }
        seedsShop.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(SeedsShopClick);
        itemsShop.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(ItemsShopClick);
        sellShop.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(SellShopClick);
        oKButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OKButtonClick);
        bridgeNotification.GetComponent<RectTransform>().localScale = new Vector3(0f, 0f, 0f);
    }


    private void SeedsShopClick()
    {
        seedsShopSlot.SetActive(true);
        itemsShopSlot.SetActive(false);
        sellShopSlot.SetActive(false);
    }
    private void ItemsShopClick()
    {
        seedsShopSlot.SetActive(false);
        itemsShopSlot.SetActive(true);
        sellShopSlot.SetActive(false);
    }

    private void SellShopClick()
    {
        seedsShopSlot.SetActive(false);
        itemsShopSlot.SetActive(false);
        sellShopSlot.SetActive(true);
    }
    public bool DisplaySelectPlough(Vector3Int position)
    {
        TileBase tile = farmland.GetTile(position);
        if (tile != null && tile.name == "Dirt_invisible")
        {
                return true;
        }
        return false;
    }

    public bool RemoveSelectPlough(Vector3Int position)
    {
        TileBase tile = farmland.GetTile(position);
        if (tile != null && tile.name == "ChooseDirtIcon")
        {
            return true;
        }
        return false;
    }

    public bool DisplaySelectPlant(Vector3Int position)
    {
        TileBase tile = farmland.GetTile(position);
        TileBase tile1 = plantland.GetTile(position);
        if (tile != null && tile.name == "FarmlandRule" && tile1 == null)
        {
            return true;
        }
        return false;
    }

    public bool RemoveSelectPlant(Vector3Int position)
    {
        TileBase tile = plantland.GetTile(position);
        if (tile != null && tile.name == "ChooseDirtIcon")
        {
            return true;
        }
        return false;
    }


    private void Update()
    {
        Vector3Int pos = new Vector3Int((int)(player.transform.position.x), (int)(player.transform.position.y), 0);

        if (currentItem.slotInBag.item.type == Type.HOE)
        {
            if (pos != prePos)
            {   
                if (DisplaySelectPlough(pos))
                {
                    farmland.SetTile(pos, chooseDirtIcon);
                    chosenDirt = true;
                }
                else
                {
                    chosenDirt = false;
                }
                if (RemoveSelectPlough(prePos))
                {
                    farmland.SetTile(prePos, hiddenDirt);
                }
                prePos = pos;
            }

            if (chosenDirt && Input.GetMouseButtonDown(0) && !Inventory_UI.GetComponent<Inventory_UI>().isUIOpen)
            {
                player.GetComponent<Player>().Plough();
                player.GetComponent<Player>().UseItem(currentItem.slotInBag);
                farmland.SetTile(pos, dirt);
                farmland.SetTile(pos, farmlandDirt);
            }
        }
        else if (currentItem.slotInBag.item.type == Type.GRAPE || currentItem.slotInBag.item.type == Type.RICE)
        {
            if (pos != prePos)
            {
                if (DisplaySelectPlant(pos))
                {
                    plantland.SetTile(pos, chooseDirtIcon);         
                    chosenLand = true;
                }
                else
                {
                    chosenLand = false;
                }
                if (RemoveSelectPlant(prePos))
                {
                    plantland.SetTile(prePos, null);
                }
                prePos = pos;
            }

            if (chosenLand && Input.GetMouseButtonDown(0) && !Inventory_UI.GetComponent<Inventory_UI>().isUIOpen)
            {
                player.GetComponent<Player>().UseItem(currentItem.slotInBag);
                plantland.SetTile(pos, hiddenDirt);
                Vector3 plantPosition = pos;
                plantPosition.x += 1;
                plantPosition.y += 0.5f;
                GameObject plant = Instantiate(currentItem.currentPlant, plantPosition, Quaternion.identity);
                plant.GetComponent<PlantGrowing>().plantlandPosition = pos;
            }
        }


            moneyText.text = player.GetComponent<Player>().money.ToString();
    }

    public void Harvest(Vector3Int plantPosition)
    {
        plantland.SetTile(plantPosition, null);
    }
    public void UseBridge()
    {
        if (currentItem.slotInBag.item.type == Type.BRIDGE)
        {
            fenceToNextIsland.BuildBridge();
            player.GetComponent<Player>().UseItem(currentItem.slotInBag);
        }
        else
        {
            BridgeNotificationToggle();
        }
    }
    public void OKButtonClick()
    {
        BridgeNotificationToggle();
    }
    public void BridgeNotificationToggle()
    {
        if (!isbridgeNotificationUIOpen)
        {
            //inventoryGameObject.SetActive(true);
            bridgeNotification.transform.DOScale(1, 0.5f).OnComplete(() => isbridgeNotificationUIOpen = true);
        }
        else
        {
            bridgeNotification.transform.DOScale(0, 0.5f).OnComplete(() =>
            { //inventoryGameObject.SetActive(false); 
                isbridgeNotificationUIOpen = false;
            });
        }
    }
    
}
