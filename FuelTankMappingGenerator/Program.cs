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
            MappingCreator mappingCreator = new MappingCreator(@"E:\OneDrive\Studia\TPDiA\Dane paliwowe wysokość dekalibracja\Dane paliwowe wysokość dekalibracja\dane\pierwotne\Zestaw 3\tankMeasures.log",
               @"E:\OneDrive\Studia\TPDiA\Dane paliwowe wysokość dekalibracja\Dane paliwowe wysokość dekalibracja\dane\pierwotne\Zestaw 3\nozzleMeasures.log",
               @"E:\OneDrive\Studia\TPDiA\Dane paliwowe wysokość dekalibracja\Dane paliwowe wysokość dekalibracja\dane\pierwotne\Zestaw 3\refuel.log",
               @"E:\OneDrive\Studia\TPDiA\Dane paliwowe wysokość dekalibracja\Dane paliwowe wysokość dekalibracja\mapowanie\pierwotne\Tank1_10012.csv",
               @"E:\OneDrive\Studia\TPDiA\Dane paliwowe wysokość dekalibracja\Dane paliwowe wysokość dekalibracja\mapowanie\pierwotne\Tank2_20000.csv",
               @"E:\OneDrive\Studia\TPDiA\Dane paliwowe wysokość dekalibracja\Dane paliwowe wysokość dekalibracja\mapowanie\pierwotne\Tank3_30000.csv",
               @"E:\OneDrive\Studia\TPDiA\Dane paliwowe wysokość dekalibracja\Dane paliwowe wysokość dekalibracja\mapowanie\pierwotne\Tank4_40000.csv"
                );

            mappingCreator.GenerateNewMapping();
            mappingCreator.MergePoints();
            mappingCreator.MergeMappings();
            mappingCreator.PrintCSV();
        }
    }
}
