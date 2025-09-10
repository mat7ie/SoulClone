using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

namespace SG
{
    public class WorldAIManager : MonoBehaviour
    {
        public static WorldAIManager instance;

        [Header("Debug")]
        [SerializeField] bool despawnAllCharacters = false;
        [SerializeField] bool respawnAllCharacters = false;

        [Header("Characters")]
        [SerializeField] GameObject[] aiCharacters;
        [SerializeField] List<GameObject> spawnedCharacters;

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
            if(NetworkManager.Singleton.IsServer)
            {
                StartCoroutine(WaitForScenceToLoadAndSpawn());
            }
        }

        private void Update()
        {
            if(respawnAllCharacters)
            {
                respawnAllCharacters = false;
                SpawnAllCharacter();
            }

            if(despawnAllCharacters)
            {
                despawnAllCharacters = false;
                DespawnAllCharacters();
            }
        }

        private IEnumerator WaitForScenceToLoadAndSpawn()
        {
            while(!SceneManager.GetActiveScene().isLoaded)
            {
                yield return null;
            }

            SpawnAllCharacter();
        }

        private void SpawnAllCharacter()
        {
            foreach (var character in aiCharacters)
            {
                GameObject instantiatedCharacter = Instantiate(character);
                instantiatedCharacter.GetComponent<NetworkObject>().Spawn();
                spawnedCharacters.Add(instantiatedCharacter);
            }
        }

        private void DespawnAllCharacters()
        {
            foreach (var character in spawnedCharacters)
            {
                character.GetComponent<NetworkObject>().Despawn();
            }
        }
    
        private void DisableAllCharacter()
        {
            
        }
    }
}
