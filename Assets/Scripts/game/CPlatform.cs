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

    private const int PLATFORM_HEIGHT = 40;
    private const int PLATFORM_WIDTH = 85;

    private SpriteRenderer _spriteRenderer;

    public Sprite _platformGreenInactive;
    public Sprite _platformGreenActive;
    public Sprite _platformRedInactive;
    public Sprite _platformRedActive;
    public Sprite _platformYellowInactive;
    public Sprite _platformYellowActive;
    public Sprite _platformBlueInactive;
    public Sprite _platformBlueActive;

    // Use this for initialization
    void Start () {

        setState(STATE_OFF);
        
        _spriteRenderer = GetComponent<SpriteRenderer>();        
        
    }

    void Update()
    {
        apiUpdate();
    }


    public override void apiUpdate()
    {
        base.apiUpdate();
        
        if (getState() == STATE_OFF)
        {
            if (getType() == PLATFORM_GREEN)
            {
                _spriteRenderer.sprite = _platformGreenInactive;
            }
            else if (getType() == PLATFORM_RED)
            {
                _spriteRenderer.sprite = _platformRedInactive;
            }
            else if (getType() == PLATFORM_YELLOW)
            {
                _spriteRenderer.sprite = _platformYellowInactive;
            }
            else if (getType() == PLATFORM_BLUE)
            {
                _spriteRenderer.sprite = _platformBlueInactive;
            }

        }

        if (getState() == STATE_ON)
        {
            if (getType() == PLATFORM_GREEN)
            {
                _spriteRenderer.sprite = _platformGreenActive;                
            }
            else if (getType() == PLATFORM_RED)
            {
                _spriteRenderer.sprite = _platformRedActive;
            }
            else if (getType() == PLATFORM_YELLOW)
            {
                _spriteRenderer.sprite = _platformYellowActive;
            }
            else if (getType() == PLATFORM_BLUE)
            {
                _spriteRenderer.sprite = _platformBlueActive;
            }

            if (getTimeState() >= 1.0f)
            {
                setState(STATE_OFF);
            }
        }

    }


}
