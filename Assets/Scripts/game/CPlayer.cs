using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayer : CGameObject
{
    public const int STATE_IDLE = 0;
    public const int STATE_WALKING = 1;
    public const int STATE_JUMPING = 2;
    public const int STATE_FALLING = 3;
    public const int STATE_DYING = 4;
    public const int STATE_CHARGING = 5;

    public const int COLOR_GREEN = 0;
    public const int COLOR_RED = 1;
    public const int COLOR_YELLOW = 2;
    public const int COLOR_BLUE = 3;
    public const int COLOR_BASE = 4;

    public float _horizontalSpeed;
    public float _verticalMaxSpeed;
    public float _verticalMinSpeed;
    public float _GRAVITY;
    private float _jumpMultiplyer = 0;
    public float _ACCEL_BOOST;
    
    public SpriteRenderer _spriteRenderer;
    public Animator _anim;
    public RuntimeAnimatorController _controllerBase;
    public RuntimeAnimatorController _controllerGreen;
    public RuntimeAnimatorController _controllerRed;
    public RuntimeAnimatorController _controllerYellow;
    public RuntimeAnimatorController _controllerBlue;


    public float _collitionOffsetLeft;
    public float _collitionOffsetRight;

    public int _height;
    public int _width;

    private float _minX;
    private float _minY;
    private float _maxX;
    private float _maxY;

    public string _idleAnim;
    public string _landingAnim;
    public string _walkingAnim;
    public string _chargingAnim;
    public string _jumpingAnim;
    public string _fallingAnim;
    public string _dyingAnim;
    

    private float _preBoostSpeed;
    
    private Vector3 _restartPos;

    void Start()
    {
        setState(STATE_IDLE);

        _restartPos = getPos();

        setColorPlayer(COLOR_BASE);

        setWidth(_width);
        setHeight(_height);
    }

	void Update()
    {
        apiUpdate();
    }

    void FixedUpdate()
    {
        checkPoints();
    }

    public override void apiUpdate()
    {
        base.apiUpdate();
        //_anim.SetFloat("Jump", getVelY());
        
        switch (getState())
        {
            case STATE_IDLE:
                //_anim.SetBool("isGrounded", true);
                // Checking if the player isn't against a wall.
                if (getX() != _maxX && getX() != _minX)
                { 
                    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                    {
                        setState(STATE_WALKING);
                        break;
                    }
                }
                // Checking that the input of the player is the oposite of the wall it's against.
                if (getX() == _maxX && Input.GetKey(KeyCode.LeftArrow))
                {
                    setState(STATE_WALKING);
                    break;
                }
                if (getX() == _minX && Input.GetKey(KeyCode.RightArrow))
                {
                    setState(STATE_WALKING);
                    break;
                }

                // Checking if there is no floor underneath.
                if (getY() - _height > _maxY)
                {
                    setState(STATE_FALLING);
                    break;
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    setState(STATE_CHARGING);
                    break;
                }
                break;

            case STATE_WALKING:
                
                if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                {
                    setState(STATE_IDLE);
                    break;
                }
                if (getX() + _width >= _maxX && Input.GetKey(KeyCode.RightArrow))
                {
                    setState(STATE_IDLE);
                    break;
                }
                if (getX() <= _minX && Input.GetKey(KeyCode.LeftArrow))
                {
                    setState(STATE_IDLE);
                    break;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    setState(STATE_CHARGING);
                    break;
                }
                // Set vel and flip according to the side.
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    setVelX(-_horizontalSpeed);
                    _spriteRenderer.flipX = true;
                    _spriteRenderer.gameObject.transform.position = new Vector3(getX() + _width, getY(), getZ());
                }
                else if(Input.GetKey(KeyCode.RightArrow))
                {
                    setVelX(_horizontalSpeed);
                    _spriteRenderer.flipX = false;
                    _spriteRenderer.gameObject.transform.position = getPos();
                }
                
                // Checking if there is no floor underneath.
                if (getY() - _height > _maxY)
                {
                    setState(STATE_FALLING);
                }
                break;

            case STATE_CHARGING:
                if (Input.GetKey(KeyCode.Space))
                {
                    //_anim.SetBool("isCharging", true);
                    _jumpMultiplyer += 0.04f;
                }
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    //_anim.SetBool("isCharging", false);
                    setState(STATE_JUMPING);
                }
                break;

            case STATE_JUMPING:
                //_anim.SetBool("isGrounded", false);
                if (getY() - _height <= _maxY && getVelY() != 0)
                {
                    setY(_maxY + _height);
                    setState(STATE_IDLE);
                }
                if (getVelY() <= 0)
                {
                    setState(STATE_FALLING);
                }
                // if no arrow is pressed then no movement on the X axis.
                if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                {
                    setVelX(0);
                }
                // Set vel and flip according to the side.
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    setVelX(-_horizontalSpeed);
                    _spriteRenderer.flipX = true;
                    _spriteRenderer.gameObject.transform.position = new Vector3(getX() + _width, getY(), getZ());
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    setVelX(_horizontalSpeed);
                    _spriteRenderer.flipX = false;
                    _spriteRenderer.gameObject.transform.position = getPos();
                }

                // if there are walls then no movement on the X axis.
                if (getX() + _width >= _maxX && Input.GetKey(KeyCode.RightArrow))
                {
                    setVelX(0);
                }
                else if (getX() <= _minX && Input.GetKey(KeyCode.LeftArrow))
                {
                    setVelX(0);
                }
                break;

            case STATE_FALLING:
                //_anim.SetBool("isGrounded", false);
                if (getY() - _height <= _maxY)
                {
                    setY(_maxY + _height);
                    setState(STATE_IDLE);
                    break;
                }
                if (getY() < -CGameConstants.SCREEN_HEIGHT)
                {
                    setState(STATE_DYING);
                    break;
                }
                // if no arrow is pressed then no movement on the X axis.
                if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                {
                    setVelX(0);
                }
                // Set vel and flip according to the side.
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    setVelX(-_horizontalSpeed);
                    _spriteRenderer.flipX = true;
                    _spriteRenderer.gameObject.transform.position = new Vector3(getX() + _width, getY(), getZ());
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    setVelX(_horizontalSpeed);
                    _spriteRenderer.flipX = false;
                    _spriteRenderer.gameObject.transform.position = getPos();
                }

                // if there are walls then no movement on the X axis.
                if (getX() + _width >= _maxX && Input.GetKey(KeyCode.RightArrow))
                {
                    setVelX(0);
                }
                else if (getX() <= _minX && Input.GetKey(KeyCode.LeftArrow))
                {
                    setVelX(0);
                }

                // Saving the last vertical speed before acceleration boost.
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    _preBoostSpeed = getVelY();
                }
                // Loading the previous speed.
                if (Input.GetKeyUp(KeyCode.DownArrow))
                {
                    setVelY(_preBoostSpeed);
                }
                // Acceleration boost if the down arrow is pressed.
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    setAccelY(_GRAVITY + _ACCEL_BOOST);
                }
                else if(!Input.GetKey(KeyCode.DownArrow))
                {
                    setAccelY(_GRAVITY);
                }

                break;

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
                    setColorPlayer(COLOR_BASE);
                }
                break;

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
                    _anim.Play(_landingAnim);
                }
                else
                {
                    _anim.Play(_idleAnim);
                }
                stopMove();
                break;
            case STATE_JUMPING:
                _anim.Play(_jumpingAnim);
                setVelY(_verticalMaxSpeed * _jumpMultiplyer);
                setAccelY(_GRAVITY);
                if (getVelY() < _verticalMinSpeed)
                {
                    setVelY(_verticalMinSpeed);
                }
                else if (getVelY() > _verticalMaxSpeed)
                {
                    setVelY(_verticalMaxSpeed);
                }
                _jumpMultiplyer = 0;
                break;
            case STATE_FALLING:
                _anim.Play(_fallingAnim);
                setAccelY(_GRAVITY);
                break;
            case STATE_WALKING:
                _anim.Play(_walkingAnim);
                break;
            case STATE_CHARGING:
                _anim.Play(_chargingAnim);
                stopMove();
                break;
            case STATE_DYING:
                _anim.Play(_dyingAnim);
                setVelX(0);
                setVelY(0);
                setAccelY(_GRAVITY);
                break;
            default:
                break;
        }
    }

    private void checkPoints()
    {
        float auxWidth = getX() + _width - _collitionOffsetRight;
        float auxX = getX() + _collitionOffsetLeft;
        
        Vector3 auxPos = new Vector3(auxX, getY(), getZ());


        // --------Checking the floor.--------
        float leftY = -CGameConstants.SCREEN_HEIGHT - _height - 10;
        float rightY = -CGameConstants.SCREEN_HEIGHT - _height - 10;
        RaycastHit hitInfo;

        // Down left.
        if (Physics.Raycast(auxPos, Vector3.down, out hitInfo))// && hitInfo.collider.tag == "Platform")
        {
            leftY = hitInfo.point.y;
        }

        // Down right.
        if (Physics.Raycast(new Vector3(auxWidth, getY(), getZ()), Vector3.down, out hitInfo) )
            //&& hitInfo.collider.tag == "Platform")
        {
            rightY = hitInfo.point.y;
        }
        // Setting the down variable.
        _maxY = Mathf.Max(rightY, leftY);

        // --------Checking the Roof.--------
        leftY = 0;// CGameConstants.SCREEN_HEIGHT;
        rightY = 0;// CGameConstants.SCREEN_HEIGHT;

        // Up left.
        Physics.Raycast(new Vector3(auxX,getY() - _height,getZ()), Vector3.up, out hitInfo, 1000f);
        if (hitInfo.collider != null && hitInfo.collider.tag == "Platform")
        {
            leftY = hitInfo.point.y;
        }

        // Up right.
        Physics.Raycast(new Vector3(auxWidth, getY() - _height, getZ()), Vector3.up, out hitInfo, 1000f);
        if (hitInfo.collider != null && hitInfo.collider.tag == "Platform")
        {
            rightY = hitInfo.point.y;
        }

        // Setting the down variable.
        _minY = Mathf.Min(leftY, rightY);

        // --------Checking the Right.--------
        float upX = CGameConstants.SCREEN_WIDTH;
        float downX = CGameConstants.SCREEN_WIDTH;

        // Down right.
        Physics.Raycast(getPos() + new Vector3(0,-_height, 0), Vector3.right, out hitInfo, 1000f);
        if (hitInfo.collider != null && hitInfo.collider.tag == "Platform")
        {
            downX = hitInfo.point.x;
        }

        // Up right.
        Physics.Raycast(getPos(), Vector3.right, out hitInfo, 1000f);
        if (hitInfo.collider != null && hitInfo.collider.tag == "Platform")
        {
            upX = hitInfo.point.x;
        }

        // Setting the down variable.
        _maxX = Mathf.Max(upX, downX);
        
        // --------Checking the Left.--------
        upX = 0;
        downX = 0;

        // Down left.
        Physics.Raycast(getPos() + new Vector3(_width, -_height, 0), Vector3.left, out hitInfo, 1000f);
        if (hitInfo.collider != null && hitInfo.collider.tag == "Platform")
        {
            downX = hitInfo.point.x;
        }

        // Up right.
        Physics.Raycast(getPos() + new Vector3(_width, 0, 0), Vector3.left, out hitInfo, 1000f);
        if (hitInfo.collider != null && hitInfo.collider.tag == "Platform")
        {
            upX = hitInfo.point.x;
        }

        // Setting the down variable.
        _minX = Mathf.Min(upX, downX);
    }

    public void setColorPlayer(int aColor)
    {
        if (aColor == COLOR_GREEN)
        {
            _anim.runtimeAnimatorController = _controllerBase;
        }
        else if (aColor == COLOR_RED)
        {
            _anim.runtimeAnimatorController = _controllerRed;
        }
        else if (aColor == COLOR_YELLOW)
        {
            _anim.runtimeAnimatorController = _controllerBase;
        }
        else if (aColor == COLOR_BLUE)
        {
            _anim.runtimeAnimatorController = _controllerRed;
        }
        else if (aColor == COLOR_BASE)
        {
            _anim.runtimeAnimatorController = _controllerBase;
        }

    }
}
