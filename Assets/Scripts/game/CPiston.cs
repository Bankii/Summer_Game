using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPiston : CGameObject {

    private CCamera _camera;
    private SpriteRenderer _sr;
    
    public int _height;	

    void Start()
    {
        setHeight(_height);
        _sr = GetComponent<SpriteRenderer>();
    }

	void Update () {
        apiUpdate();        
	}

    public override void apiUpdate()
    {
        base.apiUpdate();

        if (_camera.getY() + _camera.getHeight() / 2 < getY() - getHeight())
        {
            if (_sr.enabled != false)
            {
                _sr.enabled = false;
            }
        }
        else if (_camera.getY() - _camera.getHeight() / 2 > getY() + getHeight())
        {
            if (_sr.enabled != false)
            {
                _sr.enabled = false;
            }
        }
        else
        {
            _sr.enabled = true;
        }
        
    }

    public void setCamera(CCamera aCamera)
    {
        _camera = aCamera;
    }
}
