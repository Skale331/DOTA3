using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class ReadyButton : MonoBehaviourPunCallbacks
{
    private int playersReady = 0;
    public static bool gameStarted = false;

    public Button readyButton;

    private void Start()
    {
        readyButton.onClick.AddListener(OnReadyButtonClick);
        Debug.Log(playersReady);
    }

    public void OnReadyButtonClick()
    {
        // ������������ ������ "������" � ���������� ���� ��� ������ ������
        readyButton.interactable = false;

        // ³���������� ����������� ��� ��������� ������
        photonView.RPC("PlayerReady", target: RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void PlayerReady()
    {
        playersReady++;
        Debug.Log(playersReady);
        // ����������, �� ����� �� ������
        if (playersReady == PhotonNetwork.PlayerList.Length)
        {
            // ��������� ��� ��� �������� �������� 䳿 ��� ������� ���
            StartGame();
        }
    }

    public void StartGame()
    {
        // ������������ ��������� ��� ���������� ������� ���
        Debug.Log(playersReady);
        gameStarted = true;
        if (gameStarted)
        {
            SceneManager.LoadScene("Game");
        }
    }
}