using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayer : CGameObject
{
    private const int STATE_IDLE = 0;
    private const int STATE_WALKING = 1;
    private const int STATE_JUMPING = 2;
    private const int STATE_FALLING = 3;
    private const int STATE_DYING = 4;
    private const int STATE_CHARGING = 5;

    public float _horizontalSpeed;
    public float _verticalMaxSpeed;
    public float _verticalMinSpeed;
    public float _GRAVITY;
    private float _jumpMultiplyer = 0;

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

    void Start()
    {
        setState(STATE_IDLE);
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
        switch (getState())
        {
            case STATE_IDLE:
                // Checking if the player isn't against a wall.
                if(getX() != _maxX && getX() != _minX)
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
                    _jumpMultiplyer += 0.04f;
                }
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    setState(STATE_JUMPING);
                }
                break;
            case STATE_JUMPING:
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
                
                //if (getPos().x + _width >= _maxX && Input.GetKey(KeyCode.RightArrow))
                //{
                //    // set velX 0.
                //}
                //if (getPos().x <= _minX && Input.GetKey(KeyCode.LeftArrow))
                //{
                //    // set velX 0.
                //}
                break;
            case STATE_FALLING:
                if (getY() - _height <= _maxY)
                {
                    setY(_maxY + _height);
                    setState(STATE_IDLE);
                }
                break;
            case STATE_DYING:
                break;
            default:
                break;
        }
    }

    public override void setState(int aState)
    {
        base.setState(aState);
        switch (getState())
        {
            case STATE_IDLE:
                setVel(new CVector(Vector3.zero));
                setAccelY(0);
                break;
            case STATE_JUMPING:
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
                setAccelY(_GRAVITY);
                break;
            default:
                break;
        }
    }

    private void checkPoints()
    {
        // --------Checking the floor.--------
        float leftY = -CGameConstants.SCREEN_HEIGHT; ;
        float rightY = -CGameConstants.SCREEN_HEIGHT; ;
        RaycastHit hitInfo;

        // Down left.
        if (Physics.Raycast(getPos(), Vector3.down, out hitInfo))// && hitInfo.collider.tag == "Platform")
        {
            leftY = hitInfo.point.y;
        }

        // Down right.
        if (Physics.Raycast(new Vector3(getX() + _width, getY(), getZ()), Vector3.down, out hitInfo) )
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
        Physics.Raycast(new Vector3(getX(),getY() - _height,getZ()), Vector3.up, out hitInfo, 1000f);
        if (hitInfo.collider != null && hitInfo.collider.tag == "Platform")
        {
            leftY = hitInfo.point.y;
        }

        // Up right.
        Physics.Raycast(new Vector3(getX() + _width, getY() - _height, getZ()), Vector3.up, out hitInfo, 1000f);
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
