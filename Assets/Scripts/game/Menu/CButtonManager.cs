using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CButtonManager : MonoBehaviour {

    private bool _hasToTransition = false;
    private int _sceneToTransition;
    public float _transitionTime;
    private float _elapsedTime = 0;

    public Material _transMat;

    public void changeScene(int aScene)
    {
        _sceneToTransition = aScene;
        _hasToTransition = true;
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

    void Start()
    {
        _transMat.SetFloat("_Cutoff", 0);
    }

    void Update()
    {
        if (_hasToTransition)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime > _transitionTime)
            {
                SceneManager.LoadScene(_sceneToTransition);
                _hasToTransition = false;
                return;
            }
            Debug.Log("HERE!!");
            _transMat.SetFloat("_Cutoff", Mathf.Lerp(0, 1, _elapsedTime / _transitionTime));
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
