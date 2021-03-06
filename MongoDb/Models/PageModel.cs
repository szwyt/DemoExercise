﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDb.Models
{
    /// <summary>
    /// 通用分页信息类
    /// </summary>
    public class PageModel<T>
    {
        /// <summary>
        /// 当前页标
        /// </summary>
        public int pageIndex { get; set; } = 1;

        /// <summary>
        /// 总页数
        /// </summary>
        public int pageCount { get; set; } = 0;

        /// <summary>
        /// 数据总数
        /// </summary>
        public long dataCount { get; set; } = 0;

        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { set; get; } = 15;

        /// <summary>
        /// 返回数据
        /// </summary>
        public List<T> data { get; set; }
    }
}
