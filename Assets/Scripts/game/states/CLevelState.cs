using UnityEngine;
using System.Collections;

public class CLevelState : CGameState
{
	//private CPlayer mPlayer;
	//private CShip mPlayer;

	//private CBulletManager mBulletManager;
	//private CEnemyManager mEnemyManager;

	//private CSprite mBackground;

	//private CTileMap mMap;

	private CCamera mCamera;

	public CLevelState()
	{
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
