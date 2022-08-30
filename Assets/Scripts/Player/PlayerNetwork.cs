using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerNetwork : NetworkBehaviour
{
    private Color32 _color;
    private MeshRenderer _renderer;

    public bool CanTouchThis = true;

    private void OnEnable()
    {
        PlayerTouch.OnTouchedRpc += PlayerTouchRpc;
    }

    private void OnDisable()
    {
        PlayerTouch.OnTouchedRpc -= PlayerTouchRpc;
    }
    private void Awake()
    {
        _renderer = GetComponentInParent<MeshRenderer>();
    }

    [ClientRpc]
    private void PlayerTouchRpc(PlayerNetwork playerNetwork)
    {
        playerNetwork.GetComponentInParent<PlayerNetwork>().ChangeColor();
    }


    [ClientRpc]
    public void ChangeColor()
    {
        
        if (CanTouchThis)
            StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        Debug.Log("Touch started");
        _color = Color.red;
        _renderer.material.color = _color;
        CanTouchThis = false;
        yield return new WaitForSeconds(3f);
        _color = Color.white;
        _renderer.material.color = _color;
        CanTouchThis = true;
        Debug.Log("Touch Ended");

    }
}
