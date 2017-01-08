using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CGame : MonoBehaviour 
{
	static private CGame mInstance;
	private CGameState mState;

	private CCamera mCamera;
	private CPlayer mPlayer;
    //private CShip mShip;

    private List<int> _simonSecuence;
    private int _randomNum;
    private int _difficulty;
    private bool _isSolved;
    private bool _isGameOver; //testing

    public GameObject _platformGreen;
    public GameObject _platformRed;
    public GameObject _platformYellow;
    public GameObject _platformBlue;
    
    private const int PLATFORM_HEIGHT = 40;
    private const int PLATFORM_WIDTH = 85;

    private SpriteRenderer _spriteRendererGreen;
    private SpriteRenderer _spriteRendererRed;
    private SpriteRenderer _spriteRendererYellow;
    private SpriteRenderer _spriteRendererBlue;

    public Sprite _platformGreenInactive;
    public Sprite _platformGreenActive;
    public Sprite _platformRedInactive;
    public Sprite _platformRedActive;
    public Sprite _platformYellowInactive;
    public Sprite _platformYellowActive;
    public Sprite _platformBlueInactive;
    public Sprite _platformBlueActive;

    void Awake()
	{
		if (mInstance != null) 
		{
			throw new UnityException ("Error in CGame(). You are not allowed to instantiate it more than once.");
		}

		mInstance = this;

		CMouse.init();
		CKeyboard.init ();

		//setState(new CLevelState (_platform_Green, _platform_Red, _platform_Yellow, _platform_Blue));
		//setState(new CMainMenuState ());
	}

	static public CGame inst()
	{
		return mInstance;
	}

	// Use this for initialization
	void Start () 
	{
        _difficulty = 0;
        _simonSecuence = new List<int>();
        _isSolved = true;
        _isGameOver = false;  
                
        _platformGreen = Instantiate(_platformGreen, new Vector3(1200, -600), Quaternion.identity);
        _spriteRendererGreen = _platformGreen.GetComponent<SpriteRenderer>();
        _spriteRendererGreen.sprite = _platformGreenInactive;

        
        _platformRed = Instantiate(_platformRed, new Vector3(1200 + PLATFORM_WIDTH, -600), Quaternion.identity);
        _spriteRendererRed = _platformRed.GetComponent<SpriteRenderer>();
        _spriteRendererRed.sprite = _platformRedInactive;

        
        _platformYellow = Instantiate(_platformYellow, new Vector3(1200 + PLATFORM_WIDTH * 2, -600), Quaternion.identity);
        _spriteRendererYellow = _platformYellow.GetComponent<SpriteRenderer>();
        _spriteRendererYellow.sprite = _platformYellowInactive;

        
        _platformBlue = Instantiate(_platformBlue, new Vector3(1200 + PLATFORM_WIDTH * 3, -600), Quaternion.identity);
        _spriteRendererBlue = _platformBlue.GetComponent<SpriteRenderer>();
        _spriteRendererBlue.sprite = _platformBlueInactive;


    }
	
	// Update is called once per frame
	void Update () 
	{
		update ();
	}

	void LateUpdate()
	{
		render ();
	}

	private void update()
	{
		CMouse.update ();
		CKeyboard.update ();
		//mState.update ();

        if (_isSolved && !_isGameOver)
        {
            //If the previous secuence was solved or it's the first, show Simon Secuence
            for (int i = 0; i <= _difficulty; i++)
            {
                _randomNum = CMath.randomIntBetween(0, 3);
                
                _simonSecuence.Add(_randomNum);

                StartCoroutine(AnimatePlatform(_randomNum));

                switch (_randomNum)
                {
                    case 0:
                        //Green
                        Debug.Log("Secuence: Green");                        
                        
                        break;
                    case 1:
                        //Red
                        Debug.Log("Secuence: Red");                        
                        
                        break;
                    case 2:
                        //Yellow
                        Debug.Log("Secuence: Yellow");
                        
                        break;
                    case 3:
                        //Blue
                        Debug.Log("Secuence: Blue");                        
                       
                        break;
                    default:
                        break;
                }
                _isSolved = false;
            }
        }
        else if (!_isSolved && !_isGameOver)
        {
            //Player Input Secuence
            //Replace CKeyboard.pressed with collision detection
            if (CKeyboard.pressed(KeyCode.G))
            {
                Debug.Log("G");
                if (_simonSecuence[0] == 0)
                {
                    Debug.Log("Green: Correct");
                    _simonSecuence.RemoveAt(0);
                }
                else
                {
                    _isGameOver = true;
                }
            }
            else if (CKeyboard.pressed(KeyCode.R))
            {
                Debug.Log("R");
                if (_simonSecuence[0] == 1)
                {
                    Debug.Log("Red: Correct");
                    _simonSecuence.RemoveAt(0);
                }
                else
                {
                    _isGameOver = true;
                }
            }
            else if (CKeyboard.pressed(KeyCode.Y))
            {
                Debug.Log("Y");
                if (_simonSecuence[0] == 2)
                {
                    Debug.Log("Yellow: Correct");
                    _simonSecuence.RemoveAt(0);
                }
                else
                {
                    _isGameOver = true;
                }
            }
            else if (CKeyboard.pressed(KeyCode.B))
            {
                Debug.Log("B");
                if (_simonSecuence[0] == 3)
                {
                    Debug.Log("Blue: Correct");
                    _simonSecuence.RemoveAt(0);
                }
                else
                {
                    _isGameOver = true;
                }
            }

            if (_simonSecuence.Count == 0)
            {
                _isSolved = true;
                _difficulty = _difficulty + CGameConstants.DIFFICULTY_INCREMENT;
                Debug.Log("You WIN, next secuence...");
            }
        }
        else if (_isGameOver)
        {
            Debug.Log("You LOSE");
        }
         
        
        /*if (_platformGreen.GetComponent<CPlatform>().getState() == 0)
        {
            _spriteRendererGreen.sprite = _platformGreenInactive;
        }

        if (_platformGreen.GetComponent<CPlatform>().getState() == 1)
        {
            _spriteRendererGreen.sprite = _platformGreenActive;
        }*/
        //_platformGreen.apiUpdate();
        /*_platformRed.apiUpdate();
        _platformYellow.apiUpdate();
        _platformBlue.apiUpdate();*/

        /*if (CKeyboard.firstPress (CKeyboard.ESCAPE)) 
		{
			CGame.inst().setState(new CMainMenuState());
			return;
		}

		mBackground.update ();
		mPlayer.update ();
		mBulletManager.update ();
		mEnemyManager.update ();
		mMap.update ();*/

        //mCamera.update ();
    }

    private void render()
	{
		//mState.render ();
	}

	public void destroy()
	{
		CMouse.destroy ();
		CKeyboard.destroy ();
		/*if (mState != null) 
		{
			mState.destroy ();
			mState = null;
		}*/
		mInstance = null;
	}

	public void setState(CGameState aState)
	{
		if (mState != null) 
		{
			mState.destroy();
			mState = null;
		}

		mState = aState;
		mState.init ();
	}

	public CGameState getState()
	{
		return mState;
	}

	public void setCamera(CCamera aCamera)
	{
		mCamera = aCamera;
	}

	public CCamera getCamera()
	{
		return mCamera;
	}

	public void setPlayer(CPlayer aPlayer)
	{
		mPlayer = aPlayer;
	}

	public CPlayer getPlayer()
	{
		return mPlayer;
	}

    public IEnumerator AnimatePlatform(int aPlatform)
    {
        switch (_randomNum)
        {
            case 0:
                //Green
                _spriteRendererGreen.sprite = _platformGreenActive;
                break;
            case 1:
                //Red
                _spriteRendererRed.sprite = _platformRedActive;
                break;
            case 2:
                //Yellow
                _spriteRendererYellow.sprite = _platformYellowActive;
                break;
            case 3:
                //Blue
                _spriteRendererBlue.sprite = _platformBlueActive;
                break;
            default:
                break;
        }    
        
        yield return new WaitForSeconds(0.5f);
        
        _spriteRendererGreen.sprite = _platformGreenInactive;
        _spriteRendererRed.sprite = _platformRedInactive;
        _spriteRendererYellow.sprite = _platformYellowInactive;
        _spriteRendererBlue.sprite = _platformBlueInactive;
    }
}
