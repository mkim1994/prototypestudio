
using UnityEngine;
using System.Collections;
using Yarn.Unity;
using UnityEngine.UI;
/// Attach sprite renderer to game object
//[RequireComponent(typeof(SpriteRenderer))]
/// Attach SpriteSwitcher to game object
// <<setsprite [name of the GameObject that this script is attached to] [SpriteInfo name of the sprite you want]>> <-- put 
// e.x. <<setsprite ShipFace happy>>
public class SpriteSwitcher : MonoBehaviour
{

    [System.Serializable]
    public struct SpriteInfo
    {
        public string name;
        public Sprite sprite;
    }

    public SpriteInfo[] sprites;

    /// Create a command to use on a sprite
    [YarnCommand("setsprite")]
    public void UseSprite(string spriteName)
    {

        Sprite s = null;
        foreach (var info in sprites)
        {
            if (info.name == spriteName)
            {
                s = info.sprite;
                break;
            }
        }
        if (s == null)
        {
            Debug.LogErrorFormat("Can't find sprite named {0}!", spriteName);
            return;
        }

        //GetComponent<SpriteRenderer>().sprite = s;
        GetComponent<Image>().sprite = s;
    }
}


