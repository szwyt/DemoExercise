using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapperDemo
{
    public class ExtAutoMapper
    {
        #region 字段
        protected MapperConfiguration config;
        protected IMapper mapper;
        private static readonly ExtAutoMapper dal = new ExtAutoMapper();
        public static ExtAutoMapper GetInstance
        {
            get { return dal; }
        }
        #endregion

        #region 初始化
        public ExtAutoMapper()
        {
            config = new MapperConfiguration(cfg => Create(cfg));
            mapper = config.CreateMapper();
        }

        #endregion

        #region 方法
        /// <summary>
        /// 类型映射
        /// </summary>
        /// <param name="cfg"></param>
        public virtual void Create(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ProductEntity, ProductDTO>()
           .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))  //指定字段一一对应
           .ForMember(d => d.Date, opt => opt.MapFrom(s => s.Amount.ToString("yyyy-MM-dd HH:mm:ss")));  //指定字段一一对应
        }

        /// <summary>
        ///  类型映射,默认字段名字一一对应
        /// </summary>
        /// <typeparam name="TSource">要被转化的实体，Entity</typeparam>
        /// <typeparam name="TDestination">转化之后的model，可以理解为viewmodel</typeparam>
        /// <param name="source">可以使用这个扩展方法的类型，任何引用类型</param>
        /// <returns>转化之后的实体</returns>
        public TDestination Map<TSource, TDestination>(TSource source)
            where TDestination : class
            where TSource : class
        {
            if (source == null) return default(TDestination);
            return mapper.Map<TDestination>(source);
        }

        /// <summary>
        ///  类型映射,默认字段名字一一对应
        /// </summary>
        /// <typeparam name="TSource">要被转化的实体，Entity</typeparam>
        /// <typeparam name="TDestination">转化之后的model，可以理解为viewmodel</typeparam>
        /// <param name="source">可以使用这个扩展方法的类型，任何引用类型</param>
        /// <returns>转化之后的实体</returns>
        public IEnumerable<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> list)
            where TDestination : class
            where TSource : class
        {
            if (list == null) return Enumerable.Empty<TDestination>().ToList();
            return Array.ConvertAll(list.ToArray(), item => mapper.Map<TDestination>(item));
        }
        #endregion

    }
}
