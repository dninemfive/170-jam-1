using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonoBehavior controlling player movement.
/// </summary>
public class Player : MonoBehaviour
{
    private InputHandler _input = null;
    [SerializeField]
    private float MoveSpeed;
    [SerializeField]
    private float RotateSpeed;
    [SerializeField]
    private bool UseMouse;
    private Camera Camera => GameManager.Instance.Camera.GetComponent<Camera>();
    private void Awake()
    {
        _input = GetComponent<InputHandler>();
    }
    private void Update()
    {
        Vector3 targetVector = new(_input.InputVector.x, 0, _input.InputVector.y);
        if(UseMouse)
        {
            RotateTowardMouseVector();
        }
        else
        {
            RotateToward(MovementVectorToward(targetVector));
        }
    }
    private Vector3 MovementVectorToward(Vector3 targetVector)
    {
        float speed = MoveSpeed * Time.deltaTime;
        targetVector = Quaternion.Euler(0, Camera.gameObject.transform.eulerAngles.y, 0) * targetVector;
        Vector3 targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }
    private void RotateToward(Vector3 movementVector)
    {
        if (movementVector.magnitude == 0) return;
        Quaternion rotation = Quaternion.LookRotation(movementVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, RotateSpeed);
    }
    private void RotateTowardMouseVector()
    {
        Ray ray = Camera.ScreenPointToRay(_input.MousePosition);
        if(Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            Vector3 target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }
}