﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CGame : MonoBehaviour 
{
	static private CGame mInstance;
    
    private List<int> _simonSequence; //Color sequence for Simon
    private int _randomNum; //random platform for Simon sequence
    private int _randomPlatformX; // Random X for platform position
    private int _randomPlatformY; // Random Y for platform position
    private float _difficulty; //Simon difficulty
    private int _platformCount; //Counts the platforms to show sequence
    private int _platformNum; //Counter for platform's parent name
    private bool _isSolved; //bool to check if is needed to build a new sequence
    private bool _isShowed; //bool to check if is needed to show the sequence
    private bool _isFirstTimeShowSequence;
    private bool _isFirstPlatform; 
    private bool _isGameOver; //testing

    public GameObject _platformPrefab;

    public CPlayer _player;

    private CPlatform _platformGreen;
    private CPlatform _platformRed;
    private CPlatform _platformYellow;
    private CPlatform _platformBlue;

    private CPlatform _prevPlatformGreen;
    private CPlatform _prevPlatformRed;
    private CPlatform _prevPlatformYellow;
    private CPlatform _prevPlatformBlue;

    public const int STATE_PLATFORM_OFF = 0;
    public const int STATE_PLATFORM_ON = 1;
    public const int STATE_PLATFORM_SHUTDOWN = 2;

    public const int PLATFORM_GREEN = 0;
    public const int PLATFORM_RED = 1;
    public const int PLATFORM_YELLOW = 2;
    public const int PLATFORM_BLUE = 3;

    private const int PLATFORM_HEIGHT = 40;
    private const int PLATFORM_WIDTH = 85;

    private bool _wasGroundedLastFrame = true;
    private bool _onQueueToShutDown = false;

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

    private bool isPlayerOnPlatform(CPlatform platform)
    {
        float auxPlayerX = _player.getX() + _player.getWidth() / 2;
        return (auxPlayerX > platform.getX() && auxPlayerX < platform.getX() + platform.getWidth());
    }

	private void update()
	{
		CMouse.update ();
		CKeyboard.update ();

        if (_player.getState() == 4)
        {
            _isGameOver = true;
        }

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

        // Check the platform the player may be standing in.
        if (_player.isGrounded())
        {
            if (!_isSolved)
            {
                // Checking for platform blue
                if (isPlayerOnPlatform(_platformBlue))
                {
                    if (!checkPlatform(_platformBlue))
                    {
                        _platformBlue.setWalkable(false);
                        _player.setState(4);
                        // TODO: Add averything else that shows you lost.
                    }
                    else
                        _simonSequence.RemoveAt(0);
                }
                // Checking for platform Red
                if (isPlayerOnPlatform(_platformRed))
                {
                    if (!checkPlatform(_platformRed))
                    {
                        _platformRed.setWalkable(false);
                        _player.setState(4);
                        // TODO: Add averything else that shows you lost.
                    }
                    else
                        _simonSequence.RemoveAt(0);
                }
                // Checking for platform Yellow
                if (isPlayerOnPlatform(_platformYellow))
                {
                    if (!checkPlatform(_platformYellow))
                    {
                        _platformYellow.setWalkable(false);
                        _player.setState(4);
                        // TODO: Add averything else that shows you lost.
                    }
                    else
                        _simonSequence.RemoveAt(0);
                }
                // Checking for platform Green
                if (isPlayerOnPlatform(_platformGreen))
                {
                    if (!checkPlatform(_platformGreen))
                    {
                        _platformGreen.setWalkable(false);
                        _player.setState(4);
                        // TODO: Add averything else that shows you lost.
                    }
                    else
                        _simonSequence.RemoveAt(0);
                }
            }
            _wasGroundedLastFrame = true;
        }
        else
        {
            if (_wasGroundedLastFrame && _onQueueToShutDown)
            {
                setAllPrevPlatformsInactive();
            }
            _wasGroundedLastFrame = false;
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

    private bool checkPlatform(CPlatform platform)
    {
        platform.setState(STATE_PLATFORM_ON);
        //_onQueueToRemove = _simonSequence[0] == platform.getType();
        return _simonSequence[0] == platform.getType();
    }
    private void playerInput()
    {
        //Replace CKeyboard.pressed with collision detection
        if (Input.GetKeyUp(KeyCode.G))
        {
            Debug.Log("G");
            if (_simonSequence[0] == PLATFORM_GREEN)
            {
                _platformGreen.setState(STATE_PLATFORM_ON);
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
            if (_simonSequence[0] == PLATFORM_RED)
            {
                _platformRed.setState(STATE_PLATFORM_ON);
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
            if (_simonSequence[0] == PLATFORM_YELLOW)
            {
                _platformYellow.setState(STATE_PLATFORM_ON);
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
            if (_simonSequence[0] == PLATFORM_BLUE)
            {
                _platformBlue.setState(STATE_PLATFORM_ON);
                Debug.Log("Blue: Correct");
                _simonSequence.RemoveAt(0);
            }
            else
            {
                _isGameOver = true;
            }
        }
    }

    // Sets the variables prevPlatforms with the actual platforms, use before re-instancing new platforms.
    private void setPrevPlatforms()
    {
        _prevPlatformBlue = _platformBlue;
        _prevPlatformRed = _platformRed;
        _prevPlatformGreen = _platformGreen;
        _prevPlatformYellow = _platformYellow;
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

        setPrevPlatforms();

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

    // Makes all platforms not walkable and puts them on shutdown state.
    private void setAllPlatformsInactive()
    {
        _platformGreen.setState(STATE_PLATFORM_SHUTDOWN);
        _platformRed.setState(STATE_PLATFORM_SHUTDOWN);
        _platformYellow.setState(STATE_PLATFORM_SHUTDOWN);
        _platformBlue.setState(STATE_PLATFORM_SHUTDOWN);

        _platformBlue.setWalkable(false);
        _platformGreen.setWalkable(false);
        _platformRed.setWalkable(false);
        _platformYellow.setWalkable(false);
    }

    private void setAllPrevPlatformsInactive()
    {
        _prevPlatformGreen.setState(STATE_PLATFORM_SHUTDOWN);
        _prevPlatformRed.setState(STATE_PLATFORM_SHUTDOWN);
        _prevPlatformYellow.setState(STATE_PLATFORM_SHUTDOWN);
        _prevPlatformBlue.setState(STATE_PLATFORM_SHUTDOWN);

        _prevPlatformBlue.setWalkable(false);
        _prevPlatformGreen.setWalkable(false);
        _prevPlatformRed.setWalkable(false);
        _prevPlatformYellow.setWalkable(false);
    }

    private void checkSuccess()
    {
        if (_simonSequence.Count == 0)
        {
            _isSolved = true;
            _difficulty = _difficulty + CGameConstants.DIFFICULTY_INCREMENT;
            //setAllPlatformsInactive();
            _onQueueToShutDown = true;
            createPlatform();
            Debug.Log("You WIN, next platform...");
        }
    }



}
