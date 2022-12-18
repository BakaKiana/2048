using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
///附加到每个方格中，用于定义方格行为
/// </summary>
public class NumberSprite : MonoBehaviour {

    private Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    public void SetImage(int number)
    {
        //读取Sprite
        //读取单个使用Load，读取图集使用LoadAll
        image.sprite = ResourceManager.LoadSprite(number);
    }

}
