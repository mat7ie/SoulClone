using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerEffectManager : CharacterEffectManager
    {
        [SerializeField] InstantCharacterEffect effectToTest;
        [SerializeField] bool processEffect = false;

        private void Update()
        {
            if(processEffect)
            {
                processEffect = false;
                InstantCharacterEffect effect = Instantiate(effectToTest);
                ProcessInstantEffect(effect);
            }
        }
    }
}
