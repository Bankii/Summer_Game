using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CGame : MonoBehaviour 
{
	static private CGame mInstance;
	private CGameState mState;

	private CCamera mCamera;
	private CPlayer mPlayer;

    private List<int> _simonSequence;
    private int _randomNum;
    private int _randomPlatformX;
    private int _randomPlatformY;
    private int _prevPlatformY;
    private int _difficulty;
    private int _platformCount;
    private bool _isSolved;
    private bool _isShowed;
    private bool _firstTimeShowSequence;
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
        _simonSequence = new List<int>();
        _isSolved = true;
        _isShowed = false;
        _isGameOver = false;
        _firstTimeShowSequence = true;
        _platformCount = 0;
        //_prevPlatformY = - CGameConstants.SCREEN_HEIGHT;

        //Instantiating Platforms
        createPlatform();               

    }
	
	// Update is called once per frame
	void Update () 
	{
		update ();
	}


	private void update()
	{
		CMouse.update ();
		CKeyboard.update ();

        //If the previous sequence was solved or it's the first, build and show Simon Sequence
        if (_isSolved && !_isGameOver)
        {
            buildSimonSequence();
        }
        else if (!_isSolved && !_isGameOver)
        {
            //Player Input Sequence
            playerInput();
            checkSuccess();
        }
        else if (_isGameOver)
        {
            Debug.Log("You LOSE");
        }

        //If it isn't shown, show the Simon Sequence built.
        if (!_isShowed)
        {
            showSimonSequence();
        }

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

    private void buildSimonSequence()
    {
        //Reseting variables for showSimonSequence()
        _platformCount = 0;
        _firstTimeShowSequence = true;

        for (int i = 0; i <= _difficulty; i++)
        {
            _randomNum = CMath.randomIntBetween(0, 3);
            //_randomNum = 0;

            _simonSequence.Add(_randomNum);

            //Debug.Log for testing
            switch (_randomNum)
            {
                case 0:
                    //Green
                    Debug.Log("Sequence: Green");

                    break;
                case 1:
                    //Red
                    Debug.Log("Sequence: Red");

                    break;
                case 2:
                    //Yellow
                    Debug.Log("Sequence: Yellow");

                    break;
                case 3:
                    //Blue
                    Debug.Log("Sequence: Blue");

                    break;
                default:
                    break;
            }

        }

        _isSolved = false;
        _isShowed = false;

    }

    private void showSimonSequence()
    {
        if (_platformCount < _simonSequence.Count)
        {

            if (!_firstTimeShowSequence)
            {
                if (_platformGreenScript.getState() == STATE_PLATFORM_OFF && _platformRedScript.getState() == STATE_PLATFORM_OFF &&
                _platformYellowScript.getState() == STATE_PLATFORM_OFF && _platformBlueScript.getState() == STATE_PLATFORM_OFF)
                {
                    _platformCount++;
                }
            }
            
            _firstTimeShowSequence = false;            

            if (_platformCount < _simonSequence.Count)
            {
                if (_simonSequence[_platformCount] == PLATFORM_GREEN)
                {
                    if (_platformGreenScript.getState() == STATE_PLATFORM_OFF && _platformRedScript.getState() == STATE_PLATFORM_OFF &&
                        _platformYellowScript.getState() == STATE_PLATFORM_OFF && _platformBlueScript.getState() == STATE_PLATFORM_OFF)
                    {
                        _platformGreenScript.setState(STATE_PLATFORM_ON);
                    }

                }
                if (_simonSequence[_platformCount] == PLATFORM_RED)
                {
                    if (_platformGreenScript.getState() == STATE_PLATFORM_OFF && _platformRedScript.getState() == STATE_PLATFORM_OFF &&
                        _platformYellowScript.getState() == STATE_PLATFORM_OFF && _platformBlueScript.getState() == STATE_PLATFORM_OFF)
                    {
                        _platformRedScript.setState(STATE_PLATFORM_ON);
                    }
                }
                if (_simonSequence[_platformCount] == PLATFORM_YELLOW)
                {
                    if (_platformGreenScript.getState() == STATE_PLATFORM_OFF && _platformRedScript.getState() == STATE_PLATFORM_OFF &&
                        _platformYellowScript.getState() == STATE_PLATFORM_OFF && _platformBlueScript.getState() == STATE_PLATFORM_OFF)
                    {
                        _platformYellowScript.setState(STATE_PLATFORM_ON);
                    }
                }
                if (_simonSequence[_platformCount] == PLATFORM_BLUE)
                {
                    if (_platformGreenScript.getState() == STATE_PLATFORM_OFF && _platformRedScript.getState() == STATE_PLATFORM_OFF &&
                        _platformYellowScript.getState() == STATE_PLATFORM_OFF && _platformBlueScript.getState() == STATE_PLATFORM_OFF)
                    {
                        _platformBlueScript.setState(STATE_PLATFORM_ON);
                    }
                }
            }
            
        }
        else
        {
            _isShowed = true;
        }
    }

    private void playerInput()
    {
        //Replace CKeyboard.pressed with collision detection
        if (CKeyboard.pressed(KeyCode.G))
        {
            Debug.Log("G");
            if (_simonSequence[0] == 0)
            {
                //_platformGreenScript.setState(STATE_PLATFORM_ON);
                Debug.Log("Green: Correct");
                _simonSequence.RemoveAt(0);
            }
            else
            {
                _isGameOver = true;
            }
        }
        else if (CKeyboard.pressed(KeyCode.R))
        {
            Debug.Log("R");
            if (_simonSequence[0] == 1)
            {
                //_platformRedScript.setState(STATE_PLATFORM_ON);
                Debug.Log("Red: Correct");
                _simonSequence.RemoveAt(0);
            }
            else
            {
                _isGameOver = true;
            }
        }
        else if (CKeyboard.pressed(KeyCode.Y))
        {
            Debug.Log("Y");
            if (_simonSequence[0] == 2)
            {
                //_platformYellowScript.setState(STATE_PLATFORM_ON);
                Debug.Log("Yellow: Correct");
                _simonSequence.RemoveAt(0);
            }
            else
            {
                _isGameOver = true;
            }
        }
        else if (CKeyboard.pressed(KeyCode.B))
        {
            Debug.Log("B");
            if (_simonSequence[0] == 3)
            {
                //_platformBlueScript.setState(STATE_PLATFORM_ON);
                Debug.Log("Blue: Correct");
                _simonSequence.RemoveAt(0);
            }
            else
            {
                _isGameOver = true;
            }
        }
    }

    private void createPlatform()
    {
        _randomPlatformX = CMath.randomIntBetween(500, 1500);
        _randomPlatformY = CMath.randomIntBetween(-400, -1000);

        _platformGreen = Instantiate(_platformGreen, new Vector3(_randomPlatformX, _randomPlatformY), Quaternion.identity);
        _platformGreenScript = _platformGreen.GetComponent<CPlatform>();
        _platformGreenScript.setType(PLATFORM_GREEN);

        _platformRed = Instantiate(_platformRed, new Vector3(_randomPlatformX + PLATFORM_WIDTH, _randomPlatformY), Quaternion.identity);
        _platformRedScript = _platformRed.GetComponent<CPlatform>();
        _platformRedScript.setType(PLATFORM_RED);

        _platformYellow = Instantiate(_platformYellow, new Vector3(_randomPlatformX + PLATFORM_WIDTH * 2, _randomPlatformY), Quaternion.identity);
        _platformYellowScript = _platformYellow.GetComponent<CPlatform>();
        _platformYellowScript.setType(PLATFORM_YELLOW);

        _platformBlue = Instantiate(_platformBlue, new Vector3(_randomPlatformX + PLATFORM_WIDTH * 3, _randomPlatformY), Quaternion.identity);
        _platformBlueScript = _platformBlue.GetComponent<CPlatform>();
        _platformBlueScript.setType(PLATFORM_BLUE);

        //_prevPlatformY = _randomPlatformY;
    }

    private void checkSuccess()
    {
        if (_simonSequence.Count == 0)
        {
            _isSolved = true;
            _difficulty = _difficulty + CGameConstants.DIFFICULTY_INCREMENT;
            createPlatform();
            Debug.Log("You WIN, next platform...");
        }
    }


}
