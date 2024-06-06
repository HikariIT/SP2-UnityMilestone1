using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu {
    public class ApplySettings : MonoBehaviour {

        [SerializeField] private Button button;
        [SerializeField] private TMP_InputField seedText;
        [SerializeField] private TMP_InputField sideLengthText;
        [SerializeField] private TMP_InputField cameraSpeedText;
        [SerializeField] private Slider timeStepSlider;
        
        private void Start() {
            seedText.text = "1";
            sideLengthText.text = "40";
            cameraSpeedText.text = "100";

            // Add onClick listener
            button.onClick.AddListener(() => {
                var simSettings = new SimulationSettingsData {
                    seed = int.Parse(seedText.text),
                    sideLength = int.Parse(sideLengthText.text),
                    cameraSpeed = int.Parse(cameraSpeedText.text),
                    timeStep = timeStepSlider.value
                };

                var json = JsonUtility.ToJson(simSettings);
                var path = Application.persistentDataPath + "/SimSettings.json";
                
                File.WriteAllText(path, json);
            });
        }
    }

    [Serializable]
    public class SimulationSettingsData {
        public int seed;
        public int sideLength;
        public int cameraSpeed;
        public float timeStep;
    }
}