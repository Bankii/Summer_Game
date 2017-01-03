using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameObject : MonoBehaviour {

    private Transform _transform;

    void Awake()
    {
        _transform = GetComponent<Transform>();
    }
}
