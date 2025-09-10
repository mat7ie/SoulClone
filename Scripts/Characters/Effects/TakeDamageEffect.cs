using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(menuName = "Character Effects/Take Damage", order = 2)]
    public class TakeDamageEffect : InstantCharacterEffect
    {
        [Header("Character Causing Dmg")]
        public CharacterManager characterCausingDmg;

        [Header("Damage")]
        public float physicalDmg = 0;

        [Header("Final Damage")]
        private int finalDmg = 0;

        [Header("Animation")]
        public bool playDmgAnimation = true;
        public bool manuallySelectDmgAnimation = false;
        public string damageAnimation;

        [Header("Direction Damage Taken From")]
        public float angleHitFrom;
        public Vector3 contactPoint;

        public override void ProcessEffect(CharacterManager character)
        {
            base.ProcessEffect(character);

            // NẾU NHÂN VẬT CHẾT KHÔNG NHẬN THÊM SÁT THƯƠNG
            if(character.isDead.Value)
            {
                return;
            }
            
            CalculateDmg(character);
        }

        private void CalculateDmg(CharacterManager character)
        {
            if(!character.IsOwner)
                return;

            if(characterCausingDmg != null)
            {

            }
            
            finalDmg = Mathf.RoundToInt(physicalDmg);

            if(finalDmg <= 0)
            {
                finalDmg = 1;
            }

            character.characterNetworkManagement.currentHealth.Value -= finalDmg;
        }
    }
}
