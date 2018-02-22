using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sprite DB")]
public class SpriteDB : ScriptableObject
{
    [SerializeField]
    private Sprite handSprite;
    public Sprite HandSprite { get { return handSprite; } }

    [SerializeField]
    private Sprite handgrabSprite;
    public Sprite HandgrabSprite { get { return handgrabSprite; } }


}
