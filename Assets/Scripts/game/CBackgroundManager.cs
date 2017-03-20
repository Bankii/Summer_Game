using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBackgroundManager : MonoBehaviour {

    public List<CBackground> _backgrounds;
    public CCamera _camera;

    private int _visibleBg1 = 0;
    private int _visibleBg2 = 1;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < _backgrounds.Count; i++)
        {
            if (i != _visibleBg1 && i != _visibleBg2)
            {
                _backgrounds[i].gameObject.SetActive(false);
            }
            else
                _backgrounds[i].gameObject.SetActive(true);
        }

        //_backgrounds[_visibleBg1]._otherBg = _backgrounds[_visibleBg2];
        //_backgrounds[_visibleBg2]._otherBg = _backgrounds[_visibleBg1];

    }
	
	// Update is called once per frame
	void Update ()
    {
        

        if (_backgrounds[_visibleBg1].getY() + _backgrounds[_visibleBg1].getHeight() < _camera.getY() + CGameConstants.SCREEN_HEIGHT / 2)
        {
            int randomIndex = CMath.randomIntBetween(0, _backgrounds.Count -1);
            while (randomIndex == _visibleBg2)
            {
                randomIndex = CMath.randomIntBetween(0, _backgrounds.Count -1);
            }
            _visibleBg1 = randomIndex;
            for (int i = 0; i < _backgrounds.Count; i++)
            {
                if (i != _visibleBg1 && i != _visibleBg2)
                {
                    _backgrounds[i].gameObject.SetActive(false);
                }
                else
                    _backgrounds[i].gameObject.SetActive(true);

            }

            _backgrounds[_visibleBg1].setY(_backgrounds[_visibleBg2].getY() + _backgrounds[_visibleBg2].getHeight());
            _backgrounds[_visibleBg1]._putDeco = true;
        }

        if (_backgrounds[_visibleBg2].getY() + _backgrounds[_visibleBg2].getHeight() < _camera.getY() + CGameConstants.SCREEN_HEIGHT / 2)
        {
            int randomIndex = CMath.randomIntBetween(0, _backgrounds.Count-1);
            while (randomIndex == _visibleBg1)
            {
                randomIndex = CMath.randomIntBetween(0, _backgrounds.Count-1);
            }
            _visibleBg2 = randomIndex;
            for (int i = 0; i < _backgrounds.Count; i++)
            {
                if (i != _visibleBg1 && i != _visibleBg2)
                {
                    _backgrounds[i].gameObject.SetActive(false);
                }
                else
                    _backgrounds[i].gameObject.SetActive(true);
            }

            _backgrounds[_visibleBg2].setY(_backgrounds[_visibleBg1].getY() + _backgrounds[_visibleBg1].getHeight());
            _backgrounds[_visibleBg2]._putDeco = true;

            
        }
        //Moves the background when the player goes down
        if (_backgrounds[_visibleBg1].getY() - _backgrounds[_visibleBg1].getHeight() * 2 > _camera.getY() - CGameConstants.SCREEN_HEIGHT / 2)
        {
            _backgrounds[_visibleBg1].setY(_backgrounds[_visibleBg2].getY() - _backgrounds[_visibleBg2].getHeight());
        }
        if (_backgrounds[_visibleBg2].getY() - _backgrounds[_visibleBg2].getHeight() * 2 > _camera.getY() - CGameConstants.SCREEN_HEIGHT / 2)
        {
            _backgrounds[_visibleBg2].setY(_backgrounds[_visibleBg1].getY() - _backgrounds[_visibleBg1].getHeight());
        }

    }
}
