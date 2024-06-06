using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Menu {
    public class AutomatonOptions : MonoBehaviour {

        [SerializeField] private GameObject togglePrefab;
        [SerializeField] private Transform scrollParent;
        private GameObject[] _toggleObjects;
        private Toggle[] _toggles;
        private int[] _rules;

        private const int NO_NEIGHBORS = 6;
        private int _noRules;
        
        private void Start() {
            _noRules = (int)Math.Pow(2.0, NO_NEIGHBORS + 1.0);

            _rules = new int[_noRules];
            _toggleObjects = new GameObject[_noRules];
            _toggles = new Toggle[_noRules];
            
            for (var i = 0; i < _noRules; i++) {
                _toggleObjects[i] = Instantiate(togglePrefab, scrollParent);
                _toggleObjects[i].transform.localPosition = new Vector3(-40, 5080 + i * -80, 0);

                var toggle = _toggleObjects[i].GetComponent<Toggle>();
                var toggleText = toggle.GetComponentInChildren<TextMeshProUGUI>();
                
                if (toggleText != null) {
                    var bitsString = Convert.ToString(i, 2).PadLeft(7, '0');
                    toggleText.text = bitsString;
                }
                else {
                    Debug.LogError("TextMeshProUGUI component not found inside the Toggle.");
                }

                toggle.isOn = false;
                _toggles[i] = toggle;
                _rules[i] = 0;
            }
            
            Randomize();
        }

        private void Randomize() {
            var settingsPath = Application.persistentDataPath + "/SimSettings.json";
            var seed = 40;

            if (File.Exists(settingsPath)) {
                var json = File.ReadAllText(settingsPath);
                var settings = JsonUtility.FromJson<SimulationSettingsData>(json);
                seed = settings.seed;
            }

            Random.InitState(seed);

            for (var i = 0; i < _noRules; i++) {
                _rules[i] = Random.Range(0, 10) < 4 ? 1 : 0;
            }
            
            UpdateChecks();
        }

        public void SetData(List<int> indices) {
            for (var i = 0; i < _noRules; i++) {
                _rules[i] = 0;
            }

            foreach (var index in indices) {
                _rules[index] = 1;
            }
            UpdateChecks();
        }

        public int[] GetData() {
            return _rules;
        }

        private void UpdateChecks() {
            for (var i = 0; i < _noRules; i++) {
                _toggles[i].isOn = _rules[i] == 1 ? true : false;
            }
        }
    }
}

