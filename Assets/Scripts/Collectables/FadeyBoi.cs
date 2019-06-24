﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeyBoi : MonoBehaviour
{
    public Image mySprite;
	//sets the alpha (transparency) of a sprite
	public void SetSpriteTransparency(float transparency = .2f)
	{
		Color newColor = mySprite.color;
		newColor.a = transparency;
		mySprite.color = newColor;
	}
}
