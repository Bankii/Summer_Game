using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayer : CGameObject
{
    private const int STATE_IDLE = 0;
    private const int STATE_WALKING = 1;
    private const int STATE_JUMPING = 2;

    public float _horizontalSpeed;
    public float _verticalSpeed;
    public float _GRAVITY;

    public int _height;
    public int _width;

    private float _minX;
    private float _minY;
    private float _maxX;
    private float _maxY;

    void Start()
    {
        setState(STATE_IDLE);
    }
	void Update()
    {
        apiUpdate();
    }

    public override void apiUpdate()
    {
        base.apiUpdate();
        checkPoints();
        switch (getState())
        {
            case STATE_IDLE:

                break;
            case STATE_WALKING:
                break;
            case STATE_JUMPING:
                break;
            default:
                break;
        }
    }

    private void checkPoints()
    {
        // --------Checking the floor.--------
        float leftY = 0;
        float rightY = 0;
        RaycastHit hitInfo;

        // Down left.
        Physics.Raycast(getPos(),Vector3.down, out hitInfo);
        if (hitInfo.collider != null && hitInfo.collider.tag == "Platform")
        {
            leftY = hitInfo.point.x;
        }

        // Down right.
        Physics.Raycast(getPos() + new Vector3(0, _width, 0), Vector3.down, out hitInfo);
        if (hitInfo.collider != null && hitInfo.collider.tag == "Platform")
        {
            rightY = hitInfo.point.x;
        }

        // Setting the down variable.
        _minY = CGameConstants.SCREEN_HEIGHT - Mathf.Max(leftY, rightY);

        // --------Checking the Roof.--------
        leftY = CGameConstants.SCREEN_HEIGHT;
        rightY = CGameConstants.SCREEN_HEIGHT;

        // Up left.
        Physics.Raycast(getPos(), Vector3.up, out hitInfo);
        if (hitInfo.collider != null && hitInfo.collider.tag == "Platform")
        {
            leftY = hitInfo.point.x;
        }

        // Up right.
        Physics.Raycast(getPos() + new Vector3(0, _width, 0), Vector3.up, out hitInfo);
        if (hitInfo.collider != null && hitInfo.collider.tag == "Platform")
        {
            rightY = hitInfo.point.x;
        }

        // Setting the down variable.
        _maxY = CGameConstants.SCREEN_HEIGHT - Mathf.Max(leftY, rightY);

    }
}
