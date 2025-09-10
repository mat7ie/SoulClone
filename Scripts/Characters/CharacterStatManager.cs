using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterStatManager : MonoBehaviour
    {
        CharacterManager character;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {
            
        }

        public int CalculateHealthBasedOnLevel(int level)
        {
            float health = 0;

            health = level * 15;

            return Mathf.RoundToInt(health);
        }
    }
}
