using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelTankMappingGenerator
{
    public class MappingCreator
    {
        public List<TankRecord> TankRecords;
        public List<NozzleRecord> NozzleRecords;
        public List<RefuelRecord> RefuelRecords;

        public double TankOneRealVolume { get; set; }
        public double TankTwoRealVolume { get; set; }
        public double TankThreeRealVolume { get; set; }
        public double TankFourRealVolume { get; set; }

        public MappingCreator()
        {
            TankRecords = new List<TankRecord>();
            NozzleRecords = new List<NozzleRecord>();
            RefuelRecords = new List<RefuelRecord>();
        }

        public MappingCreator(String tankFile, String nozzleFile, String refuelFile)
        {
            TankRecords = CSVParser.ReadTankMeasures(tankFile);
            NozzleRecords = CSVParser.ReadNozzleMeasures(nozzleFile);
            RefuelRecords = CSVParser.ReadRefuelMeasures(refuelFile);
        }


    }
}
