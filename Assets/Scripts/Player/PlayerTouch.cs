using System.Collections;
using Mirror;
using UnityEngine;

public class PlayerTouch : MonoBehaviour
{

    private PlayerMovement _playerMovement;
    private PlayerNetwork _playerNetwork;

    public delegate void OnTouchedDelegate(int argument);
    public static event OnTouchedDelegate OnTouched;

    public delegate void OnTouchedRpcDelegate(PlayerNetwork playerNetwork);
    public static event OnTouchedRpcDelegate OnTouchedRpc;

    private void Awake()
    {
        _playerNetwork = GetComponentInParent<PlayerNetwork>();
        _playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerNetwork playerNetwork = other.GetComponentInParent<PlayerNetwork>();

        if (other.CompareTag("Player") && _playerMovement._jerking && playerNetwork.CanTouchThis)
        {  
            OnTouchedRpc?.Invoke(playerNetwork);
            OnTouched?.Invoke(1);
        }
    }

}
