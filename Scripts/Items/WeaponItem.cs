using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class WeaponItem : Item
    {
        [Header("Weapon Model")]
        public GameObject weaponModel;

        [Header("Weapon Base Damage")]
        public int physicalDamage = 0;

        [Header("Weapon Poise Damage")]
        public int poiseDamage = 10;
    }
}
