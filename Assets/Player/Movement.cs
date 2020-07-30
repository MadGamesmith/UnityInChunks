using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public MovementType movementType;

    public float speed; //units/meters per second
    public float acceleration; //force per second
    public Rigidbody2D rb;

    private Vector3 inputVector;

    private void Update() {
        if (!MadConsole.instance.ConsoleOn()) {
            inputVector = GetInputVector();
        }
    }

    private void FixedUpdate() {
        switch (movementType) {
            case MovementType.MoveGameObject_v1:
                MoveGameObject_v1(inputVector);
                break;
            case MovementType.MoveGameObject_v2:
                MoveGameObject_v2(inputVector);
                break;
            case MovementType.MoveGameObject_v3:
                MoveGameObject_v3(inputVector);
                break;
            case MovementType.MoveGameObject_v4:
                MoveGameObject_v4(inputVector);
                break;
            default:
                break;
        }
    }

    private Vector3 GetInputVector() {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");
        Vector3 input = new Vector3(horizontalAxis, verticalAxis, 0.0f);
        input = Vector3.Normalize(input);
        return input;
    }

    private void MoveGameObject_v1(Vector3 inputVector) {
        transform.position += inputVector * speed * Time.deltaTime;
    }

    private void MoveGameObject_v2(Vector3 inputVector) {
        transform.Translate(inputVector * speed * Time.deltaTime, Space.World);
    }

    private void MoveGameObject_v3(Vector3 inputVector) {
        rb.MovePosition(transform.position + (inputVector * speed * Time.deltaTime));
    }

    private void MoveGameObject_v4(Vector3 inputVector) {
        float currentSpeed = Vector3.Magnitude(rb.velocity);
        if (currentSpeed < speed) {
            rb.AddForce(inputVector * acceleration * Time.deltaTime);
        }
    }
}

public enum MovementType {
    MoveGameObject_v1, MoveGameObject_v2, MoveGameObject_v3, MoveGameObject_v4
}
