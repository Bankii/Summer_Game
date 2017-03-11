using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGear : CDecoration {

    private float _rotation;
    public Sprite _gear01;
    public Sprite _gear02;
    public Sprite _gear03;

    private bool _counterClockRot = false;

    // Use this for initialization
    void Start () {

        _rotation = 0;
                
        int _randomNum = CMath.randomIntBetween(0, 2);

        switch (_randomNum)
        {
            case 0:
                _sr.sprite = _gear01;
                setHeight(819);
                break;
            case 1:
                _sr.sprite = _gear02;
                setHeight(523);
                break;
            case 2:
                _sr.sprite = _gear03;
                setHeight(315);
                break;
            default:
                break;
        }
        
		
	}
	
	// Update is called once per frame
	void Update () {
        apiUpdate();       

    }

    public override void apiUpdate()
    {
        base.apiUpdate();

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, _rotation));

        if (_counterClockRot)
        {
            _rotation += 1;

            if (_rotation > 359)
            {
                _rotation = 0;
            }
        }
        else
        {
            _rotation -= 1;

            if (_rotation < -359)
            {
                _rotation = 0;
            }
        }

        

    }

    public void setCounterClockRotation()
    {
        _counterClockRot = true;
    }
    
}
