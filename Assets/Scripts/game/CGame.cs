using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CGame : MonoBehaviour 
{
	static private CGame mInstance;
    
    private List<int> _simonSequence; //Color sequence for Simon
    private int _randomNum; //random platform for Simon sequence
    private int _randomPlatformX; // Random X for platform position
    private int _randomPlatformY; // Random Y for platform position
    private int _difficulty; //Simon difficulty
    private int _platformCount; //Counts the platforms to show sequence
    private int _platformNum; //Counter for platform's parent name
    private bool _isSolved; //bool to check if is needed to build a new sequence
    private bool _isShowed; //bool to check if is needed to show the sequence
    private bool _isFirstTimeShowSequence;
    private bool _isFirstPlatform; 
    private bool _isGameOver; //testing

    public GameObject _platformPrefab;

    private CPlatform _platformGreen;
    private CPlatform _platformRed;
    private CPlatform _platformYellow;
    private CPlatform _platformBlue;

    public const int STATE_PLATFORM_OFF = 0;
    public const int STATE_PLATFORM_ON = 1;
    public const int STATE_PLATFORM_SHUTDOWN = 2;

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
        _isFirstTimeShowSequence = true;
        _isFirstPlatform = true;
        _platformCount = 0;
        _platformNum = 1;

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

        //If the previous sequence was solved or it's the first, build the Simon Sequence
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
        _isFirstTimeShowSequence = true;

        for (int i = 0; i <= _difficulty; i++)
        {
            _randomNum = CMath.randomIntBetween(0, 3);

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

            if (!_isFirstTimeShowSequence)
            {
                if (_platformGreen.getState() == STATE_PLATFORM_OFF && _platformRed.getState() == STATE_PLATFORM_OFF &&
                _platformYellow.getState() == STATE_PLATFORM_OFF && _platformBlue.getState() == STATE_PLATFORM_OFF)
                {
                    _platformCount++;
                }
            }
            
            _isFirstTimeShowSequence = false;            

            if (_platformCount < _simonSequence.Count)
            {
                if (_simonSequence[_platformCount] == PLATFORM_GREEN)
                {
                    if (_platformGreen.getState() == STATE_PLATFORM_OFF && _platformRed.getState() == STATE_PLATFORM_OFF &&
                        _platformYellow.getState() == STATE_PLATFORM_OFF && _platformBlue.getState() == STATE_PLATFORM_OFF)
                    {
                        _platformGreen.setState(STATE_PLATFORM_ON);
                    }

                }
                if (_simonSequence[_platformCount] == PLATFORM_RED)
                {
                    if (_platformGreen.getState() == STATE_PLATFORM_OFF && _platformRed.getState() == STATE_PLATFORM_OFF &&
                        _platformYellow.getState() == STATE_PLATFORM_OFF && _platformBlue.getState() == STATE_PLATFORM_OFF)
                    {
                        _platformRed.setState(STATE_PLATFORM_ON);
                    }
                }
                if (_simonSequence[_platformCount] == PLATFORM_YELLOW)
                {
                    if (_platformGreen.getState() == STATE_PLATFORM_OFF && _platformRed.getState() == STATE_PLATFORM_OFF &&
                        _platformYellow.getState() == STATE_PLATFORM_OFF && _platformBlue.getState() == STATE_PLATFORM_OFF)
                    {
                        _platformYellow.setState(STATE_PLATFORM_ON);
                    }
                }
                if (_simonSequence[_platformCount] == PLATFORM_BLUE)
                {
                    if (_platformGreen.getState() == STATE_PLATFORM_OFF && _platformRed.getState() == STATE_PLATFORM_OFF &&
                        _platformYellow.getState() == STATE_PLATFORM_OFF && _platformBlue.getState() == STATE_PLATFORM_OFF)
                    {
                        _platformBlue.setState(STATE_PLATFORM_ON);
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
        if (Input.GetKeyUp(KeyCode.G))
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
        else if (Input.GetKeyUp(KeyCode.R))
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
        else if (Input.GetKeyUp(KeyCode.Y))
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
        else if (Input.GetKeyUp(KeyCode.B))
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
        

        if (_isFirstPlatform)
        {
            _randomPlatformX = CMath.randomIntBetween(300, 600);
            _randomPlatformY = CMath.randomIntBetween(-800, -1000);
        }
        else
        {
            if (_platformGreen.getX() > CGameConstants.SCREEN_WIDTH/2)
            {
                _randomPlatformX = CMath.randomIntBetween(CGameConstants.SCREEN_WIDTH / 2, 1500);
                _randomPlatformY = CMath.randomIntBetween(-300, (int)_platformGreen.getY());
            }
            else
            {
                _randomPlatformX = CMath.randomIntBetween(300, CGameConstants.SCREEN_WIDTH / 2);
                _randomPlatformY = CMath.randomIntBetween(-300, (int)_platformGreen.getY());
            }
            
        }

        GameObject _platformParent = new GameObject();
        _platformParent.transform.name = "Colored_Platform_" + _platformNum;

        GameObject platform = Instantiate(_platformPrefab, new Vector3(_randomPlatformX, _randomPlatformY), Quaternion.identity);
        platform.name = "Platform_Green";
        platform.transform.SetParent(_platformParent.transform);
        _platformGreen = platform.GetComponent<CPlatform>();
        _platformGreen.setType(PLATFORM_GREEN);

        platform = Instantiate(_platformPrefab, new Vector3(_randomPlatformX + PLATFORM_WIDTH, _randomPlatformY), Quaternion.identity);
        platform.name = "Platform_Red";
        platform.transform.SetParent(_platformParent.transform);
        _platformRed = platform.GetComponent<CPlatform>();
        _platformRed.setType(PLATFORM_RED);

        platform = Instantiate(_platformPrefab, new Vector3(_randomPlatformX + PLATFORM_WIDTH * 2, _randomPlatformY), Quaternion.identity);
        platform.name = "Platform_Yellow";
        platform.transform.SetParent(_platformParent.transform);
        _platformYellow = platform.GetComponent<CPlatform>();
        _platformYellow.setType(PLATFORM_YELLOW);

        platform = Instantiate(_platformPrefab, new Vector3(_randomPlatformX + PLATFORM_WIDTH * 3, _randomPlatformY), Quaternion.identity);
        platform.name = "Platform_Blue";
        platform.transform.SetParent(_platformParent.transform);
        _platformBlue = platform.GetComponent<CPlatform>();
        _platformBlue.setType(PLATFORM_BLUE);

        _isFirstPlatform = false;
        _platformNum++;
    }

    // Makes all platforms not walkable. Other functionalitty can be added.
    private void setAllPlatformsInactive()
    {
        _platformBlue.setWalkable(false);
        _platformGreen.setWalkable(false);
        _platformRed.setWalkable(false);
        _platformYellow.setWalkable(false);
    }

    private void checkSuccess()
    {
        if (_simonSequence.Count == 0)
        {
            _isSolved = true;
            _difficulty = _difficulty + CGameConstants.DIFFICULTY_INCREMENT;
            shutdownPlatforms();
            setAllPlatformsInactive();
            createPlatform();
            Debug.Log("You WIN, next platform...");
        }
    }

    private void shutdownPlatforms()
    {
        _platformGreen.setState(STATE_PLATFORM_SHUTDOWN);
        _platformRed.setState(STATE_PLATFORM_SHUTDOWN);
        _platformYellow.setState(STATE_PLATFORM_SHUTDOWN);
        _platformBlue.setState(STATE_PLATFORM_SHUTDOWN);
    }


}
