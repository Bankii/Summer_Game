using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CVector
{
    public Vector3 _vector = Vector3.zero;

    // Constructor
    public CVector()
    {
        _vector = Vector3.zero;
    }

    // Constructor

    public CVector(float aX, float aY)
    {
        _vector = new Vector3(aX, aY, 0);
    }

    // Constructor
    public CVector(float aX, float aY, float aZ)
    {
        _vector = new Vector3(aX, aY, aZ);
    }

    // Constructor
    public CVector(Vector3 aVector)
    {
        _vector = aVector;
    }

    // Constructor
    // Creates a vector with the given angle and magnitude on the X and Y axis.
    // The bool is to diferienciate it from another constructor.
    public CVector(float aAngle, float aMag, bool aBool = false)
    {
        float radian = aAngle * Mathf.Deg2Rad;
        float x = aMag * Mathf.Cos(radian);
        float y = aMag * Mathf.Sin(radian);
        _vector = new Vector3(x, y, 0);
    }

    public float x()
    {
        return _vector.x;
    }

    public float y()
    {
        return _vector.y;
    }

    public float z()
    {
        return _vector.y;
    }

    public void setX(float aX)
    {
        _vector = new Vector3(aX, _vector.y, _vector.z);
    }

    public void setY(float aY)
    {
        _vector = new Vector3(_vector.x, aY, _vector.z);
    }

    public void setZ(float aZ)
    {
        _vector = new Vector3(_vector.x, _vector.y, aZ);
    }

    public static CVector operator *(CVector aVector, float aScalar)
    {
        //return new CVector(aVector._vector * aScalar);
        return new CVector(aVector._vector.x * aScalar, aVector._vector.y * aScalar, aVector._vector.z * aScalar);
    }

    public static CVector operator +(CVector aVector1, CVector aVector2)
    {
        //return new CVector(aVector1._vector + aVector2._vector);
        return new CVector(aVector1._vector.x + aVector2._vector.x, aVector1._vector.y + aVector2._vector.y, aVector1._vector.z + aVector2._vector.z);
    }

    public static CVector operator -(CVector aVector1, CVector aVector2)
    {
        //return new CVector(aVector1._vector - aVector2._vector);
        return new CVector(aVector1._vector.x - aVector2._vector.x, aVector1._vector.y - aVector2._vector.y, aVector1._vector.z - aVector2._vector.z);
    }

    // Generates a copy of this vector.
    public CVector Clone()
    {
        return new CVector(x(), y(), z());
    }

    public CVector zero()
    {
        _vector = Vector3.zero;
        return this;
    }

    // Returns true if the vector is Zero.
    public bool isZero()
    {
        return x() == 0.0f && y() == 0.0f && z() == 0.0f;
    }

    // Set the lenght or magnitude of this vector. 2D
    // Changing the lenght will change the x and the y, but not the angle of this vector.
    public void setLenght(float aLenght)
    {
        float angle = getAngle();
        _vector.x = Mathf.Cos(angle) * aLenght;
        _vector.y = Mathf.Sin(angle) * aLenght;
    }

    public float getLenght()
    {
        return Mathf.Sqrt(getLenghtSquared());
    }

    // Get the lenght of this vector, squared.
    public float getLenghtSquared()
    {
        return _vector.x * _vector.x + _vector.y * _vector.y;
    }

    // Sets the angle of the vector (angle in radians). 2D.
    // Changing the angle changes the x and y but retains the same lenght.
    public void setAngle(float aAngle)
    {
        float lenght = getLenght();
        _vector.x = Mathf.Cos(aAngle) * lenght;
        _vector.y = Mathf.Sin(aAngle) * lenght;
    }

    // Gets the angle of the vector in radians. 
    public float getAngle()
    {
        return Mathf.Atan2(_vector.y, _vector.x);
    }

    // Normalizes the vector. Equivalent to setting the lenght to one, but more efficient.
    // Returns a reference to this vector.
    public CVector Normalize()
    {
        _vector.Normalize();
        return this;
    }

    public CVector truncate(float aMax)
    {
        setLenght(Mathf.Min(aMax, getLenght()));
        return this;
    }

    // Reverses the direction of this vector.
    // Returns a reference to this vector.
    public CVector reverse()
    {
        _vector.x = -_vector.x;
        _vector.y = -_vector.y;
        return this;
    }

    // Calculates the dot product of this vector and another given vector.
    // Returns the dot product of this vector and the one passed in as a parameter.
    public float dotProd(CVector aVector)
    {
        return _vector.x * aVector._vector.x + _vector.y * aVector._vector.y;
    }

    // Whether or not this vector is normalized (if its lenght is equal to one).
    // Returns true if lenght is one, otherwise false.
    public bool isNormalized()
    {
        return getLenght() == 1.0f;
    }

    // Generates a copy of this vector.
    public CVector clone()
    {
        return new CVector(_vector);
    }

    // Calculates the angle between two vectors.
    // Returns: The angle between the two given vectors.
    public static float getAngleBetween(CVector aVector1, CVector aVector2)
    {
        if (!aVector1.isNormalized())
            aVector1.clone().Normalize();

        if (!aVector2.isNormalized())
            aVector2.clone().Normalize();

        return Mathf.Acos(aVector1.dotProd(aVector2));
    }

    // Generates a string representation of this vector.
    // Returns: a string with the description of the vector.
    public string toString()
    {
        return "[CVector (x:" + _vector.x + " y:" + _vector.y + " z:" + _vector.z + "]";
    }
}
