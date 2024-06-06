using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderChange : MonoBehaviour {

    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;
    
    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener((v) => {
            sliderText.SetText((Mathf.Round(v * 100) / 100).ToString(CultureInfo.InvariantCulture));
        });
    }
}
