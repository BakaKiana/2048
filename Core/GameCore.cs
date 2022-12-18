using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Console2048
{   /// <summary>
    /// 游戏核心类
    /// </summary>
    class GameCore
    {
        private int[,] map;
        private int[] mergeArray;
        private int[] removeZeroArray;
        private List<Location> emptyLocationList;
        private Random random;
        private int[,] originalMap;
        public bool Ischange
        {
            get;
            set;
        }
        public int[,] Map
        {
            get { return this.map; }
        }
        public GameCore(){
            map = new int[4, 4];
            mergeArray = new int[4];
            removeZeroArray = new int[4];
            emptyLocationList = new List<Location>(16);
            random = new Random();
        }

        #region 数据的合并
        private void DelZero()
        {
            Array.Clear(removeZeroArray, 0, 4);
            int index = 0;
            for (int i = 0; i < mergeArray.Length; i++)
            {
                if (mergeArray[i] != 0)
                {
                    removeZeroArray[index++] = mergeArray[i];
                }
            }
            removeZeroArray.CopyTo(mergeArray, 0);
        }
        //相邻相同的数相加
        private void Merge()
        {
            DelZero();
            for (int i = 0; i < mergeArray.Length - 1; i++)
            {
                if (mergeArray[i] != 0 && mergeArray[i] == mergeArray[i + 1])
                {
                    mergeArray[i] *= 2;
                    mergeArray[i + 1] *= 0;
                }
            }
            DelZero();
        }
        #endregion
        #region 移动
        public void MoveUp()
        {
            //从上到下获取每列，形成一维数组
            //行
            for (int c = 0; c < map.GetLength(0); c++)
            {
                //获取一列
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    mergeArray[i] = map[i, c];
                }
                Merge();
                //归还
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    map[i, c] = mergeArray[i];
                }
            }
        }
        public void MoveDown()
        {
            //从下到上获取每列，形成一维数组
            for (int c = 0; c < map.GetLength(0); c++)
            {
                for (int i = map.GetLength(0) - 1; i >= 0; i--)
                {
                    mergeArray[3 - i] = map[i, c];
                }
                Merge();
                for (int i = map.GetLength(0) - 1; i >= 0; i--)
                {
                    map[i, c] = mergeArray[3 - i];
                }
            }
        }
        public void MoveLeft()
        {
            for (int c = 0; c < map.GetLength(0); c++)
            {
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    mergeArray[i] = map[c, i];
                }
                Merge();
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    map[c, i] = mergeArray[i];
                }
            }
        }
        public void MoveRight()
        {
            for (int c = 0; c < map.GetLength(0); c++)
            {//从右往左取
                for (int i = map.GetLength(0) - 1; i >= 0; i--)
                {
                    mergeArray[3 - i] = map[c, i];
                }
                Merge();
                for (int i = map.GetLength(0) - 1; i >= 0; i--)
                {
                    map[c, i] = mergeArray[3 - i];
                }
            }
        }
        public void Move(MoveDirection direction)
        {
            Ischange = false;
            Array.Copy(map, originalMap, map.Length);
            switch (direction)
            {
                case MoveDirection.Up:
                    MoveUp();
                    break;
                case MoveDirection.Down:
                    MoveDown();
                    break;
                case MoveDirection.Left:
                    MoveLeft();
                    break;
                case MoveDirection.Right:
                    MoveRight();
                    break;
            }
            //判断有没有移动
            for (int r = 0; r < map.GetLength(0); r++)
            {
                for (int c = 0; c < map.GetLength(1); c++)
                {
                    if (map[r, c] != originalMap[r, c])
                    {
                        Ischange = true;
                        return;
                    }
                }
            }
        }
        #endregion
        #region 在随机空白位置生成数字
        private void CalculateEmpty()
        {
            //之前要清空
            emptyLocationList.Clear();
            for (int r = 0; r < map.GetLength(0); r++)
            {
                for (int c = 0; c < map.GetLength(1); c++)
                {
                    if (map[r,c]==0)
                    {
                        emptyLocationList.Add(new Location(r, c));
                    }
                }
            }
            {

            }
        }
        public void GenerateNumber(out Location? loc,out int? number)
        {
            CalculateEmpty();
            if (emptyLocationList.Count > 0)
            {
                int randomindex = random.Next(0, emptyLocationList.Count);
                loc = emptyLocationList[randomindex];
                number=map[loc.Value.Rindex, loc.Value.Cindex] = random.Next(0, 10) == 1 ? 4 : 2;
            }
            else
            {
                loc = null;
                number = null;
            }

        }
        #endregion
    }
}
