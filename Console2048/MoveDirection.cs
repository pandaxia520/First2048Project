using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2048
{
    /// <summary>
    /// 定义枚举类型:移动方向   
    /// </summary>
    enum MoveDirection
    {
        Up, 
        Down,
        Left,
        Right
        //相当于
        // Up=0, 
        //Down=1,
        //Left=2,
        //Right=3
        //枚举是整形,枚举的本质是给0，1，2，3，4赋一个标签
    }
    //同样可以写成以下形式
    enum MoveDirections:int
    {
        Up=1, 
        Down=2,
        Left=3,
        Right=4,
    }
    //简单枚举即列举某种数据的所有取值
    //作用:增强代码可读性,限定取值
    /*语法:
     * enum 名字{值1，值2，值3}   (值类型)
     *  
     * 枚举元素默认为int,准许使用的枚举类型有byte、sbyte、short、ushort、int、uint、long或ulong
     * 每个枚举元素都是由枚举值。默认情况下,第一个枚举的值为0,后面每个枚举的值递增1,可以修改值
     * 后面的枚举类型依次递增
     */

    [Flags]
    enum PersonStyle
    {
       // tall,         //00000000
       // rich,         //00000001
       // handsome,     //00000010
       // white,        //00000011 
       // beauty        //00000100
           tall=1,         //00000001
           rich=2,         //00000010
           handsome=4,     //00000100
           white=8,        //00001000 
           beauty=16       //00010000
    }

    //选择多个枚举值
    //运算符 | (按位或)  :俩个对应的二进制位中有一个为一,结果位为一
    // tall | rich  ===>  00000000  |  00000001  ===>   00000001
    /*条件:
     * 1.任意多个枚举值做 | 运算, 的结果不能和其它枚举值相同(值以2^次方递增)          重点
     * 2.定义枚举时使用[Flags]特性修饰(一般并不知道枚举是否可多选,看顶上是否有[Flags])
     * 
     * 判断标志枚举是否包含指定枚举值
     * 标识符 & (按位与):俩个对应的二进制位中都为一,结果位为一 (即判断包含是否为0)
     *  tall & rich  ===>  00000000  &  00000001  ===>   00000000
     *  注:因为根据 运算符优先顺序 &(按位与) 优先值低于 ==(bool值) 所以加括号
     */
}
