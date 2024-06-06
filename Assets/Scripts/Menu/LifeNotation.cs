using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu {
    public class LifeNotation : MonoBehaviour {

        [SerializeField] private TMP_InputField bInput;
        [SerializeField] private TMP_InputField sInput;
        [SerializeField] private Button saveButton;
        
        private void Start() {

            saveButton.onClick.AddListener(() => {
                var bValues = new List<int>();
                var sValues = new List<int>();

                foreach (var c in bInput.text) {
                    bValues.Add(int.Parse(c.ToString()));
                }

                foreach (var c in sInput.text) {
                    sValues.Add(int.Parse(c.ToString()));
                }

                var indices = new List<int>();
                
                for (var i = 0; i < 128; i++) {
                    var bitsString = Convert.ToString(i, 2).PadLeft(7, '0');
                    var oneCount = bitsString.ToCharArray().Count(c => c == '1');

                    if (i % 2 == 0 && bValues.Contains(oneCount)) {
                        indices.Add(i);
                    }
                    else if (i % 2 == 1 && sValues.Contains(oneCount)) {
                        indices.Add(i);
                    }
                }

                var automatonOptions = GetComponent<AutomatonOptions>();
                automatonOptions.SetData(indices);
            });
        }
    }

}