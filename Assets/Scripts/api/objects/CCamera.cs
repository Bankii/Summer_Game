using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCamera: CGameObject
{
    public const int WIDTH = CGameConstants.SCREEN_WIDTH;
    public const int HEIGHT = CGameConstants.SCREEN_HEIGHT;

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
            setY(mGameObjectToFollow.getY());// + HEIGHT / 2);
        }
        // Chequear que la camara no se vaya de los bordes.
        checkBorder();
    }
        
    private void checkBorder()
    {
        if (getX() != 960)//getX() <= 0)
        {
            setX(960);
        }

        if (getY() <= -540)
        {
            setY(-540);
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
