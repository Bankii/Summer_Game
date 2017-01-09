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
    
    //private SpriteRenderer _spriteRenderer;

    //public Sprite _platformInactive;
    //public Sprite _platformActive;

    //public int _platformType;
    
    //private SpriteRenderer _spriteRendererGreen;
    //private SpriteRenderer _spriteRendererRed;
    //private SpriteRenderer _spriteRendererYellow;
    //private SpriteRenderer _spriteRendererBlue;


    /*public CPlatform(int aColor, GameObject aPlatformGameObject)
    {     
       
        //_platformType = aPlatformGameObject;

        setType(aColor);

        if (getType() == PLATFORM_GREEN)
        {
            //prefab GREEN
            //_platformType = Instantiate(aPlatformGameObject, new Vector3(1200, -600), Quaternion.identity);
            _spriteRenderer = _platformType.GetComponent<SpriteRenderer>();
            //_spriteRenderer.sprite = el sprite que quieras.
            setName("Platform_Green");
        }
        //else if (getType() == PLATFORM_RED)
        //{
        //    //prefab RED
        //    Instantiate(_platformType, new Vector3(1200 + PLATFORM_WIDTH, -600), Quaternion.identity);
        //    _spriteRendererRed = _platformType.GetComponent<SpriteRenderer>();
        //    setName("Platform_Red");
        //}
        //else if (getType() == PLATFORM_YELLOW)
        //{
        //    //prefab YELLOW
        //    Instantiate(_platformType, new Vector3(1200 + PLATFORM_WIDTH * 2, -600), Quaternion.identity);
        //    _spriteRendererYellow = _platformType.GetComponent<SpriteRenderer>();
        //    setName("Platform_Yellow");
        //}
        //else if (getType() == PLATFORM_BLUE)
        //{
        //    //prefab BLUE
        //    Instantiate(_platformType, new Vector3(1200 + PLATFORM_WIDTH * 3, -600), Quaternion.identity);
        //    _spriteRendererBlue = _platformType.GetComponent<SpriteRenderer>();
        //    setName("Platform_Blue");
        //}


    }*/

    // Use this for initialization
    void Start () {
        setState(STATE_OFF);
        //setType(_platformType);
        //_spriteRenderer = GetComponent<SpriteRenderer>();
        // load all frames in _platformSprites array
        //_platformSprite = Resources.LoadAll<Sprite>("Art/Colors_Placeholders");
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
            _spriteRenderer.sprite = _platformInactive;            
        }

        if (getState() == STATE_ON)
        {
            _spriteRenderer.sprite = _platformActive;
        }*/

    }


}
