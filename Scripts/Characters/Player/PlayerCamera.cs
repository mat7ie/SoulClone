using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera instance;
        public PlayerManager player;
        public Camera cameraObject;
        [SerializeField] Transform cameraPivotTransform;

        [Header("Camera Setting")]
        private float cameraSmoothSpeed = 1;
        [SerializeField] float LeftAndRightRotationSpeed = 75;
        [SerializeField] float UpAndDownRotationSpeed = 75;
        [SerializeField] float minimumPivot = -40;
        [SerializeField] float maximumPivot = 60;
        [SerializeField] float cameraCollisionRadius = 0.2f;
        [SerializeField] LayerMask collideWithLayer;

        [Header("Camera Values")]
        private Vector3 cameraVelocity;
        private Vector3 cameraObjectPosition;
        [SerializeField] float LeftAndRightLookAngle;
        [SerializeField] float UpAndDownLookAngle;
        private float CameraPosition;
        private float targetCameraZPosition;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            CameraPosition = cameraObject.transform.localPosition.z;
        }

        public void HandleAllCameraActions()
        {
            if(player != null)
            {
                HandleFollowTarget();
                HandleRotation();
                HandleCollisions();
            }
        }

        private void HandleFollowTarget()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
            transform.position = targetCameraPosition;
        }

        private void HandleRotation()
        {
            LeftAndRightLookAngle += (PlayerInputManager.instance.cameraHorizontalInput * LeftAndRightRotationSpeed) * Time.deltaTime;
            UpAndDownLookAngle -= (PlayerInputManager.instance.cameraVerticalInput * UpAndDownRotationSpeed) * Time.deltaTime;
            UpAndDownLookAngle = Mathf.Clamp(UpAndDownLookAngle,minimumPivot,maximumPivot);

            Vector3 cameraRotation = Vector3.zero;
            Quaternion targetRotation;

            cameraRotation.y = LeftAndRightLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            transform.rotation = targetRotation;

            cameraRotation = Vector3.zero;
            cameraRotation.x = UpAndDownLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            cameraPivotTransform.localRotation = targetRotation;
        }

        private void HandleCollisions()
        {
            targetCameraZPosition = CameraPosition;
            RaycastHit hit;
            Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
            direction.Normalize();

            if(Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetCameraZPosition),collideWithLayer))
            {
                float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
                targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
            }

            if(Mathf.Abs(targetCameraZPosition) < cameraCollisionRadius)
            {
                targetCameraZPosition = -cameraCollisionRadius;
            }

            cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, 0.2f);
            cameraObject.transform.localPosition = cameraObjectPosition;
        }
    }
}
