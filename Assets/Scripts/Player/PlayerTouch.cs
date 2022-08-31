using System.Collections;
using Mirror;
using UnityEngine;

public class PlayerTouch : MonoBehaviour
{

    private PlayerMovement _playerMovement;
    private PlayerNetwork _playerNetwork;

    public delegate void OnTouchedRpcDelegate(PlayerNetwork playerNetwork);
    public static event OnTouchedRpcDelegate OnTouchedRpc;

    private void Awake()
    {
        _playerNetwork = GetComponentInParent<PlayerNetwork>();
        _playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerNetwork playerNetwork = other.GetComponentInParent<PlayerNetwork>();
            if (_playerMovement._jerking && playerNetwork.CanTouchThis)
                OnTouchedRpc?.Invoke(playerNetwork);
        }
    }

}
