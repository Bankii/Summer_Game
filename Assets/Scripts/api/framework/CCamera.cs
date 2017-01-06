using UnityEngine;
using System.Collections;

public class CCamera : CGameObject
{
	public const int WIDTH = CGameConstants.SCREEN_WIDTH;
	public const int HEIGHT = CGameConstants.SCREEN_HEIGHT;

	public const int SPEED = 60;

	private CGameObject mGameObjectToFollow;

	public CCamera()
	{
	}

	//override public void update()
	//{
	//	base.update ();

	//	if (mGameObjectToFollow != null) 
	//	{
	//		setX (mGameObjectToFollow.getX () - WIDTH / 2);
	//		setY (mGameObjectToFollow.getY () - HEIGHT / 2);
	//	} 
	//	else 
	//	{
 //           Mover la camara con el teclado.

 //           if (CKeyboard.pressed(CKeyboard.KEY_W))
 //           {
 //               setVelY(-SPEED);
 //           }
 //           else if (CKeyboard.pressed(CKeyboard.KEY_S))
 //           {
 //               setVelY(SPEED);
 //           }
 //           else {
 //               setVelY(0);
 //           }

 //           if (CKeyboard.pressed(CKeyboard.KEY_A))
 //           {
 //               setVelX(-SPEED);
 //           }
 //           else if (CKeyboard.pressed(CKeyboard.KEY_D))
 //           {
 //               setVelX(SPEED);
 //           }
 //           else {
 //               setVelX(0);
 //           }
 //       }

		// Chequear que la camara no se vaya de los bordes.
	//	checkBorder();
	//}

	private void checkBorder()
	{
		if (getX () <= 0) {
			setX (0);
		}

		if (getY () <= 0) {
			setY (0);
		}

		//if (getX () >= CTileMap.WORLD_WIDTH - WIDTH) {
		//	setX (CTileMap.WORLD_WIDTH - WIDTH);
		//}

		//if (getY () >= CTileMap.WORLD_HEIGHT - HEIGHT) {
		//	setY (CTileMap.WORLD_HEIGHT - HEIGHT);
		//}
	}

	override public void render()
	{
		base.render ();
	}

	//override public void destroy()
	//{
	//	base.destroy ();
	//}

	public void setGameObjectToFollow(CGameObject aGameObjectToFollow)
	{
		mGameObjectToFollow = aGameObjectToFollow;
	}

	public CGameObject getGameObjectToFollow()
	{
		return mGameObjectToFollow;
	}
}

