using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelTankMappingGenerator
{
    public struct Mapping
    {
        public double Height;
        public double Volume;
    }

    public class MappingCreator
    {
        public List<TankRecord> TankRecords;
        public List<NozzleRecord> NozzleRecords;
        public List<RefuelRecord> RefuelRecords;
        public List<Mapping> InitialDataTankOne;
        public List<Mapping> InitialDataTankTwo;
        public List<Mapping> InitialDataTankThree;
        public List<Mapping> InitialDataTankFour;

        public double TankOneRealVolume { get; set; }
        public double TankTwoRealVolume { get; set; }
        public double TankThreeRealVolume { get; set; }
        public double TankFourRealVolume { get; set; }

        public MappingCreator()
        {
            TankRecords = new List<TankRecord>();
            NozzleRecords = new List<NozzleRecord>();
            RefuelRecords = new List<RefuelRecord>();
            InitialDataTankOne = new List<Mapping>();
            InitialDataTankTwo = new List<Mapping>();
            InitialDataTankThree = new List<Mapping>();
            InitialDataTankFour = new List<Mapping>();
        }

        public MappingCreator(String tankFile, String nozzleFile, String refuelFile, String mappingFile1, String mappingFile2, String mappingFile3, String mappingFile4)
        {
            TankRecords = CSVParser.ReadTankMeasures(tankFile);
            NozzleRecords = CSVParser.ReadNozzleMeasures(nozzleFile);
            RefuelRecords = CSVParser.ReadRefuelMeasures(refuelFile);
            InitialDataTankOne = CSVParser.ReadMapping(mappingFile1);
            InitialDataTankTwo = CSVParser.ReadMapping(mappingFile2);
            InitialDataTankThree = CSVParser.ReadMapping(mappingFile3);
            InitialDataTankFour = CSVParser.ReadMapping(mappingFile4);
        }

        public void ReadInputFiles(String tankFile, String nozzleFile, String refuelFile, String mappingFile1, String mappingFile2, String mappingFile3, String mappingFile4)
        {
            TankRecords = CSVParser.ReadTankMeasures(tankFile);
            NozzleRecords = CSVParser.ReadNozzleMeasures(nozzleFile);
            RefuelRecords = CSVParser.ReadRefuelMeasures(refuelFile);
            InitialDataTankOne = CSVParser.ReadMapping(mappingFile1);
            InitialDataTankTwo = CSVParser.ReadMapping(mappingFile2);
            InitialDataTankThree = CSVParser.ReadMapping(mappingFile3);
            InitialDataTankFour = CSVParser.ReadMapping(mappingFile4);
        }

        public void GenerateNewMapping()
        {

        }
    }
}
