using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerStatManager : CharacterStatManager
    {
        PlayerManager player;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }

        protected override void Start()
        {
            base.Start();

            CalculateHealthBasedOnLevel(player.playerNetworkManager.level.Value);
        }
    }
}
