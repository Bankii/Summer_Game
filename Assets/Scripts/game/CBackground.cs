using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBackground : CGameObject
{
    public float SPEED;
    private float startY;
    
    public CCamera _camera;
    //public CGameObject _otherBg;
    public bool _putDeco = false;
    
    private int _randomDecoration;
    private int _randomPiston;
    private int _randomPistonAngle;

    public CPiston _pistonPrefab;    
    public CGear _gear;
    public CCable _cableA;
    public CCable _cableB;


    void Start()
    {
        setVelY(SPEED);
        
        setHeight(1080);
        setWidth(1920);
    }

    void Update()
    {
        apiUpdate();

        //Moves the background when the player goes up
        if (_putDeco)
        {
            //setY(_otherBg.getY() + _otherBg.getHeight());

            _putDeco = false;

            //Set up decorations
            _randomDecoration = CMath.randomIntBetween(0, 100);

            if (_randomDecoration < 90 && _randomDecoration > 40)
            {
                int _rand = CMath.randomIntBetween(0, 1);
                if (_rand == 0)
                {
                    CCable cable = Instantiate(_cableA, new Vector3(getX() - 70, getY()), Quaternion.identity);
                    cable.setCamera(_camera);
                }
                else
                {
                    CCable cable = Instantiate(_cableB, new Vector3(getX() + getWidth() + 70, getY()), Quaternion.identity);
                    cable.setCamera(_camera);
                }
                
            }

            if (_randomDecoration >= 80)
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
                    CPiston piston = Instantiate(_pistonPrefab, new Vector3(getX() + getWidth() + 70, getY() - _randomPiston), Quaternion.Euler(0, 0, _randomPistonAngle));
                    piston.setCamera(_camera);
                }
            }
            else if (_randomDecoration < 79 && _randomDecoration >= 50)
            {
                _randomDecoration = CMath.randomIntBetween(0, 100);
                int rand = CMath.randomIntBetween(0, 1);

                if (_randomDecoration > 80)
                {                
                    if (rand == 0)
                    {
                        CGear gear = Instantiate(_gear, new Vector3(getX(), getY()), Quaternion.identity);
                        gear.setCamera(_camera);
                    }
                    else
                    {
                        CGear gear = Instantiate(_gear, new Vector3(getX() + getWidth(), getY()), Quaternion.identity);
                        gear.setCamera(_camera);
                    }
                    
                }
                else
                {
                    if (rand == 0)
                    {
                        CGear gear = Instantiate(_gear, new Vector3(getX(), getY()), Quaternion.identity);
                        gear.setCamera(_camera);
                        CGear gear1 = Instantiate(_gear, new Vector3(getX(), getY() - gear.getHeight() * 5), Quaternion.identity);
                        gear1.setCamera(_camera);
                        gear1.setCounterClockRotation();
                    }
                    else
                    {
                        CGear gear = Instantiate(_gear, new Vector3(getX() + getWidth(), getY()), Quaternion.identity);
                        gear.setCamera(_camera);
                        CGear gear1 = Instantiate(_gear, new Vector3(getX() + getWidth(), getY() - gear.getHeight() * 5), Quaternion.identity);
                        gear1.setCamera(_camera);
                        gear1.setCounterClockRotation();
                    }
                }                
            }

            
        }

        ////Moves the background when the player goes down
        //if (getY() - getHeight()*2 > _camera.getY() - CGameConstants.SCREEN_HEIGHT / 2)
        //{
        //    setY(_otherBg.getY() - _otherBg.getHeight());
        //}
    }
       
}
