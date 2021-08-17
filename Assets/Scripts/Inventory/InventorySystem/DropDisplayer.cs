using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

/// <summary>
/// Class responsible for behaviour of drop UI window and its user interaction.
/// </summary>
public class DropDisplayer : MonoBehaviour
{
    private Image window;
    private PlayerInventory inventory;
    public GameObject dropPrefab;
    public Image closeButton;
    public Image obtainAllButton;
    public Image cancelButton;
    public TextMeshProUGUI killedMonsterName;
    private List<ItemObject> droppedItems = new List<ItemObject>();

    private void Awake()
    {
        window = GetComponent<Image>();
        inventory = FindObjectOfType<PlayerInventory>();
    }
    private void Start()
    {
        SetWindowInactive();
    }
    Drop drop = null;
    /// <summary>
    /// Creates a UI window with 1 or 2 clickable images, adjusts images' positions accordingly and ensures its functionality depending on player's actions.
    /// E.g. closes the window when there's no dropped items left, removes item's image upon mouse press and adds the item to player's inventory.
    /// </summary>
    /// <param name="_killedMobName">Monster's name to be displayed on top of the window.</param>
    /// <param name="_itemsToBeDisplayed">Items to wrap with drop prefab and display correctly.</param>
    /// <param name="_drop">Instance of the drop item.</param>
    public void SetWindowActive(string _killedMobName, List<ItemObject> _itemsToBeDisplayed, Drop _drop)
    {
        drop = _drop;
        droppedItems = new List<ItemObject>();
        int iterations = 0;
        for (int i = 0; i < _itemsToBeDisplayed.Count; i++)
        {
            //var drop = Instantiate(dropPrefab, new Vector3(transform.position.x, transform.position.y - iterations * 70 + 45), Quaternion.identity);
            var drop = Instantiate(dropPrefab, new Vector3(transform.position.x, transform.position.y - iterations * 40 + 20), Quaternion.identity);
            iterations++;
            drop.transform.SetParent(gameObject.transform);
            drop.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f);
            var item = _itemsToBeDisplayed[i];
            droppedItems.Add(item);
            AddEvent(drop, EventTriggerType.PointerDown, delegate { OnClick(drop); });
            void OnClick(GameObject obj)
            {
                _drop.inventory.inventory.AddItem(new Item(item), 1);
                _drop.UpdateDropState(item);
                Destroy(drop);
                if (_drop.itemsToBeDisplayed.Count == 0)
                {
                    SetWindowInactive();
                }
            }
            drop.GetComponent<DropPrefab>().SetDroppedItemUI(item.uiSprite, item.name);
        }
        window.enabled = true;
        cancelButton.gameObject.SetActive(true);
        obtainAllButton.gameObject.SetActive(true);
        killedMonsterName.enabled = true;
        closeButton.enabled = true;
        killedMonsterName.text = _killedMobName;
    }
    /// <summary>
    /// Collects all dropped items and closes the window. Called from the button. 
    /// </summary>
    public void ObtainAll()
    {
        foreach (var item in droppedItems)
        {
            inventory.inventory.AddItem(new Item(item), 1);
        }
        SetWindowInactive();
        Destroy(drop.gameObject);
    }
    /// <summary>
    /// Ensures correct functionality both in displayed and hidden states.
    /// </summary>
    public void SetWindowInactive()
    {
        window.enabled = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == dropPrefab.name + "(Clone)")
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        cancelButton.gameObject.SetActive(false);
        obtainAllButton.gameObject.SetActive(false);
        closeButton.enabled = false;
        killedMonsterName.enabled = false;
    }
    /// <summary>
    /// Method used to generate a callback. 
    /// </summary>
    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        EventTrigger.Entry eventTrigger = new EventTrigger.Entry
        {
            eventID = type
        };
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
}
