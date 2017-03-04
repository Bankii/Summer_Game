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

    private int _randomPiston;
    private int _randomPistonAngle;

    public CPiston _pistonPrefab;
    

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

            _randomPiston = CMath.randomIntBetween(0, 100);
            if (_randomPiston >= 80)
            {
                _randomPiston = CMath.randomIntBetween(1, 2);
                _randomPistonAngle = CMath.randomIntBetween(60, 90);
                if (_randomPiston == 1)
                {
                    CPiston piston = Instantiate(_pistonPrefab, new Vector3(getX() - 70, getY() - _randomPiston), Quaternion.Euler(0, 0, -_randomPistonAngle));
                    piston.setCamera(_camera);
                }
                else
                {
                    CPiston piston = Instantiate(_pistonPrefab, new Vector3(getX() + _otherBg.getWidth() + 70, getY() - _randomPiston), Quaternion.Euler(0, 0, _randomPistonAngle));
                    piston.setCamera(_camera);
                }
                
            }             
        }

        //Moves the background when the player goes down
        if (getY() - getHeight()*2 > _camera.getY() - CGameConstants.SCREEN_HEIGHT / 2)
        {
            setY(_otherBg.getY() - _otherBg.getHeight());
        }
    }
       
}
