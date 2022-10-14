using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileState { Visible, Hidden, Destroyed }
public class Tile : MonoBehaviour
{
    private TileState _state = TileState.Visible;
    private TileState State
    {
        get => _state;
        set
        {
            _state = value;
            if(_state is TileState.Visible)
            {
                MeshRenderer.enabled = true;
            } else
            {
                MeshRenderer.enabled = false;
            }
        }
    }
    private MeshRenderer MeshRenderer => GetComponent<MeshRenderer>();
    public void SetVisible(bool visible)
    {
        if (State is not TileState.Destroyed) State = visible.ToTileState();
    }
    public void Destroy() => State = TileState.Destroyed;
}
