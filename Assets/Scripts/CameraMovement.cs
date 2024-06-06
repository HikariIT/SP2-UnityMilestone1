using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMovement : MonoBehaviour
{
    public SimSettings simSettings;
    private float moveSpeed = 30f;

    private Vector3 target;

    void Start()
    {
        // Set the camera's background color to dark gray
        Camera.main.backgroundColor = new Color(0.2f, 0.2f, 0.2f);

        this.moveSpeed = simSettings.moveSpeed;

        var numCubes = simSettings.CubesPerAxis;
        this.target = new Vector3(numCubes / 2, numCubes / 2, numCubes / 2);
        transform.LookAt(target);
    }

    void Update()
    {
        
        // Move the camera backward on the "Q" key press
        if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(target, Vector3.forward, moveSpeed * Time.deltaTime);
            //transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }

        // Move the camera forward on the "E" key press
        if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(target, Vector3.back, moveSpeed * Time.deltaTime);
            //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        // Move the camera left on the "A" key press
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(target, Vector3.up, moveSpeed * Time.deltaTime);
            //transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        // Move the camera right on the "D" key press
        if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(target, Vector3.down, moveSpeed * Time.deltaTime);
            //transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        // Move the camera up on the "W" key press
        if (Input.GetKey(KeyCode.W))
        {
            transform.RotateAround(target, Vector3.left, moveSpeed * Time.deltaTime);
            //transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }

        // Move the camera down on the "S" key press
        if (Input.GetKey(KeyCode.S))
        {
            transform.RotateAround(target, Vector3.right, moveSpeed * Time.deltaTime);
            //transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.R))
        {
            transform.LookAt(target);
        }
        //transform.LookAt(target);
    }
}
