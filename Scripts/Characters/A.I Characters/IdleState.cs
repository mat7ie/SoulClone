using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(menuName = "A.I/States/Idle", order = 3)]
    public class IdleState : AIState
    {
        public override AIState Tick(AICharacterManager aICharacter)
        {
            // if(aICharacter.characterCombatManager.currentTarget != null)
            // {
            //     Debug.Log("WE HAVE A TARGET");
            // }
            // else
            // {
            //     Debug.Log("WE HAVE NO TARGET");
            // }

            return this;
        }
    }
}
