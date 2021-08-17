using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Class responsible for drop item behaviour. Ensures proper interaction with dropped items UI window .
/// </summary>
public class Drop : MonoBehaviour
{
    public PlayerInventory inventory;
    public int droppedItemsAmount;
    public DropDisplayer dropWindow;
    public Dictionary<ItemObject, int> dropTable = new Dictionary<ItemObject, int>();
    public ItemObject[] itemsToDrop;
    public int[] dropChance;
    [HideInInspector] public List<ItemObject> itemsToBeDisplayed = new List<ItemObject>();
    [HideInInspector] public string killedMobName;
    private int pressed = 0;

    /// <summary>
    /// Fills an item-chance dictionary. Note that Unity engine cannot serialize dictionaries thus for utter flexibility one is merged from two lists
    /// that are seriaziled correctly.
    /// </summary>
    public void Start()
    {
        for (int i = 0; i < itemsToDrop.Length; i++)
        {
            dropTable.Add(itemsToDrop[i], dropChance[i]);
        }
    }
    /// <summary>
    /// Determines the amount of dropped items and creates a UI window with correct parameters.
    /// </summary>
    private void OnMouseDown()
    {
        pressed++;
        if (pressed == 1)
        {
            itemsToBeDisplayed = new List<ItemObject>();
            if (Random.Range(1, 101) <= 33)
                droppedItemsAmount = 2;
            else
                droppedItemsAmount = 1;
            for (int i = 0; i < droppedItemsAmount; i++)
            {
                var droppedItem = DetermineDroppedItem(dropTable);
                itemsToBeDisplayed.Add(droppedItem);
                //inventory.inventory.AddItem(new Item(droppedItem), 1);        Add on spot instead of going for any kind of UIs.
            }
            dropWindow.SetWindowActive(killedMobName, itemsToBeDisplayed, this);
        }
        else
        {
            dropWindow.SetWindowActive(killedMobName, itemsToBeDisplayed, this);
        }
        //Destroy(gameObject);               Destroy on spot instead of going for any kind of UIs.
    }

    /// <summary>
    /// Determines what items should be dropped according to item-chance dictionary.
    /// </summary>
    public ItemObject DetermineDroppedItem(Dictionary<ItemObject, int> _dropTable)
    {
        int randomValue = Random.Range(0, 101);
        for (int i = 0; i < _dropTable.Keys.Count; i++)
        {
            var pair = _dropTable.ElementAt(i); // Item - DropChance.
            if (randomValue <= pair.Value)
                return pair.Key;
            else
            {
                randomValue -= pair.Value;
            }
        }
        return null;
    }
    /// <summary>
    /// Updates the state of dropped items list. Called from UI component.
    /// </summary>
    /// <param name="_item">Item removed from the list.</param>
    public void UpdateDropState(ItemObject _item)
    {
        itemsToBeDisplayed.Remove(_item);
        if (itemsToBeDisplayed.Count == 0)
        {
            Destroy(gameObject);
        }
    }
}
