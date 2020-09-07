using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewJson
{
    class Program
    {
        static void Main(string[] args)
        {
            string fp = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "1.json");
            writeJson(fp, new ConfigInfo
            {
                txt_batCode = "LG_1400001",
                txt_weightHi = 2510,
                txt_weightLow = 5470,
                txt_maxKun = 100,
                ck_flag = true,
                txt_weight = 20
            });

            Console.WriteLine(JsonConvert.SerializeObject(readJson(fp)));
            Console.ReadKey();
        }

        static void writeJson(string path, ConfigInfo obj)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            FileStream fs1 = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            fs1.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(obj));
        }

        static ConfigInfo readJson(string path)
        {
            if (File.Exists(path))
            {
                return JsonConvert.DeserializeObject<ConfigInfo>(File.ReadAllText(path));
            }
            return null;
        }
    }

    public class ConfigInfo
    {
        [Description("当前炉号")]
        public string txt_batCode { get; set; }

        [Description("高于")]
        public int txt_weightHi { get; set; }

        [Description("低于")]
        public int txt_weightLow { get; set; }

        [Description("打到捆号")]
        public int txt_maxKun { get; set; }

        [Description("是否加磅差")]
        public bool ck_flag { get; set; }

        [Description("磅差")]
        public int txt_weight { get; set; }
    }
}
