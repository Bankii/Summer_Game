using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDestroyOnPlayerNotCharging : MonoBehaviour {

    CPlayer _player;

    public void setPlayer(CPlayer aPlayer)
    {
        _player = aPlayer;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(_player != null)
        { 
            if (_player.getState() != CPlayer.STATE_CHARGING)
            {
                Destroy(gameObject);
            }
        }
	}
}
