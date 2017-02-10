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
            rectTransf.localPosition = new Vector3(-696 + _offsetX * i, -40, 0);
            rectTransf.sizeDelta = new Vector2(120, 254);
            rectTransf.localScale = new Vector3(3, 3, 3);
            _panels.Add(panel.GetComponent<CShopPanel>());
            CPlayerController controller = CSkinManager.inst.getSkin(i);
            _panels[i].setIndex(i);
            _panels[i].setPreview(controller._preview);
            _panels[i].setName(controller._name);
            _panels[i].setEquipped(_panels[i].getIndex() == CSaveLoad.equipped);

            // TODO change this to the actual stuff once I manage buying things
            _panels[i].setBought(true);
        }	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
