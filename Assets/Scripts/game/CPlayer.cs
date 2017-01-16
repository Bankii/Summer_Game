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

    public float _horizontalSpeed;
    public float _verticalMaxSpeed;
    public float _verticalMinSpeed;
    public float _GRAVITY;
    private float _jumpMultiplyer = 0;
    public float _ACCEL_BOOST;

    public SpriteRenderer _spriteRenderer;
    public Animator _anim;

    public float _collitionOffsetLeft;
    public float _collitionOffsetRight;

    public int _height;
    public int _width;

    private float _minX;
    private float _minY;
    private float _maxX;
    private float _maxY;

    private string _idleAnim;
    private string _landingAnim;
    private string _walkingAnim;
    private string _chargingAnim;
    private string _jumpingAnim;
    private string _fallingAnim;
    public string _dyingAnim;

    public string _idleBaseAnim;
    public string _landingBaseAnim;
    public string _walkingBaseAnim;
    public string _chargingBaseAnim;
    public string _jumpingBaseAnim;
    public string _fallingBaseAnim;

    public auxiliarAnimations _colorAnimations;

    void Start()
    {
        setState(STATE_IDLE);
        _idleAnim = _idleBaseAnim;
        _landingAnim = _landingBaseAnim;
        _walkingAnim = _walkingBaseAnim;
        _chargingAnim = _chargingBaseAnim;
        _jumpingAnim = _jumpingBaseAnim;
        _fallingAnim = _fallingBaseAnim;

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

                // Acceleration boos if the down arrow is pressed.
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
                }
                break;

            default:
                break;
        }
    }

    public bool isGrounded()
    {
        return getState() == STATE_IDLE || getState() == STATE_WALKING;
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
                setVel(new CVector(Vector3.zero));
                setAccelY(0);
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
}

[System.Serializable]
public class auxiliarAnimations
{
    public string _idleGreenAnim;
    public string _landingGreenAnim;
    public string _walkingGreenAnim;
    public string _chargingGreenAnim;
    public string _jumpingGreenAnim;
    public string _fallingGreenAnim;

    public string _idleRedAnim;
    public string _landingRedAnim;
    public string _walkingRedAnim;
    public string _chargingRedAnim;
    public string _jumpingRedAnim;
    public string _fallingRedAnim;

    public string _idleYellowAnim;
    public string _landingYellowAnim;
    public string _walkingYellowAnim;
    public string _chargingYellowAnim;
    public string _jumpingYellowAnim;
    public string _fallingYellowAnim;

    [Space(10)]
    [Header("Blue Animations")]
    public string _idleBlueAnim;
    public string _landingBlueAnim;
    public string _walkingBlueAnim;
    public string _chargingBlueAnim;
    public string _jumpingBlueAnim;
    public string _fallingBlueAnim;
}
