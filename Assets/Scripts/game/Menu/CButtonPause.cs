using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CButtonPause : CGameObject {
    
	// Use this for initialization

	void Start () {
        setVelX(1500);
	}

    public override void apiUpdate()
    {
        base.apiUpdate();
        if (getX() >= 960 / 2)
        {
            setVelX(0);
            setX(960/2);
        }

    }
    

    void Update () {
        apiUpdate();
	}
}
