using Photon.Pun;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPlayer;

    void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPlayer.position, Quaternion.identity);
    }
}
