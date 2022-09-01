using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class UIManager : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    private readonly SyncDictionary<int, string> _playerNames = new SyncDictionary<int, string>();

    private void OnEnable()
    {
        //EventBus.OnConnected += OnPlayerConnectedToServer;
        //EventBus.OnDisconnected += OnPlayerDisconnectedTextChange;
        //EventBus.OnServerDisconnectedEvent += OnServerDisconnectedTextChange;
    }

    private void OnDisable()
    {
        //EventBus.OnConnected -= OnPlayerConnectedToServer;
        //EventBus.OnDisconnected -= OnPlayerDisconnectedTextChange;
        //EventBus.OnServerDisconnectedEvent -= OnServerDisconnectedTextChange;
    }

    [ClientRpc]
    public void PooPooPeePee()
    {
        Debug.LogWarning("On all client AAAAAAAAAAAAAAAAAAAAAAAAAAAAAA!!!!!!!!!!");
    }

}
