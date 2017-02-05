using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSkinManager : MonoBehaviour {

    static public CSkinManager inst;

    public CPlayerController _baseSkin;
    public CPlayerController _skin1;

    private CPlayerController _equiped;

	// Use this for initialization
	void Start () {
        inst = this;
	}
	
    public void equip(int aIndex)
    {
        if (_baseSkin._index == aIndex)
        {
            _equiped = _baseSkin;
            return;
        }
        else if (_skin1._index == aIndex)
        {
            _equiped = _skin1;
            return;
        }
    }

    public CPlayerController getEquipedSkin()
    {
        return _equiped;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
