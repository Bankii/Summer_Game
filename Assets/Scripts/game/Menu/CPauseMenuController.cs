using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CPauseMenuController : MonoBehaviour {

    public bool _isResume;
    public bool _isMenu;
    public bool _isParent;

    public Button _button;

	// Use this for initialization
	void Start () {
        if (_isResume)
        {
            _button.onClick.AddListener(OnResume);
        }
        if (_isMenu)
        {
            _button.onClick.AddListener(OnMenu);
        }
    }
	
    public void OnResume()
    {
        CGame.inst().setPause(false);
    }

    public void OnMenu()
    {
        SceneManager.LoadScene(0);
    }

	// Update is called once per frame
	void Update () {
        if (_isParent && !CGame.inst().getPause())
        {
            Destroy(gameObject);
        }
	}
}
