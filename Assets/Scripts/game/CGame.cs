using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class CGame : MonoBehaviour 
{
    public bool _PlatformLerp = true;

	static private CGame mInstance;
    
    private List<int> _simonSequence; //Color sequence for Simon
    private int _randomNum; //random platform for Simon sequence
    private int _randomPlatformX; // Random X for platform position
    private int _randomPlatformY; // Random Y for platform position    
    private int _platformCount; //Counts the platforms to show sequence
    private int _platformNum; //Counter for platform's parent name
    private bool _isSolved; //bool to check if is needed to build a new sequence
    private bool _isShowed; //bool to check if is needed to show the sequence
    private bool _isFirstTimeShowSequence;
    private bool _isFirstPlatform; 
    private bool _isGameOver; //testing

    public float _difficulty; //Simon difficulty
    public float _difficultyIncrement; //Increment for each platform solved
    public int _platformSeparation;
    public float _spawnCoinRate;
    public float _spawnRewardRate;
    public CTextGo _goText;


    public GameObject _platformPrefab;
    //public GameObject _coinPrefab;
    //public GameObject _boxPrefab;
    public CCamera _camera;
    //public GameObject _backgroundPrefab;

    public GameObject _canvas;

    public CPlayer _player;

    private CPlatform _platformGreen;
    private CPlatform _platformRed;
    private CPlatform _platformYellow;
    private CPlatform _platformBlue;
    
    private CPlatform _prevPlatformGreen;
    private CPlatform _prevPlatformRed;
    private CPlatform _prevPlatformYellow;
    private CPlatform _prevPlatformBlue;

    //private GameObject _backgroundParent;
    //private GameObject _firstBackground;
    //private CBackground _background;

    public const int STATE_PLATFORM_OFF = 0;
    public const int STATE_PLATFORM_ON = 1;
    public const int STATE_PLATFORM_SHUTDOWN = 2;
    public const int STATE_PLATFORM_DONE = 3;
    public const int STATE_PLATFORM_TRANSITION = 4;
    public const int STATE_PLATFORM_TRANSITION_DONE = 5;

    public const int PLATFORM_GREEN = 0;
    public const int PLATFORM_RED = 1;
    public const int PLATFORM_YELLOW = 2;
    public const int PLATFORM_BLUE = 3;
    public const int COLOR_BASE = 4;

    private const int PLATFORM_HEIGHT = 78;
    private const int PLATFORM_WIDTH = 200;
    
    private bool _wasGroundedLastFrame = true;
    private bool _onQueueToShutDown = false;

    private bool _restartGame;

    private bool _wasRestartLastFrame;

    public float _comparisonError;

    private bool _checkPlatforms = true;

    private CPlatform _lastPlatform;

    private float _comboElapsedTime = 0;
    private int _comboMultip = 1;
    public float _comboMaxTime;
    public int _comboMaxMultip;
    private bool _comboPause = false;

    
    private float _spawnDeterminator; //Used to determinate if there will be coins or rewards in the platform
    private int _platCoinReward; //Used to determinate in which platform the reward/coin will spawn

    private float _resetDifficulty;
    public float _resetSpawnCoinRate;
    public float _resetSpawnRewardRate;

    private bool _pause = false;

    private int _score = 0;

    void Awake()
	{
		if (mInstance != null) 
		{
			throw new UnityException ("Error in CGame(). You are not allowed to instantiate it more than once.");
		}

		mInstance = this;

		CMouse.init();
		CKeyboard.init ();
        
	}

	static public CGame inst()
	{
		return mInstance;
	}

	// Use this for initialization
	void Start () 
	{
        _simonSequence = new List<int>();
        _isSolved = true;
        _isShowed = false;
        _isGameOver = false;
        _isFirstTimeShowSequence = true;
        _isFirstPlatform = true;
        _platformCount = 0;
        _platformNum = 1;

        _resetDifficulty = _difficulty;
        _resetSpawnCoinRate = _spawnCoinRate;
        _resetSpawnRewardRate = _spawnRewardRate;
        

        _camera.setGameObjectToFollow(_player);

        //Instantiating Platforms
        createPlatform();    
    }
	
	// Update is called once per frame
	void Update () 
	{
        if (!_pause)
        {
            update();
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            _pause = !_pause;
            if (_pause)
            {
                GameObject pauseMenu = Instantiate(Resources.Load<GameObject>("Prefabs/Menus/PauseMenu"), _canvas.transform);
                pauseMenu.transform.localScale = new Vector3(3, 3, 3);
                pauseMenu.transform.localPosition = new Vector3(0, 0, 0);
                Cursor.visible = true;
            }
            else
            {
                Cursor.visible = false;
            }
        }
    }

    public bool isRestart()
    {
        return _restartGame;
    }

    public void setRestart(bool aRestart)
    {
        _restartGame = aRestart;
    }

    private bool isPlayerOnPlatform(CPlatform platform)
    {
        int auxPlayerX = (int)_player.getX() + _player.getWidth() / 2;
        int auxPlayerY = (int)_player.getY() - _player.getHeight();
        bool auxBool = auxPlayerY <= platform.getY() + _comparisonError && auxPlayerY >= platform.getY() - _comparisonError;
        return (auxPlayerX > platform.getX() && auxPlayerX < platform.getX() + platform.getWidth()) && auxBool;
    }

    private void resetVariables()
    {
        _score = 0;
        _simonSequence = new List<int>();
        _isSolved = true;
        _isShowed = false;
        _isGameOver = false;
        _isFirstTimeShowSequence = true;
        _isFirstPlatform = true;
        _difficulty = _resetDifficulty;
        _spawnCoinRate = _resetSpawnCoinRate;
        _spawnRewardRate = _resetSpawnRewardRate;
        _platformCount = 0;
        _platformNum = 1;
        _platformGreen = null;
        _platformRed = null;
        _platformYellow = null;
        _platformBlue = null;
        _comboElapsedTime = 0;
        _comboMultip = 1;
        _comboPause = false;

    #region BACKGROUND COMENTED STUFF
    //_backgroundParent = new GameObject();
    //_backgroundParent.transform.name = "Background";
    //_backgroundParent.AddComponent<CDestroyOnRestart>();
    //_firstBackground = Instantiate(_backgroundPrefab, new Vector3(_camera.getX() - CGameConstants.SCREEN_WIDTH / 2, _camera.getY() + CGameConstants.SCREEN_HEIGHT / 2 + 1100), Quaternion.identity);
    //_firstBackground.name = "Background";
    //_firstBackground.transform.SetParent(_backgroundParent.transform);
    //_background = _firstBackground.GetComponent<CBackground>();
    #endregion

    _camera.setGameObjectToFollow(_player);
    }

	private void update()
	{
		CMouse.update ();
		CKeyboard.update ();


        // adding time to the combo and checking it's not over.
        if (_isShowed)
        {
            if (!_comboPause)
            {
                _comboElapsedTime += Time.deltaTime;
                if (_comboElapsedTime >= _comboMaxTime)
                {
                    _comboElapsedTime = _comboMaxTime;
                    _comboMultip = 1;
                }
            }
            
        }

        if (_restartGame && _wasRestartLastFrame)
        {
            _restartGame = false;
            resetVariables();

            createPlatform();
            _platformGreen.setState(STATE_PLATFORM_TRANSITION);
            _platformRed.setState(STATE_PLATFORM_TRANSITION);
            _platformYellow.setState(STATE_PLATFORM_TRANSITION);
            _platformBlue.setState(STATE_PLATFORM_TRANSITION);

            #region OLD CODE
            //if (_platformGreen.getState() != STATE_PLATFORM_TRANSITION_DONE && _platformRed.getState() != STATE_PLATFORM_TRANSITION_DONE
            //    && _platformYellow.getState() != STATE_PLATFORM_TRANSITION_DONE && _platformBlue.getState() != STATE_PLATFORM_TRANSITION_DONE)
            //{
            //    createPlatform();
            //}
            #endregion
        }

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
            //playerInput();
            checkSuccess();
            
        }
        else if (_isGameOver)
        {            
            // TODO: see if this doesn't make it tiring for the 
            // person to wait for the character to fall to the bottom.
        }

        //If it isn't shown, show the Simon Sequence built.
        if (!_isShowed)
        {
            if (_platformGreen != null && _platformRed != null && _platformYellow != null && _platformBlue != null)
            {                
                if (_platformGreen.getState() != STATE_PLATFORM_TRANSITION && _platformRed.getState() != STATE_PLATFORM_TRANSITION 
                && _platformYellow.getState() != STATE_PLATFORM_TRANSITION && _platformBlue.getState() != STATE_PLATFORM_TRANSITION
                && _platformGreen.getState() != CPlatform.STATE_INITIAL && _platformRed.getState() != CPlatform.STATE_INITIAL 
                && _platformYellow.getState() != CPlatform.STATE_INITIAL && _platformBlue.getState() != CPlatform.STATE_INITIAL)
                {
                    showSimonSequence();
                                      
                }   
                //_comboPause = true;
            }

        }
        //else
        //{
        //    _comboPause = false;
        //    //_camera.getX(), _player.getY());
        //}

        // Check the platform the player may be standing in.
        if (_player.isGrounded())
        {
            if (_player.getState() == 1 && _lastPlatform != null)
            {
                if(!isPlayerOnPlatform(_lastPlatform))
                    _checkPlatforms = true;
            }
            if (!_isSolved && _checkPlatforms)
            {
                if ((_platformGreen.getState() != STATE_PLATFORM_DONE && _platformGreen.getState() != STATE_PLATFORM_TRANSITION_DONE) &&
                (_platformRed.getState() != STATE_PLATFORM_DONE && _platformRed.getState() != STATE_PLATFORM_TRANSITION_DONE) &&
                (_platformYellow.getState() != STATE_PLATFORM_DONE && _platformYellow.getState() != STATE_PLATFORM_TRANSITION_DONE) &&
                (_platformBlue.getState() != STATE_PLATFORM_DONE && _platformBlue.getState() != STATE_PLATFORM_TRANSITION_DONE))
                {
                    // Checking for platform blue
                    if (isPlayerOnPlatform(_platformBlue))
                    {
                        if (!checkPlatform(_platformBlue))
                        {
                            _platformBlue.setWalkable(false);
                            _player.setState(CPlayer.STATE_DYING);
                            // TODO: Add everything else that shows you lost.
                        }
                        else
                        {
                            _simonSequence.RemoveAt(0);
                            //_player.setColor(_platformBlue.getType());
                            _lastPlatform = _platformBlue;
                            increaseCombo();
                        }

                    }
                    // Checking for platform Red
                    else if (isPlayerOnPlatform(_platformRed))
                    {
                        if (!checkPlatform(_platformRed))
                        {
                            _platformRed.setWalkable(false);
                            _player.setState(CPlayer.STATE_DYING);
                            // TODO: Add everything else that shows you lost.
                        }
                        else
                        {
                            _simonSequence.RemoveAt(0);
                            //_player.setColor(_platformRed.getType());
                            _lastPlatform = _platformRed;
                            increaseCombo();
                        }
                    }
                    // Checking for platform Yellow
                    else if (isPlayerOnPlatform(_platformYellow))
                    {
                        if (!checkPlatform(_platformYellow))
                        {
                            _platformYellow.setWalkable(false);
                            _player.setState(CPlayer.STATE_DYING);
                            // TODO: Add everything else that shows you lost.
                        }
                        else
                        {
                            _simonSequence.RemoveAt(0);
                            //_player.setColor(_platformYellow.getType());
                            _lastPlatform = _platformYellow;
                            increaseCombo();
                        }
                    }
                    // Checking for platform Green
                    else if (isPlayerOnPlatform(_platformGreen))
                    {
                        if (!checkPlatform(_platformGreen))
                        {
                            _platformGreen.setWalkable(false);
                            _player.setState(CPlayer.STATE_DYING);
                            // TODO: Add everything else that shows you lost.
                        }
                        else
                        {
                            _simonSequence.RemoveAt(0);
                            //_player.setColor(_platformGreen.getType());
                            _lastPlatform = _platformGreen;
                            increaseCombo();
                        }
                    }
                }
                
            }
            _wasGroundedLastFrame = true;
        }
        else
        {
            if (_wasGroundedLastFrame && _onQueueToShutDown)
            {
                setAllPrevPlatformsInactive();
                _onQueueToShutDown = false;
            }
            _checkPlatforms = true;
            _wasGroundedLastFrame = false;
        }

        changePlayerColor();
        
        _wasRestartLastFrame = _restartGame;
                
    }

    public void increaseCombo()
    {
        _comboMultip += 1;
        if (_comboMultip > _comboMaxMultip)
        {
            _comboMultip = _comboMaxMultip;
        }
        _comboElapsedTime = 0;
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
            _randomNum = CMath.randomIntBetween(CGameConstants.COLOR_GREEN, CGameConstants.COLOR_BLUE);

            _simonSequence.Add(_randomNum);                       

        }

        _isSolved = false;
        _isShowed = false;

    }

    private void showSimonSequence()
    {
        
        //_camera.goTo(_camera.getX(), _platformBlue.getY());

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
                        _platformGreen.playPlatformFX();
                        _platformGreen.setState(STATE_PLATFORM_ON);
                    }

                }
                if (_simonSequence[_platformCount] == PLATFORM_RED)
                {
                    if (_platformGreen.getState() == STATE_PLATFORM_OFF && _platformRed.getState() == STATE_PLATFORM_OFF &&
                        _platformYellow.getState() == STATE_PLATFORM_OFF && _platformBlue.getState() == STATE_PLATFORM_OFF)
                    {
                        _platformRed.playPlatformFX();
                        _platformRed.setState(STATE_PLATFORM_ON);
                    }
                }
                if (_simonSequence[_platformCount] == PLATFORM_YELLOW)
                {
                    if (_platformGreen.getState() == STATE_PLATFORM_OFF && _platformRed.getState() == STATE_PLATFORM_OFF &&
                        _platformYellow.getState() == STATE_PLATFORM_OFF && _platformBlue.getState() == STATE_PLATFORM_OFF)
                    {
                        _platformYellow.playPlatformFX();
                        _platformYellow.setState(STATE_PLATFORM_ON);
                    }
                }
                if (_simonSequence[_platformCount] == PLATFORM_BLUE)
                {
                    if (_platformGreen.getState() == STATE_PLATFORM_OFF && _platformRed.getState() == STATE_PLATFORM_OFF &&
                        _platformYellow.getState() == STATE_PLATFORM_OFF && _platformBlue.getState() == STATE_PLATFORM_OFF)
                    {
                        _platformBlue.playPlatformFX();
                        _platformBlue.setState(STATE_PLATFORM_ON);
                    }
                }
            }
        }
        else
        {
            _isShowed = true;
            //_comboPause = false;
            _platformGreen.setState(STATE_PLATFORM_ON);
            _platformRed.setState(STATE_PLATFORM_ON);
            _platformYellow.setState(STATE_PLATFORM_ON);
            _platformBlue.setState(STATE_PLATFORM_ON);

            /*_camera.releaseToGo();

            if (_platformNum >= 4)
            {
                _camera.goToPlayer();
            }
            */

            _goText.goSizeBounce(400, 35, 50);
        }
    }

    private bool checkPlatform(CPlatform platform)
    {
        platform.playPlatformFX();
        platform.setState(STATE_PLATFORM_ON);
        _checkPlatforms = false;
        //_onQueueToRemove = _simonSequence[0] == platform.getType();
        return _simonSequence[0] == platform.getType();
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
        _comboPause = true;
        if (_isFirstPlatform)
        {
            _randomPlatformX = CMath.randomIntBetween(300, 450);
            _randomPlatformY = CMath.randomIntBetween(-800, -1000);
        }
        else
        {
            if (_platformGreen.getX() > CGameConstants.SCREEN_WIDTH/2) //If platform is on the right
            {
                _randomPlatformX = CMath.randomIntBetween(CGameConstants.SCREEN_WIDTH / 2, 1500);
                _randomPlatformY = CMath.randomIntBetween((int)_camera.getY() + CGameConstants.SCREEN_HEIGHT / 2 - 300, (int)_player.getY() + _player.getHeight()/3);
            }
            else //If platform is on the left
            {
                _randomPlatformX = CMath.randomIntBetween(300, CGameConstants.SCREEN_WIDTH / 2);
                _randomPlatformY = CMath.randomIntBetween((int)_camera.getY() + CGameConstants.SCREEN_HEIGHT / 2 - 300, (int)_player.getY() + _player.getHeight() / 3);
            }
            
        }

        setPrevPlatforms();

        GameObject _platformParent = new GameObject();
        _platformParent.transform.name = "Colored_Platform_" + _platformNum;
        _platformParent.AddComponent<CDestroyOnRestart>();

        GameObject platform = Instantiate(_platformPrefab, new Vector3(_randomPlatformX, _randomPlatformY), Quaternion.identity);
        platform.name = "Platform_Green";
        platform.transform.SetParent(_platformParent.transform);
        _platformGreen = platform.GetComponent<CPlatform>();
        _platformGreen.setType(PLATFORM_GREEN);

        platform = Instantiate(_platformPrefab, new Vector3(_randomPlatformX + PLATFORM_WIDTH + _platformSeparation, _randomPlatformY), Quaternion.identity);
        platform.name = "Platform_Red";
        platform.transform.SetParent(_platformParent.transform);
        _platformRed = platform.GetComponent<CPlatform>();
        _platformRed.setType(PLATFORM_RED);

        platform = Instantiate(_platformPrefab, new Vector3((_randomPlatformX + PLATFORM_WIDTH * 2) + _platformSeparation *2, _randomPlatformY), Quaternion.identity);
        platform.name = "Platform_Yellow";
        platform.transform.SetParent(_platformParent.transform);
        _platformYellow = platform.GetComponent<CPlatform>();
        _platformYellow.setType(PLATFORM_YELLOW);

        platform = Instantiate(_platformPrefab, new Vector3((_randomPlatformX + PLATFORM_WIDTH * 3) + _platformSeparation *3, _randomPlatformY), Quaternion.identity);
        platform.name = "Platform_Blue";
        platform.transform.SetParent(_platformParent.transform);
        _platformBlue = platform.GetComponent<CPlatform>();
        _platformBlue.setType(PLATFORM_BLUE);

        _isFirstPlatform = false;
        _platformNum++; 

        _spawnDeterminator = CMath.randomIntBetween(0, 100);
        if (_spawnDeterminator < 100 / _spawnCoinRate)
        {
            #region COIN_SPAWN
            _platCoinReward = CMath.randomIntBetween(CGameConstants.COLOR_GREEN, CGameConstants.COLOR_BLUE);
            if (_platCoinReward == CGameConstants.COLOR_GREEN)
            {
                createCoin(_platformGreen);
            }
            else if (_platCoinReward == CGameConstants.COLOR_RED)
            {
                createCoin(_platformRed);

            }
            else if (_platCoinReward == CGameConstants.COLOR_YELLOW)
            {
                createCoin(_platformYellow);

            }
            else if (_platCoinReward == CGameConstants.COLOR_BLUE)
            {
                createCoin(_platformBlue);
            }
            #endregion
        }

        _spawnDeterminator = CMath.randomIntBetween(0, 100);
        if (_spawnDeterminator < 100 / _spawnRewardRate)
        {
            #region REWARD_SPAWN
            _platCoinReward = CMath.randomIntBetween(CGameConstants.COLOR_GREEN, CGameConstants.COLOR_BLUE);
            if (_platCoinReward == CGameConstants.COLOR_GREEN)
            {
                createBox(_platformGreen);
            }
            else if (_platCoinReward == CGameConstants.COLOR_RED)
            {
                createBox(_platformRed);

            }
            else if (_platCoinReward == CGameConstants.COLOR_YELLOW)
            {
                createBox(_platformYellow);

            }
            else if (_platCoinReward == CGameConstants.COLOR_BLUE)
            {
                createBox(_platformBlue);
            }
            #endregion
        }

        _camera.setMax(_randomPlatformY - PLATFORM_HEIGHT + CGameConstants.SCREEN_HEIGHT / 2);

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
        if (_prevPlatformGreen != null)
        {
            _prevPlatformGreen.setState(STATE_PLATFORM_SHUTDOWN);
            _prevPlatformGreen.setWalkable(false);
            //GameObject particle = Resources.Load<GameObject>("Prefabs/Platform_Explotion_Particle");
            //particle = Instantiate(particle, new Vector3(_prevPlatformGreen.getX() + _prevPlatformGreen.getWidth() / 2, _prevPlatformGreen.getY() - _prevPlatformGreen.getHeight() / 2, 0), Quaternion.Euler(-90, 0, 0));
        }
        if (_prevPlatformRed != null)
        {
            _prevPlatformRed.setState(STATE_PLATFORM_SHUTDOWN);
            _prevPlatformRed.setWalkable(false);
            //GameObject particle = Resources.Load<GameObject>("Prefabs/Platform_Explotion_Particle");
            //particle = Instantiate(particle, new Vector3(_prevPlatformRed.getX() + _prevPlatformRed.getWidth() / 2, _prevPlatformRed.getY() - _prevPlatformRed.getHeight()/2, 0), Quaternion.Euler(-90, 0, 0));
            GameObject particle = Resources.Load<GameObject>("Prefabs/Platform_Explotion_Particle");
            particle = Instantiate(particle, new Vector3(_prevPlatformRed.getX() + _prevPlatformRed.getWidth(), _prevPlatformRed.getY() - _prevPlatformRed.getHeight()/2, 0), Quaternion.Euler(-90, 0, 0));
        }
        if (_prevPlatformYellow != null)
        {
            _prevPlatformYellow.setState(STATE_PLATFORM_SHUTDOWN);
            _prevPlatformYellow.setWalkable(false);
            //GameObject particle = Resources.Load<GameObject>("Prefabs/Platform_Explotion_Particle");
            //particle = Instantiate(particle, new Vector3(_prevPlatformYellow.getX() + _prevPlatformYellow.getWidth() / 2, _prevPlatformYellow.getY() - _prevPlatformYellow.getHeight() / 2, 0), Quaternion.Euler(-90, 0, 0));
            
        }
        if (_prevPlatformBlue != null)
        {
            _prevPlatformBlue.setState(STATE_PLATFORM_SHUTDOWN);
            _prevPlatformBlue.setWalkable(false);
            //GameObject particle = Resources.Load<GameObject>("Prefabs/Platform_Explotion_Particle");
            //particle = Instantiate(particle, new Vector3(_prevPlatformBlue.getX() + _prevPlatformBlue.getWidth() / 2, _prevPlatformBlue.getY() - _prevPlatformBlue.getHeight() / 2, 0), Quaternion.Euler(-90, 0, 0));
        }        
    }

    private void setAllPlatformsDone()
    {
        _platformGreen.setState(STATE_PLATFORM_TRANSITION_DONE);
        _platformRed.setState(STATE_PLATFORM_TRANSITION_DONE);
        _platformYellow.setState(STATE_PLATFORM_TRANSITION_DONE);
        _platformBlue.setState(STATE_PLATFORM_TRANSITION_DONE);        
    }

    private void checkSuccess()
    {
        if (_simonSequence.Count == 0)
        {
            if ((_platformGreen.getState() != STATE_PLATFORM_DONE && _platformGreen.getState() != STATE_PLATFORM_TRANSITION_DONE) &&
                (_platformRed.getState() != STATE_PLATFORM_DONE && _platformRed.getState() != STATE_PLATFORM_TRANSITION_DONE) &&
                (_platformYellow.getState() != STATE_PLATFORM_DONE && _platformYellow.getState() != STATE_PLATFORM_TRANSITION_DONE) &&
                (_platformBlue.getState() != STATE_PLATFORM_DONE && _platformBlue.getState() != STATE_PLATFORM_TRANSITION_DONE))
            {
                setAllPlatformsDone();
                _onQueueToShutDown = true;                
            }
            if (_platformGreen.getState() == STATE_PLATFORM_DONE && _platformRed.getState() == STATE_PLATFORM_DONE
                && _platformYellow.getState() == STATE_PLATFORM_DONE && _platformBlue.getState() == STATE_PLATFORM_DONE)
            {
                _isSolved = true;
                _difficulty = _difficulty + _difficultyIncrement;
                _score = (int)(_difficulty * 100);

                _spawnCoinRate -= _difficultyIncrement;
                if (_spawnCoinRate < 1 )
                {
                    _spawnCoinRate = 1;
                }

                _spawnRewardRate -= _difficultyIncrement;
                if (_spawnRewardRate < 1)
                {
                    _spawnRewardRate = 1;
                }
                createPlatform();
            }
            
        }
    }

    public void changePlayerColor()
    {
        if (isPlayerOnPlatform(_platformGreen))
        {
            _player.setColor(CGameConstants.COLOR_GREEN);
        }
        else if (isPlayerOnPlatform(_platformRed))
        {
            _player.setColor(CGameConstants.COLOR_RED);
        }
        else if (isPlayerOnPlatform(_platformYellow))
        {
            _player.setColor(CGameConstants.COLOR_YELLOW);
        }
        else if (isPlayerOnPlatform(_platformBlue))
        {
            _player.setColor(CGameConstants.COLOR_BLUE);
        }
        else
        {
            _player.setColor(CGameConstants.COLOR_BASE);
        }
    }

    public void createCoin(CPlatform aPlatform)
    {
        Object coin = Resources.Load("Prefabs/Coin");
        Instantiate(coin, new Vector3(aPlatform.getX() + PLATFORM_WIDTH / 2, aPlatform.getY() + _player.getHeight() / 2, 0), Quaternion.identity);
    }

    public void createBox(CPlatform aPlatform)
    {
        Object box = Resources.Load("Prefabs/Box");
        Instantiate(box, new Vector3(aPlatform.getX() + PLATFORM_WIDTH / 2, aPlatform.getY() + CBox.HEIGHT, 0), Quaternion.identity, aPlatform.gameObject.transform);
    }

    public int getCoinMultip()
    {
        return _comboMultip;
    }

    public float getComboElapsedTime()
    {
        return _comboElapsedTime;
    }

    public bool isShowed()
    {
        return _isShowed;
    }

    public int getStateGO()
    {
        return _goText.getState();
    }

    public void setPause(bool aBool)
    {
        _pause = aBool;
    }

    public bool getPause()
    {
        return _pause;
    }

    public void setComboPause(bool aComboPause)
    {
        _comboPause = aComboPause;
    }

    public int getScore()
    {
        return _score;
    }

    void OnDestroy()
    {
        CSaveLoad.setBestScore(_score);
    }
}
