using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Mirror;

public class EventBus : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    [SyncVar] private List<string> _playerNames = new List<string>();

    
    private void OnEnable()
    {
        PlayerTouch.OnTouched += OnPlayerScoreChanged;
       //NetworkManager.OnPlayerConnected += OnPlayerConnectedTextChanged;
        NetworkManager.OnPlayerConnected += OnPlayerConnectedToServer;
    }

    private void OnDisable()
    {
        PlayerTouch.OnTouched -= OnPlayerScoreChanged;
        //NetworkManager.OnPlayerConnected -= OnPlayerConnectedTextChanged;
        NetworkManager.OnPlayerConnected -= OnPlayerConnectedToServer;
    }

    public void OnPlayerScoreChanged(int score)
    {
        _textMeshPro.text = score.ToString();
        Debug.Log("EVENT BUS : TOUCHED PLAYER");
    }

    public void OnPlayerConnectedToServer(string name)
    {
        _playerNames.Add(name);
        OnPlayerConnectedTextChanged();
    }

    [ClientRpc]
    public void OnPlayerConnectedTextChanged()
    {
        Debug.Log("Client side active");
        for (int i = 0; i < _playerNames.Count; i++)
        {
            _textMeshPro.text += _playerNames[i];
            Debug.Log("Player connected with name: " + _playerNames);
        }
    }

}
