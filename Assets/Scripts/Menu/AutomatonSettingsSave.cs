using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Menu {
    public class AutomatonSettingsSave : MonoBehaviour {
        [SerializeField] private Button saveButton;
    
        private void Start() {
            saveButton.onClick.AddListener(() => {
                var automatonOptions = GetComponent<AutomatonOptions>();
                var data = automatonOptions.GetData();

                var json = JsonUtility.ToJson(new AutomatonRuleData { Rules = data });
                var filePath = Application.persistentDataPath + "/CurrentRules.json";
                File.WriteAllText(filePath, json);
            });
        }
    }

    public class AutomatonRuleData {
        public int[] Rules;
    }
}

