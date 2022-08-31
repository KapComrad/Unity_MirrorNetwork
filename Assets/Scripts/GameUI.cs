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
        EventBus.OnDisconnected += OnPlayerDisconnectedTextChange;
        EventBus.OnServerDisconnectedEvent += OnServerDisconnectedTextChange;
    }

    private void OnDisable()
    {
        EventBus.OnConnected -= OnPlayerConnectedToServer;
        EventBus.OnDisconnected -= OnPlayerDisconnectedTextChange;
        EventBus.OnServerDisconnectedEvent -= OnServerDisconnectedTextChange;
    }

    public void OnPlayerConnectedToServer(string name, int idNumber)
    {
        if (_playerNames.ContainsKey(idNumber)) return;
        Debug.Log("Player not in a Dictionary!");
        _playerNames.Add(idNumber, name);
        OnPlayerConnectedTextChange();
    }

    [ClientRpc]
    public void OnPlayerScoreChanged(int score)
    {
        _textMeshPro.text = score.ToString();
        Debug.Log("EVENT BUS : TOUCHED PLAYER");
    }

    [ClientRpc]
    public void OnPlayerConnectedTextChange()
    {
        Debug.Log("Client side active");
        for (int i = 0; i < _playerNames.Count; i++)
        {
            _textMeshPro.text += _playerNames[i].ToString() + i.ToString();
            Debug.Log("Player connected with name: " + _playerNames);
        }
    }

    [ClientRpc]
    public void OnPlayerDisconnectedTextChange(int idNumber)
    {
        _playerNames.Remove(idNumber);
        OnPlayerConnectedTextChange();
    }

    [ClientRpc]
    public void OnServerDisconnectedTextChange()
    {
        _playerNames.Clear();
        _textMeshPro.text = "";
        Debug.Log("Server disconnected!");
    }


}
