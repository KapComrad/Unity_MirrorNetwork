using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Mirror;

public class PlayerNetwork : NetworkBehaviour
{
    private Color32 _color;
    private MeshRenderer _renderer;

    public bool CanTouchThis = true;

    public delegate void OnTouchedDelegate(int argument);
    public static event OnTouchedDelegate OnTouched;

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

    private void PlayerTouchRpc(PlayerNetwork playerNetwork)
    {
        playerNetwork.ChangeColor();
    }

    [Command(requiresAuthority = false)]
    public void ChangeColor()
    {
        ColorChanger();
    }

    [ClientRpc]
    public void ColorChanger()
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
        OnTouched?.Invoke(1);
        yield return new WaitForSeconds(3f);
        _color = Color.white;
        _renderer.material.color = _color;
        CanTouchThis = true;
        Debug.Log("Touch Ended");

    }
}
