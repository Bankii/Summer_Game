using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CShopPanel : MonoBehaviour {

    private int _index;
    public Image _preview;
    public Button _button;
    private bool _isBought = false;
    private bool _isEquipped = false;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        
        
    }

    public void setBought(bool aBool)
    {
        _isBought = aBool;
        if (_isBought)
        {
            _button.GetComponent<GUIText>().text = "Equip";
            _button.onClick.AddListener(equip);
        }
        else
        {
            _button.GetComponent<GUIText>().text = "Buy";
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
    }

    public void setIndex(int aIndex)
    {
        _index = aIndex;
    }

    public void setPreview(Sprite aPreview)
    {
        _preview.sprite = aPreview;
    }
}
