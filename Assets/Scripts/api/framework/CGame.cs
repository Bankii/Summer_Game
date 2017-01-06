using UnityEngine;
using System.Collections;

public class CGame : MonoBehaviour 
{
	static private CGame mInstance;
	private CGameState mState;

	private CCamera mCamera;
	private CPlayer mPlayer;
    //private CShip mShip;

    public GameObject _platform_Green;
    public GameObject _platform_Red;
    public GameObject _platform_Yellow;
    public GameObject _platform_Blue;

    void Awake()
	{
		if (mInstance != null) 
		{
			throw new UnityException ("Error in CGame(). You are not allowed to instantiate it more than once.");
		}

		mInstance = this;

		CMouse.init();
		CKeyboard.init ();

		setState(new CLevelState (_platform_Green, _platform_Red, _platform_Yellow, _platform_Blue));
		//setState(new CMainMenuState ());
	}

	static public CGame inst()
	{
		return mInstance;
	}

	// Use this for initialization
	void Start () 
	{
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
		mState.update ();
	}

	private void render()
	{
		mState.render ();
	}

	public void destroy()
	{
		CMouse.destroy ();
		CKeyboard.destroy ();
		if (mState != null) 
		{
			mState.destroy ();
			mState = null;
		}
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

	/*public void setShip(CShip aShip)
	{
		mShip = aShip;
	}

	public CShip getShip()
	{
		return mShip;
	}*/
}
