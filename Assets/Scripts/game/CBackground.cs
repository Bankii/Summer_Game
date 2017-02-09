using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBackground : CGameObject
{
    public float SPEED;
    private float startY;
    
    public CCamera _camera;
    public CGameObject _otherBg;

    private SpriteRenderer sp;

    void Start()
    {
        setVelY(SPEED);

        sp = GetComponent<SpriteRenderer>();

        setHeight(1080);
        setWidth(1920);
    }

    void Update()
    {
        apiUpdate();

        //Moves the background when the player goes up
        if (getY() + getHeight() < _camera.getY() + CGameConstants.SCREEN_HEIGHT /2)
        {
            setY(_otherBg.getY() + _otherBg.getHeight());                    
        }

        //Moves the background when the player goes down
        if (getY() - getHeight()*2 > _camera.getY() - CGameConstants.SCREEN_HEIGHT / 2)
        {
            setY(_otherBg.getY() - _otherBg.getHeight());
        }
    }
}
