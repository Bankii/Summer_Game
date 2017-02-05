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
    private const int STATE_TRANSITION_DONE = 5;


    public const int PLATFORM_GREEN = 0;
    public const int PLATFORM_RED = 1;
    public const int PLATFORM_YELLOW = 2;
    public const int PLATFORM_BLUE = 3;

    private const int PLATFORM_HEIGHT = 78;
    private const int PLATFORM_WIDTH = 200;

    public SpriteRenderer _spriteRenderer;
    public Collider2D _collider;

    public Animator _anim;
       

    private bool _walkable = true;

    public Sprite _platformShutdown;
    public Sprite _platformDone;

    private AudioSource _platformFX;
    public AudioClip _greenFX;
    public AudioClip _redFX;
    public AudioClip _yellowFX;
    public AudioClip _blueFX;

    public CPlatformController _platformControllers;

    void Awake()
    {
        apiAwake();
        _platformFX = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {

        setState(STATE_TRANSITION);
        _anim = GetComponentInChildren<Animator>();
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
        #region STATE_OFF
        if (getState() == STATE_OFF)
        {
            if (getType() == PLATFORM_GREEN)
            {
                _anim.runtimeAnimatorController = _platformControllers.getController(PLATFORM_GREEN, STATE_OFF);
                //_spriteRenderer.sprite = _platformGreenInactive;
            }
            else if (getType() == PLATFORM_RED)
            {
                _anim.runtimeAnimatorController = _platformControllers.getController(PLATFORM_RED, STATE_OFF);
                //_spriteRenderer.sprite = _platformRedInactive;
            }
            else if (getType() == PLATFORM_YELLOW)
            {
                _anim.runtimeAnimatorController = _platformControllers.getController(PLATFORM_YELLOW, STATE_OFF);
                //_spriteRenderer.sprite = _platformYellowInactive;
            }
            else if (getType() == PLATFORM_BLUE)
            {
                _anim.runtimeAnimatorController = _platformControllers.getController(PLATFORM_BLUE, STATE_OFF);
                //_spriteRenderer.sprite = _platformBlueInactive;
            }

        }
        #endregion

        #region STATE_ON
        if (getState() == STATE_ON)
        {
            if (getType() == PLATFORM_GREEN)
            {
                _anim.runtimeAnimatorController = _platformControllers.getController(PLATFORM_GREEN, STATE_ON); 
                //_spriteRenderer.sprite = _platformGreenActive;                
            }
            else if (getType() == PLATFORM_RED)
            {
                _anim.runtimeAnimatorController = _platformControllers.getController(PLATFORM_RED, STATE_ON);
                //_spriteRenderer.sprite = _platformRedActive;
            }
            else if (getType() == PLATFORM_YELLOW)
            {
                _anim.runtimeAnimatorController = _platformControllers.getController(PLATFORM_YELLOW, STATE_ON);
                //_spriteRenderer.sprite = _platformYellowActive;
            }
            else if (getType() == PLATFORM_BLUE)
            {
                _anim.runtimeAnimatorController = _platformControllers.getController(PLATFORM_BLUE, STATE_ON);
                //_spriteRenderer.sprite = _platformBlueActive;
            }

            if (getTimeState() >= 1.0f)
            {
                setState(STATE_TRANSITION);
            }
        }
        #endregion

        #region STATE_TRANSITION
        if (getState() == STATE_TRANSITION)
        {
            if (getType() == PLATFORM_GREEN)
            {
                _anim.runtimeAnimatorController = _platformControllers.getController(PLATFORM_GREEN, STATE_OFF);
                //_spriteRenderer.sprite = _platformGreenInactive;
            }
            else if (getType() == PLATFORM_RED)
            {
                _anim.runtimeAnimatorController = _platformControllers.getController(PLATFORM_RED, STATE_OFF);
                //_spriteRenderer.sprite = _platformRedInactive;
            }
            else if (getType() == PLATFORM_YELLOW)
            {
                _anim.runtimeAnimatorController = _platformControllers.getController(PLATFORM_YELLOW, STATE_OFF);
                //_spriteRenderer.sprite = _platformYellowInactive;
            }
            else if (getType() == PLATFORM_BLUE)
            {
                _anim.runtimeAnimatorController = _platformControllers.getController(PLATFORM_BLUE, STATE_OFF);
                //_spriteRenderer.sprite = _platformBlueInactive;
            }

            if (getTimeState() >= 0.5f)
            {
                setState(STATE_OFF);
            }
        }
        #endregion

        #region STATE_SHUTDOWN
        if (getState() == STATE_SHUTDOWN)
        {
            _anim.runtimeAnimatorController = null;
            _spriteRenderer.sprite = _platformShutdown;            
        }

        if (getState() == STATE_TRANSITION_DONE)
        {
            _anim.runtimeAnimatorController = _platformControllers.getController(CGameConstants.COLOR_BASE, STATE_OFF);
            //_spriteRenderer.sprite = _platformDone;

            if (getTimeState() >= 0.5f)
            {
                setState(STATE_DONE);
            }
        }
        #endregion

        #region STATE_DONE
        if (getState() == STATE_DONE)
        {
            _spriteRenderer.sprite = _platformDone;
        }
        #endregion
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

    [System.Serializable]
    public class CPlatformController
    {
        public RuntimeAnimatorController _platformGreenInactiveAnim;
        public RuntimeAnimatorController _platformGreenActiveAnim;
        public RuntimeAnimatorController _platformRedInactiveAnim;
        public RuntimeAnimatorController _platformRedActiveAnim;
        public RuntimeAnimatorController _platformYellowInactiveAnim;
        public RuntimeAnimatorController _platformYellowActiveAnim;
        public RuntimeAnimatorController _platformBlueInactiveAnim;
        public RuntimeAnimatorController _platformBlueActiveAnim;
        public RuntimeAnimatorController _platformNeutralAnim;

        public RuntimeAnimatorController getController(int aPlatform, int aState)
        {
            if (aPlatform == CGameConstants.COLOR_GREEN)
            {
                if (aState == STATE_ON)
                {
                    return _platformGreenActiveAnim;
                }
                else
                {
                    return _platformGreenInactiveAnim;
                }
            }
            else if (aPlatform == CGameConstants.COLOR_RED)
            {
                if (aState == STATE_ON)
                {
                    return _platformRedActiveAnim;
                }
                else
                {
                    return _platformRedInactiveAnim;
                }
            }
            else if (aPlatform == CGameConstants.COLOR_YELLOW)
            {
                if (aState == STATE_ON)
                {
                    return _platformYellowActiveAnim;
                }
                else
                {
                    return _platformYellowInactiveAnim;
                }
            }
            else if (aPlatform == CGameConstants.COLOR_BLUE)
            {
                if (aState == STATE_ON)
                {
                    return _platformBlueActiveAnim;
                }
                else
                {
                    return _platformBlueInactiveAnim;
                }
            }
            else if (aPlatform == CGameConstants.COLOR_BASE)
            {
                return _platformNeutralAnim;
            }
            else
            {
                return null;
            }

        }
    }
}
