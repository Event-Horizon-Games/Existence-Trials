using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class PlayerSettings : MonoBehaviour
{
    //Visible to inspector
    [SerializeField] private List<AudioSource> audioSources;
    [Space(10)]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_InputField volumeInput;

    //Only this class
    private float playerVolume;
    
    // Start is called before the first frame update
    void Start()
    {
        //Get values from PlayerPrefs
        if(PlayerPrefs.HasKey("playerVolume"))
        {
            playerVolume = PlayerPrefs.GetFloat("playerVolume");
            SetAudioSourceVolumes(playerVolume);
            volumeSlider.value = playerVolume;
            volumeInput.text = playerVolume.ToString();
        }
        else
        {
            PlayerPrefs.SetFloat("playerVolume", 1);
            playerVolume = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeVolumeSlider(float volume)
    {
        PlayerPrefs.SetFloat("playerVolume", volume);
        SetAudioSourceVolumes(playerVolume);
        playerVolume = volume;
        volumeInput.text = volume.ToString("0.00");
    }

    public void ChangeVolumeInput(string volString)
    {
        float parsed = float.Parse(volString);
        SetAudioSourceVolumes(playerVolume);
        PlayerPrefs.SetFloat("playerVolume", parsed);
        volumeSlider.value = parsed;
    }

    public void SetAudioSourceVolumes(float volume)
    {
        //Loop through all sources and set them to the player's volume
        if(audioSources.Count > 0)
        {
            for(int i = 0; i < audioSources.Count; i++)
            {
                audioSources[i].volume = volume;
            }
        }
    }
}
