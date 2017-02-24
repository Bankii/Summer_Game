using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGearPause : CGameObject {

    // Use this for initialization
    public CButtonPause _buttonPause;

    private float _rotation;

    public RectTransform _gearTransform;

	void Start () {
        _rotation = 0;
    }

    public override void apiUpdate()
    {
        base.apiUpdate();
        if (_buttonPause.getVelX() != 0)
        {
            _gearTransform.rotation = Quaternion.Euler(new Vector3(0, 0, _rotation));

            _rotation -= _buttonPause.getVelX() / _buttonPause.getVelX();
        }
        

    }
    

    void Update () {
        apiUpdate();
	}
}
