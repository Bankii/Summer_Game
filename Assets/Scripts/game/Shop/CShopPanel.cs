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
    public Text _priceTxt;
    public Animator _anim;
    private int _price;
    private bool _isUnlocked = false;
    private bool _isBought = false;
    private bool _isEquipped = false;

    private AudioSource _panelAudio;

    public AudioClip _buildFX;
    public AudioClip _equipFX;

	// Use this for initialization
	void Start () {
        _panelAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CSaveLoad.equipped != _index)
        {
            setEquipped(false);
        }
        if (_isUnlocked)
        {
            if (!_isBought)
            {
                _button.interactable = CSaveLoad.money >= _price;
            }
            else
            {
                if (_isEquipped)
                {
                    _button.interactable = false;
                }
                else
                {
                    _button.interactable = true;
                }
            }
        }
        
    }

    public void setBought(bool aBool)
    {
        _isBought = aBool;
        if (_isEquipped)
        {
            _isBought = true;
            if (!CSaveLoad.isBought(_index))
            {
                CSaveLoad.bought.Add(_index);
            }

        }
        if (_isBought)
        {
            Text text = _button.GetComponentInChildren<Text>();
            text.text = "EQUIP";
            if (_isEquipped)
            {
                text.text = "EQUIPPED";
                text.fontSize = 15;
            }
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(equip);
        }
        else
        {
            Text text = _button.GetComponentInChildren<Text>();
            text.text = "BUILD";
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(buy);
            if (CSaveLoad.money < _price)
            {
                _button.interactable = false;
            }
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
            Text text = _button.GetComponentInChildren<Text>();
            text.text = "EQUIPPED";
            text.fontSize = 15;
        }
        else
        {
            if (_isBought)
            {
                Text text = _button.GetComponentInChildren<Text>();
                text.text = "EQUIP";
                text.fontSize = 20;
            }
        }
    }

    void equip()
    {
        CSkinManager.inst.equip(_index);
        CShopManager.equip(_index);
        _panelAudio.clip = _equipFX;
        _panelAudio.Play();        
    }

    void buy()
    {
        if (CSaveLoad.money >= _price)
        {
            CSaveLoad.money -= _price;
            setBought(true);
            CSaveLoad.bought.Add(_index);
            _panelAudio.clip = _buildFX;
            _panelAudio.Play();
        }
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
        _price = aPrice;
        _priceTxt.text = aPrice.ToString();
    }
}
