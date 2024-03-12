using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Slider volumeSliderMainMenu;
    [SerializeField] Slider volumeSliderPauseMenu;
    [SerializeField] GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeVolume()
    {
        if (gameManager.principioJuego)
        {
            AudioListener.volume = volumeSliderPauseMenu.value;
        }
        else
        {
            AudioListener.volume = volumeSliderMainMenu.value;
            volumeSliderPauseMenu.value = volumeSliderMainMenu.value;
        }
    }
}
