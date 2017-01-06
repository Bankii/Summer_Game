using UnityEngine;
using System.Collections;

public class CKeyboard
{
	static private bool mInitialized = false;

	public const KeyCode RIGHT = KeyCode.RightArrow;
	public const KeyCode LEFT = KeyCode.LeftArrow;
	public const KeyCode SPACE = KeyCode.Space;
	public const KeyCode ESCAPE = KeyCode.Escape;
	public const KeyCode UP = KeyCode.UpArrow;
	public const KeyCode DOWN = KeyCode.DownArrow;

	public const KeyCode KEY_W = KeyCode.W;
	public const KeyCode KEY_A = KeyCode.A;
	public const KeyCode KEY_S = KeyCode.S;
	public const KeyCode KEY_D = KeyCode.D;

    public const KeyCode KEY_G = KeyCode.G;
    public const KeyCode KEY_R = KeyCode.R;
    public const KeyCode KEY_Y = KeyCode.Y;
    public const KeyCode KEY_B = KeyCode.B;

    public CKeyboard()
	{
		throw new UnityException ("Error in CKeyboard(). You're not supposed to instantiate this class.");
	}
	
	public static void init()
	{
		if (mInitialized) 
		{
			return;
		}
		mInitialized = true;
	}
	
	public static void update()
	{
	}
	
	public static void destroy()
	{
		if (mInitialized) 
		{
			mInitialized = false;
		}
	}
	
	public static bool pressed(KeyCode aKey)
	{
		return Input.GetKey(aKey);
	}

	public static bool firstPress(KeyCode aKey)
	{
		return Input.GetKeyDown (aKey);
	}
}