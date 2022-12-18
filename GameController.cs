using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Console2048;
using UnityEngine.EventSystems;
using MoveDirection = Console2048.MoveDirection;

/// <summary>
///游戏控制器
/// </summary>
public class GameController : MonoBehaviour,IPointerDownHandler,IDragHandler {
    private GameCore core;
    private NumberSprite[,] spritActionArray;

    private void Start()
    {
        core = new GameCore();
        spritActionArray = new NumberSprite[4, 4];
        Init();
        GenerateNewNumber();
        GenerateNewNumber();
    }

    private void Init()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                CreateSprite(i, j);
            }
        }
    }

    private void CreateSprite(int i, int j)
    {
        GameObject go = new GameObject(i.ToString() + j.ToString());

        go.AddComponent<Image>();
        NumberSprite action = go.AddComponent<NumberSprite>();
        //将引用存储到二维数组中
        spritActionArray[i, j] = action;
        action.SetImage(0);
        //创建的游戏对象，scale默认为1（false 不使用世界位置）
        go.transform.SetParent(this.transform, false);


    }

    private void GenerateNewNumber()
    {
        Location? loc;
        int? number;
        core.GenerateNumber(out loc, out number);
        //根据位置获取脚本对象引用
        spritActionArray[loc.Value.Rindex, loc.Value.Cindex].SetImage(number.Value);
    }

    private void Update()
    {
        //如果地图有更新
        if (core.Ischange)
        {
            //更新界面
            UpdateMap();
            //产生新数字
            GenerateNewNumber();
            //判断游戏是否结束
            core.Ischange=false;
        }
    }

    private void UpdateMap()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                spritActionArray[i, j].SetImage(core.Map[i, j]);
            }
        }
    }

    private Vector2 startPoint;
    private bool isDown=false;
    public void OnPointerDown(PointerEventData eventData)
    {
        startPoint = eventData.position;
        isDown = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDown == false) return;
        Vector3 offset=eventData.position - startPoint;
        float x = Mathf.Abs(offset.x);
        float y = Mathf.Abs(offset.y);


        MoveDirection? dir = null ;
        //水平
        if (x > y)
        {
            dir= offset.x >0? MoveDirection.Right: MoveDirection.Left;
        }
        //垂直
        if(x < y)
        {
            dir= offset.y >0?MoveDirection.Up: MoveDirection.Down;
        }
        else
        {
            print("aaa");
        }

        core.Move(dir.Value);
            isDown = false;
        
        
    }
}
