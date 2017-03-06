using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCoin : CGameObject {

    public const int STATE_NORMAL = 0;
    public const int STATE_GOING = 1;

    private SpriteRenderer _sr;
    private const float SPEED = 1700;

    void Start () {
        setState(STATE_NORMAL);

        _sr = GetComponent<SpriteRenderer>();
	}
	
	void Update () {

        apiUpdate();

	}

    public override void apiUpdate()
    {
        base.apiUpdate();

        if (CGame.inst().getStatePlatform() == CPlatform.STATE_DONE)
        {
            setState(STATE_GOING);
        }

        if (getState() == STATE_GOING)
        {
            CVector coinPos = new CVector(getX(), getY());
            CVector aux = new CVector(CGame.inst().getPlayer().getPos());
            aux.setX(aux.x() + CGame.inst().getPlayer().getWidth()/2);
            CVector vel = aux - coinPos;
                        
            if (vel.getLenght() <= 2)
            {
                setVelXY(0, 0);
                setXY(CGame.inst().getPlayer().getX(), CGame.inst().getPlayer().getY());
            }
            else
            {
                vel = vel.normalize();
                vel *= SPEED;
                setVel(vel);
            }
        }
    }

    public void setVisible(bool aBool)
    {
        _sr.enabled = aBool;
    }
}
