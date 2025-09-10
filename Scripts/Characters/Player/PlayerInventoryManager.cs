using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerInventoryManager : CharacterIventoryManager
    {
        public WeaponItem currentRightHandWeapon;
        public WeaponItem currentLeftHandWeapon;

        [Header("Quick Slots")]
        public WeaponItem[] weaponInRightHandSlots = new WeaponItem[3];
        public WeaponItem[] weaponInLeftHandSlots = new WeaponItem[3];
    }
}
