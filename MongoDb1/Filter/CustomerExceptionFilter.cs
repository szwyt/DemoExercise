using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Hosting;
using MongoDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDb.Filter
{
    public class CustomerExceptionFilter : Attribute, IExceptionFilter
    {
        private readonly IHostEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public CustomerExceptionFilter(IHostEnvironment hostingEnvironment,
          IModelMetadataProvider modelMetadataProvider)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
        }

        /// <summary>
        /// 发生异常进入
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)//如果异常没有处理
            {
                if (_hostingEnvironment.IsDevelopment())//如果是开发环境
                {
                    var result = new ViewResult { ViewName = "~/Views/Shared/Error.cshtml" };
                    result.ViewData = new ViewDataDictionary(_modelMetadataProvider,
                                          context.ModelState);
                    result.ViewData.Add("Exception", context.Exception);//传递数据
                    context.Result = result;
                }
                else
                {
                    context.Result = new JsonResult(new
                    {
                        Result = false,
                        Code = 500,
                        Message = context.Exception.Message
                    });
                }
                context.ExceptionHandled = true;//异常已处理
            }
        }
    }
}
