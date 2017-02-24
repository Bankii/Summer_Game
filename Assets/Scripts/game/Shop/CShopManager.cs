using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CShopManager : MonoBehaviour {

    public GameObject _skinPanel;

    public float _offsetX;

    private List<CShopPanel> _panels;

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
                rectTransf.localPosition = new Vector3(-450 + _offsetX * i, 220, 0);
            }
            else
            {
                rectTransf.localPosition = new Vector3(-450 + _offsetX * i, -167, 0);
            }
            
            rectTransf.sizeDelta = new Vector2(120, 254);
            rectTransf.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            _panels.Add(panel.GetComponent<CShopPanel>());
            CPlayerController controller = CSkinManager.inst.getSkin(i);
            _panels[i].setIndex(i);
            _panels[i].setPreview(controller._preview);
            _panels[i].setName(controller._name);
            _panels[i].setEquipped(_panels[i].getIndex() == CSaveLoad.equipped);
            _panels[i].setBought(CSaveLoad.isBought(i));
            _panels[i].setUnlockText(controller._unlockableAt.ToString());
            _panels[i].setUnlocked(CSaveLoad.bestScore >= controller._unlockableAt);
            _panels[i].setPrice(controller._price);
        }	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
