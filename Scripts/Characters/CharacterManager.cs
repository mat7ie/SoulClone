using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

namespace SG{
    public class CharacterManager : NetworkBehaviour
    {
        [Header("Status")]
        public NetworkVariable<bool> isDead = new NetworkVariable<bool>(false,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
        
        [HideInInspector] public CharacterController characterController;
        [HideInInspector] public Animator animator;
        [HideInInspector] public CharacterNetworkManagement characterNetworkManagement;
        [HideInInspector] public CharacterEffectManager characterEffectManager;

        [Header("Flags")]
        public bool isPerformingAction = false;
        public bool isJumping = false;
        public bool isGrounded = true;
        public bool canRotate = true;
        public bool canMove = true;
        public bool applyRootMotion = false;
        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);

            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            characterNetworkManagement = GetComponent<CharacterNetworkManagement>();
            characterEffectManager = GetComponent<CharacterEffectManager>();
        }

        protected virtual void Update()
        {
            animator.SetBool("isGrounded", isGrounded);

            if(IsOwner)
            {
                characterNetworkManagement.networkPosition.Value = transform.position;
                characterNetworkManagement.networkRotation.Value = transform.rotation;
            }
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position,
                characterNetworkManagement.networkPosition.Value,
                ref characterNetworkManagement.networkPositionVelocity,
                characterNetworkManagement.networkPositionSmoothTime);

                transform.rotation = Quaternion.Slerp(transform.rotation,
                characterNetworkManagement.networkRotation.Value,
                characterNetworkManagement.networkRotationSmoothTime);
            }
        }

        protected virtual void FixedUpdate()
        {
            
        }

        protected virtual void LateUpdate()
        {
            
        }
    }
}