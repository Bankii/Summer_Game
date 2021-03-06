﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBackgroundMusic : MonoBehaviour {
    
    static public CBackgroundMusic inst;

    [HideInInspector]
    public bool _hasInstantiated = false;

    private bool _load = true;

    void Start () {

        GameObject other = GameObject.Find("Background_Music");
        if (other != null && other != gameObject)
        {
            Destroy(gameObject);
            _load = false;
            return;
        }
        

        DontDestroyOnLoad(gameObject);
        inst = this;
        
        _hasInstantiated = true;
    }
	
}
