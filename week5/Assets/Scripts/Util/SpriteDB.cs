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

    [SerializeField]
    private Sprite[] jacketCanadaSprites;
    public Sprite[] JacketCanadaSprites { get { return jacketCanadaSprites; } }

    [SerializeField]
    private Sprite[] jacketUSASprites;
    public Sprite[] JacketUSASprites { get { return jacketUSASprites; } }

    [SerializeField]
    private Sprite[] sockRCanadaSprites;
    public Sprite[] SockRCanadaSprites { get { return sockRCanadaSprites; } }

    [SerializeField]
    private Sprite[] sockRUSASprites;
    public Sprite[] SockRUSASprites { get { return sockRUSASprites; } }

    [SerializeField]
    private Sprite[] sockLCanadaSprites;
    public Sprite[] SockLCanadaSprites { get { return sockLCanadaSprites; } }

    [SerializeField]
    private Sprite[] sockLUSASprites;
    public Sprite[] SockLUSASprites { get { return sockLUSASprites; } }

}
