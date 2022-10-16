using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{

    private InputHandler _input;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private bool useMouse;

    private Camera Camera => GameManager.Instance.Camera.GetComponent<Camera>();

    private void Awake()
    {
        _input = GetComponent<InputHandler>();
    }
    
    // Update is called once per frame
    void Update()
    {
        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);

        var movementVector = MoveTowardTarget(targetVector);

        if (!useMouse) {
            RotateTowardMovementVector(movementVector);
        } else {
            RotateTowardMouseVector();
        }
        ShootIfNecessary();
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = moveSpeed * Time.deltaTime;
        targetVector = Quaternion.Euler(0, Camera.gameObject.transform.eulerAngles.y , 0) * targetVector;
        var targetPosition = transform.position + targetVector  * speed;
        transform.position = targetPosition;
        return targetVector;
    }

    private void RotateTowardMovementVector(Vector3 movementVector)
    {
        if (movementVector.magnitude == 0) { return; }
        var rotation = Quaternion.LookRotation(movementVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);
    }

    private void RotateTowardMouseVector()
    {
        Ray ray = Camera.ScreenPointToRay(_input.MousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
            //LOCKS SPRITE TO x=90!
            transform.Rotate(90, 0, 0);
        }
    }

    private void ShootIfNecessary()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(Prefabs.Bullet, transform.position, transform.rotation);
        }
    }
}
