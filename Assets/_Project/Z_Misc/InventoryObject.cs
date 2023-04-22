using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    [CreateAssetMenu(fileName = "New Inventory Object", menuName = "Inventory System/Inventory")]
    public class InventoryObject : ScriptableObject
    {
        public static Action OnRefreshInventoryUI;

        public int StackSize;
        [SerializeField] private AudioClip _pip;
        public Inventory Container;

        private bool _canAddItem;

        public void UpdateCapacity(int newCapacity)
        {
            Container = new Inventory(newCapacity);
        }

        public void AddItem(Item item, int amount, ItemWorldBehavior iwb)
        {
            _canAddItem = true;

            for (int i = 0; i < Container.Slots.Length; i++)
            {
                if (Container.Slots[i].ID == item.Id && Container.Slots[i].CurrentStack < StackSize)
                {
                    Container.Slots[i].AddToStack(amount);

                    if (Container.Slots[i].CurrentStack > StackSize)
                    {
                        SetFirstEmptySlot(item, Container.Slots[i].CurrentStack - StackSize);
                        Container.Slots[i].OverrideSlot(item.Id, item, StackSize);
                    }

                    _canAddItem = false;
                    AudioManager.Instance.PlayClip(_pip, false, true, 0.5f, 1f);
                    Destroy(iwb.gameObject);

                    return;
                }
            }

            SetFirstEmptySlot(item, amount);
            OnRefreshInventoryUI?.Invoke();

            if (_canAddItem)
            {
                _canAddItem = false;
                AudioManager.Instance.PlayClip(_pip, false, true, 0.5f, 1f);
                Destroy(iwb.gameObject);
            }
        }

        public void SetFirstEmptySlot(Item item, int amount)
        {
            for (int i = 0; i < Container.Slots.Length; i++)
            {
                if (Container.Slots[i].ID <= -1)
                {
                    Container.Slots[i].OverrideSlot(item.Id, item, amount);

                    if (Container.Slots[i].CurrentStack > StackSize)
                    {
                        SetFirstEmptySlot(item, Container.Slots[i].CurrentStack - StackSize);
                        Container.Slots[i].OverrideSlot(item.Id, item, StackSize);
                    }
                    OnRefreshInventoryUI?.Invoke();
                    return;
                }
            }

            _canAddItem = false;
        }

        public int GetItemAmount(int id)
        {
            int amount = 0;

            for (int i = 0; i < Container.Slots.Length; i++)
            {
                if (Container.Slots[i].ID == id)
                {
                    amount += Container.Slots[i].CurrentStack;
                }
            }

            return amount;
        }

        // its subtracting another 20 for some reason
        public void RemoveItemAmount(ItemObject item, int amount)
        {
            int amountToRemove = amount;

            for (int i = 0; i < Container.Slots.Length; i++)
            {
                if (Container.Slots[i].ID == item.Id)
                {
                    amountToRemove -= Container.Slots[i].CurrentStack;

                    if (amountToRemove > 0)
                    {
                        //Debug.Log("Amount to Remove is positive " + amountToRemove);
                        Container.Slots[i].OverrideSlot(-1, new Item(), -1);
                        RemoveItemAmount(item, amountToRemove);
                        return;
                    }
                    else if (amountToRemove == 0)
                    {
                        //Debug.Log("Amount to Remove is 0 " + item.name);
                        Container.Slots[i].OverrideSlot(-1, new Item(), -1);
                        return;
                    }
                    else if (amountToRemove < 0)
                    {
                        //Debug.Log("Amount to Remove is negative " + item.name);
                        Container.Slots[i].AddToStack(-amount);
                        return;
                    }
                }
            }
        }

        public void SwapSlots(InventorySlot item1, InventorySlot item2)
        {
            InventorySlot temp = new InventorySlot(item2.ID, item2.SlotItem, item2.CurrentStack);
            item2.OverrideSlot(item1.ID, item1.SlotItem, item1.CurrentStack);
            item1.OverrideSlot(temp.ID, temp.SlotItem, temp.CurrentStack);
            OnRefreshInventoryUI?.Invoke();
        }

        [ContextMenu("Clear")]
        public void Clear()
        {
            Container = new Inventory(30);
        }
    }

    [System.Serializable]
    public class Inventory
    {
        public InventorySlot[] Slots;

        public Inventory(int inventoryCapacity)
        {
            Slots = new InventorySlot[inventoryCapacity];

            for (int i = 0; i < inventoryCapacity; i++)
            {
                Slots[i] = new InventorySlot();
            }
        }
    }

    [System.Serializable]
    public class InventorySlot
    {
        public int ID = -1;
        public string Name;
        public int CurrentStack;
        public Item SlotItem;

        public InventorySlot()
        {
            ID = -1;
            Name = "";
            CurrentStack = 0;
            SlotItem = new Item();
        }

        public InventorySlot(int id, Item item, int amount)
        {
            ID = id;
            Name = item.ItemName;
            CurrentStack = amount;
            SlotItem = item;
        }

        public int OverrideSlot(int id, Item item, int amount)
        {
            ID = id;
            Name = item.ItemName;
            CurrentStack = amount;
            SlotItem = item;

            if (CurrentStack <= 0)
            {
                ID = -1;
                Name = "";
                CurrentStack = 0;
                SlotItem = new Item();
            }

            InventoryObject.OnRefreshInventoryUI?.Invoke();
            return CurrentStack;
        }

        public bool HasItem()
        {
            return ID > -1;
        }

        public void AddToStack(int amount)
        {
            CurrentStack += amount;

            if (CurrentStack <= 0)
            {
                ID = -1;
                Name = "";
                CurrentStack = 0;
                SlotItem = new Item();
            }

            InventoryObject.OnRefreshInventoryUI?.Invoke();
        }
    }
}
