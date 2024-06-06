using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Menu;

public class CameraMovement : MonoBehaviour {
    private float _moveSpeed;
    private int _sideLength;

    private Vector3 target;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float distance;

    private void Start() {
        if (Camera.main == null)
            return;
        
        var settingsPath = Application.persistentDataPath + "/SimSettings.json";
        var json = File.ReadAllText(settingsPath);
        var settingsObject = JsonUtility.FromJson<SimulationSettingsData>(json);
        
        Camera.main.backgroundColor = new Color(0.2f, 0.2f, 0.2f);

        _sideLength = settingsObject.sideLength;
        _moveSpeed = settingsObject.cameraSpeed;
        distance = _sideLength * 2;
        
        target = new Vector3(
            (float) settingsObject.sideLength / 2, 
            (float) settingsObject.sideLength / 2, 
            (float) settingsObject.sideLength / 2);

        transform.LookAt(target);
    }

    private void LateUpdate() {
        currentX += ((Input.GetKey("s") ? -1.0f : 0.0f) + (Input.GetKey("w") ? 1.0f : 0.0f)) * _moveSpeed * Time.deltaTime * 10;
        currentY += ((Input.GetKey("d") ? -1.0f : 0.0f) + (Input.GetKey("a") ? 1.0f : 0.0f)) * _moveSpeed * Time.deltaTime * 10;
        
        var dir = new Vector3(0, 0, -distance);
        var rotation = Quaternion.Euler(currentX, currentY, 0);
        transform.position = target + rotation * dir;
        transform.LookAt(target);

        distance -= Input.mouseScrollDelta.y * 5f;
    }
}
