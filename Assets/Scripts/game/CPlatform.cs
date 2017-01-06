using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlatform : CGameObject {

    private const int STATE_OFF = 0;
    private const int STATE_ON = 1;

    public const int PLATFORM_GREEN = 0;
    public const int PLATFORM_RED = 1;
    public const int PLATFORM_YELLOW = 2;
    public const int PLATFORM_BLUE = 3;

    private const int PLATFORM_HEIGHT = 40;
    private const int PLATFORM_WIDTH = 85;

    GameObject _platformType;
    public Sprite[] _platformSprite;
    SpriteRenderer _spriteRendererGreen;
    SpriteRenderer _spriteRendererRed;
    SpriteRenderer _spriteRendererYellow;
    SpriteRenderer _spriteRendererBlue;


    public CPlatform(int aColor, GameObject aPlatformGameObject)
    {     
           
        _platformType = aPlatformGameObject;

        setType(aColor);

        if (getType() == PLATFORM_GREEN)
        {
            //prefab GREEN
            Instantiate(_platformType, new Vector3(1200,-600), Quaternion.identity);
            _spriteRendererGreen = _platformType.GetComponent<SpriteRenderer>();
            Debug.Log("Constructor Renderer Green " + _spriteRendererGreen);
            setName("Platform_Green");            
        }
        else if (getType() == PLATFORM_RED)
        {
            //prefab RED
            Instantiate(_platformType, new Vector3(1200 + PLATFORM_WIDTH, -600), Quaternion.identity);
            _spriteRendererRed = _platformType.GetComponent<SpriteRenderer>();
            Debug.Log("Constructor Renderer Red " + _spriteRendererGreen);
            setName("Platform_Red");
        }
        else if (getType() == PLATFORM_YELLOW)
        {
            //prefab YELLOW
            Instantiate(_platformType, new Vector3(1200 + PLATFORM_WIDTH *2, -600), Quaternion.identity);
            _spriteRendererYellow = _platformType.GetComponent<SpriteRenderer>();
            setName("Platform_Yellow");
        }
        else if (getType() == PLATFORM_BLUE)
        {
            //prefab BLUE
            Instantiate(_platformType, new Vector3(1200 + PLATFORM_WIDTH * 3, -600), Quaternion.identity);
            _spriteRendererBlue = _platformType.GetComponent<SpriteRenderer>();
            setName("Platform_Blue");
        }

        
    }



    public GameObject getPlatformType()
    {
        return _platformType;
    }

    public Sprite[] getPlatformSprites()
    {
        return _platformSprite;
    }


    // Use this for initialization
    void Start () {
        setState(STATE_OFF);        
        // load all frames in _platformSprites array
        _platformSprite = Resources.LoadAll<Sprite>("Art/Colors_Placeholders");
    }

    void Update()
    {
        apiUpdate();
    }
    

    public override void apiUpdate()
    {
        base.apiUpdate();
        /*if (getState() == STATE_OFF)
        {
            Debug.Log("OFF " + getType());
            if (getType() == PLATFORM_GREEN)
            {
                _spriteRendererGreen.sprite = _platformSprite[0];
                Debug.Log("State OFF Renderer Green " + _spriteRendererGreen);
            }
            else if (getType() == PLATFORM_RED)
            {
                _spriteRendererRed.sprite = _platformSprite[2];
                Debug.Log("State OFF Renderer Red " + _spriteRendererRed);
            }
            else if (getType() == PLATFORM_YELLOW)
            {
                _spriteRendererYellow.sprite = _platformSprite[4];
            }
            else if (getType() == PLATFORM_BLUE)
            {
                _spriteRendererBlue.sprite = _platformSprite[6];
            }
        }
        if (getState() == STATE_ON)
        {
            Debug.Log("ON");
            if (getType() == PLATFORM_GREEN)
            {
                _spriteRendererGreen.sprite = _platformSprite[1];
            }
            else if (getType() == PLATFORM_RED)
            {
                _spriteRendererRed.sprite = _platformSprite[3];
            }
            else if (getType() == PLATFORM_YELLOW)
            {
                _spriteRendererYellow.sprite = _platformSprite[5];
            }
            else if (getType() == PLATFORM_BLUE)
            {
                _spriteRendererBlue.sprite = _platformSprite[7];
            }
        }*/

    }
}
