using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBackground : CGameObject
{
    public float SPEED;
    private float startY;
    
    public CCamera _camera;
    public CGameObject _otherBg;

    void Start()
    {
        setVelY(SPEED);

        setHeight(1080);
        setWidth(1920);
    }

    void Update()
    {
        apiUpdate();
        if (getY() + getHeight() < _camera.getY() + CGameConstants.SCREEN_HEIGHT /2)
        {
            setY(_otherBg.getY() + _otherBg.getHeight());
        }
    }
}
