using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CShopManager : MonoBehaviour {

    public GameObject _skinPanel;

    public float _offsetX;

    private static List<CShopPanel> _panels;

	// Use this for initialization
	void Start ()
    {
        _panels = new List<CShopPanel>();

        GameObject canvas = GameObject.Find("Canvas");
        for (int i = 0; i < CSkinManager.inst.getSkinCount(); i++)
        {
            GameObject panel = Instantiate(_skinPanel, canvas.transform);
            RectTransform rectTransf = panel.GetComponent<RectTransform>();
            if (i <= 3)
            {
                rectTransf.localPosition = new Vector3(-590 + _offsetX * i, 220, 0);
            }
            else
            {
                rectTransf.localPosition = new Vector3(-590 + _offsetX * (i -4), -220, 0);
            }
            
            rectTransf.sizeDelta = new Vector2(120, 254);
            rectTransf.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            _panels.Add(panel.GetComponent<CShopPanel>());
            CPlayerController controller = CSkinManager.inst.getSkin(i);
            _panels[i].setIndex(i);
            _panels[i]._anim.runtimeAnimatorController = controller._controllerBase;
            _panels[i]._anim.enabled = true;
            _panels[i]._anim.Play("ShopPreview");
            _panels[i]._anim.enabled = false;
            _panels[i].setName(controller._name);
            _panels[i].setEquipped(_panels[i].getIndex() == CSaveLoad.equipped);
            if (_panels[i].getIndex() == CSaveLoad.equipped)
            {
                equip(i);
            }
            _panels[i].setBought(CSaveLoad.isBought(i));
            _panels[i].setUnlockText(controller._unlockableAt.ToString());
            _panels[i].setUnlocked(CSaveLoad.bestScore >= controller._unlockableAt);
            _panels[i].setPrice(controller._price);
            _panels[i].setPreview(controller._preview);
        }
    }
	
    public static void equip(int aIndex)
    {
        for (int i = 0; i < _panels.Count; i++)
        {
            _panels[i].setEquipped(_panels[i].getIndex() == aIndex);
            if (_panels[i].getIndex() == aIndex)
            {
                _panels[i]._anim.enabled = true;
                _panels[i]._anim.Play("ShopPreview");
            }
            else
            {
                _panels[i]._anim.enabled = false;
            }
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
