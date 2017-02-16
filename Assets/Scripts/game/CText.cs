using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

public class CText : CGameObject
{
    public Text _text;

    private GameObject mSprite;
    private bool isSizeBounce;
    private bool isGrowing;
    private int maxSize;
    private int normalSize;
    private Transform mTransform;
    private bool mFlip = false;

    private int _speedSizeGrow;
    private int _speedSizeReduce;

    public enum alignment
    {
        TOP_CENTER,
        CENTER,
    }


    /*public CText(string aText = "noText", alignment aAlign = alignment.CENTER, int aFontSize = 38, string aFontName = "Arial")
    {
        mSprite = new GameObject();
        mSprite.name = aText;

        mText = mSprite.AddComponent<Text>();

        GameObject canvas = GameObject.Find("Canvas");
        mSprite.transform.SetParent(canvas.transform);
        mSprite.transform.localScale = new Vector3(1, 1, 1);

        Font font = Resources.Load("Fonts/" + aFontName, typeof(Font)) as Font;
        mText.text = aText;
        mText.font = font;
        mText.fontSize = aFontSize;

        mText.rectTransform.sizeDelta = new Vector2(400, 400);

        mTransform = mSprite.transform;

        if (aAlign == alignment.CENTER)
        {
            mText.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            mText.alignment = TextAnchor.MiddleCenter;

        }
        else if (aAlign == alignment.TOP_CENTER)
        {
            mText.rectTransform.pivot = new Vector2(0f, 1.0f);
            mText.alignment = TextAnchor.UpperLeft;
        }
        render();
    }*/

    void Update()
    {
        apiUpdate();
    }

    public override void apiUpdate()
    {
        base.apiUpdate();
        
        if (isSizeBounce)
        {
            if (getFontSize() > normalSize && !isGrowing)
            {
                setFontSize(getFontSize() - _speedSizeReduce);
            }
            else if (getFontSize() < maxSize && isGrowing)
            {
                setFontSize(getFontSize() + _speedSizeGrow);                
            }
            else if (getFontSize() <= normalSize)
            {
                isSizeBounce = false;
            }
            else if (getFontSize() >= maxSize)
            {
                isGrowing = false;
            }
            
        }
    }
    public override void render()
    {
        base.render();

        Vector3 pos = new Vector3(getX(), (getY() * -1) + getZ(), 0.0f);
        mTransform.position = pos;

        if (mFlip)
        {

            mTransform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
        {
            mTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

    }
  
    public void scale(float aX, float aY, float aZ = 1.0f)
    {
        mSprite.transform.localScale = new Vector3(aX, aY, aZ);
    }

    public void setVisible(bool aIsVisible)
    {
        _text.enabled = aIsVisible;
    }

    public bool isVisible()
    {
        return _text.enabled;
    }
    

    public void setFontSize(int aFontSize)
    {
        _text.fontSize = aFontSize;
    }

    public int getFontSize()
    {
        return _text.fontSize;
    }

    public void setScale(float aScale)
    {
        mSprite.transform.localScale = new Vector3(aScale, aScale, 0.0f);
    }

    public void setScale(float aScaleX, float aSclaeY)
    {
        mSprite.transform.localScale = new Vector3(aScaleX, aSclaeY, 0.0f);
    }

    public void setRotation(float aRotation)
    {
        mSprite.transform.rotation = Quaternion.Euler(0, 0, aRotation);
    }
    public void setAlpha(float aAlpha)
    {
        Color color = _text.color;
        _text.color = new Color(color.r, color.g, color.b, aAlpha);
    }
    public void setColor(Color aColor)
    {
        _text.color = aColor;
    }
    public Color getColor()
    {
        return _text.color;
    }

    public void setText(string aText)
    {
        _text.text = aText;
    }

    public void setSize(float aWidth, float aHeight)
    {
        _text.rectTransform.sizeDelta = new Vector2(aWidth, aHeight);
    }
    public void setStyle(FontStyle on)
    {
        _text.fontStyle = on;
    }
    public void setHorizontalOverflow(HorizontalWrapMode aWrap)
    {
        _text.horizontalOverflow = aWrap;
    }

    public string getText()
    {
        return _text.text;
    }
    
    public virtual void sizeBounce(int aMaxSize, int aSpeedGrow, int aSpeedReduce)
    {
        isSizeBounce = true;
        isGrowing = true;
        maxSize = aMaxSize;
        normalSize = getFontSize();
        _speedSizeGrow = aSpeedGrow;
        _speedSizeReduce = aSpeedReduce;
    }
        

}

