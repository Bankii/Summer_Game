using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CShopPanel : MonoBehaviour {

    private int _index;
    public Image _preview;
    public GameObject _lockImg;
    public Button _button;
    public Text _name;
    public Text _unlocksAtText;
    public Text _price;
    private bool _isUnlocked = false;
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

    public void setUnlocked(bool aUnlocked)
    {
        _isUnlocked = aUnlocked;
        if (_isUnlocked)
        {
            _lockImg.SetActive(false);
            _button.interactable = true;
        }
        else
        {
            _lockImg.SetActive(true);
            _button.interactable = false;
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

    public void setUnlockText(string aText)
    {
        _unlocksAtText.text = aText;
    }

    public void setPrice(int aPrice)
    {
        _price.text = aPrice.ToString();
    }
}
