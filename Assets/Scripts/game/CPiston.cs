﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPiston : CDecoration {
        
    public int _height;	

    void Start()
    {
        setHeight(_height);
    }

	void Update () {
        apiUpdate();        
	}

    public override void apiUpdate()
    {
        base.apiUpdate();        
        
    }

    
}
