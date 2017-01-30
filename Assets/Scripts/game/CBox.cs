using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBox : CGameObject
{
    public const int STATE_IDLE = 0;
    public const int STATE_DIE = 1;

    public const float HEIGHT = 40;
    public const float WIDTH = 40;

    public string _dieAnim;

    private Animator _anim;

    // Use this for initialization
    void Start () {
        setState(STATE_IDLE);
        _anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        apiUpdate();
        if (getState() == STATE_DIE)
        {
            if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && _anim.GetCurrentAnimatorStateInfo(0).IsName(_dieAnim))
            {
                Destroy(gameObject);
            }
        }
	}

    public override void setState(int aState)
    {
        base.setState(aState);
        if (getState() == STATE_DIE)
        {
            if (_dieAnim != null)
            {
                _anim.Play(_dieAnim);
            }
        }
    }
}
