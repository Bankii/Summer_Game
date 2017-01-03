using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameObject : MonoBehaviour {

    private Transform _transform;
    private CVector mVel;
    private CVector mAccel;

    private bool mIsDead = false;

    private int mState = 0;
    private float mTimeState = 0.0f;

    private string mName;

    private int mRadius = 100;

    private int mType;

    private int mWidth = 100;
    private int mHeight = 100;

    private float mFriction = 1.0f;

    void Awake()
    {
        _transform = GetComponent<Transform>();
        mVel = new CVector();
        mAccel = new CVector();
    }

    public void setX(float aX)
    {
        _transform.position = new Vector3(aX, _transform.position.y, transform.position.z);
    }

    public void setY(float aY)
    {
        _transform.position = new Vector3(_transform.position.x, aY, transform.position.z);
    }

    public void setZ(float aZ)
    {
        _transform.position = new Vector3(_transform.position.x, _transform.position.y, aZ);
    }

    public void setXY(float aX, float aY)
    {
        _transform.position = new Vector3(aX, aY, transform.position.z);
    }
    public void setPos(CVector aPos)
    {
        _transform.position = aPos._vector;
    }
}
