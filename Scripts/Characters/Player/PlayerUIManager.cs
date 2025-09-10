using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace SG{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager instance;
        [Header("Network JOIN")]
        [SerializeField] bool startGameAsClient;
        [HideInInspector] public PlayerUIHudManager playerUIHudManager;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }else
            {
                Destroy(gameObject);
            }

            playerUIHudManager = GetComponent<PlayerUIHudManager>();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if(startGameAsClient)
            {
                startGameAsClient = false;
                NetworkManager.Singleton.Shutdown();
                NetworkManager.Singleton.StartClient();
            }
        }
    }
}