using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    public GameObject prefabToggle;
    public RectTransform ParentPanel;

    private int[] _rules;



    public static bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        isPaused = false;

        _rules = CubeGrid.Rules;

        for(int i = 0; i < _rules.Length; i++)
        {
            GameObject goToggle = (GameObject)Instantiate(prefabToggle);
            goToggle.transform.SetParent(ParentPanel, false);
            goToggle.transform.localScale = new Vector3(2, 2, 2);
            goToggle.transform.position = new Vector3(0, i*-40, 0);
           
            Toggle tempToggle = goToggle.GetComponent<Toggle>();
            tempToggle.isOn = (_rules[i] == 1);
            int tempInt = i;
            
            var toggleText = tempToggle.GetComponentInChildren<TextMeshProUGUI>();

            if (toggleText != null)
            {
                // Change the text
                toggleText.text = System.Convert.ToString(i, 2).PadLeft(9, '0');
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component not found inside the Toggle.");
            }
           
            tempToggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(tempToggle, tempInt);
            });
        }

    }

    //Output the new state of the Toggle into Text
    void ToggleValueChanged(Toggle change, int number)
    {
        if (change.isOn){
            _rules[number] = 1;
        } else {
            _rules[number] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (isPaused){
                ResumeGame();
            } else {
                PauseGame();
            }
        }
        
    }

    public void PauseGame(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame(){
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

    }

    public void QuitGame(){
        Application.Quit();
    }

    public void OpenSettings(){
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void CloseSettings(){
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
}
