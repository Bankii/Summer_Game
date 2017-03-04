using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CButtonFX : MonoBehaviour {

    private AudioSource _as;

    static public CButtonFX inst;

    [HideInInspector]
    public bool _hasInstantiated = false;

    private bool _load = true;

    void Start () {

        _as = GetComponent<AudioSource>();

        GameObject other = GameObject.Find("Button_FX");
        if (other != null && other != gameObject)
        {
            Destroy(other.gameObject);
            _load = false;
        }

        DontDestroyOnLoad(gameObject);
        inst = this;

        _hasInstantiated = true;
    }
	    
    public void playSound()
    {
        _as.Play();
    }
}
