using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlatform : CGameObject {

    public const int STATE_OFF = 0;
    public const int STATE_ON = 1;
    public const int STATE_SHUTDOWN = 2;
    public const int STATE_DONE = 3;
    public const int STATE_TRANSITION = 4;
    public const int STATE_TRANSITION_DONE = 5;
    public const int STATE_INITIAL = 6;

    public float _initialStateTime;

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

    private bool _isShutdown;

    [Space(10)]
    public GameObject _bright;
    private SpriteRenderer _brightRenderer;
    
    public Sprite _greenBright;
    public Sprite _redBright;
    public Sprite _yellowBright;
    public Sprite _blueBright;

    [Space(10)]
    public float _yLerpRange;
    public float _yLerpTime;
    private float _origY;
    private float _elapsedTime;
    private bool _goingUp = true;

    private float _origX;
    private float _scaleY;

    void Awake()
    {
        apiAwake();
        _platformFX = GetComponent<AudioSource>();

        _brightRenderer = _bright.GetComponent<SpriteRenderer>();


    }

    // Use this for initialization
    void Start () {

        _anim = GetComponentInChildren<Animator>();
        setWidth(PLATFORM_WIDTH);
        setHeight(PLATFORM_HEIGHT);
        setState(STATE_INITIAL);

        _isShutdown = false;

        _origY = getY();
        float random = CMath.randomFloatBetween(0.1f, 1);
        setY(getY() - _yLerpRange / 2 + _yLerpRange * random);
        _scaleY = getY();
        _elapsedTime = _yLerpTime *random;
        int boolRand = CMath.randomIntBetween(0, 1);
        _goingUp = boolRand == 1;
        _origX = getX();
    }

    void Update()
    {
        if (!CGame.inst().getPause())
        {
            apiUpdate();
        }
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
        if (CGame.inst()._PlatformLerp && getState() != STATE_INITIAL)
        {
            if (_goingUp)
            {
                _elapsedTime += Time.deltaTime;
            }
            else
                _elapsedTime -= Time.deltaTime;

            setY(Mathf.Lerp(_origY - _yLerpRange / 2, _origY + _yLerpRange / 2, _elapsedTime / _yLerpTime));
            if (_elapsedTime >= _yLerpTime)
            {
                _goingUp = false;
            }
            else if (_elapsedTime < 0)
            {
                _goingUp = true;
            }
        }

        #region STATE_OFF
        if (getState() == STATE_OFF)
        {
            _bright.SetActive(false);

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
        else if (getState() == STATE_ON)
        {
            _bright.SetActive(true);

            if (getType() == PLATFORM_GREEN)
            {
                _anim.runtimeAnimatorController = _platformControllers.getController(PLATFORM_GREEN, STATE_ON);
                _brightRenderer.sprite = _greenBright;
                //_spriteRenderer.sprite = _platformGreenActive;                
            }
            else if (getType() == PLATFORM_RED)
            {
                _anim.runtimeAnimatorController = _platformControllers.getController(PLATFORM_RED, STATE_ON);
                _brightRenderer.sprite = _redBright;
                //_spriteRenderer.sprite = _platformRedActive;
            }
            else if (getType() == PLATFORM_YELLOW)
            {
                _anim.runtimeAnimatorController = _platformControllers.getController(PLATFORM_YELLOW, STATE_ON);
                _brightRenderer.sprite = _yellowBright;
                //_spriteRenderer.sprite = _platformYellowActive;
            }
            else if (getType() == PLATFORM_BLUE)
            {
                _anim.runtimeAnimatorController = _platformControllers.getController(PLATFORM_BLUE, STATE_ON);
                _brightRenderer.sprite = _blueBright;
                //_spriteRenderer.sprite = _platformBlueActive;
            }

            if (getTimeState() >= 0.5f)
            {
                setState(STATE_TRANSITION);
            }
        }
        #endregion

        #region STATE_TRANSITION
        else if (getState() == STATE_TRANSITION)
        {
            _bright.SetActive(false);

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

            if (getTimeState() >= 0.2f)
            {
                setState(STATE_OFF);
            }
        }
        #endregion

        #region STATE_SHUTDOWN
        else if (getState() == STATE_SHUTDOWN)
        {
            _bright.SetActive(false);

            if (!_isShutdown)
            {
                _anim.runtimeAnimatorController = _platformControllers.getController(CGameConstants.COLOR_BASE, STATE_SHUTDOWN);
                _isShutdown = true;
            }
                        
            if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !_anim.IsInTransition(0))
            {
                _anim.runtimeAnimatorController = null;
                _spriteRenderer.sprite = null;
                enabled = false;
                Destroy(this.gameObject);
            }
                     
        }
        #endregion

        #region STATE_TRANSITION_DONE
        if (getState() == STATE_TRANSITION_DONE)
        {
            _bright.SetActive(false);

            _anim.runtimeAnimatorController = _platformControllers.getController(CGameConstants.COLOR_BASE, STATE_DONE);

            if (getTimeState() >= 0.5f)
            {
                setState(STATE_DONE);
            }
        }
        #endregion

        #region STATE_DONE
        else if (getState() == STATE_DONE)
        {
            _spriteRenderer.sprite = _platformDone;
            _bright.SetActive(false);
        }
        #endregion

        #region STATE_INITIAL
        else if (getState() == STATE_INITIAL)
        {
            _bright.SetActive(false);

            if (getTimeState() < _initialStateTime)
            {
                float aux = Mathf.Lerp(0, 1, getTimeState() / _initialStateTime);
                setScale(new Vector3(aux, aux, 1));
                setX(_origX + PLATFORM_WIDTH / 2 - PLATFORM_WIDTH / 2 * aux);
                setY(_scaleY - (PLATFORM_HEIGHT) / 2 + (PLATFORM_HEIGHT) / 2 * aux);
            }
            else
            {
                setX(_origX);
                setY(_scaleY);
                setState(STATE_OFF);
            }
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

    public override void setState(int aState)
    {
        base.setState(aState);
        setScale(new Vector3(1, 1, 1));
        if (getState() == STATE_INITIAL)
        {
            setScale(new Vector3(0, 0, 1));

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
        public RuntimeAnimatorController _platformNeutralDestructionAnim;

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
                if (aState == STATE_DONE)
                {
                    return _platformNeutralAnim; ;
                }
                else
                {
                    return _platformNeutralDestructionAnim;
                }
                
            }
            else
            {
                return null;
            }

        }
    }
}
