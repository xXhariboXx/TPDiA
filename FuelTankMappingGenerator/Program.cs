using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelTankMappingGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            CSVParser.ReadTankMeasures(@"E:\OneDrive\Studia\TPDiA\Dane paliwowe wysokość dekalibracja\Dane paliwowe wysokość dekalibracja\dane\pierwotne\Zestaw 1\tankMeasures.log");
            CSVParser.ReadNozzleMeasures(@"E:\OneDrive\Studia\TPDiA\Dane paliwowe wysokość dekalibracja\Dane paliwowe wysokość dekalibracja\dane\pierwotne\Zestaw 1\nozzleMeasures.log");
            CSVParser.ReadRefuelMeasures(@"E:\OneDrive\Studia\TPDiA\Dane paliwowe wysokość dekalibracja\Dane paliwowe wysokość dekalibracja\dane\pierwotne\Zestaw 1\refuel.log");
        }
    }
}
