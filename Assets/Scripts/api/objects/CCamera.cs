﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCamera: CGameObject
{
    public const int WIDTH = CGameConstants.SCREEN_WIDTH;
    public const int HEIGHT = CGameConstants.SCREEN_HEIGHT;

    private float _max = -1080;

    public const int SPEED = 60;

    private CGameObject mGameObjectToFollow;

    private bool _center = false;
    private float _centerY;

    private bool _hasToGo = false;
    private bool _hasToGoPlayer = false;
    private int _goX;
    private int _goY;

    private bool _screenshake = false;
    private bool _wasScreenshakeLastFrame;
    private float _shakeStrength;
    private Vector2 _preShakePos;
    public float _shakeDuration;
    private float _timer;
    

    void Update()
    {
        apiUpdate();
    }
    public override void apiUpdate()
    {
        base.apiUpdate();


        if (_preShakePos != null && (_screenshake || _wasScreenshakeLastFrame))
        {
            setXY(_preShakePos.x, _preShakePos.y);
        }

        /*if (mGameObjectToFollow != null)
        {
            setX (mGameObjectToFollow.getX () - WIDTH / 2);
            setY(mGameObjectToFollow.getY() - HEIGHT / 4);// + HEIGHT / 2);
        }*/


        // Chequear que la camara no se vaya de los bordes.


        if (_center)
        {
            setY(_centerY);
            _center = false;
        }
    
        if (getGameObjectToFollow() != null)
        {
            if (_hasToGo)
            {
                
                CVector gotoPos = new CVector(_goX, _goY);
                
                if (getX() == _goX && getY() >= _goY)
                {
                    setVelXY(0, 0);                    
                    setXY(_goX, _goY);
                    
                }
                else
                {                                        
                    CVector actualPos = new CVector(getX(), getY());
                    CVector diference = gotoPos - actualPos;
                    diference = diference.normalize() * 800;
                    setVelXY(diference.x(), diference.y());
                }
                //setXY(mGoX, mGoY);
            }
            else if (_hasToGoPlayer)
            {
                CVector gotoPos = new CVector(_goX, _goY);

                if (getX() == _goX && getY() <= _goY)
                {
                    setVelXY(0, 0);
                    setY(mGameObjectToFollow.getY() + 78);
                    releaseToGo();
                    //setXY(_goX, _goY);
                }
                else
                {
                    CVector actualPos = new CVector(getX(), getY());
                    CVector diference = gotoPos - actualPos;
                    diference = diference.normalize() * 1200;
                    setVelXY(diference.x(), diference.y());
                }
            }else
            {
                setY(mGameObjectToFollow.getY() + 78);
                //    setX(getGameObjectToFollow().getX() - WIDTH / 2);
                //    setY(getGameObjectToFollow().getY() - HEIGHT / 2);
            }
        }
        checkBorder();

        _preShakePos = new Vector2(getX(), getY());

        _wasScreenshakeLastFrame = _screenshake;

        if (_screenshake)
        {
            _timer -= Time.deltaTime;
            Vector3 aux = Random.insideUnitCircle * _shakeStrength;
            setXY(getX() + aux.x, getY() + aux.y);
            if (_timer <= 0)
            {
                _screenshake = false;
            }
        }
        
    }
        
    public void setMax(float aMax)
    {
        _max = aMax;
    }
    private void checkBorder()
    {

        if (getX() != 960)//getX() <= 0)
        {
            setX(960);
        }

        if (getY() < -HEIGHT / 2)
        {
            setY(-HEIGHT / 2);
        }

        //if (getX() >= CTileMap.inst().WORLD_WIDTH - WIDTH)
        //{
        //    setX(CTileMap.inst().WORLD_WIDTH - WIDTH);
        //    onBorder = true;
        //}

        if (getY() >= _max)
        {
            setY(_max);
        }
               
    }

    public void setGameObjectToFollow(CGameObject aGameObjectToFollow)
    {
        mGameObjectToFollow = aGameObjectToFollow;
    }

    public CGameObject getGameObjectToFollow()
    {
        return mGameObjectToFollow;
    }

    // Centers camera for a frame.
    public void centerYCameraTo(float aY)
    {
        _center = true;
        _centerY = aY;
    }

    public void goTo(float aX, float aY)
    {
        _hasToGo = true;
        _goX = (int)aX;// - WIDTH / 2;
        _goY = (int)aY;// + HEIGHT / 2;
    }

    public void goToPlayer()// float aX, float aY)
    {
        _hasToGoPlayer = true;
        //_goX = (int)aX;
        //_goY = (int)aY;
        _goX = (int)getX();
        _goY = (int)mGameObjectToFollow.getY() + 78;
    }

    public void releaseToGo()
    {
        _hasToGo = false;
        _hasToGoPlayer = false;
        _goX = 0;
        _goY = 0;
    }

    public bool hasToGoPlayer()
    {
        return _hasToGoPlayer;
    }

    public void makeItShake(float aStrength)
    {
        _preShakePos = new Vector2(getX(), getY());
        _screenshake = true;
        _shakeStrength = aStrength;
        _timer = _shakeDuration;
    }
}
