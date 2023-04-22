using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    public enum ItemType
    {
        Default,
        Resource,
        Consumable,
        Tool,
        Deployable,
        WaterDeployable
    }

    public enum ToolType
    {
        None,
        Pickaxe,
        Axe,
        Sword,
        Hammer,
        Key,
    }

    public abstract class ItemObject : ScriptableObject
    {
        public int Id;
        public string Name;
        public GameObject ItemPrefab;
        public GameObject DeployPrefab;
        public Sprite UiDisplay;
        public ItemType ItemType;
        public ToolType ToolType;
        public float CoolDown;
        public int AttackDamage;
        public int HitValue;
        public int HpValue;
        public int NrgValue;
        public int MpValue;
        public string Description = "";
    }

    [Serializable]
    public class Item
    {
        public string ItemName;
        public int Id;
        public GameObject ItemPrefab;
        public GameObject DeployPrefab;
        public Sprite UiDisplay;
        public ItemType ItemType;
        public ToolType ToolType;
        public float CoolDown;
        public int AttackDamage;
        public int HitValue;
        public int HpValue;
        public int NrgValue;
        public int MpValue;
        public string Description = "";

        public Item()
        {
            ItemName = "";
            Id = -1;
            ItemPrefab = null;
            DeployPrefab = null;
            UiDisplay = null;
            ItemType = ItemType.Default;
            ToolType = ToolType.None;
            CoolDown = 0f;
            AttackDamage = 0;
            HitValue = 0;
            HpValue = 0;
            NrgValue = 0;
            MpValue = 0;
            Description = "";
        }

        public Item(ItemObject item)
        {
            ItemName = item.Name;
            Id = item.Id;
            ItemPrefab = item.ItemPrefab;
            DeployPrefab = item.DeployPrefab;
            UiDisplay = item.UiDisplay;
            ItemType = item.ItemType;
            ToolType = item.ToolType;
            AttackDamage = item.AttackDamage;
            HitValue = item.HitValue;
            CoolDown = item.CoolDown;
            HpValue = item.HpValue;
            NrgValue = item.NrgValue;
            MpValue = item.MpValue;
            Description = item.Description;
        }
    }
}
