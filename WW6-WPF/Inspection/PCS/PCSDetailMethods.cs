using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinWam6.Inspection.PCS
{
    public partial class PCSDetail
    {
        // Inspection Calculation Section
        private double averageTare = 0;
        private bool totalTareFlag = false;
        private bool conversionValid = false;
        private bool volOKforTest = false;
        private double averageWeight = 0;
        private decimal costError;
        public bool mavFailure = false;

        private void CalcSampleSize()
        {
            int oldSampleSize = this.SampleSize;

            switch (this.InspCat)
            {
                case PCSInspectionCategory.CatB:
                    {
                        mavError = 0;
                        if (LotSize < 10)
                        {
                            SampleSize = LotSize;
                            tareSampleSize = 2;
                            break;
                        }

                        if (LotSize < 251)
                        {
                            SampleSize = 10;
                            tareSampleSize = 2;
                            break;
                        }

                        SampleSize = 30;
                        tareSampleSize = 5;
                        break;
                    }

                case PCSInspectionCategory.CatA:
                    {
                        tareSampleSize = 2;

                        if (Glass && (LotSize > 250))
                            tareSampleSize = 3;

                        if (LotSize < 12)
                        {
                            SampleSize = LotSize;
                            mavError = 0;
                            break;
                        }

                        if (LotSize < 251)
                        {
                            SampleSize = 12;
                            mavError = 0;
                            break;
                        }

                        SampleSize = 48;
                        mavError = 1;

                        if (MavType == PCSMAVType.Mulch)
                        {
                            mavError = SampleSize / 12;
                        }

                        break;
                    }


                case PCSInspectionCategory.Audit:
                    {
                        if (LotSize > 48)
                        {
                            //ToDo MessageBox alerting user that size > 48
                            SampleSize = 48;
                        }
                        else
                        {
                            SampleSize = LotSize;
                        }
                        break;
                    }

                case PCSInspectionCategory.SingleComm:
                    {
                        if (LotSize > 200)
                        {
                            //ToDo Error Messagebox -- set size to 200 max
                            SampleSize = 200;
                        }
                        else
                        {
                            SampleSize = LotSize;
                        }
                        break;
                    }
            }

            AdjustTestCount();

            // Adjust Tare Sample Size if Sample Size is very low, or for mulch
            if (SampleSize < 2) tareSampleSize = SampleSize;

            if (MavType == PCSMAVType.Mulch) tareSampleSize = 0;


        }

        private void AdjustTestCount()
        {
            // Do we now have too many tests? If so, chop off the extras
            while (PCSTests.Count > SampleSize)
            {
                PCSTests.RemoveAt(PCSTests.Count - 1);
            }

            // Not enough tests? Add more
            while (PCSTests.Count < SampleSize)
            {
                var pcsTest = new PCSTest();
                if (PackageType.Standard == PackType)
                {
                    pcsTest.MarkedWeight = Marked;
                }
                pcsTest.Pack_ID = Pack_ID;
                pcsTest.Test = (short)(pcsTests.Count + 1);
                pcsTest.Parent = this;
                PCSTests.Add(pcsTest);
            }
        }

        private double CorrectionFactor(int sampleSize)
        {
            if (SampleSize < 2) return -99.0;
            if (SampleSize == 2) return -8.984;
            if (SampleSize == 3) return -2.484;
            if (SampleSize == 4) return -1.591;
            if (SampleSize == 5) return -1.241;
            if (SampleSize == 6) return -1.05;
            if (SampleSize == 7) return -0.925;
            if (SampleSize == 8) return -0.836;
            if (SampleSize == 9) return -0.769;
            if (SampleSize == 10) return -0.715;
            if (SampleSize == 11) return -0.672;
            if (SampleSize == 12) return -0.635;
            if (SampleSize == 24) return -0.422;
            return -0.291;
        }

        private bool Use2002Tables()
        {
            return true;
        }

        private void CalculateAdjustedTare()
        {
            if ((pcsTests.Count == 0) || (PCSTests.Count < tareSampleSize))
                return;

            double minTare = 0;
            double maxTare = 0;
            double minNet = 0;
            double maxNet = 0;
            double curNet;
            double curTare;

            for (int i = 0; i < tareSampleSize; i++ )
            {
                curTare = pcsTests.ElementAt(i).Tare;
                curNet = pcsTests.ElementAt(i).GrossWeight - curTare;

                minTare = Math.Min(minTare, curTare);
                maxTare = Math.Max(maxTare, curTare);
                minNet = Math.Min(minNet, curNet);
                maxNet = Math.Max(maxNet, curNet);
            }

            double rcrt;

            if (maxTare != minTare)
            {
                rcrt = (maxNet - minNet) / (maxTare - minTare);
                rcrt = Math.Round(rcrt + 0.0001, 2);
            }
            else
            {
                rcrt = 9999;
            }

            RcRt rcrtTable = RcRt.Instance;
            int adjustedTare = rcrtTable.AlternateTare(rcrt, SampleSize, tareSampleSize);

            adjustedTare = Math.Max(adjustedTare, tareSampleSize);  // adjust for '3' on Glass Tare

            if (InspCat == PCSInspectionCategory.SingleComm)
                adjustedTare = 2;

            if (MavType == PCSMAVType.Mulch)
                adjustedTare = 0;

            finalTareSampleSize = adjustedTare;
            //TODO Raise various event flags.
        }

        private void CalculateAverageTare()
        {
            if (IsZeroTare())
            {
                averageTare = 0;
                return;
            }

            int tareCount = 0;
            double tareSum = 0;
            double oldAverageTare = averageTare;

            foreach (var pt in pcsTests)
            {
                if (pt.Tare > 0)
                {
                    tareCount++;
                    tareSum += pt.Tare;
                }
            }
            if (tareCount >= tareSampleSize && tareCount != 0)
            {
                averageTare = WWH.Truncate(tareSum + 0.00001, 3);
                totalTareFlag = (pcsTests.Count == tareCount);
            }
            else
            {
                averageTare = 0;
                totalTareFlag = false;
            }

            if (averageTare != oldAverageTare)
            {
                //TODO RaiseUpdateEvent
            }
        }

        public bool IsZeroTare()
        {
            return pcsTests.All(pt => pt.Tare == 0);
        }

        public double CalculateMAV(double pkgMarked)
        {
            switch (MavType)
            {
                case PCSMAVType.Normal:
                case PCSMAVType.USDAStd:
                case PCSMAVType.USDAFluid:
                    return units.CalcMAV(pkgMarked, MavType);
                
                case PCSMAVType.Mulch:
                    return pkgMarked * 0.05;

                    case PCSMAVType.PE:
                    return pkgMarked * 0.04;

                default:
                    return 0;

            }
        }

        public void UpdateVolumeMultiple()
        {
            double avgWt;

            volOKforTest = false;

            if (Vol1 == 0 || Vol2 == 0)
            {
                avgWt = Vol1 + Vol2;
            }
            else
            {
                avgWt = (Vol1 + Vol2) / 2;
            }

            if (StdVol == 0 || avgWt == 0)
            {
                volumeMultiple = 1;
                conversionValid = false;
            }
            else
            {
                volumeMultiple = StdVol / avgWt;
                if (Math.Abs(Vol1 - Vol2) > 0.01)
                {
                    conversionValid = true;
                    volOKforTest = true;
                }
            }
        }

        public void UpdateAllMAVs()
        {
            foreach (var pt in pcsTests)
            {
                pt.MAV = CalculateMAV(pt.MarkedWeight);
            }
        }

        public void UpdateResults()
        {

            // First check for cases that do not require any calculations

            if (InspCat == PCSInspectionCategory.Audit)
            {
                Result = PCSResults.Audit;
                return;
            }

            if (InspCat == PCSInspectionCategory.SingleComm)
            {
                Result = PCSResults.SingleComm;
                return;
            }

            if (pcsTests.Count < 1)
                return;

            //Now we're into the meat of it

            // Calculate the statistics required by the different category calculations

            var pcsResult = PCSResults.Pass;

            int numTests = 0;
            double totalError = 0;
            double totalWeight = 0;
            int pkgFail = 0;
            int pkgGray = 0;
            const double epsilon = 0.00001;
            mavFailure = false;

            foreach (var pt in pcsTests)
            {
                double curNet = pt.Volume;

                if (curNet > 0)
                {
                    double weightError = curNet - pt.MarkedWeight;
                    if (Math.Abs(weightError) > pt.MAV + epsilon)
                    {
                        if (GrayPct > 0 && (Math.Abs(weightError) < epsilon + (pt.MAV + (pt.MarkedWeight * (GrayPct / 100)))))
                        {
                            pkgGray += 1;
                        }
                        else
                        {
                            pkgFail += 1;
                        }
                    }

                    totalError += weightError;
                    totalWeight += pt.MarkedWeight;
                    numTests++;
                }
            }

            if (numTests > 0)
            {
                totalError = Math.Sign(totalError) * (Math.Abs(totalError) + epsilon);
                AvgErr = WWH.Truncate(totalError / numTests, 3);
                averageWeight = totalWeight / numTests;
                costError = (decimal)AvgErr * CostPer;
            }
            else
            {
                AvgErr = 0;
                averageWeight = 0;
                costError = 0;
            }

            // Now determine the Pass/Fail based on the category type

            switch (InspCat)
            {
                case PCSInspectionCategory.CatA:
                    {
                        if (pkgFail > mavError)
                            pcsResult = PCSResults.Fail;
                        else
                        {
                            if (pkgGray + pkgFail > mavError)
                            {
                                pcsResult = PCSResults.Gray;
                                mavFailure = true;
                            }
                        }

                        //Standard Deviation Check

                        if (numTests > 1 && numTests <= LotSize)
                        {
                            double testSD = pcsTests.Where(pt => pt.Volume > 0).Sum(pt => Math.Pow((pt.Volume - pt.MarkedWeight - AvgErr), 2)); //Standard deviation accumulator

                            StdDeviation = Math.Sqrt(testSD / numTests - 1);
                            testSD = StdDeviation * CorrectionFactor(SampleSize);

                            if (AvgErr + epsilon < testSD && pcsResult != PCSResults.Fail)
                            {
                                if (GrayPct > 0 && Math.Abs(AvgErr - testSD) < (averageWeight * GrayPct))
                                {
                                    pcsResult = PCSResults.Gray;
                                }
                                else
                                {
                                    pcsResult = PCSResults.Fail;
                                }
                            }
                        }
                        break;

                    }

                case PCSInspectionCategory.CatB:
                {
                    if (pkgGray > 0) pcsResult = PCSResults.Gray;
                    if (pkgFail > 0) pcsResult = PCSResults.Fail;

                    if (AvgErr < (-1 * epsilon) && pcsResult != PCSResults.Fail)
                    {
                        if (GrayPct > 0 && Math.Abs(AvgErr) < (averageWeight * GrayPct))
                        {
                            pcsResult = PCSResults.Gray;
                        }
                        else
                        {
                            pcsResult = PCSResults.Fail;
                        }
                    }
                    break;
                }
                    
            }

            Result = pcsResult;

        }

        // Events

        private void TareUpdated(object sender, EventArgs e)
        {
            CalculateAdjustedTare();
            //TODO other stuff
        }

        private void FullUpdate(object sender, EventArgs e)
        {
            if (UpdateInProcess)
            return;

            UpdateInProcess = true;
            //TODO update everything

            UpdateInProcess = false;
        }


    }
}
