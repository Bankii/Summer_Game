using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLerp : MonoBehaviour {

    private CGameObject _object;

    public float _yLerpRange;
    public float _yLerpTime;
    private float _origY;
    private float _elapsedTime;
    private bool _goingUp = true;

    // Use this for initialization
    void Start ()
    {
        _object = gameObject.GetComponent<CGameObject>();

        _origY = _object.getY();
        float random = CMath.randomFloatBetween(0, 1);
        _object.setY(_object.getY() - _yLerpRange / 2 + _yLerpRange * random);
        _elapsedTime = _yLerpTime * random;
        int boolRand = CMath.randomIntBetween(0, 1);
        _goingUp = boolRand == 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (_goingUp)
        {
            _elapsedTime += Time.deltaTime;
        }
        else
            _elapsedTime -= Time.deltaTime;

        _object.setY(Mathf.Lerp(_origY - _yLerpRange / 2, _origY + _yLerpRange / 2, _elapsedTime / _yLerpTime));
        if (_elapsedTime >= _yLerpTime)
        {
            _goingUp = false;
        }
        else if (_elapsedTime < 0)
        {
            _goingUp = true;
        }
    }
}
