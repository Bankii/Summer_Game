using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPlayer : CGameObject
{
    public const int STATE_IDLE = 0;
    public const int STATE_WALKING = 1;
    public const int STATE_JUMPING = 2;
    public const int STATE_FALLING = 3;
    public const int STATE_DYING = 4;
    public const int STATE_CHARGING = 5;

    public float _horizontalSpeed;
    //public float _verticalMaxSpeed;
    //public float _verticalMinSpeed;
    public float _jumpSpeed;
    public float _maxHoldTime;
    public float _jumpReleaseSpeed;
    public float _GRAVITY_JUMP;
    public float _GRAVITY_FALL;
    private float _jumpMultiplyer = 0;
    public float _ACCEL_BOOST;

    public SpriteRenderer _spriteRenderer;
    public Animator _anim;

    public float _collitionOffsetLeft;
    public float _collitionOffsetRight;

    public float _collitionOffsetUp;

    public int _height;
    public int _width;

    private float _minX;
    private float _minY;
    private float _maxX = 1920;
    private float _maxY;

    private int _coins;  


    private float _preBoostSpeed;

    private Vector3 _restartPos;

    private AudioSource _playerFX;
    public AudioClip _jumpFX1;
    public AudioClip _jumpFX2;
    public AudioClip _gameOverFX;

    //public Text _coinUI;
    //private CText _coinUIScript;

    public CPlayerController _playerControllers;

    private LayerMask _platformMask = 1 << 8;

    public float _timeToMaxCharge;

    private bool _hasPeakedJump = false;

    void Start()
    {
        if (CSkinManager.inst != null)
        {
            _playerControllers = CSkinManager.inst.getEquipedSkin();
        }

        setState(STATE_IDLE);
        checkPoints();

        _restartPos = getPos();

        _playerFX = GetComponent<AudioSource>();
        
        //_coinUIScript = _coinUI.GetComponent<CText>();

        setColor(CGameConstants.COLOR_BASE);

        setWidth(_width);
        setHeight(_height);
    }

    void Update()
    {
        if (!CGame.inst().getPause())
        {
           apiUpdate();
        }
    }

    void FixedUpdate()
    {
        //if (getState() != STATE_IDLE && getState() != STATE_CHARGING)
        //{
            checkPoints();
        //}
    }

    public override void apiUpdate()
    {
        base.apiUpdate();
        //_anim.SetFloat("Jump", getVelY());

        switch (getState())
        {
            #region STATE IDLE
            case STATE_IDLE:
                //_anim.SetBool("isGrounded", true);
                // Checking if the player isn't against a wall.
                if (getX() != _maxX && getX() != _minX)
                {
                    //if (Input.GetKey("left") || Input.GetKey("right") || Input.GetKey("joystick button 8") || Input.GetKey("joystick button 9"))
                    //{
                    //    setState(STATE_WALKING);
                    //    break;
                    //}
                    if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0)
                    {
                        if (CGame.inst().isShowed() && (CGame.inst().getStateGO() == CTextGo.STATE_OFF || CGame.inst().getStateGO() == CTextGo.STATE_REDUCING) || CGame.inst().getStateGO() == CTextGo.STATE_BIG_GO)
                        {
                            setState(STATE_WALKING);
                            break;
                        }                        
                    }
                }
                // Checking that the input of the player is the oposite of the wall it's against.
                //if (getX() == _maxX && Input.GetKey("joystick button 8"))//Input.GetKey("left"))
                //{
                //    setState(STATE_WALKING);
                //    break;
                //}
                //if (getX() == _minX && Input.GetKey("joystick button 9"))//Input.GetKey("right"))
                //{
                //    setState(STATE_WALKING);
                //    break;
                //}

                if (getX() == _maxX && Input.GetAxisRaw("Horizontal") < 0)
                {
                    if (CGame.inst().isShowed() && (CGame.inst().getStateGO() == CTextGo.STATE_OFF || CGame.inst().getStateGO() == CTextGo.STATE_REDUCING) || CGame.inst().getStateGO() == CTextGo.STATE_BIG_GO)
                    {
                        setState(STATE_WALKING);
                        break;
                    }
                }
                if (getX() == _minX && Input.GetAxisRaw("Horizontal") > 0)
                {
                    if (CGame.inst().isShowed() && (CGame.inst().getStateGO() == CTextGo.STATE_OFF || CGame.inst().getStateGO() == CTextGo.STATE_REDUCING) || CGame.inst().getStateGO() == CTextGo.STATE_BIG_GO)
                    {
                        setState(STATE_WALKING);
                        break;
                    }
                }

                // Checking if there is no floor underneath in over 100 pixels.
                if (getY() - _height > _maxY && getY() - _height - _maxY > 100)
                {
                    setState(STATE_FALLING);
                    break;
                }
                // Checking if there is no floor underneath, below a 100 pixels, to correct to the platform.
                if (getY() - _height > _maxY && getY() - _height - _maxY <= 100)
                {
                    setY(_maxY + _height);
                }
                // Checking if the floor is higher.
                if (getY() - _height < _maxY)
                {
                    setY(_maxY + _height);
                }

                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 1") || Input.GetKeyDown("joystick button 0"))
                {
                    if (CGame.inst().isShowed() && (CGame.inst().getStateGO() == CTextGo.STATE_OFF || CGame.inst().getStateGO() == CTextGo.STATE_REDUCING) || CGame.inst().getStateGO() == CTextGo.STATE_BIG_GO)
                    {
                        playJumpFX();
                        setState(STATE_JUMPING);
                    }                    
                    //setState(STATE_CHARGING);
                    break;
                }
                break;
#endregion

            #region STATE WALKING
            case STATE_WALKING:

                //if (!Input.GetKey("left") && !Input.GetKey("right") && !Input.GetKey("joystick button 8") && !Input.GetKey("joystick button 9"))
                //{
                //    setState(STATE_IDLE);
                //    break;
                //}
                //if (getX() + _width >= _maxX && Input.GetKey("joystick button 9"))//Input.GetKey("right"))
                //{
                //    setState(STATE_IDLE);
                //    break;
                //}
                //if (getX() <= _minX && Input.GetKey("joystick button 8"))//Input.GetKey("left"))
                //{
                //    setState(STATE_IDLE);
                //    break;
                //}
                if (Input.GetAxisRaw("Horizontal") == 0)
                {
                    setState(STATE_IDLE);
                    break;
                }
                if (getX() + _width >= _maxX && Input.GetAxisRaw("Horizontal") > 0)
                {
                    setState(STATE_IDLE);
                    break;
                }
                if (getX() <= _minX && Input.GetAxisRaw("Horizontal") < 0)
                {
                    setState(STATE_IDLE);
                    break;
                }
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 1") || Input.GetKeyDown("joystick button 0"))
                {
                    if (CGame.inst().isShowed() && (CGame.inst().getStateGO() == CTextGo.STATE_OFF || CGame.inst().getStateGO() == CTextGo.STATE_REDUCING) || CGame.inst().getStateGO() == CTextGo.STATE_BIG_GO)
                    {
                        playJumpFX();
                        setState(STATE_JUMPING);
                    }
                    //setState(STATE_CHARGING);
                    break;
                }

                // Set vel and flip according to the side.
                //if (Input.GetKey("left") || Input.GetKey("joystick button 8"))
                //{
                //    setVelX(-_horizontalSpeed);
                //    _spriteRenderer.flipX = true;
                //    _spriteRenderer.gameObject.transform.position = new Vector3(getX() + _width, getY(), getZ());
                //}
                //else if (Input.GetKey("right") || Input.GetKey("joystick button 9"))
                //{
                //    setVelX(_horizontalSpeed);
                //    _spriteRenderer.flipX = false;
                //    _spriteRenderer.gameObject.transform.position = getPos();
                //}

                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    setVelX(-_horizontalSpeed);
                    _spriteRenderer.flipX = true;
                    _spriteRenderer.gameObject.transform.position = new Vector3(getX() + _width, getY(), getZ());
                }
                else if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    setVelX(_horizontalSpeed);
                    _spriteRenderer.flipX = false;
                    _spriteRenderer.gameObject.transform.position = getPos();
                }

                // Checking if there is no floor underneath in over 100 pixels.
                if (getY() - _height > _maxY && getY() - _height - _maxY > 100)
                {
                    setState(STATE_FALLING);
                    break;
                }
                // Checking if there is no floor underneath, below a 100 pixels, to correct to the platform.
                if (getY() - _height > _maxY && getY() - _height - _maxY <= 100)
                {
                    setY(_maxY + _height);
                }
                // Checking if the floor is higher.
                if (getY() - _height < _maxY)
                {
                    setY(_maxY + _height);
                }

                break;
            #endregion

            #region STATE CHARGING
            case STATE_CHARGING:
                if (Input.GetKey(KeyCode.Space) || Input.GetKey("joystick button 1") || Input.GetKey("joystick button 0"))
                {                    
                    _jumpMultiplyer += 1/_timeToMaxCharge * Time.deltaTime;
                    _anim.SetFloat("chargeValue", _jumpMultiplyer);
                }
                if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp("joystick button 1") || Input.GetKeyUp("joystick button 0"))
                {
                    //_anim.SetBool("isCharging", false);
                    playJumpFX();
                    setState(STATE_JUMPING);
                    goto case STATE_JUMPING;
                }
                break;
            #endregion

            #region STATE JUMPING
            case STATE_JUMPING:

                if ((Input.GetKey(KeyCode.Space) || Input.GetKey("joystick button 1") || Input.GetKey("joystick button 0")) 
                    && (getTimeState() <= _maxHoldTime))
                {
                    setVelY(_jumpSpeed);
                }
                if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp("joystick button 1") || Input.GetKeyUp("joystick button 0"))
                    || (getTimeState() > _maxHoldTime && !_hasPeakedJump))
                {
                    _hasPeakedJump = true;
                    setVelY(_jumpReleaseSpeed);
                }

                if (getVelY() <= 0)
                {
                    setState(STATE_FALLING);
                }

                #region Commented Code
                //if no arrow is pressed then no movement on the X axis.
                //if (!Input.GetKey("left") && !Input.GetKey("right") && !Input.GetKey("joystick button 8") && !Input.GetKey("joystick button 9"))
                //{
                //    setVelX(0);
                //}
                //// Set vel and flip according to the side.
                //else if (Input.GetKey("left") || Input.GetKey("joystick button 8"))
                //{
                //    setVelX(-_horizontalSpeed);
                //    _spriteRenderer.flipX = true;
                //    _spriteRenderer.gameObject.transform.position = new Vector3(getX() + _width, getY(), getZ());
                //}
                //else if (Input.GetKey("right") || Input.GetKey("joystick button 9"))
                //{
                //    setVelX(_horizontalSpeed);
                //    _spriteRenderer.flipX = false;
                //    _spriteRenderer.gameObject.transform.position = getPos();
                //}
                #endregion

                if (Input.GetAxisRaw("Horizontal") == 0)
                {
                    setVelX(0);
                }
                // Set vel and flip according to the side.
                else if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    setVelX(-_horizontalSpeed);
                    _spriteRenderer.flipX = true;
                    _spriteRenderer.gameObject.transform.position = new Vector3(getX() + _width, getY(), getZ());
                }
                else if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    setVelX(_horizontalSpeed);
                    _spriteRenderer.flipX = false;
                    _spriteRenderer.gameObject.transform.position = getPos();
                }


                //if there are walls then no movement on the X axis.
                //if (getX() + _width >= _maxX && Input.GetKey("joystick button 9"))//Input.GetKey("right"))
                //{
                //    setVelX(0);
                //}
                //else if (getX() <= _minX && Input.GetKey("joystick button 8"))//Input.GetKey("left"))
                //{
                //    setVelX(0);
                //}
                //break;

                if (getX() + _width >= _maxX && Input.GetAxisRaw("Horizontal") > 0)
                {
                    setVelX(0);
                }
                else if (getX() <= _minX && Input.GetAxisRaw("Horizontal") < 0)
                {
                    setVelX(0);
                }

                // Saving the last vertical speed before acceleration boost.
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    _preBoostSpeed = getVelY();
                }
                // Loading the previous speed.
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    setVelY(_preBoostSpeed);
                }
                // Acceleration boost if the down arrow is pressed.
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    setAccelY(_GRAVITY_JUMP + _ACCEL_BOOST);
                }
                else if (Input.GetAxisRaw("Vertical") == 0)
                {
                    setAccelY(_GRAVITY_JUMP);
                }
                break;
#endregion

            #region STATE FALLING
            case STATE_FALLING:
                //_anim.SetBool("isGrounded", false);
                if (getY() < -CGameConstants.SCREEN_HEIGHT)
                {                    
                    setState(STATE_DYING);
                    break;
                }
                else if (getY() - _height <= _maxY)
                {
                    setY(_maxY + _height - 1);
                    setState(STATE_IDLE);
                    break;
                }

                if (Input.GetAxisRaw("Horizontal") == 0)
                {
                    setVelX(0);
                }
                // Set vel and flip according to the side.
                else if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    setVelX(-_horizontalSpeed);
                    _spriteRenderer.flipX = true;
                    _spriteRenderer.gameObject.transform.position = new Vector3(getX() + _width, getY(), getZ());
                }
                else if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    setVelX(_horizontalSpeed);
                    _spriteRenderer.flipX = false;
                    _spriteRenderer.gameObject.transform.position = getPos();
                }

                // if there are walls then no movement on the X axis.
                if (getX() + _width >= _maxX && Input.GetAxisRaw("Horizontal") > 0)//Input.GetKey("right"))
                {
                    setVelX(0);
                }
                else if (getX() <= _minX && Input.GetAxisRaw("Horizontal") < 0)//Input.GetKey("left"))
                {
                    setVelX(0);
                }

                // Saving the last vertical speed before acceleration boost.
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxisRaw("Vertical") < 0)
                {
                    _preBoostSpeed = getVelY();
                }
                // Loading the previous speed.
                if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetAxisRaw("Vertical") < 0)
                {
                    setVelY(_preBoostSpeed);
                }
                // Acceleration boost if the down arrow is pressed.
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetAxisRaw("Vertical") < 0)
                {
                    setAccelY(_GRAVITY_FALL + _ACCEL_BOOST);
                }
                else if (!Input.GetKey(KeyCode.DownArrow) && Input.GetAxisRaw("Vertical") == 0)
                {
                    setAccelY(_GRAVITY_FALL);
                }

                break;
#endregion

            #region STATE DYING
            case STATE_DYING:
                if (getY() - _height <= -CGameConstants.SCREEN_HEIGHT - _height - 10)
                {
                    setY(_maxY + _height);
                    setVelY(0);
                    setAccelY(0);
                    CGame.inst().setRestart(true);
                }
                if (CGame.inst().isRestart())
                {
                    setPos(new CVector(_restartPos));
                    setState(STATE_IDLE);
                    setColor(CGameConstants.COLOR_BASE);
                }
                break;
            #endregion
                

            default:
                break;
        }
    }

    public bool isGrounded()
    {
        return getState() == STATE_IDLE || getState() == STATE_WALKING || getState() == STATE_CHARGING;
    }

    public override void setState(int aState)
    {
        int previousState = getState();
        base.setState(aState);
        switch (getState())
        {
            case STATE_IDLE:
                if (previousState == STATE_FALLING || previousState == STATE_JUMPING)
                {
                    _anim.Play(_playerControllers._landingAnim);
                }
                else
                {
                    _anim.Play(_playerControllers._idleAnim);
                }
                stopMove();
                break;
            case STATE_JUMPING:
                _anim.Play(_playerControllers._jumpingAnim);
                //setVelY(_verticalMaxSpeed * _jumpMultiplyer);
                setAccelY(_GRAVITY_JUMP);
                _hasPeakedJump = false;
                setVelY(_jumpSpeed);

                #region
                //if (getVelY() < _verticalMinSpeed)
                //{
                //    setVelY(_verticalMinSpeed);
                //}
                //else if (getVelY() > _verticalMaxSpeed)
                //{
                //    setVelY(_verticalMaxSpeed);
                //}
                #endregion

                _jumpMultiplyer = 0;
                break;
            case STATE_FALLING:
                _anim.Play(_playerControllers._fallingAnim);
                setAccelY(_GRAVITY_FALL);
                break;
            case STATE_WALKING:
                _anim.Play(_playerControllers._walkingAnim);
                break;

            case STATE_CHARGING:
                _anim.Play(_playerControllers._chargingAnim);
                GameObject particle = Resources.Load<GameObject>("Prefabs/JumpCharge");
                particle = Instantiate(particle, new Vector3(getX() + _width /2, getY() - _height, 0), Quaternion.Euler(-90,0,0));
                CDestroyOnPlayerNotCharging particleScript = particle.GetComponent<CDestroyOnPlayerNotCharging>();
                particleScript.setPlayer(this);
                stopMove();
                break;
            case STATE_DYING:
                _playerFX.clip = _gameOverFX;
                _playerFX.Play();
                _anim.Play(_playerControllers._dyingAnim);
                setVelX(0);
                setVelY(0);
                setAccelY(_GRAVITY_FALL);
                break;
            default:
                break;
        }
    }

    private void checkPoints()
    {
        float auxWidth = getX() + _width - _collitionOffsetRight;
        float auxX = getX() + _collitionOffsetLeft;
        float auxY = getY() - _collitionOffsetUp;

        //Vector3 auxPos = new Vector3(auxX, getY(), getZ());
        Vector3 auxPos = new Vector3(auxX, auxY, getZ());


        // --------Checking the floor.--------
        float leftY = -CGameConstants.SCREEN_HEIGHT - _height - 10;
        float rightY = -CGameConstants.SCREEN_HEIGHT - _height - 10;
        RaycastHit2D hitInfo;

        // Down left.
        hitInfo = Physics2D.Raycast(auxPos, Vector3.down, 1000, _platformMask);
        if (hitInfo.collider != null && hitInfo.collider.tag == "Platform")
        {
            leftY = hitInfo.point.y;
        }

        // Down right.
        //hitInfo = Physics2D.Raycast(new Vector3(auxWidth, getY(), getZ()), Vector3.down, 1000, _platformMask);
        hitInfo = Physics2D.Raycast(new Vector3(auxWidth, auxY, getZ()), Vector3.down, 1000, _platformMask);
        if (hitInfo.collider != null && hitInfo.collider.tag == "Platform")
        {
            rightY = hitInfo.point.y;
        }
        // Setting the down variable.
        _maxY = Mathf.Max(rightY, leftY);

        // --------Checking the Roof.--------
        leftY = 0;// CGameConstants.SCREEN_HEIGHT;
        rightY = 0;// CGameConstants.SCREEN_HEIGHT;

        // Up left.
        hitInfo = Physics2D.Raycast(new Vector3(auxX, getY() - _height, getZ()), Vector3.up, 1000, _platformMask);
        if (hitInfo.collider != null && hitInfo.collider.tag == "Platform")
        {
            leftY = hitInfo.point.y;
        }

        // Up right.
        hitInfo = Physics2D.Raycast(new Vector3(auxWidth, getY() - _height, getZ()), Vector3.up, 1000, _platformMask);
        if (hitInfo.collider != null && hitInfo.collider.tag == "Platform")
        {
            rightY = hitInfo.point.y;
        }

        // Setting the down variable.
        _minY = Mathf.Min(leftY, rightY);

        #region sides
        //// --------Checking the Right.--------
        //float upX = CGameConstants.SCREEN_WIDTH;
        //float downX = CGameConstants.SCREEN_WIDTH;

        //// Down right.
        //Physics.Raycast(getPos() + new Vector3(0, -_height, 0), Vector3.right, out hitInfo, 1000f);
        //if (hitInfo.collider != null && hitInfo.collider.tag == "Platform")
        //{
        //    downX = hitInfo.point.x;
        //}

        //// Up right.
        //Physics.Raycast(getPos(), Vector3.right, out hitInfo, 1000f);
        //if (hitInfo.collider != null && hitInfo.collider.tag == "Platform")
        //{
        //    upX = hitInfo.point.x;
        //}

        //// Setting the down variable.
        //_maxX = Mathf.Max(upX, downX);

        //// --------Checking the Left.--------
        //upX = 0;
        //downX = 0;

        //// Down left.
        //Physics.Raycast(getPos() + new Vector3(_width, -_height, 0), Vector3.left, out hitInfo, 1000f);
        //if (hitInfo.collider != null && hitInfo.collider.tag == "Platform")
        //{
        //    downX = hitInfo.point.x;
        //}

        //// Up right.
        //Physics.Raycast(getPos() + new Vector3(_width, 0, 0), Vector3.left, out hitInfo, 1000f);
        //if (hitInfo.collider != null && hitInfo.collider.tag == "Platform")
        //{
        //    upX = hitInfo.point.x;
        //}

        //// Setting the down variable.
        //_minX = Mathf.Min(upX, downX);
        #endregion
    }

    public void setColor(int aColor)
    {
        if (aColor == CGameConstants.COLOR_GREEN)
        {
            _anim.runtimeAnimatorController = _playerControllers.getController(CGameConstants.COLOR_GREEN);
        }
        else if (aColor == CGameConstants.COLOR_RED)
        {
            _anim.runtimeAnimatorController = _playerControllers.getController(CGameConstants.COLOR_RED);
        }
        else if (aColor == CGameConstants.COLOR_YELLOW)
        {
            _anim.runtimeAnimatorController = _playerControllers.getController(CGameConstants.COLOR_YELLOW);
        }
        else if (aColor == CGameConstants.COLOR_BLUE)
        {
            _anim.runtimeAnimatorController = _playerControllers.getController(CGameConstants.COLOR_BLUE);
        }
        else if (aColor == CGameConstants.COLOR_BASE)
        {
            _anim.runtimeAnimatorController = _playerControllers.getController(CGameConstants.COLOR_BASE);
        }

    }

    public void playJumpFX()
    {
        int randomJump = CMath.randomIntBetween(0, 1);
        if (randomJump == 0)
        {
            _playerFX.clip = _jumpFX1;
            _playerFX.Play();
        }
        else
        {
            _playerFX.clip = _jumpFX2;
            _playerFX.Play();
        }

    }
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Coin")
        {
            addCoins(CGame.inst().getCoinMultip());
            GameObject coinParticle = Resources.Load<GameObject>("Prefabs/Coin_Pick_Up_Particle");
            coinParticle = Instantiate(coinParticle, new Vector3(getX() + _width / 2, getY() - _height /2, -900), Quaternion.Euler(-90, 0, 0));
            Destroy(coll.gameObject);
        }
        if (coll.gameObject.tag == "Box")
        {
            CBox box = coll.gameObject.GetComponent<CBox>();
            if (box != null)
            {
                box.setState(CBox.STATE_DIE);
            }
        }
    }

    public void addCoins(int aCoins)
    {
        CSaveLoad.money += aCoins;
        //_coins += aCoins;
        //_coinUI.text = _coins.ToString();
        //_coinUIScript.sizeBounce(30, 1, 1);
    }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawRay(new Vector3(getX() + _collitionOffsetLeft, getY() - _collitionOffsetUp, -3), Vector3.down*100);

    }
}

[System.Serializable]
public class CPlayerController
{
    [Header("Shop Information")]
    public int _index;

    public Sprite _preview;

    public string _name;

    public int _unlockableAt;

    public int _price;

    [Space(10)]
    [Header("Player Skin Attributes")]
    public string _idleAnim = "Idle";
    public string _landingAnim = "Landing";
    public string _walkingAnim = "Walk";
    public string _chargingAnim = "Charge";
    public string _jumpingAnim = "Jump";
    public string _fallingAnim = "Fall";
    public string _dyingAnim = "Die";

    public RuntimeAnimatorController _controllerBase;
    public RuntimeAnimatorController _controllerGreen;
    public RuntimeAnimatorController _controllerRed;
    public RuntimeAnimatorController _controllerYellow;
    public RuntimeAnimatorController _controllerBlue;
       

    public RuntimeAnimatorController getController(int aController)
    {
        if (aController == CGameConstants.COLOR_GREEN)
        {
            return _controllerGreen;
        }
        else if (aController == CGameConstants.COLOR_RED)
        {
            return _controllerRed;
        }
        else if (aController == CGameConstants.COLOR_YELLOW)
        {
            return _controllerYellow;
        }
        else if (aController == CGameConstants.COLOR_BLUE)
        {
            return _controllerBlue;
        }
        else
        {
            return _controllerBase;
        }
    }



}
