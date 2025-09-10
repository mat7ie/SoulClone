using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG {
    public class PlayerManager : CharacterManager
    {
        [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
        [HideInInspector] public PlayerAnimationManager playerAnimationManager;
        [HideInInspector] public PlayerNetworkManager playerNetworkManager;
        [HideInInspector] public PlayerStatManager playerStatManager;
        [HideInInspector] public PlayerInventoryManager playerInventoryManager;
        protected override void Awake()
        {
            base.Awake();

            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            playerAnimationManager = GetComponent<PlayerAnimationManager>();
            playerNetworkManager = GetComponent<PlayerNetworkManager>();
            playerStatManager = GetComponent<PlayerStatManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
        }

        protected override void Update()
        {
            base.Update();

            if(!IsOwner)
                return;

            playerLocomotionManager.HandleAllMovement();
        }

        protected override void LateUpdate()
        {
            if(!IsOwner)
                return;
            
            base.LateUpdate();

            PlayerCamera.instance.HandleAllCameraActions();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if(IsOwner)
            {
                PlayerCamera.instance.player = this;
                PlayerInputManager.instance.player = this;

                playerNetworkManager.level.OnValueChanged += playerNetworkManager.SetNewMaxHealthValue;

                // ArgumentException: Value does not fall within the expected range. (STILL FINDING SOLUTION)
                // playerNetworkManager.currentHealth.OnValueChanged += PlayerUIManager.instance.playerUIHudManager.SetNewHealthValue;
            }
        }
    }
}
