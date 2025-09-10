using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace SG{
    public class CharacterNetworkManagement : NetworkBehaviour
    {
        [Header("Position")]
        public NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<Quaternion> networkRotation = new NetworkVariable<Quaternion>(Quaternion.identity, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        public Vector3 networkPositionVelocity;
        public float networkPositionSmoothTime = 0.1f;
        public float networkRotationSmoothTime = 0.1f;
        public NetworkVariable<float> horizontalMovement = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> verticalMovement = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> moveAmount = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header ("Stats")]
        public NetworkVariable<int> level = new NetworkVariable<int>(1 ,NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> currentHealth = new NetworkVariable<float>(0 ,NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> maxHealth = new NetworkVariable<int>(0 ,NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    }
}
