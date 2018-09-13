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

        public static bool operator ==(Mapping a, Mapping b)
        {
            return a.Height == b.Height && a.Volume == b.Volume;
        }

        public static bool operator !=(Mapping a, Mapping b)
        {
            return a.Height != b.Height || a.Volume != b.Volume;
        }
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
        public List<Mapping> NewInitialDataTankOne;
        public List<Mapping> NewInitialDataTankTwo;
        public List<Mapping> NewInitialDataTankThree;
        public List<Mapping> NewInitialDataTankFour;

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
            NewInitialDataTankOne = new List<Mapping>();
            NewInitialDataTankTwo = new List<Mapping>();
            NewInitialDataTankThree = new List<Mapping>();
            NewInitialDataTankFour = new List<Mapping>();
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
            NewInitialDataTankOne = new List<Mapping>();
            NewInitialDataTankTwo = new List<Mapping>();
            NewInitialDataTankThree = new List<Mapping>();
            NewInitialDataTankFour = new List<Mapping>();
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
            double RefuelRate = 333.333333333333;

            double Tank1AmountLeftToPump = 0;
            double Tank2AmountLeftToPump = 0;
            double Tank3AmountLeftToPump = 0;
            double Tank4AmountLeftToPump = 0;
            DateTime LastTank1RefulTime = NozzleRecords[0].Time;
            DateTime LastTank2RefulTime = NozzleRecords[0].Time;
            DateTime LastTank3RefulTime = NozzleRecords[0].Time;
            DateTime LastTank4RefulTime = NozzleRecords[0].Time;
            double LastNozzle1Total = 0;
            double LastNozzle2Total = 0;
            double LastNozzle3Total = 0;
            double LastNozzle4Total = 0;
            double CurrentTank1Volume = TankRecords[0].FuelVolume;
            double CurrentTank2Volume = TankRecords[1].FuelVolume;
            double CurrentTank3Volume = TankRecords[2].FuelVolume;
            double CurrentTank4Volume = TankRecords[3].FuelVolume;

            double totalfuelremoved = 0;
            double startfuel = TankRecords[0].FuelVolume;
            foreach (TankRecord tankRecord in TankRecords)
            {
                switch (tankRecord.TankID)
                {
                    case 1:
                        {
                            Mapping newMappingPoint;
                            newMappingPoint.Height = tankRecord.FuelHeight;

                            double tempNozzlesTotal = 0;
                            foreach (NozzleRecord nozzleRecord in NozzleRecords)
                            {
                                if (nozzleRecord.Time == tankRecord.Time && nozzleRecord.TankID == tankRecord.TankID)
                                {
                                    tempNozzlesTotal += nozzleRecord.TotalCounter;
                                }
                                else if (nozzleRecord.Time > tankRecord.Time)
                                {
                                    CurrentTank1Volume -= (tempNozzlesTotal - LastNozzle1Total);
                                    LastNozzle1Total = tempNozzlesTotal;
                                    break;
                                }
                            }

                            foreach (RefuelRecord refuelRecord in RefuelRecords)
                            {
                                DateTime dt = DateTime.ParseExact("2014-01-02 10:55:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                                if (refuelRecord.Time < tankRecord.Time && refuelRecord.Time > LastTank1RefulTime && refuelRecord.TankID == tankRecord.TankID)
                                {
                                    Tank1AmountLeftToPump += refuelRecord.FuelVolume;

                                    double difference = (refuelRecord.Time - tankRecord.Time).TotalMinutes;

                                    LastTank1RefulTime = refuelRecord.Time;
                                    //CurrentTank1Volume += difference * RefuelRate;

                                    break;
                                }
                            }

                            if (Tank1AmountLeftToPump > 0)
                            {
                                Tank1AmountLeftToPump -= RefuelRate*5;
                                CurrentTank1Volume += Tank1AmountLeftToPump > 0 ? RefuelRate*5 : (Tank1AmountLeftToPump + RefuelRate*5);

                                if (Tank1AmountLeftToPump < 0)
                                {
                                    Tank1AmountLeftToPump = 0;
                                }
                            }

                            newMappingPoint.Volume = CurrentTank1Volume;
                            NewInitialDataTankOne.Add(newMappingPoint);

                            break;
                        }
                    case 2:
                        {
                            break;
                        }
                    case 3:
                        {
                            break;
                        }
                    case 4:
                        {
                            break;
                        }
                }
            }
        }

        public void MergePoints()
        {
            List<Mapping> TempMapping = new List<Mapping>();
            TempMapping.AddRange(NewInitialDataTankOne);
            NewInitialDataTankOne.Clear();

            foreach (Mapping mapping in TempMapping)
            {
                if (!NewInitialDataTankOne.Contains(mapping))
                {
                    NewInitialDataTankOne.Add(mapping);
                }
            }
        }
    }
}
