﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayer : CGameObject
{
    private const int STATE_IDLE = 0;
    private const int STATE_WALKING = 1;
    private const int STATE_JUMPING = 2;
    private const int STATE_FALLING = 3;
    private const int STATE_DYING = 4;

    public float _horizontalSpeed;
    public float _verticalSpeed;
    public float _GRAVITY;

    public Transform _spriteTransf;
    public SpriteRenderer _spriteRenderer;

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

    public override void apiUpdate()
    {
        base.apiUpdate();
        checkPoints();
        switch (getState())
        {
            case STATE_IDLE:
                if(getX() != _maxX && getX() != _minX)
                { 
                    if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        setState(STATE_WALKING);
                    }
                }
                if (getY() - _height > _maxY)
                {
                    setState(STATE_FALLING);
                }
                break;
            case STATE_WALKING:
                // setear velocidad y flipear segun lado.
                if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                {
                    setState(STATE_IDLE);
                }
                if (getPos().x + _width >= _maxX && Input.GetKey(KeyCode.RightArrow))
                {
                    setState(STATE_IDLE);
                }
                if (getPos().x <= _minX && Input.GetKey(KeyCode.LeftArrow))
                {
                    setState(STATE_IDLE);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    setVelX(_horizontalSpeed);
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    setVelX(-_horizontalSpeed);
                    _spriteRenderer.flipX = true;
                    _spriteTransf.position = new Vector3(transform.position.x + _width, transform.position.y, transform.position.z);
                }
                break;
            case STATE_JUMPING:
                if (getPos().y + _height >= _maxY)
                {
                    setState(STATE_IDLE);
                }
                if (getPos().y <= _minY)
                {
                    setY(_minY + 1);
                    setState(STATE_FALLING);
                }
                if (getPos().x + _width >= _maxX && Input.GetKey(KeyCode.RightArrow))
                {
                    // set velX 0.
                }
                if (getPos().x <= _minX && Input.GetKey(KeyCode.LeftArrow))
                {
                    // set velX 0.
                }
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
                setVelY(_verticalSpeed);
                setAccelY(_GRAVITY);
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
            Debug.Log(rightY);
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
    void OnDrawGizmos()
    {
        Gizmos.DrawRay(getPos(), Vector3.down *1000f);
    }
}