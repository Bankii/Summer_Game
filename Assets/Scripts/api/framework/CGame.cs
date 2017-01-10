using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CGame : MonoBehaviour 
{
	static private CGame mInstance;
	private CGameState mState;

	private CCamera mCamera;
	private CPlayer mPlayer;

    private List<int> _simonSecuence;
    private int _randomNum;
    private int _difficulty;
    private bool _isSolved;
    private bool _isShowing;
    private bool _isGameOver; //testing

    public GameObject _platformGreen;
    public GameObject _platformRed;
    public GameObject _platformYellow;
    public GameObject _platformBlue;

    private CPlatform _platformGreenScript;
    private CPlatform _platformRedScript;
    private CPlatform _platformYellowScript;
    private CPlatform _platformBlueScript;

    public const int STATE_PLATFORM_OFF = 0;
    public const int STATE_PLATFORM_ON = 1;

    public const int PLATFORM_GREEN = 0;
    public const int PLATFORM_RED = 1;
    public const int PLATFORM_YELLOW = 2;
    public const int PLATFORM_BLUE = 3;

    private const int PLATFORM_HEIGHT = 40;
    private const int PLATFORM_WIDTH = 85;



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
        _isShowing = false;
        _isGameOver = false;  
         
        //Instantiating Platforms       
        _platformGreen = Instantiate(_platformGreen, new Vector3(1200, -600), Quaternion.identity);
        _platformGreenScript =  _platformGreen.GetComponent<CPlatform>();
        _platformGreenScript.setType(PLATFORM_GREEN);

        _platformRed = Instantiate(_platformRed, new Vector3(1200 + PLATFORM_WIDTH, -600), Quaternion.identity);
        _platformRedScript = _platformRed.GetComponent<CPlatform>();
        _platformRedScript.setType(PLATFORM_RED);

        _platformYellow = Instantiate(_platformYellow, new Vector3(1200 + PLATFORM_WIDTH * 2, -600), Quaternion.identity);
        _platformYellowScript = _platformYellow.GetComponent<CPlatform>();
        _platformYellowScript.setType(PLATFORM_YELLOW);

        _platformBlue = Instantiate(_platformBlue, new Vector3(1200 + PLATFORM_WIDTH * 3, -600), Quaternion.identity);
        _platformBlueScript = _platformBlue.GetComponent<CPlatform>();
        _platformBlueScript.setType(PLATFORM_BLUE);

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

        //If the previous secuence was solved or it's the first, build and show Simon Secuence
        if (_isSolved && !_isGameOver)
        {            
            for (int i = 0; i <= _difficulty; i++)
            {
                _randomNum = CMath.randomIntBetween(0, 3);
                //_randomNum = 0;

                _simonSecuence.Add(_randomNum);               

                //Debug.Log for testing
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
                
            }                    

            _isSolved = false;

            //Show secuence in platforms
            foreach (int platform in _simonSecuence)
            {
                Debug.Log("Showing: " + platform);
                if (platform == PLATFORM_GREEN)
                {
                    _platformGreenScript.setState(STATE_PLATFORM_ON);
                    _isShowing = true;
                    return;
                }
                if (platform == PLATFORM_RED)
                {
                    _platformRedScript.setState(STATE_PLATFORM_ON);
                    _isShowing = true;
                    return;
                }
                if (platform == PLATFORM_YELLOW)
                {
                    _platformYellowScript.setState(STATE_PLATFORM_ON);
                    _isShowing = true;
                    return;
                }
                if (platform == PLATFORM_BLUE)
                {
                    _platformBlueScript.setState(STATE_PLATFORM_ON);
                    _isShowing = true;
                    return;
                }

                while (_isShowing)
                {
                    if (_platformGreenScript.getState() == STATE_PLATFORM_OFF && _platformRedScript.getState() == STATE_PLATFORM_OFF &&
                        _platformYellowScript.getState() == STATE_PLATFORM_OFF && _platformBlueScript.getState() == STATE_PLATFORM_OFF)
                    {
                        _isShowing = false;
                    }
                }
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
      
}
