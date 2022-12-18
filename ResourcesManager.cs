using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///资源管理类，负责管理加载资源
/// </summary>
public class ResourceManager : MonoBehaviour {

    private static Dictionary<int,Sprite> spriteDic;
    //类被加载时调用
    static ResourceManager()
    {
        spriteDic = new Dictionary<int,Sprite>();
        var spriteArray = Resources.LoadAll<Sprite>("2048Atlas");
        foreach (var item in spriteArray)
        {
            int IntSpriteName =int.Parse(item.name);
            spriteDic.Add(IntSpriteName, item);
        }
    }

    /// <summary>
    /// 读取数字Sprite
    /// </summary>
    /// <param name="number">Sprite表示的数字</param>
    /// <returns>Sprite</returns>
    public static Sprite LoadSprite(int number)
    {
        
          return spriteDic[number];
    }

}
