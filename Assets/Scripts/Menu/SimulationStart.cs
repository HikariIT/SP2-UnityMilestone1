using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu {
    public class SimulationStart : MonoBehaviour {
        public void StartSim() {
            SceneManager.LoadScene("SampleScene");
        }
    }
}