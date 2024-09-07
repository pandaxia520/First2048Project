using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2048
{
    internal class project
    {
        static void Main(string[] args)
        {
            GameCore core = new GameCore();
            core.GenerateNumber();
            core.GenerateNumber();
            //显示界面
            DrawMap(core.Map);
            //移动
            while (true)
            {
                KeyDown(core);
                //如果map中的数据有变化就做这个事，没变化就不做
                //既然是移动判断则放入移动
                if (core.IsChange)
                {
                core.GenerateNumber();
                DrawMap(core.Map);    
                }
            
            }
        }
        private static void DrawMap(int[,] map)
        {
            Console.Clear();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(map[i, j] + "\t");
                }
                Console.WriteLine();
            }

        }
        
        private static void KeyDown(GameCore core)
        {
            switch (Console.ReadLine())
            {
                case "w":
                    core.Move(MoveDirection.Up);
                    break;
                case "s":
                    core.Move(MoveDirection.Down);
                    break;
                case "a":
                    core.Move(MoveDirection.Left);
                    break;
                case "d":
                    core.Move(MoveDirection.Right);
                    break;
                default: break;
            }
        }
    }
}
