using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCamera: CGameObject
{
    public const int WIDTH = CGameConstants.SCREEN_WIDTH;
    public const int HEIGHT = CGameConstants.SCREEN_HEIGHT;

    private float _max = -540;

    public const int SPEED = 60;

    private CGameObject mGameObjectToFollow;

    private bool _center = false;
    private float _centerY;

    private bool _hasToGo = false;
    private float _goX;
    private float _goY;

    void Update()
    {
        apiUpdate();
    }
    public override void apiUpdate()
    {
        base.apiUpdate();
        if (mGameObjectToFollow != null)
        {
            //setX (mGameObjectToFollow.getX () - WIDTH / 2);
            setY(mGameObjectToFollow.getY() - HEIGHT / 4);// + HEIGHT / 2);
        }
        // Chequear que la camara no se vaya de los bordes.
        checkBorder();

        /*if (_center)
        {
            setY(_centerY);
            _center = false;
        }*/
    
        if (getGameObjectToFollow() != null)
        {
            if (_hasToGo)
            {
                CVector gotoPos = new CVector(_goX, _goY);
                if (getX() == _goX && getY() == _goY)
                {
                    setVelXY(0, 0);
                    setXY(_goX, _goY);
                }
                else
                {
                    CVector actualPos = new CVector(getX(), getY());
                    CVector diference = gotoPos - actualPos;
                    diference = diference.normalize() * 200;
                    setVelXY(diference.x(), diference.y());
                }
                //setXY(mGoX, mGoY);
            }
            /*else
            {
                setX(getGameObjectToFollow().getX() - WIDTH / 2);
                setY(getGameObjectToFollow().getY() - HEIGHT / 2);
            }*/
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

        if (getY() <= -HEIGHT / 2)
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
        _goX = aX - WIDTH / 2;
        _goY = aY - HEIGHT / 2;
    }
    public void releaseToGo()
    {
        _hasToGo = false;
        _goX = 0f;
        _goY = 0f;
    }
}
