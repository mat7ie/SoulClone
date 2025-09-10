using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class WeaponItemAction : ScriptableObject
    {
        public int actionID;

        public virtual void AttempToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {

        }
    }
}
