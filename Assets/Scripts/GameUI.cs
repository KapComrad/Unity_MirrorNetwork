using TMPro;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class GameUI : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    private readonly SyncDictionary<int, string> _playerNames = new SyncDictionary<int, string>();

    private void OnEnable()
    {
        EventBus.OnConnected += OnPlayerConnectedToServer;
        PlayerNetwork.PlayerConnected += OnPlayerConnectedTextChange;
        EventBus.OnDisconnected += OnPlayerDisconnectedTextChange;
        EventBus.OnServerDisconnectedEvent += OnServerDisconnectedTextChange;
    }

    private void OnDisable()
    {
        EventBus.OnConnected -= OnPlayerConnectedToServer;
        PlayerNetwork.PlayerConnected -= OnPlayerConnectedTextChange;
        EventBus.OnDisconnected -= OnPlayerDisconnectedTextChange;
        EventBus.OnServerDisconnectedEvent -= OnServerDisconnectedTextChange;
    }

    [Server]
    public void OnPlayerConnectedToServer(string name, int idNumber)
    {
        if (_playerNames.ContainsKey(idNumber)) return;
        Debug.Log("Player not in a Dictionary!");
        _playerNames.Add(idNumber, name);
    }

    [Command]
    public void OnPlayerScoreChanged(int score)
    {
        _textMeshPro.text = score.ToString();
        Debug.Log("EVENT BUS : TOUCHED PLAYER");
    }

    [Command(requiresAuthority = false)]
    public void OnPlayerConnectedTextChange()
    {
        Debug.Log("Server side active");
        for (int i = 0; i < _playerNames.Count; i++)
        {
            _textMeshPro.text += _playerNames[i].ToString() + i.ToString();
            Debug.Log("Player connected with name: " + _playerNames);
        }
    }


    [Server]
    public void OnPlayerDisconnectedTextChange(int idNumber)
    {
        _playerNames.Remove(idNumber);
        Debug.Log("Server side active");
        for (int i = 0; i < _playerNames.Count; i++)
        {
            _textMeshPro.text += _playerNames[i].ToString() + i.ToString();
            Debug.Log("Player connected with name: " + _playerNames);
        }
    }

    [ClientRpc]
    public void OnServerDisconnectedTextChange()
    {
        _playerNames.Clear();
        _textMeshPro.text = "";
        Debug.Log("Server disconnected!");
    }



}
