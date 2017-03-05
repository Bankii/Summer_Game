using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSkinManager : MonoBehaviour {

    static public CSkinManager inst;

    //public int _skinCount;

    //public CPlayerController _baseSkin;
    //public CPlayerController _skin1;

    public List<CPlayerController> _skins;

    private CPlayerController _equiped;

    [HideInInspector]
    public bool _hasInstantiated = false;

    private bool _load = true;

	// Use this for initialization
	void Start () {
        GameObject other = GameObject.Find("SkinsManager");
        if (other != null && other != gameObject)
        {
            Destroy(gameObject);
            _load = false;
            return;
        }

        CSaveLoad.load();

        DontDestroyOnLoad(gameObject);
        inst = this;

        equip(CSaveLoad.equipped);

        _hasInstantiated = true;
        
	}
	
    public void equip(int aIndex)
    {
        _equiped = _skins[aIndex];
        CSaveLoad.equipped = aIndex;
        //_equiped = _skins[aIndex];
        //if (_baseSkin._index == aIndex)
        //{
        //    _equiped = _baseSkin;
        //    Debug.Log("Equiped: " + _equiped._name);
        //    return;
        //}
        //else if (_skin1._index == aIndex)
        //{
        //    _equiped = _skin1;
        //    Debug.Log("Equiped: " + _equiped._name);
        //    return;
        //}
    }

    public CPlayerController getEquipedSkin()
    {
        return _equiped;
    }

    //public int getEquipedSkinIndex()
    //{
    //    return _equiped._index;
    //}

    public CPlayerController getSkin(int aIndex)
    {
        return _skins[aIndex];
        //if (_baseSkin._index == aIndex)
        //{
        //    return _baseSkin;
        //}
        //else if (_skin1._index == aIndex)
        //{
        //    return _skin1;
        //}
        //return null;
    }

    public int getSkinCount()
    {
        return _skins.Count;
    }

	// Update is called once per frame
	void Update () {
        if (CSaveLoad.equipped != _equiped._index)
        {
            equip(CSaveLoad.equipped);
        }
	}
    

    void OnDestroy()
    {
        if(_load)
            CSaveLoad.save();
    }
}
