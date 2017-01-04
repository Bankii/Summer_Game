using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlatform : CGameObject {

    private const int STATE_OFF = 0;
    private const int STATE_ON = 1;

    public const int PLATFORM_GREEN = 0;
    public const int PLATFORM_RED = 1;
    public const int PLATFORM_YELLOW = 2;
    public const int PLATFORM_BLUE = 3;

    public CPlatform(int aColor)
    {

        setType(aColor);

        if (getType() == PLATFORM_GREEN)
        {
            //prefab GREEN
        }
        else if (getType() == PLATFORM_RED)
        {
            //prefab RED
        }
        else if (getType() == PLATFORM_YELLOW)
        {
            //prefab YELLOW
        }
        else if (getType() == PLATFORM_BLUE)
        {
            //prefab BLUE
        }

        setName("Platform " + getType());
        
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
