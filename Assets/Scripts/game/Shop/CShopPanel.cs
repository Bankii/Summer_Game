using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CShopPanel : MonoBehaviour {

    private int _index;
    public Image _preview;
    public Button _button;
    public Text _name;
    private bool _isBought = false;
    private bool _isEquipped = false;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (CSaveLoad.equipped != _index)
        {
            setEquipped(false);
        }
        
    }

    public void setBought(bool aBool)
    {
        _isBought = aBool;
        if (_isBought)
        {
            Text text = _button.GetComponentInChildren<Text>();
            text.text = "Equip";
            _button.onClick.AddListener(equip);
        }
        else
        {
            Text text = _button.GetComponentInChildren<Text>();
            text.text = "Buy";
        }
    }

    public void setEquipped(bool aBool)
    {
        _isEquipped = aBool;
        if (_isEquipped)
        {
            _button.interactable = false;
        }
        else
        {
            _button.interactable = true;
        }
    }

    void equip()
    {
        CSkinManager.inst.equip(_index);
        setEquipped(true);
    }

    public void setIndex(int aIndex)
    {
        _index = aIndex;
    }

    public int getIndex()
    {
        return _index;
    }

    public void setPreview(Sprite aPreview)
    {
        _preview.sprite = aPreview;
    }

    public void setName(string aName)
    {
        _name.text = aName;
    }
}
