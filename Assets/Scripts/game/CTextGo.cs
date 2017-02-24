using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CTextGo : CGameObject {


    public const int STATE_OFF = 0;
    public const int STATE_GROWING = 1;
    public const int STATE_REDUCING = 2;
    public const int STATE_BIG_READY = 3;
    public const int STATE_BIG_GO = 4;
    
    public Text _text;
    private bool _isSizeBounce;
    private bool _isGrowing;
    private int _maxSize;
    private int _normalSize;
    private bool _isMaxSize;
    
    private int _speedSizeGrow;
    private int _speedSizeReduce;

    // Use this for initialization
    void Start () {
        setState(STATE_OFF);
	}
	
	// Update is called once per frame
	void Update ()
    {
        apiUpdate();
	}

    public override void apiUpdate()
    {
        base.apiUpdate();

        if (_isSizeBounce)
        {

            if (_text.fontSize > _normalSize && !_isGrowing && getState() == STATE_REDUCING)
            {
                _text.fontSize = _text.fontSize - _speedSizeReduce;
            }
            else if (_text.fontSize < _maxSize && _isGrowing && getState() == STATE_GROWING)
            {
                _text.fontSize = _text.fontSize + _speedSizeGrow;
            }
            else if (_text.fontSize <= _normalSize)
            {
                _text.fontSize = _normalSize;
                _isSizeBounce = false;
                setState(STATE_OFF);
                _text.fontSize = _normalSize;
            }
            else if (_text.fontSize >= _maxSize)
            {
                _isGrowing = false;
                if (!_isMaxSize)
                {
                    //setState(STATE_BIG_READY);
                    setState(STATE_BIG_GO);
                    _isMaxSize = true;
                }
                
            }

        }


        if (getState() == STATE_OFF)
        {
            //_text.text = "READY?";
            _text.enabled = false;
        }
        else if (getState() == STATE_GROWING)
        {
            _text.enabled = true;
        }
        else if (getState() == STATE_REDUCING)
        {
            _text.enabled = true;
        }
        //else if (getState() == STATE_BIG_READY)
        //{
        //    if (getTimeState() >= 0.5f)
        //    {
        //        setState(STATE_BIG_GO);
        //    }
        //}
        else if (getState() == STATE_BIG_GO)
        {
            CGame.inst().setComboPause(false);
            //_text.text = "GO!";
            if (getTimeState() >= 0.3f)
            {
                setState(STATE_REDUCING);
            }
        }
    }

    public void goSizeBounce(int aMaxSize, int aSpeedGrow, int aSpeedReduce)
    {
        setState(STATE_GROWING);
        _isSizeBounce = true;
        _isGrowing = true;
        _maxSize = aMaxSize;
        _normalSize = _text.fontSize;
        _speedSizeGrow = aSpeedGrow;
        _speedSizeReduce = aSpeedReduce;
        _isMaxSize = false;
    }
}
