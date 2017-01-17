using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameObject : MonoBehaviour {

    private Transform _transform;
    private CVector mPos;
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

    private float mMaxSpeed = 10000.0f;

    private float mMass = 1.0f;

    void Awake()
    {
        _transform = GetComponent<Transform>();
        mPos = new CVector();
        mPos._vector = _transform.position;
        mVel = new CVector();
        mAccel = new CVector();
    }

    public void setX(float aX)
    {
        _transform.position = new Vector3(aX, _transform.position.y, _transform.position.z);
        //mPos.setX(aX);
    }

    public void setY(float aY)
    {
        _transform.position = new Vector3(_transform.position.x, aY, _transform.position.z);
        //mPos.setY(aY);
    }

    public void setZ(float aZ)
    {
        _transform.position = new Vector3(_transform.position.x, _transform.position.y, aZ);
        //mPos.setZ(aZ);
    }

    public void setXY(float aX, float aY)
    {
        _transform.position = new Vector3(aX, aY, transform.position.z);
        //mPos.setX(aX);
        //mPos.setY(aY);
    }

    public void setPos(CVector aPos)
    {
        _transform.position = aPos._vector;
        //mPos = aPos;
    }
    
    public float getX()
    {
        return _transform.position.x;
        //return mPos.x();
    }

    public float getY()
    {
        return _transform.position.y;
        //return mPos.y();
    }

    public float getZ()
    {
        return _transform.position.z;
        //return mPos.z();
    }

    public Vector3 getPos()
    {
        //return mPos._vector;
        return _transform.position;
    }

    public void setVelX(float aVelX)
    {
        mVel.setX(aVelX);
        mVel.truncate(mMaxSpeed);
    }

    public void setVelY(float aVelY)
    {
        mVel.setY(aVelY);
        mVel.truncate(mMaxSpeed);
    }

    public void setVelXY(float aVelX, float aVelY)
    {
        mVel.setX(aVelX);
        mVel.setY(aVelY);
        mVel.truncate(mMaxSpeed);
    }

    public void setVelZ(float aVelZ)
    {
        mVel.setZ(aVelZ);
        mVel.truncate(mMaxSpeed);
    }

    public void setVel(CVector aVel)
    {
        mVel = aVel;
    }

    public float getVelX()
    {
        return mVel.x();
    }

    public float getVelY()
    {
        return mVel.y();
    }

    public float getVelZ()
    {
        return mVel.z();
    }

    public CVector getVel()
    {
        return mVel;
    }

    public void setAccel(CVector aAccel)
    {
        mAccel = aAccel;
    }

    public void setAccelX(float aAccelX)
    {
        mAccel.setX(aAccelX);
    }

    public void setAccelY(float aAccelY)
    {
        mAccel.setY(aAccelY);
    }

    public void setAccelZ(float aAccelZ)
    {
        mAccel.setZ(aAccelZ);
    }

    public void setAccelXY(float aAccelX, float aAccelY)
    {
        mAccel.setX(aAccelX);
        mAccel.setY(aAccelY);
    }

    public float getAccelX()
    {
        return mAccel.x();
    }

    public float getAccelY()
    {
        return mAccel.y();
    }

    public float getAccelZ()
    {
        return mAccel.z();
    }

    public CVector getAccel()
    {
        return mAccel;
    }


    virtual public void apiUpdate()
    {
        mTimeState = mTimeState + Time.deltaTime;

        mVel = mVel + mAccel * Time.deltaTime;
        mVel = mVel * mFriction;

        mVel.truncate(mMaxSpeed);

        _transform.position = _transform.position + mVel._vector * Time.deltaTime;
    }

    virtual public void render()
    {
    }

    /*virtual public void destroy()
    {
        mPos.destroy();
        mPos = null;
        mVel.destroy();
        mVel = null;
        mAccel.destroy();
        mAccel = null;
    }*/

    virtual public void setState(int aState)
    {
        mState = aState;
        mTimeState = 0.0f;
    }

    public int getState()
    {
        return mState;
    }

    public float getTimeState()
    {
        return mTimeState;
    }

    public void setDead(bool aIsDead)
    {
        mIsDead = aIsDead;
    }

    public bool isDead()
    {
        return mIsDead;
    }

    public void setRadius(int aRadius)
    {
        mRadius = aRadius;
    }

    public int getRadius()
    {
        return mRadius;
    }

    public void setType(int aType)
    {
        mType = aType;
    }

    public int getType()
    {
        return mType;
    }

    virtual public void setName(string aName)
    {
        mName = aName;
    }

    virtual public string getName()
    {
        return mName;
    }

    public void setWidth(int aWidth)
    {
        mWidth = aWidth;
    }

    public int getWidth()
    {
        return mWidth;
    }

    public void setHeight(int aHeight)
    {
        mHeight = aHeight;
    }

    public int getHeight()
    {
        return mHeight;
    }

    public void stopMove()
    {
        setVelXY(0.0f, 0.0f);
        setAccelXY(0.0f, 0.0f);
    }

    public bool collides(CGameObject aGameObject)
    {
        if (CMath.dist(getX(), getY(), aGameObject.getX(), aGameObject.getY()) < (getRadius() + aGameObject.getRadius()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void setFriction(float aFriction)
    {
        mFriction = aFriction;
    }

    public float getFriction()
    {
        return mFriction;
    }

    public void setMaxSpeed(float aMaxSpeed)
    {
        mMaxSpeed = aMaxSpeed;
    }

    public float getMaxSpeed()
    {
        return mMaxSpeed;
    }

    public float getMass()
    {
        return mMass;
    }

    public void setMass(float aMass)
    {
        mMass = aMass;
    }

}
