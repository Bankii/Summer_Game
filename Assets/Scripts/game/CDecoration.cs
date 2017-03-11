using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDecoration : CGameObject {

    CCamera _camera;
    
    public SpriteRenderer _sr;

    // Use this for initialization
    void Start () {

        _sr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        apiUpdate();
	}

    public override void apiUpdate()
    {
        base.apiUpdate();

        if (_camera.getY() + _camera.getHeight() / 2 < getY() - getHeight()*1.5)
        {
            if (_sr.enabled != false)
            {
                _sr.enabled = false;
            }
        }
        else if (_camera.getY() - _camera.getHeight() / 2 > getY() + getHeight()*1.5)
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
