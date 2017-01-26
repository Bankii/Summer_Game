using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBackground : CGameObject
{
    public float SPEED;
    private float startY;

    private Transform _camera;
    public CGameObject _otherBg;

    void Start()
    {
        setVelY(SPEED);
        _camera = Camera.main.transform;
        //startY = transform.position.y;

        setHeight(1100);
        setWidth(1600);

    }

    void Update()
    {
        apiUpdate();
        if (getY() + getHeight() < _camera.position.y - CGameConstants.SCREEN_HEIGHT /2)
        {
            setY(_otherBg.getY() + _otherBg.getHeight());
        }
    }
}
