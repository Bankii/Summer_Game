using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CButtonManager : MonoBehaviour {

    public void changeScene(int aScene)
    {
        CTransitionManager.inst.setToTransition(aScene);
        //SceneManager.LoadScene(aScene);
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
