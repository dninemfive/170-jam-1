using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    /// <summary>
    /// The vector corresponding to the player's inputs in the horizontal and vertical directions, respectively.
    /// </summary>
    public Vector2 InputVector { get; private set; }
    /// <summary>
    /// The location of the mouse.
    /// </summary>
    public Vector3 MousePosition { get; private set; }
    public GameObject Mouse;

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        InputVector = new Vector2(h, v);

        MousePosition = Input.mousePosition;
        Mouse.transform.position = MousePosition;
    }
}
