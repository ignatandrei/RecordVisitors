using LightBDD.Core.Configuration;
using LightBDD.Core.Results;
using LightBDD.Framework.Configuration;
using LightBDD.Framework.Reporting.Formatters;
using LightBDD.XUnit2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: AutomatedTestRecord.ConfiguredLightBddScope] 
namespace AutomatedTestRecord
{
    internal class ConfiguredLightBddScopeAttribute : LightBddScopeAttribute
    {
        protected override void OnConfigure(LightBddConfiguration configuration)
        {
            configuration
                .ReportWritersConfiguration()
                .Clear()
                .AddFileWriter<XmlReportFormatter>(@"~/Reports/FeaturesReport.xml")
                .AddFileWriter<PlainTextReportFormatter>("~/Reports/{TestDateTimeUtc:yyyy-MM-dd-HH_mm_ss}_FeaturesReport.txt")
                .AddFileWriter<HtmlReportFormatter>(@"~/Reports/LightBDDHtmlReport.html")
                .AddFileWriter<MarkdownReportFormatter>(@"~/Reports/LightBDDReport.md")
                ;

        }
    }
    public class MarkdownReportFormatter : IReportFormatter
    {
        public void Format(Stream stream, params IFeatureResult[] features)
        {
            var categories = GroupCategories(features);
            var sb = new StringBuilder();
            var parents = categories
                    .Select(it => it.Info.Parent)
                    .Distinct()
                    .ToArray();
            foreach (var par in parents)
            {
                var items = categories.Where(it => it.Info.Parent == par).ToArray();
                if (items.Length == 0)
                    continue;
                sb.AppendLine($"# {par.Name}");
                foreach (var sc in items)
                {

                    sb.AppendLine($"## {sc.Info.ToString()}");
                    sb.AppendLine("| Number| Name|Status|");
                    sb.AppendLine("| ----------- | ----------- |----------- |");
                    foreach(var step in sc.GetSteps())
                    {
                        sb.AppendLine($"|{step.Info.Number}|{step.Info.Name}|{step.Status}|");
                        //put also step sub steps


                    }
                }
                
            }

            using (MemoryStream ms = new MemoryStream())
            {
                var sw = new StreamWriter(ms);
                try
                {
                    sw.Write(sb);
                    sw.Flush();//otherwise you are risking empty stream
                    ms.Seek(0, SeekOrigin.Begin);

                    ms.WriteTo(stream);
                }
                finally
                {
                    sw.Dispose();
                }
            }
        }
        private static IScenarioResult[] GroupCategories(IEnumerable<IFeatureResult> features)
        {
            var f = features
                .SelectMany(f => f.GetScenarios())
                .ToArray()
                ;
            return f;
        }
    }
}
