using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MiniRazor;
using RazorEngineDemo.Templates;
namespace RazorEngineDemo;
class Program
{
    public static async Task Main()
    {
        // Templates are automatically transpiled into C# classes.

        // You can use the static RenderAsync method to render
        // the template against a model.

        // Note that the model is statically typed and aligned
        // with the @inherits directive specified in the template.

        //var result = await TemplateFoo.RenderAsync("John");

        //Console.WriteLine("### TemplateFoo");
        //Console.WriteLine(result);

        //Console.WriteLine("### TemplateBar");
        //await TemplateBar.RenderAsync(Console.Out, null);
        StringBuilder sb = new StringBuilder();
        await using TextWriter writer = new StringWriter(sb);
        //if (template.TemplateFile == "内置")
        //{
        //    Type innerTemplateType = template.GetInnerTemplateType();
        //    ITemplate template2 = (ITemplate)(Activator.CreateInstance(innerTemplateType)
        //        ?? throw new OsharpException($"代码配置“{template.Name}”的模板类型实例化失败"));
        //    template2.Model = model;
        //    template2.Output = writer;
        //    await template2.ExecuteAsync();
        //}
        //else
        //{
        //    if (template.TemplateFile == null || !File.Exists(template.TemplateFile))
        //    {
        //        throw new OsharpException($"代码配置“{template.Name}”的模板文件“{template.TemplateFile}”不存在");
        //    }

        //    string templateSource = await File.ReadAllTextAsync(template.TemplateFile);
        //    TemplateDescriptor descriptor = Razor.Compile(templateSource);
        //    await descriptor.RenderAsync(writer, model);
        //}
        string templateSource = await File.ReadAllTextAsync($"{Path.Combine(AppContext.BaseDirectory, "Templates", "TemplateFoo.cshtml")}");
        TemplateDescriptor descriptor = Razor.Compile(templateSource);
        await descriptor.RenderAsync(writer, new List<string> { "John", "小史" });

        string codeSource = sb.ToString();
        Console.WriteLine(codeSource);
        Console.ReadKey();
    }
}