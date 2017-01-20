using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlatform : CGameObject {

    private const int STATE_OFF = 0;
    private const int STATE_ON = 1;    
    private const int STATE_SHUTDOWN = 2;
    private const int STATE_DONE = 3;
    private const int STATE_TRANSITION = 4;


    public const int PLATFORM_GREEN = 0;
    public const int PLATFORM_RED = 1;
    public const int PLATFORM_YELLOW = 2;
    public const int PLATFORM_BLUE = 3;

    private const int PLATFORM_HEIGHT = 78;
    private const int PLATFORM_WIDTH = 200;

    private SpriteRenderer _spriteRenderer;
    public Collider _collider;

    private bool _walkable = true;

    public Sprite _platformGreenInactive;
    public Sprite _platformGreenActive;
    public Sprite _platformRedInactive;
    public Sprite _platformRedActive;
    public Sprite _platformYellowInactive;
    public Sprite _platformYellowActive;
    public Sprite _platformBlueInactive;
    public Sprite _platformBlueActive;
    public Sprite _platformShutdown;
    public Sprite _platformDone;

    private AudioSource _platformFX;
    public AudioClip _greenFX;
    public AudioClip _redFX;
    public AudioClip _yellowFX;
    public AudioClip _blueFX;

    // Use this for initialization
    void Start () {

        setState(STATE_OFF);
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _platformFX = GetComponent<AudioSource>();
        setWidth(PLATFORM_WIDTH);
        setHeight(PLATFORM_HEIGHT);

    }

    void Update()
    {
        apiUpdate();
    }

    public void setWalkable(bool aWalkable)
    {
        if (_collider != null)
        {
            _walkable = aWalkable;
            _collider.enabled = _walkable;
        }
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

            if (getTimeState() >= 0.5f)
            {
                setState(STATE_TRANSITION);
            }
        }

        if (getState() == STATE_TRANSITION)
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

            if (getTimeState() >= 0.1f)
            {
                setState(STATE_OFF);
            }
        }

        if (getState() == STATE_SHUTDOWN)
        {
            _spriteRenderer.sprite = _platformShutdown;
        }

        if (getState() == STATE_DONE)
        {
            _spriteRenderer.sprite = _platformDone;
        }

    }

    public void playPlatformFX()
    {
        if (getType() == PLATFORM_GREEN)
        {
            _platformFX.clip = _greenFX;
            _platformFX.Play();            
        }
        else if (getType() == PLATFORM_RED)
        {
            _platformFX.clip = _redFX;
            _platformFX.Play();
        }
        else if (getType() == PLATFORM_YELLOW)
        {
            _platformFX.clip = _yellowFX;
            _platformFX.Play();
        }
        else if (getType() == PLATFORM_BLUE)
        {
            _platformFX.clip = _blueFX;
            _platformFX.Play();
        }
    }


}
