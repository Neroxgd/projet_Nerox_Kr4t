using TMPro;
using UnityEngine;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField createField, joinField;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createField.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinField.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("LevelTest");
    }
}
