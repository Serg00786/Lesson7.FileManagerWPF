using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public sealed class DiskUtility
    {
        public long DiskSize { get; set; }
        public long PlaceLeft { get; set; }
    }
    public sealed class ReportService
    {
        public string GenerateReport(DiskUtility diskUtility, string output = "")
        {
            string template = "Размер диска @Model.DiskSize Гб, Осталось @Model.PlaceLeft Гб";

            string report = Engine.Razor.RunCompile(template, "myUniqueReport", null, diskUtility);

            return report;
        }
    }

}
