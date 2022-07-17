using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public Text masterVolumeText;
    public Text musicVolumeText;

    public Button musicVolumeButton;
    public Button masterVolumeButton;

    public Button backButton;

    // Start is called before the first frame update
    void Start()
    {
        masterVolumeButton.onClick.AddListener(() => {
            AudioManager.instance.ChangeMasterVolume();
        });

        musicVolumeButton.onClick.AddListener(() => {
            AudioManager.instance.ChangeMusicVolume();
        });

        backButton.onClick.AddListener(Back);
    }

    // Update is called once per frame
    void Update()
    {
        double musicVolume = AudioManager.musicVolume * 100;
        musicVolumeText.text = "Music Volume " + (int)musicVolume + "%";
        
        double masterVolume = AudioManager.masterVolume * 100;
        masterVolumeText.text = "Master Volume " + (int)masterVolume + "%";
    }

    void Back() {
        SceneHelper.LoadScene("StartingMenu", false);
    }
}
