using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CLevelState : CGameState
{
	//private CPlayer mPlayer;
	//private CShip mPlayer;

	//private CBulletManager mBulletManager;
	//private CEnemyManager mEnemyManager;

	//private CSprite mBackground;

	//private CTileMap mMap;

	private CCamera mCamera;

    //Simon variables
    private List<int> _simonSecuence;
    private int _randomNum;
    private int _difficulty;
    private bool _isSolved;
    private bool _isGameOver; //testing

    private CPlatform _platformGreen;
    private CPlatform _platformRed;
    private CPlatform _platformYellow;
    private CPlatform _platformBlue;
    

    public CLevelState(GameObject aPlatformGreen, GameObject aPlatformRed, GameObject aPlatformYellow, GameObject aPlatformBlue)
    {
        _difficulty = 0;
        _simonSecuence = new List<int>();
        _isSolved = true;
        _isGameOver = false;

        _platformGreen = new CPlatform(0, aPlatformGreen);
        _platformRed = new CPlatform(1, aPlatformRed);
        _platformYellow = new CPlatform(2, aPlatformYellow);
        _platformBlue = new CPlatform(3, aPlatformBlue);


        //_platformGreen.setVel(new CVector(100, 100, 200));
        //Debug.Log(_platformGreen.getVel());

        //_platformGreen.setAccel(new CVector(100, 100, 200));
        //Debug.Log(_platformGreen.getAccel());

        //      mPlayer = new CPlayer();
        //      CGame.inst().setPlayer(mPlayer);

        //      mPlayer = new CShip();
        //      CGame.inst().setShip(mPlayer);

        //      mBulletManager = new CBulletManager();
        //      mEnemyManager = new CEnemyManager();

        //      mMap = new CTileMap(1);

        //      mCamera = new CCamera ();
        //mCamera.setXY (0, 0);
        //mCamera.setVelX (30);
        //CGame.inst ().setCamera (mCamera);
        //mCamera.setGameObjectToFollow (mPlayer);
    }

	override public void init()
	{
		base.init ();

		//mBackground = new CSprite ();
		//mBackground.setImage (Resources.Load<Sprite> ("Sprites/game_background"));
		//mBackground.setXY (0, 0);
		//mBackground.setSortingLayerName ("Background");
		//mBackground.setName ("background");

		//createAsteroids ();
		//createCannons ();
        
	}

	override public void update()
	{
		base.update ();

        if (_isSolved && !_isGameOver)
        {
            //If the previous secuence was solved or it's the first, show Simon Secuence
            for(int i=0; i<=_difficulty; i++)
            {
                _randomNum = CMath.randomIntBetween(0, 3);
                _simonSecuence.Add(_randomNum);
                switch (_randomNum)
                {
                    case 0:
                        //Green
                        Debug.Log("Secuence: Green");
                        //_platformGreen.GetComponent<SpriteRenderer>().sprite = _platformGreen.getPlatformSprites()[1];
                        break;
                    case 1:
                        //Red
                        Debug.Log("Secuence: Red");
                        //_platformGreen.GetComponent<SpriteRenderer>().sprite = _platformGreen.getPlatformSprites()[3];
                        break;
                    case 2:
                        //Yellow
                        Debug.Log("Secuence: Yellow");
                        //_platformGreen.GetComponent<SpriteRenderer>().sprite = _platformGreen.getPlatformSprites()[5];
                        break;
                    case 3:
                        //Blue
                        Debug.Log("Secuence: Blue");
                        //_platformGreen.GetComponent<SpriteRenderer>().sprite = _platformGreen.getPlatformSprites()[7];
                        break;
                    default:
                        break;
                }
                _isSolved = false;
            }            
        }
        else if(!_isSolved && !_isGameOver)
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
            else if(CKeyboard.pressed(KeyCode.R))
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
        else if(_isGameOver)
        {
            Debug.Log("You LOSE");
        }


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

        _platformGreen.apiUpdate();
        /*_platformRed.apiUpdate();
        _platformYellow.apiUpdate();
        _platformBlue.apiUpdate();*/
    }

    //override public void render()
    //{
    //	base.render ();

    //	/*mBackground.render ();
    //	mPlayer.render ();
    //	mBulletManager.render ();
    //	mEnemyManager.render ();
    //	mMap.render ();*/

    //	mCamera.render ();
    //}


    // TODO: Al apretar Escape da error.
    override public void destroy()
	{
		base.destroy ();

		/*mBackground.destroy ();
		mBackground = null;
		mPlayer.destroy ();
		mPlayer = null;
		mBulletManager.destroy ();
		mBulletManager = null;
		mEnemyManager.destroy ();
		mEnemyManager = null;

		mMap.destroy ();
		mMap = null;*/
		//mCamera.destroy ();
		//mCamera = null;
	}

	/*private void createAsteroids()
	{
		CAsteroid a;

		for (int i = 0; i < 3; i++) 
		{
			a = new CAsteroid(CAsteroid.ASTEROID_BIG);
			a.setX(CMath.randomIntBetween(a.getRadius(), CTileMap.WORLD_WIDTH - a.getRadius()));
			a.setY (CMath.randomIntBetween(a.getRadius(), CTileMap.WORLD_HEIGHT - a.getRadius()));
			a.setVelX(CMath.randomIntBetween(-300, 300));
			a.setVelY(CMath.randomIntBetween(-300, 300));
			CEnemyManager.inst().add(a);
		}
	}*/

	/*private void createCannons()
	{
		CCannon c;

		//c = new CCannon (3, 3);
		//CEnemyManager.inst().add(c);

		//c = new CCannon (11, 3);
		//CEnemyManager.inst().add(c);

		//c = new CCannon (16, 6);
		//CEnemyManager.inst().add(c);

		CEnemyShip e;
		e = new CEnemyShip (10, 8);
		CEnemyManager.inst().add(e);
		e.setSeek (CGame.inst ().getShip ());
		e.setMaxForce (500.0f);
        e.setColor(Color.blue);

        e = new CEnemyShip (11, 8);
		CEnemyManager.inst().add(e);
		e.setFlee (CGame.inst ().getShip ());
		e.setMaxForce (50.0f);
        e.setColor(Color.green);

        e = new CEnemyShip(15, 8);
        CEnemyManager.inst().add(e);
        e.setArrive(CGame.inst().getShip());
        e.setMaxForce(100.0f);
        e.setArriveThreshold(5.0f);
        e.setColor(Color.red);

        e = new CEnemyShip(20, 10);
        CEnemyManager.inst().add(e);
        e.setPursue(CGame.inst().getShip());
        e.setMaxForce(200.0f);
        e.setColor(Color.magenta);

        e = new CEnemyShip(20, 10);
        CEnemyManager.inst().add(e);
        e.setEvade(CGame.inst().getShip());
        e.setMaxForce(50.0f);
        e.setColor(Color.yellow);

        e = new CEnemyShip(20, 10);
        CEnemyManager.inst().add(e);
        e.setVel(new CVector(100, -100));
        e.setWander(CGame.inst().getShip().getPos());
        e.setMaxForce(100.0f);
        e.setColor(Color.gray);
    }*/
}
