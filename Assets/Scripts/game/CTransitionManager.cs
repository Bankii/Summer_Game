using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CTransitionManager : MonoBehaviour {

    static public CTransitionManager inst;

    private bool _hasToTransition = false;
    private int _sceneToTransition;
    public float _transitionTime;
    private float _elapsedTime = 0;

    public Material _transMat;

    private bool _isTransitionOver = false;

    private bool _hasToTransitionInv = true;

    // Use this for initialization
    void Start ()
    {
        inst = this;
        _transMat.SetFloat("_Cutoff", 1);
    }

    // Update is called once per frame
    void Update()
    {
        _isTransitionOver = false;
        if (_hasToTransition)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime > _transitionTime)
            {
                SceneManager.LoadScene(_sceneToTransition);
                _hasToTransition = false;
                _isTransitionOver = true;
                _transMat.SetFloat("_Cutoff", 1);
                _elapsedTime = 0;
                return;
            }
            _transMat.SetFloat("_Cutoff", Mathf.Lerp(0, 1, _elapsedTime / _transitionTime));
        }
        if (_hasToTransitionInv)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime > _transitionTime)
            {
                //SceneManager.LoadScene(_sceneToTransition);
                _hasToTransitionInv = false;
                _isTransitionOver = true;
                _transMat.SetFloat("_Cutoff", 0);
                _elapsedTime = 0;
                return;
            }
            _transMat.SetFloat("_Cutoff", Mathf.Lerp(1, 0, _elapsedTime / _transitionTime));
        }
    }

    public void setToTransition(int aScene)
    {
        _sceneToTransition = aScene;
        _hasToTransition = true;
        _transMat.SetFloat("_Cutoff", 0);
    }

    public bool isTransitioning()
    {
        return _hasToTransition || _hasToTransitionInv;
    }

    // Returns true in the frame the transition is over.
    public bool finishedTransition()
    {
        return _isTransitionOver;
    }
}
