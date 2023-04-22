using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    [CreateAssetMenu(fileName = "New Item Object", menuName = "Inventory System/Items/Create Item")]
    public class ItemCreate : ItemObject
    {
        private void Awake()
        {
            if (CoolDown == 0)
            {
                CoolDown = 0.8f;
            }
        }
    }
}
