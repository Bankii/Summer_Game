using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CParticleCoinPickUp : MonoBehaviour {

    private ParticleSystem ps;
    
	// Use this for initialization
	void Start () {

        ps = GetComponent<ParticleSystem>();

        ps.maxParticles = CGame.inst().getCoinMultip();
	}
	
	
}
