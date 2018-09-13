using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelTankMappingGenerator
{
    public struct TankRecord
    {
        public DateTime Time;
        public int TankID;
        public double FuelHeight;
        public double FuelVolume;
    }

    public struct NozzleRecord
    {
        public DateTime Time;
        public int TankID;
        public double TotalCounter;
    }

    public struct RefuelRecord
    {
        public DateTime Time;
        public int TankID;
        public double FuelVolume;
        public double RefuleRate;
    }

    static public class CSVParser
    {
        static public List<TankRecord> ReadTankMeasures(String FilePath)
        {
            List<TankRecord> fuelRecords = new List<TankRecord>();

            using (var reader = new StreamReader(FilePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    TankRecord tankRecord;
                    tankRecord.Time = DateTime.ParseExact(values[0], "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    tankRecord.TankID = Int32.Parse(values[3]);
                    tankRecord.FuelHeight = Double.Parse(values[4]);
                    tankRecord.FuelVolume = Double.Parse(values[5]);

                    fuelRecords.Add(tankRecord);
                }
            }

            return fuelRecords;
        }

        static public List<NozzleRecord> ReadNozzleMeasures(String FilePath)
        {
            List<NozzleRecord> fuelRecords = new List<NozzleRecord>();

            using (var reader = new StreamReader(FilePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    NozzleRecord nozzleRecord;
                    nozzleRecord.Time = DateTime.ParseExact(values[0], "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    nozzleRecord.TankID = Int32.Parse(values[3]);
                    nozzleRecord.TotalCounter = Double.Parse(values[5]);

                    fuelRecords.Add(nozzleRecord);
                }
            }

            return fuelRecords;
        }

        static public List<RefuelRecord> ReadRefuelMeasures(String FilePath)
        {
            List<RefuelRecord> fuelRecords = new List<RefuelRecord>();

            using (var reader = new StreamReader(FilePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    RefuelRecord refuelRecord;
                    refuelRecord.Time = DateTime.ParseExact(values[0], "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    refuelRecord.TankID = Int32.Parse(values[1]);
                    refuelRecord.FuelVolume = Double.Parse(values[2]);
                    refuelRecord.RefuleRate = Double.Parse(values[3]);

                    fuelRecords.Add(refuelRecord);
                }
            }

            return fuelRecords;
        }
    }
}
