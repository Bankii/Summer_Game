using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CButtonManager : MonoBehaviour {
    
    
    public void changeScene(int aScene)
    {
        SceneManager.LoadScene(aScene);
        // TODO: add a loading image.
        if (aScene == 1)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }

    public void quit()
    {
        Application.Quit();
    }

    public void eraseLoad()
    {
        CSaveLoad.eraseLoad();
    }

    public void setMusicVolume(Slider aSlider)
    {
        CSaveLoad.musicVolume = aSlider.value;
    }

    public void setSoundVolume(Slider aSlider)
    {
        CSaveLoad.soundVolume = aSlider.value;
    }
    
}
