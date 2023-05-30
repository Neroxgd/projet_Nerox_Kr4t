using Photon.Pun;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private GameObject[] playerPrefab;
    [SerializeField] private Transform spawnPlayer;

    void Start()
    {
        for (int i = 0; i < playerPrefab.Length; i++)
        {
            if (i == ChooseCharacter.IndexCharacter)
            {
                PhotonNetwork.Instantiate(playerPrefab[i].name, spawnPlayer.position, Quaternion.identity);
                return;
            }
        }
        PhotonNetwork.Instantiate(playerPrefab[0].name, spawnPlayer.position, Quaternion.identity);
    }
}
