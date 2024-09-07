using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2048
{
    /// <summary>
    /// 空白位置
    /// </summary>
    struct Location
    {
        /// <summary>
        /// 行索引
        /// </summary>
        public int RIndex {  get; set; }
        /// <summary>
        /// 列索引
        /// </summary>
        public int CIndex {  get; set; }
        /// <summary>
        /// 创建一个新位置
        /// </summary>
        /// <param name="RIndex">行索引</param>
        /// <param name="CIndex">列索引</param>
        public Location(int RIndex, int CIndex):this()
        {
            this.RIndex = RIndex;
            this.CIndex = CIndex;
        }

    }
}
