using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        PlayerManager player;
        [HideInInspector] public float verticalMovement;
        [HideInInspector] public float horizontalMovement;
        [HideInInspector] public float moveAmount;

        [Header("Movement Settings")]
        private Vector3 moveDirection;
        private Vector3 targetRotationDirection;
        [SerializeField] float rotationSpeed = 15;
        [SerializeField] float walkingSpeed = 2;
        [SerializeField] float runningSpeed = 5;

        [Header("Dodge")]
        private Vector3 rollDirection;

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }

        protected override void Update()
        {
            base.Update();

            if(player.IsOwner)
            {
                player.characterNetworkManagement.verticalMovement.Value = verticalMovement;
                player.characterNetworkManagement.horizontalMovement.Value = horizontalMovement;
                player.characterNetworkManagement.moveAmount.Value = moveAmount;
            }
            else
            {
                verticalMovement = player.characterNetworkManagement.verticalMovement.Value;
                horizontalMovement = player.characterNetworkManagement.horizontalMovement.Value;
                moveAmount = player.characterNetworkManagement.moveAmount.Value;

                player.playerAnimationManager.UpdateAnimatorMovementParameters(0, moveAmount);
            }
        }

        public void HandleAllMovement()
        {
            HandleGroundMovement();
            HandleRotation();
        }

        private void GetMovementValues()
        {
            verticalMovement = PlayerInputManager.instance.verticalInput;
            horizontalMovement = PlayerInputManager.instance.horizontalInput;
            moveAmount = PlayerInputManager.instance.moveAmount;
        }

        private void HandleGroundMovement()
        {
            if(!player.canMove)
            {
                if(PlayerInputManager.instance.moveAmount > 0.5f)
                {
                    player.characterController.Move(moveDirection * runningSpeed * 0.75f * Time.deltaTime);
                }
                else if(PlayerInputManager.instance.moveAmount <= 0.5f)
                {
                    player.characterController.Move(moveDirection * walkingSpeed * 0.75f * Time.deltaTime);
                }

                return;
            }
            GetMovementValues();
            moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
            moveDirection = moveDirection + PlayerCamera.instance.transform.right * horizontalMovement;
            moveDirection.Normalize();
            moveDirection.y = 0;

            if(PlayerInputManager.instance.moveAmount > 0.5f)
            {
                player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
            }
            else if(PlayerInputManager.instance.moveAmount <= 0.5f)
            {
                player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
            }
        }

        private void HandleRotation()
        {
            if(!player.canRotate)
                return;
            targetRotationDirection = Vector3.zero;
            targetRotationDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
            targetRotationDirection = targetRotationDirection + PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
            targetRotationDirection.Normalize();
            targetRotationDirection.y = 0;
            
            if(targetRotationDirection == Vector3.zero)
            {
                targetRotationDirection = transform.forward;
            }

            Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }

        public void AttemptToPerformDodge()
        {
            if(player.isPerformingAction)
                return;
            
            if(PlayerInputManager.instance.moveAmount > 0)
            {
                rollDirection = PlayerCamera.instance.cameraObject.transform.forward * PlayerInputManager.instance.verticalInput;
                rollDirection += PlayerCamera.instance.cameraObject.transform.right * PlayerInputManager.instance.horizontalInput;
                rollDirection.y = 0;
                rollDirection.Normalize();

                Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
                player.transform.rotation = playerRotation;

                player.playerAnimationManager.PlayTargetActionAnimation("Roll_forward_01",true,true);
            }
        }

        public void AttemptToJump()
        {
            if(player.isPerformingAction)
                return;

            if(player.isJumping)
                return;
                
            if(player.isGrounded)
                return;
            
            player.playerAnimationManager.PlayTargetActionAnimation("Jump", false);
            player.isJumping = true;
        }
        }
}
