using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelTankMappingGenerator
{
    public struct Mapping : IComparable<Mapping>, IEquatable<Mapping>
    {
        public double Height;
        public double Volume;
        public DateTime Date;

        public int CompareTo(Mapping other)
        {
            if (Height > other.Height)
            {
                return 1;
            }
            else if (Height < other.Height)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public bool Equals(Mapping other)
        {
            return Height == other.Height;
        }

        public static bool operator ==(Mapping a, Mapping b)
        {
            return a.Height == b.Height && a.Volume == b.Volume;
        }

        public static bool operator !=(Mapping a, Mapping b)
        {
            return a.Height != b.Height || a.Volume != b.Volume;
        }

        public static bool operator <(Mapping a, Mapping b)
        {
            return a.Height < b.Height;
        }

        public static bool operator >(Mapping a, Mapping b)
        {
            return a.Height < b.Height;
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

        public void GenerateTankMeasures()
        {
            foreach (TankRecord tankRecord in TankRecords)
            {
                switch (tankRecord.TankID)
                {
                    case 1:
                        {
                            Mapping newMappingPoint;
                            newMappingPoint.Height = tankRecord.FuelHeight;
                            newMappingPoint.Volume = tankRecord.FuelVolume;
                            newMappingPoint.Date = tankRecord.Time;
                            NewInitialDataTankOne.Add(newMappingPoint);

                            break;
                        }
                    case 2:
                        {
                            Mapping newMappingPoint;
                            newMappingPoint.Height = tankRecord.FuelHeight;
                            newMappingPoint.Volume = tankRecord.FuelVolume;
                            newMappingPoint.Date = tankRecord.Time;
                            NewInitialDataTankTwo.Add(newMappingPoint);

                            break;
                        }
                    case 3:
                        {
                            Mapping newMappingPoint;
                            newMappingPoint.Height = tankRecord.FuelHeight;
                            newMappingPoint.Volume = tankRecord.FuelVolume;
                            newMappingPoint.Date = tankRecord.Time;
                            NewInitialDataTankThree.Add(newMappingPoint);

                            break;
                        }
                    case 4:
                        {
                            Mapping newMappingPoint;
                            newMappingPoint.Height = tankRecord.FuelHeight;
                            newMappingPoint.Volume = tankRecord.FuelVolume;
                            newMappingPoint.Date = tankRecord.Time;
                            NewInitialDataTankFour.Add(newMappingPoint);

                            break;
                        }
                }
            }
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
                                if (refuelRecord.Time < tankRecord.Time && refuelRecord.Time > LastTank1RefulTime && refuelRecord.TankID == tankRecord.TankID)
                                {
                                    Tank1AmountLeftToPump += refuelRecord.FuelVolume;

                                    double difference = (refuelRecord.Time - tankRecord.Time).TotalMinutes;

                                    LastTank1RefulTime = refuelRecord.Time;

                                    break;
                                }
                            }

                            if (Tank1AmountLeftToPump > 0)
                            {
                                Tank1AmountLeftToPump -= RefuelRate * 5;
                                CurrentTank1Volume += Tank1AmountLeftToPump > 0 ? RefuelRate * 5 : (Tank1AmountLeftToPump + RefuelRate * 5);

                                if (Tank1AmountLeftToPump < 0)
                                {
                                    Tank1AmountLeftToPump = 0;
                                }
                            }

                            newMappingPoint.Volume = CurrentTank1Volume;
                            newMappingPoint.Date = tankRecord.Time;
                            NewInitialDataTankOne.Add(newMappingPoint);

                            break;
                        }
                    case 2:
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
                                    CurrentTank2Volume -= (tempNozzlesTotal - LastNozzle2Total);
                                    LastNozzle2Total = tempNozzlesTotal;
                                    break;
                                }
                            }

                            foreach (RefuelRecord refuelRecord in RefuelRecords)
                            {
                                if (refuelRecord.Time < tankRecord.Time && refuelRecord.Time > LastTank2RefulTime && refuelRecord.TankID == tankRecord.TankID)
                                {
                                    Tank2AmountLeftToPump += refuelRecord.FuelVolume;

                                    double difference = (refuelRecord.Time - tankRecord.Time).TotalMinutes;

                                    LastTank2RefulTime = refuelRecord.Time;

                                    break;
                                }
                            }

                            if (Tank2AmountLeftToPump > 0)
                            {
                                Tank2AmountLeftToPump -= RefuelRate * 5;
                                CurrentTank2Volume += Tank2AmountLeftToPump > 0 ? RefuelRate * 5 : (Tank2AmountLeftToPump + RefuelRate * 5);

                                if (Tank2AmountLeftToPump < 0)
                                {
                                    Tank2AmountLeftToPump = 0;
                                }
                            }

                            newMappingPoint.Volume = CurrentTank2Volume;
                            newMappingPoint.Date = tankRecord.Time;
                            NewInitialDataTankTwo.Add(newMappingPoint);

                            break;
                        }
                    case 3:
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
                                    CurrentTank3Volume -= (tempNozzlesTotal - LastNozzle3Total);
                                    LastNozzle3Total = tempNozzlesTotal;
                                    break;
                                }
                            }

                            foreach (RefuelRecord refuelRecord in RefuelRecords)
                            {
                                if (refuelRecord.Time < tankRecord.Time && refuelRecord.Time > LastTank3RefulTime && refuelRecord.TankID == tankRecord.TankID)
                                {
                                    Tank3AmountLeftToPump += refuelRecord.FuelVolume;

                                    double difference = (refuelRecord.Time - tankRecord.Time).TotalMinutes;

                                    LastTank3RefulTime = refuelRecord.Time;

                                    break;
                                }
                            }

                            if (Tank3AmountLeftToPump > 0)
                            {
                                Tank3AmountLeftToPump -= RefuelRate * 5;
                                CurrentTank3Volume += Tank3AmountLeftToPump > 0 ? RefuelRate * 5 : (Tank3AmountLeftToPump + RefuelRate * 5);

                                if (Tank3AmountLeftToPump < 0)
                                {
                                    Tank3AmountLeftToPump = 0;
                                }
                            }

                            newMappingPoint.Volume = CurrentTank3Volume;
                            newMappingPoint.Date = tankRecord.Time;
                            NewInitialDataTankThree.Add(newMappingPoint);

                            break;
                        }
                    case 4:
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
                                    CurrentTank4Volume -= (tempNozzlesTotal - LastNozzle4Total);
                                    LastNozzle4Total = tempNozzlesTotal;
                                    break;
                                }
                            }

                            foreach (RefuelRecord refuelRecord in RefuelRecords)
                            {
                                if (refuelRecord.Time < tankRecord.Time && refuelRecord.Time > LastTank4RefulTime && refuelRecord.TankID == tankRecord.TankID)
                                {
                                    Tank4AmountLeftToPump += refuelRecord.FuelVolume;

                                    double difference = (refuelRecord.Time - tankRecord.Time).TotalMinutes;

                                    LastTank4RefulTime = refuelRecord.Time;

                                    break;
                                }
                            }

                            if (Tank4AmountLeftToPump > 0)
                            {
                                Tank4AmountLeftToPump -= RefuelRate * 5;
                                CurrentTank4Volume += Tank4AmountLeftToPump > 0 ? RefuelRate * 5 : (Tank4AmountLeftToPump + RefuelRate * 5);

                                if (Tank4AmountLeftToPump < 0)
                                {
                                    Tank4AmountLeftToPump = 0;
                                }
                            }

                            newMappingPoint.Volume = CurrentTank4Volume;
                            newMappingPoint.Date = tankRecord.Time;
                            NewInitialDataTankFour.Add(newMappingPoint);

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
                else
                {
                    if (NewInitialDataTankOne.Any(x => x.Height == mapping.Height && mapping.Date > x.Date))
                    {
                        Mapping m = NewInitialDataTankOne.Find(x => x.Height == mapping.Height);
                        NewInitialDataTankOne.Remove(m);
                        NewInitialDataTankOne.Add(mapping);
                    }
                }
            }

            TempMapping.Clear();
            TempMapping.AddRange(NewInitialDataTankTwo);
            NewInitialDataTankTwo.Clear();

            foreach (Mapping mapping in TempMapping)
            {
                if (!NewInitialDataTankTwo.Contains(mapping))
                {
                    NewInitialDataTankTwo.Add(mapping);
                }
                else
                {
                    if (NewInitialDataTankTwo.Any(x => x.Height == mapping.Height && mapping.Date > x.Date))
                    {
                        Mapping m = NewInitialDataTankTwo.Find(x => x.Height == mapping.Height);
                        NewInitialDataTankTwo.Remove(m);
                        NewInitialDataTankTwo.Add(mapping);
                    }
                }
            }

            TempMapping.Clear();
            TempMapping.AddRange(NewInitialDataTankThree);
            NewInitialDataTankThree.Clear();

            foreach (Mapping mapping in TempMapping)
            {
                if (!NewInitialDataTankThree.Contains(mapping))
                {
                    NewInitialDataTankThree.Add(mapping);
                }
                else
                {
                    if (NewInitialDataTankThree.Any(x => x.Height == mapping.Height && mapping.Date > x.Date))
                    {
                        Mapping m = NewInitialDataTankThree.Find(x => x.Height == mapping.Height);
                        NewInitialDataTankThree.Remove(m);
                        NewInitialDataTankThree.Add(mapping);
                    }
                }
            }

            TempMapping.Clear();
            TempMapping.AddRange(NewInitialDataTankFour);
            NewInitialDataTankFour.Clear();

            foreach (Mapping mapping in TempMapping)
            {
                if (!NewInitialDataTankFour.Contains(mapping))
                {
                    NewInitialDataTankFour.Add(mapping);
                }
                else
                {
                    if (NewInitialDataTankFour.Any(x => x.Height == mapping.Height && mapping.Date > x.Date))
                    {
                        Mapping m = NewInitialDataTankFour.Find(x => x.Height == mapping.Height);
                        NewInitialDataTankFour.Remove(m);
                        NewInitialDataTankFour.Add(mapping);
                    }
                }
            }


            NewInitialDataTankOne.Sort();
            NewInitialDataTankTwo.Sort();
            NewInitialDataTankThree.Sort();
            NewInitialDataTankFour.Sort();
        }

        public void MergeMappings()
        {
            double startHeight;
            double endHeight;

            startHeight = NewInitialDataTankOne[0].Height;
            endHeight = NewInitialDataTankOne[NewInitialDataTankOne.Count - 1].Height;

            InitialDataTankOne.RemoveAll(x => (x.Height >= startHeight && x.Height <= endHeight));
            InitialDataTankOne.AddRange(NewInitialDataTankOne);
            InitialDataTankOne.Sort();

            startHeight = NewInitialDataTankTwo[0].Height;
            endHeight = NewInitialDataTankTwo[NewInitialDataTankTwo.Count - 1].Height;

            InitialDataTankTwo.RemoveAll(x => (x.Height >= startHeight && x.Height <= endHeight));
            InitialDataTankTwo.AddRange(NewInitialDataTankTwo);
            InitialDataTankTwo.Sort();

            startHeight = NewInitialDataTankThree[0].Height;
            endHeight = NewInitialDataTankThree[NewInitialDataTankThree.Count - 1].Height;

            InitialDataTankThree.RemoveAll(x => (x.Height >= startHeight && x.Height <= endHeight));
            InitialDataTankThree.AddRange(NewInitialDataTankThree);
            InitialDataTankThree.Sort();

            startHeight = NewInitialDataTankFour[0].Height;
            endHeight = NewInitialDataTankFour[NewInitialDataTankFour.Count - 1].Height;

            InitialDataTankFour.RemoveAll(x => (x.Height >= startHeight && x.Height <= endHeight));
            InitialDataTankFour.AddRange(NewInitialDataTankFour);
            InitialDataTankFour.Sort();
        }

        public void PrintCSV()
        {
            using (var w = new StreamWriter("NewMappingTank1.csv"))
            {
                //w.WriteLine("Height;Volume;Time");
                w.WriteLine("Height;Volume;Time");
                w.Flush();

                foreach (Mapping mapping in InitialDataTankOne)
                {
                    var first = mapping.Height;
                    var second = mapping.Volume;
                    var third = mapping.Date.ToLongTimeString();
                    //var line = string.Format("{0};{1};{2}", first, second, third);
                    var line = string.Format("{0};{1}", first, second);
                    w.WriteLine(line);
                    w.Flush();
                }
            }

            using (var w = new StreamWriter("NewMappingTank2.csv"))
            {
                w.WriteLine("Height;Volume");
                w.Flush();

                foreach (Mapping mapping in InitialDataTankTwo)
                {
                    var first = mapping.Height;
                    var second = mapping.Volume;
                    var line = string.Format("{0};{1}", first, second);
                    w.WriteLine(line);
                    w.Flush();
                }
            }

            using (var w = new StreamWriter("NewMappingTank3.csv"))
            {
                w.WriteLine("Height;Volume");
                w.Flush();

                foreach (Mapping mapping in InitialDataTankThree)
                {
                    var first = mapping.Height;
                    var second = mapping.Volume;
                    var line = string.Format("{0};{1}", first, second);
                    w.WriteLine(line);
                    w.Flush();
                }
            }

            using (var w = new StreamWriter("NewMappingTank4.csv"))
            {
                w.WriteLine("Height;Volume");
                w.Flush();

                foreach (Mapping mapping in InitialDataTankFour)
                {
                    var first = mapping.Height;
                    var second = mapping.Volume;
                    var line = string.Format("{0};{1}", first, second);
                    w.WriteLine(line);
                    w.Flush();
                }
            }
        }
    }
}
