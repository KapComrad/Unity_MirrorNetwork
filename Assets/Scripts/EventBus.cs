using UnityEngine;
using Mirror;

public class EventBus : NetworkManager
{
    public delegate void OnPlayerConnected(string name, int idNumber);
    public static event OnPlayerConnected OnConnected;

    public delegate void OnPlayerDisconnected(int idNumber);
    public static event OnPlayerDisconnected OnDisconnected;

    public delegate void OnServerDisconnected();
    public static event OnServerDisconnected OnServerDisconnectedEvent;

    [System.Obsolete]
    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log($"Player {conn.connectionId} connected ");

        OnConnected?.Invoke("Player", conn.connectionId);
    }

    [System.Obsolete]
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        OnDisconnected?.Invoke(conn.connectionId);
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        OnServerDisconnectedEvent?.Invoke();
    }

}
