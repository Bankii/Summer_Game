using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CSpriteTrail : CGameObject
{
    public SpriteRenderer _sr;
    private Color _color;
    
    void Start()
    {
        _color = GetComponent<SpriteRenderer>().color;
    }

    public override void apiUpdate()
    {
        base.apiUpdate();

        if (_color.a >= 0)
        {
            _color.a = _color.a - 0.10f;

            _sr.color = _color;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }


        void Update()
    {
        apiUpdate();
    }

    public void setSprite(Sprite aSprite)
    {
        _sr.sprite = aSprite;
    }

}