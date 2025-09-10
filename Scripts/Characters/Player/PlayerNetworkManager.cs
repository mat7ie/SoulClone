using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

namespace SG{
    public class PlayerNetworkManager : CharacterNetworkManagement
    {
        PlayerManager player;

        public NetworkVariable<FixedString64Bytes> characterName = new NetworkVariable<FixedString64Bytes>("Character", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        protected void Awake()
        {
            player = GetComponent<PlayerManager>();
        }

        public void SetNewMaxHealthValue(int oldLevel, int newLevel)
        {
            maxHealth.Value = player.playerStatManager.CalculateHealthBasedOnLevel(newLevel);
            PlayerUIManager.instance.playerUIHudManager.SetMaxHealthValue(maxHealth.Value);
            currentHealth.Value = maxHealth.Value;
        }
    }
}
