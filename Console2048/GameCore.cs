using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2048
{
    /// <summary>
    /// 游戏核心类,负责处理游戏核心算法,与界面无关:游戏核心类不适合做成静态类
    /// </summary>
    internal class GameCore
    {
        private int[,] map;
        //出于复用，将调用数组改写成成员变量，并且在构造函数中初始化
        private int[] mergeArray;
        private int[] removeZeroArray;
        private int[,] originalMap;
        public bool IsChange { get; private set; }
        public int[,] Map { get { return this.map; } }
        /// <summary>
        /// 在构造函数中初始化二维数组与调用数组
        /// </summary>
        public GameCore()
        {
            map = new int[4, 4];
            mergeArray = new int[4];
            removeZeroArray = new int[4];
            emptyLocationList = new List<Location>(16);
            random = new Random();
            originalMap = new int[4, 4];
        }
        /// <summary>
        /// 移动方法
        /// </summary>
        /// <param name="map">移动数组</param>
        /// <param name="direction">移动方向</param>
        public void Move(/*int[,] map,因为是负责算法，所以二维数组内算法写作人最清楚，由这个类来确定二维数组*/ MoveDirection direction)
        {
            //移动前记录map
            Array.Copy(map, originalMap, map.Length);
            IsChange = false;
            switch (direction)
            {
                case MoveDirection.Left: MoveLeft(/*map*/); break;
                case MoveDirection.Up: MoveUp(/*map*/); break;
                case MoveDirection.Down: MoveDown(/*map*/); break;
                case MoveDirection.Right: MoveRight(/*map*/); break;
                default: break;
            }
            //移动后对比map
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == originalMap[i, j])
                    {
                        IsChange = true;
                        return;//取返回值Unity里面不好操作
                    }
                }
            }
        }
        /// <summary>
        /// 将零元素移至数组末尾
        /// </summary>
        /// <param name="array">移动数组</param>
        /// <returns>移动完毕数组</returns>
        private void RemoveZero(/*int[] array*/)//引用
        {
            //移动零元素即可创建新数组将非零元素依次赋值到新数组
            //int[] newarray = new int[array.Length];
            //每次去零操作清空去零数组
            Array.Clear(removeZeroArray, 0, removeZeroArray.Length);
            int Index = 0;
            for (int i = 0; i < mergeArray.Length; i++)
            {
                if (mergeArray[i] != 0)
                {
                    removeZeroArray[Index++] = mergeArray[i];//传递堆中数据
                    //Index++;
                }

            }
            // array = newarray;  替换引用,方法外不受到影响
            //通过引用修改堆中数据
            removeZeroArray.CopyTo(mergeArray, 0);
            //Array.Copy(newarray, array, array.Length);
            //或者   newarray.CopyTo(array,0);//表示从该数组第零个开始复制
        }
        /// <summary>
        /// 合并数据
        /// </summary>
        /// <param name="array">合并数组</param>
        /// <returns>合并完毕数组</returns>
        private void Merge(/*int[] array*/)
        {
            RemoveZero(/*array*/);
            //合并数据
            for (int i = 0; i < mergeArray.Length - 1; i++)
            {
                //相邻相同
                if (mergeArray[i] != 0 && mergeArray[i] == mergeArray[i + 1])
                //加入非零元素判断是为了之后统计使用(便于做动画)统计合并位置
                {
                    mergeArray[i] += mergeArray[i + 1];
                    mergeArray[i + 1] = 0;
                }
            }
            RemoveZero();
        }
        /// <summary>
        /// 上移方法
        /// </summary>
        /// <param name="map">待上移二维数组</param>
        /// <returns>上移完毕二维数组</returns>
        private void MoveUp(/*int[,] map*/)
        {
            //从上到下形成一维数组
            //int[] mergeArray = new int[map.GetLength(0)];//每移动一次就产生一个创建完毕的数组垃圾
            for (int j = 0; j < map.GetLength(1); j++)//GetLength(1)是读取列数
            {
                for (int i = 0; i < map.GetLength(0); i++)//GetLength(0)是读取行数
                {
                    mergeArray[i] = map[i, j];
                }
                Merge(/*mergeArray*/);
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    map[i, j] = mergeArray[i];
                }
            }
            //CreateRandom(map);

        }
        /// <summary>
        /// 下移方法
        /// </summary>
        /// <param name="map">待下移二维数组</param>
        /// <returns>下移完毕二维数组</returns>
        private void MoveDown(/*int[,] map*/)
        {
            //从下到上形成一维数组
            //int[] mergeArray = new int[map.GetLength(0)];
            for (int j = 0; j < map.GetLength(1); j++)//GetLength(1)是读取列数
            {
                for (int i = map.GetLength(0) - 1; i >= 0; i--)//GetLength(0)是读取行数
                {
                    mergeArray[(map.GetLength(0) - 1) - i] = map[i, j];
                }
                Merge(/*mergeArray*/);
                for (int i = map.GetLength(0) - 1; i >= 0; i--)
                {
                    map[i, j] = mergeArray[(map.GetLength(0) - 1) - i];
                }
            }
            //CreateRandom(map);

        }
        /// <summary>
        /// 输出二维数组
        /// </summary>
        /// <param name="array">待输出二维数组</param>
        private void PrintDoubleArray(/*Array array*/)
        {
            for (int i = 0; i < mergeArray.GetLength(0); i++)
            {
                for (int j = 0; j < mergeArray.GetLength(1); j++)
                {
                    Console.Write(mergeArray.GetValue(i, j) + "\t");
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// 左移方法
        /// </summary>
        /// <param name="map">待左移二维数组</param>
        /// <returns>左移完毕二维数组</returns>
        private void MoveLeft(/*int[,] map*/)
        {
            //int[] mergeArray = new int[map.GetLength(1)];
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    mergeArray[j] = map[i, j];
                }
                Merge(/*mergeArray*/);
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = mergeArray[j];
                }
            }
            //CreateRandom(map);
        }
        /// <summary>
        /// 右移方法
        /// </summary>
        /// <param name="map">待右移二维数组</param>
        /// <returns>右移完毕二维数组</returns>
        private void MoveRight(/*int[,] map*/)
        {
            //int[] mergeArray = new int[map.GetLength(1)];
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = map.GetLength(1) - 1; j >= 0; j--)
                {
                    mergeArray[(map.GetLength(1) - 1) - j] = map[i, j];
                }
                Merge(/*mergeArray*/);
                for (int j = map.GetLength(1) - 1; j >= 0; j--)
                {
                    map[i, j] = mergeArray[(map.GetLength(1) - 1) - j];
                }
            }
            //CreateRandom(map);
        }
        /// <summary>
        /// 产生随机数
        /// </summary>
        private Random random;
        //如果仅在数字中随机放置越到后面越卡，所以需要把它拿出来
        //先计算所有的空白位置，再随机一个位置
        //再随机2，4
        //因为位置数量一直变化所以用集合进行存储而不用数组
        private List<Location> emptyLocationList;
        /// <summary>
        /// 统计空位置
        /// </summary>
        private void CalculateEmpty()
        {
            //每次统计空位置清空集合
            emptyLocationList.Clear();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 0)
                    {
                        //记录 i j
                        //类【数据类型】 类数据成员[多个类型]
                        //将多个基本数据类型，复合封装为一个复合数据类型
                        emptyLocationList.Add(new Location(i, j));
                    }
                }
            }
        }

        /// <summary>
        /// 选择一个位置生成一个数字
        /// </summary>
        public void GenerateNumber()
        {
            CalculateEmpty();
            if (emptyLocationList.Count > 0)
            {
                //随机索引 选择一个随机数
                int randomIndex = random.Next(0, emptyLocationList.Count);
                Location location = emptyLocationList[randomIndex];
                map[location.RIndex, location.CIndex] = random.Next(0, 10) == 1 ? 4 : 2;
            }

        }


        ///// <summary>
        ///// 将二维数组中随机等于零位置放入数字2，二的概率是90 四是十
        ///// </summary>
        ///// <param name="map">待放入二维数组</param>
        ///// <returns>放置完毕二维数组</returns>
        //private void CreateRandom(/*int[,] map*/) 
        //{
        //    int CreateZero = -1;
        //    int CreateOne = -1;
        //    do
        //    {
        //        CreateZero = random.Next(0, map.GetLength(0));
        //        CreateOne = random.Next(0, map.GetLength(1));
        //    } while (map[CreateZero, CreateOne] != 0);
        //    map[CreateZero, CreateOne] = 2;
        //}
    }
}
