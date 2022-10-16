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
            if (_state is TileState.Visible or TileState.Hidden)
            {
                gameObject.SetActive(true);
                MeshRenderer.enabled = _state.ToBool().Value;
            } 
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
    private MeshRenderer MeshRenderer => GetComponent<MeshRenderer>();
    public void SetVisible(bool visible)
    {
        if (State is not TileState.Destroyed) State = visible.ToTileState();
    }
    public void MarkDestroyed() => State = TileState.Destroyed;
}
