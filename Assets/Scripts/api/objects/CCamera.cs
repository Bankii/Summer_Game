using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCamera: CGameObject
{
    public const int WIDTH = CGameConstants.SCREEN_WIDTH;
    public const int HEIGHT = CGameConstants.SCREEN_HEIGHT;

    private float _min = -540;

    private CGameObject mGameObjectToFollow;

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
    }
        
    public void setMin(float aMin)
    {
        _min = aMin;
    }
    private void checkBorder()
    {
        if (getX() != 960)//getX() <= 0)
        {
            setX(960);
        }

        // The camera will keep the lowest of between 
        // the object to follow's Y or the _min.
        // However it will not go lower than -Height/2.
        float min = Mathf.Min(_min, mGameObjectToFollow.getY() - HEIGHT / 4);
        if (min < -HEIGHT / 2)
        {
            min = -HEIGHT / 2;
        }
        if (getY() <= min)
        {
            setY(min);
        }

        //if (getX() >= CTileMap.inst().WORLD_WIDTH - WIDTH)
        //{
        //    setX(CTileMap.inst().WORLD_WIDTH - WIDTH);
        //    onBorder = true;
        //}

        //if (getY() >= CTileMap.inst().WORLD_HEIGHT - HEIGHT)
        //{
        //    setY(CTileMap.inst().WORLD_HEIGHT - HEIGHT);
        //}
    }

    public void setGameObjectToFollow(CGameObject aGameObjectToFollow)
    {
        mGameObjectToFollow = aGameObjectToFollow;
    }

    public CGameObject getGameObjectToFollow()
    {
        return mGameObjectToFollow;
    }
}
