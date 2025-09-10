using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SG{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager instance;
        public PlayerManager player;
        PlayerControls playerControls;

        [Header("Movement Input")]
        [SerializeField] Vector2 movementInput;
        public float horizontalInput;
        public float verticalInput;
        public float moveAmount;

        [Header("Camera Movement Input")]
        [SerializeField] Vector2 cameraInput;
        public float cameraHorizontalInput;
        public float cameraVerticalInput;

        [Header("Player Actions Input")]
        [SerializeField] bool dodgeInput = false;
        [SerializeField] bool jumpInput = false;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else{
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.activeSceneChanged += OnSceneChange;
            instance.enabled = false;
        }
        private void OnSceneChange(Scene oldScene, Scene newScene)
        {
            if(newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex())
            {
                instance.enabled = true;
            }
            else{
                instance.enabled = false;
            }
        }
        private void OnEnable()
        {
            if(playerControls == null)
            {
                playerControls = new PlayerControls();

                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                playerControls.CameraMovement.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
                playerControls.PlayerActions.Dodge.performed += i => dodgeInput = true;
                playerControls.PlayerActions.Jump.performed += i => jumpInput = true;
            }

            playerControls.Enable();
        }
        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnSceneChange;
        }
        private void OnApplicationFocus(bool focus)
        {
            if(enabled)
            {
                if(focus)
                {
                    playerControls.Enable();
                }
                else
                {
                    playerControls.Disable();
                }
            }
        }
        private void Update()
{
            HandleAllInputs();
        }
        private void HandleAllInputs()
        {
            HandlePlayerMovementInput();
            HandleCameraMovementInput();
            HandleDodgeMovement();
            HandleJump();
        }
        private void HandlePlayerMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;

            moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

            if(moveAmount <= 0.5 && moveAmount > 0)
            {
                moveAmount = 0.5f;
            }
            else if (moveAmount > 0.5 && moveAmount <= 1)
            {
                moveAmount = 1;
            }

            if(player == null)
                return;

            player.playerAnimationManager.UpdateAnimatorMovementParameters(0, moveAmount);
        }
        private void HandleCameraMovementInput()
        {
            cameraVerticalInput = cameraInput.y;
            cameraHorizontalInput = cameraInput.x;
        }
        private void HandleDodgeMovement()
        {
            if (dodgeInput)
            {
                dodgeInput = false;

                player.playerLocomotionManager.AttemptToPerformDodge();
            }
        }
        private void HandleJump()
        {
            if(jumpInput)
            {
                jumpInput = false;

                player.playerLocomotionManager.AttemptToJump();
            }
        }
    }
}
