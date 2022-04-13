using ABB.Robotics.Controllers.RapidDomain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
using System.Text;

namespace ShoesV6
{
    public partial class Form1 : Form
    {
        [DllImport("pcldllv22.dll", EntryPoint = "Normal", CallingConvention = CallingConvention.Cdecl)]
        private extern static void Normal();
        private Thread Thread_bw;
        private Mat imagesource;
        private int count_photo = 0;
        private List<List<double>> linecompose = new List<List<double>>();
        Stopwatch sw = new Stopwatch();
        Stopwatch sw2 = new Stopwatch();
        bool flll = false;
        string startpath = Application.StartupPath;
        private void scan(object message)
        {
            Mat image_linecompose;
            List<List<double>> newHeight = new List<List<double>>();
            List<Point3D> MinpointHeightL = new List<Point3D>();
            List<Point3D> MinpointHeightR = new List<Point3D>();
            List<double> widthL = new List<double>();
            List<double> widthR = new List<double>();
            string dir;
            bool sampleBool = true;
            double size = 0;
            while (sampleBool)
            {
                if ((string)message == "sample")
                    sampleBool = false;
                MinpointHeightL.Clear();
                MinpointHeightR.Clear();
                newHeight.Clear();
                linecompose.Clear();
                widthL.Clear();
                widthR.Clear();
                count_photo = 0;
                flll = false;
                dir = "null";
                GC.Collect();
                while (!ShareArea.input[0])
                    Thread.Sleep(0);
                size = ShareArea.size;
                ShareArea.input[0] = false;
                ShareArea.input[1] = false;
                sw.Start();
                sw2.Start();
                ShareArea.isscan = true;
                Thread_bw = new Thread(bw);
                Thread_bw.IsBackground = true;
                Thread_bw.Start();
                Thread_bw.Join();
                sw2.Stop();
                Console.WriteLine("取像+處理" + sw2.ElapsedMilliseconds + "ms");
                ShareArea.isscan = false;
                ShareArea.isshifpath = true;
                image_linecompose = new Mat(linecompose.Count(), linecompose[0].Count(), MatType.CV_8UC1);

                for (int y = 0; y < linecompose.Count(); y++)
                {
                    for (int x = 0; x < linecompose[0].Count(); x++)
                    {
                        image_linecompose.Set(y, x, (int)linecompose[y][x]);
                    }
                }
                image_linecompose.SaveImage(startpath + @"\Save\2.5D\original.bmp");
                Mat bottom_spliced = null;
                Interpolation(image_linecompose, linecompose, out bottom_spliced);
                bottom_spliced.SaveImage(startpath + @"\Save\2.5D\bottom_spliced.bmp");
                fixedgeAndFindheight(bottom_spliced, linecompose, out newHeight, out MinpointHeightL, out MinpointHeightR, out widthL, out widthR, out dir);
                for (int y = 0; y < newHeight.Count(); y++)
                {
                    for (int x = 0; x < newHeight[0].Count(); x++)
                    {
                        image_linecompose.Set(y, x, (int)newHeight[y][x]);
                    }
                }
                image_linecompose.SaveImage(startpath + @"\Save\2.5D\2.bmp");
                Console.WriteLine("取像張數:" + count_photo);
                sw2.Restart();
                makepcd(image_linecompose, linecompose);
                edgeHeight_pcd(MinpointHeightL, MinpointHeightR, widthL, widthR, (string)message);
                sw2.Stop();
                sw2.Restart();
                Normal();
                sw2.Stop();
                MovePath(dir, (string)message, size);
                ShareArea.isshifpath = false;
                sw.Stop();
                Console.WriteLine("取像與處理時間" + sw.ElapsedMilliseconds + "ms");
                sw.Reset();
                if(!sampleBool)
                    btn_pathadj_Click(null, null);
            }
        }
        private void resample()
        {
            string message = "sample";
            Mat image_linecompose;
            List<List<double>> newHeight = new List<List<double>>();
            List<Point3D> MinpointHeightL = new List<Point3D>();
            List<Point3D> MinpointHeightR = new List<Point3D>();
            List<double> widthL = new List<double>();
            List<double> widthR = new List<double>();
            MinpointHeightL.Clear();
            MinpointHeightR.Clear();
            newHeight.Clear();
            linecompose.Clear();
            widthL.Clear();
            widthR.Clear();
            string dir;
            count_photo = 0;
            flll = false;
            dir = "null";
            GC.Collect();
            string path = @"C:\Users\user\Desktop\ShoesV6\ShoesV6\bin\x64\Debug\Save\0";
            for (int i = 0; i < 272; i++) 
            {
                ShareArea.bitmaps.Enqueue(new Bitmap(path + @"\" + i + ".bmp"));
            }
            sw.Start();
            sw2.Start();
            ShareArea.isscan = true;
            Thread_bw = new Thread(bw);
            Thread_bw.IsBackground = true;
            ShareArea.input[1] = true;
            Thread_bw.Start();
            Thread_bw.Join();
            ShareArea.input[1] = false;
            sw2.Stop();
            Console.WriteLine("取像+處理" + sw2.ElapsedMilliseconds + "ms");
            ShareArea.isscan = false;
            ShareArea.isshifpath = true;
            image_linecompose = new Mat(linecompose.Count(), linecompose[0].Count(), MatType.CV_8UC1);

            for (int y = 0; y < linecompose.Count(); y++)
            {
                for (int x = 0; x < linecompose[0].Count(); x++)
                {
                    image_linecompose.Set(y, x, (int)linecompose[y][x]);
                }
            }
            image_linecompose.SaveImage(startpath + @"\Save\2.5D\original.bmp");
            Mat bottom_spliced = null;
            Interpolation(image_linecompose, linecompose, out bottom_spliced);
            bottom_spliced.SaveImage(startpath + @"\Save\2.5D\bottom_spliced.bmp");
            fixedgeAndFindheight(bottom_spliced, linecompose, out newHeight, out MinpointHeightL, out MinpointHeightR, out widthL, out widthR, out dir);
            for (int y = 0; y < newHeight.Count(); y++)
            {
                for (int x = 0; x < newHeight[0].Count(); x++)
                {
                    image_linecompose.Set(y, x, (int)newHeight[y][x]);
                }
            }
            image_linecompose.SaveImage(startpath + @"\Save\2.5D\2.bmp");
            Console.WriteLine("取像張數:" + count_photo);
            sw2.Restart();
            makepcd(image_linecompose, linecompose);
            edgeHeight_pcd(MinpointHeightL, MinpointHeightR, widthL, widthR, (string)message);
            sw2.Stop();
            sw2.Restart();
            Normal();
            sw2.Stop();
            MovePath(dir, (string)message, 0);
            ShareArea.isshifpath = false;
            UploadtoFTP();
            sw.Stop();
            Console.WriteLine("取像與處理時間" + sw.ElapsedMilliseconds + "ms");
            sw.Reset();
        }
        private void bw()
        {
            while (!ShareArea.input[1] || ShareArea.bitmaps.Count() > 0)
            {
                if (ShareArea.bitmaps.Count() > 0)
                {
                    Bitmap c;
                    ShareArea.bitmaps.TryDequeue(out c);
                    imagesource = BitmapConverter.ToMat(c);
                    ProcessImage();
                }
            }
        }
        private void ProcessImage()
        {
            if (checkBox_saveimage.Checked)
            {
                imagesource.SaveImage(startpath + @"\Save\0\" + count_photo.ToString() + ".bmp");
            }
            count_photo++;
            if (count_photo > 350)
                Thread_bw.Abort();
            //質心法參數
            int LowYIndex;
            List<System.Drawing.Point> Centroid = new List<System.Drawing.Point>();
            double weight = 0, denominator = 0;
            System.Drawing.Point CPoint = new System.Drawing.Point();
            //
            Mat result = new Mat();
            Mat draw = imagesource.Clone();
            Cv2.CvtColor(imagesource, result, ColorConversionCodes.BGRA2GRAY);
            Mat element = Cv2.GetStructuringElement(MorphShapes.Cross,
                           new OpenCvSharp.Size(ShareArea.Dilate, ShareArea.Dilate), new OpenCvSharp.Point(-1, -1));
            Cv2.Threshold(result, result, ShareArea.Threshold, 255, ThresholdTypes.Binary);
            draw = result.Clone();
            Cv2.CvtColor(draw, draw, ColorConversionCodes.GRAY2BGR);
            Cv2.MorphologyEx(result, result, MorphTypes.Close, element, new OpenCvSharp.Point(-1, -1));
            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchies;
            Cv2.FindContours(result, out contours, out hierarchies, RetrievalModes.Tree, ContourApproximationModes.ApproxNone);
            List<OpenCvSharp.Point[]> allcontour = new List<OpenCvSharp.Point[]>();
            int[] laser = new int[imagesource.Width];
            double[] tmp25D = new double[imagesource.Width];
            if (contours != null)
            {
                for (int k = 0; k < contours.Length; k++)
                {
                    if (contours[k].Average(p => p.Y) > 70 && contours[k].Average(p => p.Y) < 240 && Cv2.ContourArea(contours[k]) > 50)
                    {
                        //Cv2.DrawContours(result, contours, k, new Scalar(0, 0, 255));
                        //result.SaveImage(Application.StartupPath + @"\save\2\" + i + ".bmp");
                        Cv2.DrawContours(draw, contours, k, new Scalar(0, 0, 255));
                        draw.SaveImage(startpath + @"\save\1\" + count_photo + ".bmp");
                        allcontour.Add(contours[k]);
                    }
                }
                for (int k = 0; k < allcontour.Count(); k++)
                {
                    allcontour[k] = allcontour[k].OrderBy(p => p.Y).ToArray();
                    allcontour[k] = allcontour[k].OrderByDescending(p => p.X).ToArray();
                }

                //////找到雷射的質心//////
                if (allcontour.Count() != 0)
                {
                    if (!flll)
                        Console.WriteLine(count_photo);
                    flll = true;
                    for (int m = 0; m < allcontour.Count(); m++)
                    {
                        CPoint.X = allcontour[m][0].X;

                        for (int k = 0; k < allcontour[m].Count(); k++)
                        {
                            if (allcontour[m][k].X == CPoint.X)
                            {
                                for (LowYIndex = k; LowYIndex <= k + 15 && LowYIndex < allcontour[m].Count() - 1; LowYIndex++)
                                {
                                    if (allcontour[m][LowYIndex].X != CPoint.X)
                                    {
                                        LowYIndex -= 1;
                                        break;
                                    }
                                }
                                for (int j = allcontour[m][k].Y; j <= allcontour[m][LowYIndex].Y; j++)
                                {
                                    weight += imagesource.Get<byte>(j, allcontour[m][k].X) * (j - allcontour[m][k].Y + 1);
                                    denominator += imagesource.Get<byte>(j, allcontour[m][k].X);
                                }

                                if (weight == 0 && denominator == 0)
                                    CPoint.Y = 0;
                                else
                                    CPoint.Y = (int)Math.Round(weight / denominator) + allcontour[m][k].Y - 1;
                                Centroid.Add(CPoint);
                                weight = 0;
                                denominator = 0;
                                if (LowYIndex == allcontour[m].Count() - 1)
                                    break;
                                CPoint.X = allcontour[m][LowYIndex + 1].X;
                            }
                        }
                    }
                }
                /////////////


                for (int j = 0; j < Centroid.Count(); j++)
                {
                    double tmpHeight = (Math.Pow(Centroid[j].Y, 2) * ShareArea.abc[0, 0] + (Centroid[j].Y) * ShareArea.abc[0, 1] + ShareArea.abc[0, 2]);
                    if (tmpHeight <= 0 || tmpHeight > 75)
                    {
                        tmpHeight = 0;
                    }
                    if (tmp25D[Centroid[j].X] != 0)
                        tmp25D[Centroid[j].X] = tmpHeight > tmp25D[Centroid[j].X] ? tmpHeight : tmp25D[Centroid[j].X];
                    else
                        tmp25D[Centroid[j].X] = tmpHeight;
                }
            }
            else
            {
                for (int j = 0; j < tmp25D.Count(); j++)
                    tmp25D[j] = 0;
            }
            linecompose.Add(tmp25D.ToList<double>());
        }
        private void fixedgeAndFindheight(Mat bottom_spliced, List<List<double>> linecompose, out List<List<double>> newHeight, out List<Point3D> MinpointHeightL, out List<Point3D> MinpointHeightR,
            out List<double> widthL, out List<double> widthR, out string dir)
        {
            sw2.Restart();
            Mat original = bottom_spliced.Clone();
            Mat thresholdimg = original.Clone();
            List<int> line_mid = new List<int>();
            List<int> lineAt = new List<int>();
            double max, minvalue = 0;
            int counter = 0, lx = 0, rx = 0, xMid = 0, checkPosition1 = 0, checkPosition2 = 0;
            bool leftfind = false, rightfind = false;
            System.Drawing.Point maxpointL = new System.Drawing.Point();
            System.Drawing.Point maxpointR = new System.Drawing.Point();
            System.Drawing.Point minpointL = new System.Drawing.Point();
            System.Drawing.Point minpointR = new System.Drawing.Point();
            newHeight = new List<List<double>>(linecompose);
            MinpointHeightL = new List<Point3D>();
            MinpointHeightR = new List<Point3D>();
            widthL = new List<double>();
            widthR = new List<double>();
            Point3D pointMinHeight = new Point3D();
            Point3D pointMaxHeight = new Point3D();
            Mat element = Cv2.GetStructuringElement(MorphShapes.Cross, new OpenCvSharp.Size(3, 3), new OpenCvSharp.Point(-1, -1));

            //影像處理
            Cv2.Threshold(original, thresholdimg, 0, 255, ThresholdTypes.Binary);
            Cv2.MorphologyEx(thresholdimg, thresholdimg, MorphTypes.Close, element, new OpenCvSharp.Point(-1, -1));
            Cv2.MorphologyEx(thresholdimg, thresholdimg, MorphTypes.Open, element, new OpenCvSharp.Point(-1, -1));
            thresholdimg.SaveImage(startpath + @"\Save\2.5D\contour1.bmp");
            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchies;
            Cv2.FindContours(thresholdimg, out contours, out hierarchies, RetrievalModes.Tree, ContourApproximationModes.ApproxNone);
            Rect BoundingRectangle = new Rect();
            var b = contours.Select((c, index) => new { index, c }).OrderByDescending(n => n.c.Length).Take(1).ToArray();
            BoundingRectangle = Cv2.BoundingRect(contours[b[0].index]);
            Mat boundingbox = thresholdimg.CvtColor(ColorConversionCodes.GRAY2RGB);
            Cv2.Rectangle(boundingbox, BoundingRectangle, new Scalar(255, 0, 0), 1);
            boundingbox.SaveImage(startpath + @"\Save\2.5D\bound.bmp");
            //

            //判斷左右
            var TopPList = b[0].c.OrderByDescending(n => n.Y).ToArray();
            TopPList = TopPList.Where(p => p.Y == TopPList[0].Y).ToArray();
            System.Drawing.Point TopPoint = new System.Drawing.Point((TopPList.Max(p => p.X) + TopPList.Min(p => p.X)) / 2, TopPList[0].Y);
            System.Drawing.Point MidPoint = new System.Drawing.Point(0, (BoundingRectangle.Top + BoundingRectangle.Bottom) / 2);
            //
            if (contours != null)
            {
                for (int i = BoundingRectangle.Top; i < BoundingRectangle.Bottom; i++)
                {
                    for (int j = BoundingRectangle.Left; j < BoundingRectangle.Right; j++)//找左半
                    {
                        if (original.At<int>(i, j) != 0)
                        {
                            lx = j;
                            leftfind = true;
                            break;
                        }

                    }
                    for (int j = BoundingRectangle.Right; j > BoundingRectangle.Left; j--)//找右半
                    {
                        if (original.At<int>(i, j) != 0)
                        {
                            rx = j;
                            rightfind = true;
                            break;
                        }
                    }
                    if (leftfind && rightfind)
                    {
                        xMid = (rx + lx) / 2;
                        line_mid.Add(xMid);
                        lineAt.Add(i);
                        if (i == MidPoint.Y)
                            MidPoint.X = xMid;
                    }
                }
            }
            for (int i = 0; i < line_mid.Count; i++)
            {
                leftfind = false;
                rightfind = false;
                counter = 0;
                max = 0;
                for (int j = line_mid[i]; j > BoundingRectangle.Left; j--)
                {
                    if (newHeight[lineAt[i]][j] > max)//找到左邊最高點
                    {
                        max = newHeight[lineAt[i]][j];
                        maxpointL.X = j;
                        maxpointL.Y = lineAt[i];
                    }
                }
                for (int s = maxpointL.X - 3; s >= BoundingRectangle.Left; s--)
                {
                    newHeight[lineAt[i]][s] = 0;
                }
                max = 0;

                for (int j = line_mid[i]; j <= BoundingRectangle.Right - 1; j++) 
                {
                    if (newHeight[lineAt[i]][j] > max)//找到右邊最高點
                    {
                        max = newHeight[lineAt[i]][j];
                        maxpointR.X = j;
                        maxpointR.Y = lineAt[i];
                    }
                }
                for (int s = maxpointR.X + 3; s <= BoundingRectangle.Right - 1; s++) 
                {
                    newHeight[lineAt[i]][s] = 0;
                }
                if (newHeight[lineAt[i]].Count(s => s != 0) > 30)
                {
                    for (int s = maxpointL.X; s < line_mid[i]; s++)//找尋左邊最低點與最高點
                    {

                        if (newHeight[lineAt[i]][s + 1] != 0)
                        {

                            if (Math.Abs(newHeight[lineAt[i]][s] - newHeight[lineAt[i]][s + 1]) < 0.01)
                            {
                                counter += 1;
                            }
                            else
                            {
                                counter = 0;
                            }
                            if (counter == 5)
                            {
                                minpointL.X = s - 4;
                                minpointL.Y = lineAt[i];
                                minvalue = newHeight[maxpointL.Y][maxpointL.X] - newHeight[minpointL.Y][minpointL.X];

                                if (/*minvalue > 4 &&*/ minvalue < 40)
                                {
                                    pointMinHeight.X = minpointL.X;
                                    pointMinHeight.Y = minpointL.Y;
                                    pointMinHeight.Z = newHeight[minpointL.Y][minpointL.X];
                                    //if (pointMinHeight.Z <= 45)
                                    //{
                                    checkPosition1 = minpointL.X - line_mid[i];
                                    MinpointHeightL.Add(pointMinHeight);
                                    leftfind = true;
                                    //}

                                    widthL.Add(minvalue);
                                    pointMaxHeight.X = maxpointL.X;
                                    pointMaxHeight.Y = maxpointL.Y;
                                    pointMaxHeight.Z = minvalue;
                                    break;
                                }
                            }
                        }
                    }
                    for (int s = maxpointR.X; s > line_mid[i]; s--)//找尋右邊最低點與最高點
                    {
                        if (newHeight[lineAt[i]][s - 1] != 0)
                        {
                            if (Math.Abs(newHeight[lineAt[i]][s] - newHeight[lineAt[i]][s - 1]) < 0.01)
                            {
                                counter += 1;
                            }
                            else
                                counter = 0;
                            if (counter == 5)
                            {
                                minpointR.X = s + 4;
                                minpointR.Y = lineAt[i];
                                minvalue = newHeight[maxpointR.Y][maxpointR.X] - newHeight[minpointR.Y][minpointR.X];



                                if (/*minvalue > 4 &&*/ minvalue < 40)
                                {
                                    pointMinHeight.X = minpointR.X;
                                    pointMinHeight.Y = minpointR.Y;
                                    pointMinHeight.Z = newHeight[minpointR.Y][minpointR.X];
                                    //if (pointMinHeight.Z <= 45)
                                    //{
                                    checkPosition2 = minpointR.X - line_mid[i];
                                    MinpointHeightR.Add(pointMinHeight);
                                    rightfind = true;
                                    //}

                                    widthR.Add(minvalue);
                                    pointMaxHeight.X = maxpointR.X;
                                    pointMaxHeight.Y = maxpointR.Y;
                                    pointMaxHeight.Z = minvalue;
                                    break;
                                }
                            }
                        }
                    }

                    if (Math.Abs(checkPosition2 + checkPosition1) > 35 && leftfind && rightfind)
                    {
                        if (-checkPosition1 > checkPosition2)
                        {
                            leftfind = false;
                            MinpointHeightL.RemoveAt(MinpointHeightL.Count() - 1);
                            widthL.RemoveAt(widthL.Count() - 1);
                        }
                        else
                        {
                            rightfind = false;
                            MinpointHeightR.RemoveAt(MinpointHeightR.Count() - 1);
                            widthR.RemoveAt(widthR.Count() - 1);
                        }
                    }
                    if (leftfind && !rightfind)
                    {
                        pointMinHeight.X = line_mid[i] + (line_mid[i] - minpointL.X);
                        pointMinHeight.Y = minpointL.Y;
                        pointMinHeight.Z = newHeight[minpointL.Y][minpointL.X];
                        MinpointHeightR.Add(pointMinHeight);

                        widthR.Add(minvalue);

                    }
                    else if (rightfind && !leftfind)
                    {
                        pointMinHeight.X = line_mid[i] - (minpointR.X - line_mid[i]);
                        pointMinHeight.Y = minpointR.Y;
                        pointMinHeight.Z = newHeight[minpointR.Y][minpointR.X];
                        MinpointHeightL.Add(pointMinHeight);

                        widthL.Add(minvalue);
                    }
                }
            }
            double M = (double)(-TopPoint.Y + MidPoint.Y) / (TopPoint.X - MidPoint.X);
            bool direction = M >= 0 ? true : false;//True:Left False:Right
            if (direction == true)
            {
                Console.WriteLine("Left");
                dir = "Left";
            }
            else
            {
                Console.WriteLine("Right");
                dir = "Right";
            }

            #region 中值濾波
            int calPosition, ArrayIndex, order;
            List<Point3D> _MinpointHeightL = new List<Point3D>();
            List<Point3D> _MinpointHeightR = new List<Point3D>();
            List<double> _widthL = new List<double>();
            List<double> _widthR = new List<double>();
            order = 7;//取幾個點進行濾波
            double[] data = new double[order];
            for (int CurrentPosition = 0; CurrentPosition < MinpointHeightL.Count(); CurrentPosition++)
            {
                ArrayIndex = 0;
                Array.Clear(data, 0, data.Length);
                calPosition = CurrentPosition;
                if (CurrentPosition <= order / 2)
                    calPosition = order / 2;
                if (CurrentPosition >= MinpointHeightL.Count - (order / 2 + 1))
                    calPosition = MinpointHeightL.Count() - (order / 2 + 1);
                for (int j = calPosition - order / 2; j <= calPosition + order / 2; j++)
                {
                    data[ArrayIndex++] = MinpointHeightL[j].Z;
                }
                data = data.OrderBy(c => c).ToArray();
                if (Math.Abs(MinpointHeightL[CurrentPosition].Z - data[2]) < 7)
                {
                    _MinpointHeightL.Add(MinpointHeightL[CurrentPosition]);
                    _widthL.Add(widthL[CurrentPosition]);
                }
            }
            for (int CurrentPosition = 0; CurrentPosition < MinpointHeightR.Count(); CurrentPosition++)
            {
                ArrayIndex = 0;
                Array.Clear(data, 0, data.Length);
                calPosition = CurrentPosition;
                if (CurrentPosition <= order / 2)
                    calPosition = order / 2;
                if (CurrentPosition >= MinpointHeightR.Count - (order / 2 + 1))
                    calPosition = MinpointHeightR.Count() - (order / 2 + 1);
                for (int j = calPosition - order / 2; j <= calPosition + order / 2; j++)
                {
                    data[ArrayIndex++] = MinpointHeightR[j].Z;
                }
                data = data.OrderBy(c => c).ToArray();
                if (Math.Abs(MinpointHeightR[CurrentPosition].Z - data[2]) < 7)
                {
                    _MinpointHeightR.Add(MinpointHeightR[CurrentPosition]);
                    _widthR.Add(widthR[CurrentPosition]);
                }
            }
            MinpointHeightR.Clear();
            MinpointHeightL.Clear();
            widthR.Clear();
            widthL.Clear();
            MinpointHeightR.AddRange(_MinpointHeightR);
            MinpointHeightL.AddRange(_MinpointHeightL);
            widthR.AddRange(_widthR);
            widthL.AddRange(_widthL);
            #endregion

            MinpointHeightL = MinpointHeightL.OrderBy(p => p.Y).ToList();
            MinpointHeightR = MinpointHeightR.OrderBy(p => p.Y).ToList();
            sw2.Stop();
            Console.WriteLine("Fix Time" + sw2.ElapsedMilliseconds + "ms");
        }
        private void makepcd(Mat input, List<List<double>> linecompose)
        {

            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchies;
            Cv2.FindContours(input, out contours, out hierarchies, RetrievalModes.Tree, ContourApproximationModes.ApproxNone);
            Rect BoundingRectangle = new Rect();
            double contour_size = 0;
            int maxIndex = 0;
            for (int i = 0; i < contours.Length; i++)
            {
                if (Cv2.ContourArea(contours[i]) > contour_size)
                {
                    contour_size = Cv2.ContourArea(contours[i]);
                    maxIndex = i;
                }
            }
            BoundingRectangle = Cv2.BoundingRect(contours[maxIndex]);
            int jump = 1;
            Rectangle rectangle = new Rectangle();
            int counter = 0;
            Cv2.Rectangle(input, BoundingRectangle, new Scalar(255, 0, 0), 1);
            rectangle.X = BoundingRectangle.X;
            rectangle.Y = BoundingRectangle.Y;
            rectangle.Width = BoundingRectangle.Width;
            rectangle.Height = BoundingRectangle.Height;

            for (int y = rectangle.Top; y <= rectangle.Bottom; y++)
            {
                if (y <= rectangle.Height * 0.05 + rectangle.Y || y >= rectangle.Height * 0.95 + rectangle.Y)
                    jump = 1;
                else
                    jump = 5;
                for (int x = BoundingRectangle.Left; x <= BoundingRectangle.Right; x += jump)
                {
                    if (linecompose[y][x] != 0)
                        counter += 1;
                }
            }
            Console.WriteLine("長度: " + rectangle.Height * ShareArea.par_y);
            string[] header = { "#.PCD v0.7 - Point Cloud Data file format by klj", "VERSION .7", "FIELDS x y z", "SIZE 4 4 4", "TYPE F F F", "COUNT 1 1 1", "WIDTH " + counter.ToString(), "HEIGHT 1", "VIEWPOINT 0 0 0 1 0 0 0", "POINTS " + counter.ToString(), "DATA ascii" };
            System.IO.File.WriteAllLines(startpath + $@"\PCD\{DateTime.Now.ToString("yyyy-MM-dd")}\Shoes{DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss")}.pcd", header);
            System.IO.File.WriteAllLines(startpath + @"\PCD\Shoes.pcd", header);
            using (System.IO.StreamWriter fileLog = new System.IO.StreamWriter(startpath + $@"\PCD\{DateTime.Now.ToString("yyyy-MM-dd")}\Shoes{DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss")}.pcd", true))
            {
                using (StreamWriter file = new StreamWriter(startpath + @"\PCD\Shoes.pcd",true)) 
                {
                    for (int h = BoundingRectangle.Top; h <= BoundingRectangle.Bottom; h++)
                    {
                        if (h <= rectangle.Height * 0.05 + rectangle.Y || h >= rectangle.Height * 0.95 + rectangle.Y)
                            jump = 1;
                        else
                            jump = 5;
                        for (int w = BoundingRectangle.Left; w <= BoundingRectangle.Right; w += jump)
                        {
                            if (linecompose[h][w] != 0)
                            {
                                double y = -(h * ShareArea.par_y) + ShareArea.shiftY;
                                double z = linecompose[h][w];  //實際高度值
                                double x = w * (Math.Pow(z, 2) * ShareArea.abc[1, 0] + z * ShareArea.abc[1, 1] + ShareArea.abc[1, 2]) + ShareArea.shiftX;
                                string pcldata = x.ToString("0.000") + " " + y.ToString("0.000") + " " + z.ToString("0.000");
                                fileLog.WriteLine(pcldata);
                                file.WriteLine(pcldata);
                            }
                        }
                    }
                }
            }

        }
        public void edgeHeight_pcd(List<Point3D> MinpointHeightL, List<Point3D> MinpointHeightR, List<double> widthL, List<double> widthR,string message)
        {
            MinpointHeightR = MinpointHeightR.OrderBy(p => p.Y).ToList();
            MinpointHeightL = MinpointHeightL.OrderBy(p => p.Y).ToList();
            double MaxY = Math.Max(MinpointHeightL.Max(point => point.Y), MinpointHeightR.Max(Point => Point.Y));
            double MinY = Math.Min(MinpointHeightL.Min(Point => Point.Y), MinpointHeightR.Min(Point => Point.Y));
            string[] headerR = { "#.PCD v0.7 - Point Cloud Data file format by klj", "VERSION .7", "FIELDS x y z", "SIZE 4 4 4", "TYPE F F F", "COUNT 1 1 1", "WIDTH " + MinpointHeightR.Count().ToString(), "HEIGHT 1", "VIEWPOINT 0 0 0 1 0 0 0", "POINTS " + MinpointHeightR.Count().ToString(), "DATA ascii" };
            System.IO.File.WriteAllLines(startpath + $@"\PCD\{DateTime.Now.ToString("yyyy-MM-dd")}\EdgeR{DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss")}.pcd", headerR);
            System.IO.File.WriteAllLines(startpath + @"\PCD\EdgeR.pcd", headerR);
            using (System.IO.StreamWriter fileRLog = new System.IO.StreamWriter(startpath + $@"\PCD\{DateTime.Now.ToString("yyyy-MM-dd")}\EdgeR{DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss")}.pcd", true))
            {
                using (StreamWriter fileR = new StreamWriter(startpath + @"\PCD\EdgeR.pcd",true)) 
                {
                    for (int i = 0; i < MinpointHeightR.Count(); i++)
                    {
                        double y = -(MinpointHeightR[i].Y * ShareArea.par_y) + ShareArea.shiftY;
                        double z = MinpointHeightR[i].Z;
                        double x = (MinpointHeightR[i].X) * (Math.Pow(z, 2) * ShareArea.abc[1, 0] + z * ShareArea.abc[1, 1] + ShareArea.abc[1, 2]) + ShareArea.shiftX;
                        string pcldata = x.ToString("0.000") + " " + y.ToString("0.000") + " " + z.ToString("0.000");
                        fileRLog.WriteLine(pcldata);
                        fileR.WriteLine(pcldata);
                    }
                }

            }
            string[] headerL = { "#.PCD v0.7 - Point Cloud Data file format by klj", "VERSION .7", "FIELDS x y z", "SIZE 4 4 4", "TYPE F F F", "COUNT 1 1 1", "WIDTH " + MinpointHeightL.Count().ToString(), "HEIGHT 1", "VIEWPOINT 0 0 0 1 0 0 0", "POINTS " + MinpointHeightL.Count().ToString(), "DATA ascii" };
            System.IO.File.WriteAllLines(startpath + $@"\PCD\{DateTime.Now.ToString("yyyy-MM-dd")}\EdgeL{DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss")}.pcd", headerL);
            System.IO.File.WriteAllLines(startpath + @"\PCD\EdgeL.pcd", headerL);
            using (System.IO.StreamWriter fileLLog = new System.IO.StreamWriter(startpath + $@"\PCD\{DateTime.Now.ToString("yyyy-MM-dd")}\EdgeL{DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss")}.pcd", true))
            {
                using (StreamWriter fileL = new StreamWriter(startpath + @"\PCD\EdgeL.pcd",true))
                {
                    for (int i = 0; i < MinpointHeightL.Count(); i++)
                    {
                        double y = -(MinpointHeightL[i].Y * ShareArea.par_y) + ShareArea.shiftY;
                        double z = MinpointHeightL[i].Z;
                        double x = (MinpointHeightL[i].X) * (Math.Pow(z, 2) * ShareArea.abc[1, 0] + z * ShareArea.abc[1, 1] + ShareArea.abc[1, 2]) + ShareArea.shiftX;

                        string pcldata = x.ToString("0.000") + " " + y.ToString("0.000") + " " + z.ToString("0.000");
                        fileLLog.WriteLine(pcldata);
                        fileL.WriteLine(pcldata);
                    }
                }
            }
            //Console.WriteLine("Edge pcd save");
            if (message == "sample")
            {
                double average = 0;
                int a;
                for (int i = 0; i < widthL.Count(); i += 5)
                {
                    average = 0;
                    a = i + 5 > widthL.Count() ? widthL.Count() - 1 : i + 4;
                    for (int k = i; k <= a; k++)
                        average += widthL[k];
                    average /= (a - i + 1);
                    for (int k = i; k <= a; k++)
                    {
                        if (widthL[k] < average - 2.5 || widthL[k] > average + 2.5)
                        {
                            widthL.RemoveAt(k);
                            MinpointHeightL.RemoveAt(k);
                            a -= 1;
                            k -= 1;
                        }
                    }
                }
                a = 0;
                for (int i = 0; i < widthR.Count(); i += 5)
                {
                    average = 0;
                    a = i + 5 > widthR.Count() ? widthR.Count() - 1 : i + 4;
                    for (int k = i; k <= a; k++)
                        average += widthR[k];
                    average /= (a - i + 1);
                    for (int k = i; k <= a; k++)
                    {
                        if (widthR[k] < average - 2.5 || widthR[k] > average + 2.5)
                        {
                            widthR.RemoveAt(k);
                            MinpointHeightR.RemoveAt(k);
                            a -= 1;
                            k -= 1;
                        }
                    }
                }
                string[] headerLowPointHeight = { "#.PCD v0.7 - Point Cloud Data file format by klj", "VERSION .7", "FIELDS x y z", "SIZE 4 4 4", "TYPE F F F", "COUNT 1 1 1", "WIDTH " + (MinpointHeightL.Count() + MinpointHeightR.Count()).ToString(), "HEIGHT 1", "VIEWPOINT 0 0 0 1 0 0 0", "POINTS " + (MinpointHeightL.Count() + MinpointHeightR.Count()).ToString(), "DATA ascii" };
                System.IO.File.WriteAllLines(startpath + "\\PCD\\LowPointHeight.pcd", headerLowPointHeight);
                using (System.IO.StreamWriter fileLow = new System.IO.StreamWriter(startpath + "\\PCD\\LowPointHeight.pcd", true))
                {
                    for (int i = 0; i < MinpointHeightL.Count(); i++)
                    {
                        double y = (MinpointHeightL[i].Y * ShareArea.par_y);
                        double z = widthL[i];
                        double x = (MinpointHeightL[i].X) * (Math.Pow(z, 2) * ShareArea.abc[1, 0] + z * ShareArea.abc[1, 1] + ShareArea.abc[1, 2]);
                        string pcldata = x.ToString("0.000") + " " + y.ToString("0.000") + " " + z.ToString("0.000");
                        fileLow.WriteLine(pcldata);
                    }

                    for (int i = 0; i < MinpointHeightR.Count(); i++)
                    {
                        double y = (MinpointHeightR[i].Y * ShareArea.par_y);
                        double z = widthR[i];
                        double x = (MinpointHeightR[i].X) * (Math.Pow(z, 2) * ShareArea.abc[1, 0] + z * ShareArea.abc[1, 1] + ShareArea.abc[1, 2]);
                        string pcldata = x.ToString("0.000") + " " + y.ToString("0.000") + " " + z.ToString("0.000");
                        fileLow.WriteLine(pcldata);
                    }
                }
            }
        }
        private void MovePath(string dir,string message,double size)
        {
            if (dir == "null")
            {
                MessageBox.Show("沒有檢測到物件");
                return;
            }
            sw2.Restart();
            int counter_MovePath = 0, half_path;
            string normalRPath = startpath + @"\PCD\bottom_edge_normalR.pcd";
            string normalLPath = startpath + @"\PCD\bottom_edge_normalL.pcd";
            List<double> y = new List<double>();
            List<double> normalx = new List<double>();
            List<List<double>> Allinf = new List<List<double>>();
            List<double> ListY = new List<double>();
            List<double[]> Robotabc = new List<double[]>();
            List<double[]> RobotPosition = new List<double[]>();
            StreamReader sr = new StreamReader(normalRPath);
            Normal2Quaternion quaternion = new Normal2Quaternion();
            Quaternion2Euler quaternion2Euler = new Quaternion2Euler();
            bool halfFind = false;
            double[] startaxis = { 1, 0, 0 };
            double[] rotation = { 0, 1, 0, 0 };
            double[] Euler = new double[3];
            double[] halfpath = new double[2];
            double[] rotation2 = { 0.70711, 0.70711, 0, 0 };
            double[] rotation3 = { 0.70711, -0.70711, 0, 0 };
            double[] rotation45 = { 0.92388, 0.38268, 0, 0 };
            double[] xyz;
            double[] targetaxis;
            double[] Quaternion;
            double[] heelmid;
            double[] arrayPosition;
            double[] arrayabc;
            double Ex, Ey, Ez;
            double Miny, length;
            double ShrinkX, ShrinkY;
            double preX = 0, preY = 0, pre2X = 0, pre2Y = 0;
            string line;
            string[] Allsplit;
            int finalPosition = 0;
            int halfPosition = 0;
            Orient orient = new Orient();
            Robotabc.Clear();
            RobotPosition.Clear();
            while ((line = sr.ReadLine()) != null)
            {
                if (counter_MovePath > 10)
                {
                    Allsplit = line.Split(' ');
                    if (Allsplit[4] == "nan" || Allsplit[5] == "nan" || Allsplit[6] == "nan")
                        continue;
                    xyz = new double[6] { Convert.ToDouble(Allsplit[0]), Convert.ToDouble(Allsplit[1]), Convert.ToDouble(Allsplit[2]), Convert.ToDouble(Allsplit[4]), Convert.ToDouble(Allsplit[5]), Convert.ToDouble(Allsplit[6]) };
                    Allinf.Add(xyz.ToList());
                    y.Add(Convert.ToDouble(Allsplit[1]));
                    normalx.Add(Convert.ToDouble(Allsplit[4]));
                    halfPosition += 1;
                }
                counter_MovePath += 1;
            }
            counter_MovePath = 0;
            line = null;
            sr = new StreamReader(normalLPath);
            while ((line = sr.ReadLine()) != null)
            {
                if (counter_MovePath > 10)
                {
                    Allsplit = line.Split(' ');
                    if (Allsplit[4] == "nan" || Allsplit[5] == "nan" || Allsplit[6] == "nan")
                        continue;
                    xyz = new double[6] { Convert.ToDouble(Allsplit[0]), Convert.ToDouble(Allsplit[1]), Convert.ToDouble(Allsplit[2]), Convert.ToDouble(Allsplit[4]), Convert.ToDouble(Allsplit[5]), Convert.ToDouble(Allsplit[6]) };
                    Allinf.Add(xyz.ToList());
                    y.Add(Convert.ToDouble(Allsplit[1]));
                    normalx.Add(Convert.ToDouble(Allsplit[4]));
                }
                counter_MovePath += 1;
            }
            if (Allinf.Count() < 40)
            {
                MessageBox.Show("參數設定錯誤");
                return;
            }
            Miny = y.Min();
            length = y.Max() - Miny;
            //double length = Math.Abs(y.Max() - y.Min());
            //for (int i = 0; i < y.Count(); i++)//計算需要修正角度的地方
            //{
            //    if (Math.Abs(y[i]) < length * 0.05)
            //        heel.Add(i);
            //    if (Math.Abs(y[i]) > length * 0.95)
            //        head.Add(i);
            //}
            //fixangle(heel, out radian);
            //tan = Math.Tan(radian);
            //for (int i = 0; i < heel.Count(); i++)
            //    Allinf[heel[i]][3] = tan * Math.Sqrt(robotpos[heel[i]][2] * robotpos[heel[i]][2] + robotpos[heel[i]][1] * robotpos[heel[i]][1]);
            //fixangle(head, out radian);
            //tan = Math.Tan(radian);
            //for (int i = 0; i < head.Count(); i++)
            //    Allinf[head[i]][3] = tan * Math.Sqrt(robotpos[head[i]][2] * robotpos[head[i]][2] + robotpos[head[i]][1] * robotpos[head[i]][1]);
            //double[] rotation3 = new double[4];
            counter_MovePath = 1;
            for (int i = 0; i < halfPosition; i += counter_MovePath)
            {
                if (Math.Abs(Allinf[i][1]) <= length * 0.15 || Math.Abs(Allinf[i][1]) >= length * 0.85)
                    counter_MovePath = 1;
                else
                    counter_MovePath = 1;
                //if ((Math.Abs(Allinf[i][3]) <= 0.01 || Math.Abs(Allinf[i][4]) <= 0.01 || Math.Abs(Allinf[i][5]) <= 0.01) && Allinf[i][1] != Miny)
                //    continue;
                targetaxis = new double[3] { Allinf[i][3], Allinf[i][4], Allinf[i][5] };

                Quaternion = quaternion.calculateQuaternion(startaxis, targetaxis);
                //FixSprayAngle(targetaxis, out angle, sign);
                //FixSprayAngle(targetaxis, out angle, sign);
                orient.Q1 = Quaternion[0];
                orient.Q2 = Quaternion[1];
                orient.Q3 = Quaternion[2];
                orient.Q4 = Quaternion[3];
                orient.ToEulerAngles(out Ex, out Ey, out Ez);
                if (Math.Abs(Allinf[i][1]) >= length * 0.9 + Math.Abs(Allinf[0][1]))
                {
                    if (Ex > 0 || Ez < 0)
                    {
                        Ex = -Ex;
                        Ez = -Ez;
                        orient.FillFromEulerAngles(Ex, Ey, Ez);
                    }
                }
                //orient.FillFromEulerAngles(Ex, -70, Ez);
                //orient.FillFromEulerAngles(-90, Ey, Ez);
                //orient.FillFromEulerAngles(180, Ey, Ez);
                Quaternion[0] = orient.Q1;
                Quaternion[1] = orient.Q2;
                Quaternion[2] = orient.Q3;
                Quaternion[3] = orient.Q4;
                //Quaternion = quaternion.QuatMultiply(Quaternion, rotation2);
                //Quaternion = quaternion.QuatMultiply(Quaternion, rotation3);
                Euler = quaternion2Euler.calculateEulerD(Quaternion);
                ShrinkEdge(Euler, Allinf[i], ShareArea.shrink, out ShrinkX, out ShrinkY);
                if (RobotPosition.Count() > 0 && Allinf[i][1] != Miny)
                {
                    if (Math.Abs(preX - ShrinkX) <= 5 && Math.Abs(preY - ShrinkY) <= 5 || preY < ShrinkY)
                    {
                        if ((Math.Abs(preX - ShrinkX) > 15) && i < halfPosition * 0.3)
                        {
                            if (Math.Abs(ShrinkX - pre2X) < 7)
                            {
                                RobotPosition.RemoveAt(RobotPosition.Count() - 1);
                                Robotabc.RemoveAt(Robotabc.Count() - 1);
                                ListY.RemoveAt(ListY.Count() - 1);
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                if ((Math.Abs(preX - ShrinkX) > 15 && i != 0) && i < halfPosition * 0.3)
                {
                    if (Math.Abs(ShrinkX - pre2X) < 7)
                    {
                        RobotPosition.RemoveAt(RobotPosition.Count() - 1);
                        Robotabc.RemoveAt(Robotabc.Count() - 1);
                        ListY.RemoveAt(ListY.Count() - 1);

                    }

                }
                if (i > 0)
                {
                    pre2X = preX;
                    pre2Y = preY;
                }
                if (Math.Sqrt(Math.Pow(preX - ShrinkX, 2) + Math.Pow(preY - ShrinkY, 2)) < 10)
                {
                    continue;
                }
                preX = ShrinkX;
                preY = ShrinkY;

                //0512
                Quaternion = quaternion.QuatMultiply(Quaternion, rotation);
                if (i == halfPosition - 1)
                    Quaternion = quaternion.QuatMultiply(Quaternion, rotation3);
                //

                arrayPosition = new double[3];
                orient.ToEulerAngles(out Ex, out Ey, out Ez);
                arrayabc = new double[3] { Ez, Ey, Ex - 180 };
                orient.FillFromEulerAngles(Ex - 180, Ey, Ez);
                Quaternion[0] = orient.Q1;
                Quaternion[1] = orient.Q2;
                Quaternion[2] = orient.Q3;
                Quaternion[3] = orient.Q4;
                arrayPosition[0] = ShrinkX;
                arrayPosition[1] = ShrinkY;
                arrayPosition[2] = Allinf[i][2] + ShareArea.shiftHeight;

                RobotPosition.Add(arrayPosition);
                Robotabc.Add(arrayabc);


                ListY.Add(ShrinkY);
                if (RobotPosition.Count() == 1)
                {
                    orient.FillFromEulerAngles(Ex, Ey - 30, Ez);
                    Quaternion[0] = orient.Q1;
                    Quaternion[1] = orient.Q2;
                    Quaternion[2] = orient.Q3;
                    Quaternion[3] = orient.Q4;
                    //0512
                    Quaternion = quaternion.QuatMultiply(Quaternion, rotation);
                    //
                    arrayPosition = new double[3];
                    orient.ToEulerAngles(out Ex, out Ey, out Ez);
                    arrayabc = new double[3] { Ez, Ey, Ex - 180 };
                    arrayPosition[0] = ShrinkX + 50;
                    arrayPosition[1] = ShrinkY;
                    arrayPosition[2] = Allinf[i][2] + 50;

                    RobotPosition.Insert(0, arrayPosition);
                    Robotabc.Insert(0, arrayabc);
                    ListY.Insert(0, ShrinkY);
                }


                ///
                //if (i == halfPosition - 1)
                //{
                //    Quaternion = quaternion.QuatMultiply(Quaternion, rotation3);
                //    strQ = Quaternion[0].ToString() + "," + Quaternion[1].ToString() + "," + Quaternion[2].ToString() + "," + Quaternion[3].ToString();
                //    RobotQ.Add(strQ);
                //    RobotPosition.Add(strPosition);

                //}
                ///

                //if (Allinf[i][1] == Miny)
                //    finalPosition = RobotPosition.Count() - 1;
            }
            //finalPosition = RobotPosition.Count();
            //rotation2=new double[4] { 0.985, 0, 0.174, 0 };
            int OwO = Allinf.Count() - halfPosition;
            counter_MovePath = 1;
            halfpath[0] = RobotPosition[RobotPosition.Count() - 1][0];
            halfpath[1] = RobotPosition[RobotPosition.Count() - 1][1];
            half_path = RobotPosition.Count();
            for (int i = Allinf.Count() - 1; i >= halfPosition; i -= counter_MovePath)
            {
                if (Math.Abs(Allinf[i][1]) <= length * 0.15 || Math.Abs(Allinf[i][1]) >= length * 0.85)
                    counter_MovePath = 1;
                else
                    counter_MovePath = 1;
                //if (Math.Abs(Allinf[i][3]) <= 0.01 || Math.Abs(Allinf[i][4]) <= 0.01 || Math.Abs(Allinf[i][5]) <= 0.01 && Allinf[i][1] != Miny)
                //    continue;
                targetaxis = new double[3] { Allinf[i][3], Allinf[i][4], Allinf[i][5] };

                Quaternion = quaternion.calculateQuaternion(startaxis, targetaxis);
                //FixSprayAngle(targetaxis, out angle, sign);
                orient.Q1 = Quaternion[0];
                orient.Q2 = Quaternion[1];
                orient.Q3 = Quaternion[2];
                orient.Q4 = Quaternion[3];
                orient.ToEulerAngles(out Ex, out Ey, out Ez);
                if (Math.Abs(Allinf[i][1]) >= length * 0.9 + Math.Abs(Allinf[0][1]))
                {
                    if (Ex > 0 || Ez < 0)
                    {
                        Ex = -Ex;
                        Ez = -Ez;
                        orient.FillFromEulerAngles(Ex, Ey, Ez);
                    }
                }
                //orient.FillFromEulerAngles(Ex, -70, Ez);
                //orient.FillFromEulerAngles(-90, Ey, Ez);
                //orient.FillFromEulerAngles(180, Ey, Ez);
                Quaternion[0] = orient.Q1;
                Quaternion[1] = orient.Q2;
                Quaternion[2] = orient.Q3;
                Quaternion[3] = orient.Q4;
                //Quaternion = quaternion.QuatMultiply(Quaternion, rotation2);
                Euler = quaternion2Euler.calculateEulerD(Quaternion);
                ShrinkEdge(Euler, Allinf[i], ShareArea.shrink, out ShrinkX, out ShrinkY);
                if (Allinf[i][1] != Miny)
                {
                    if ((Math.Abs(preX - ShrinkX) <= 5 && Math.Abs(preY - ShrinkY) <= 5) || preY > ShrinkY)
                    {
                        if (Math.Abs(preX - ShrinkX) > 15 && i <= (halfPosition + OwO * 0.2))
                        {
                            if (Math.Abs(ShrinkX - pre2X) < 7)
                            {
                                RobotPosition.RemoveAt(RobotPosition.Count() - 1);
                                Robotabc.RemoveAt(Robotabc.Count() - 1);
                                ListY.RemoveAt(ListY.Count() - 1);
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }

                }
                if (Math.Abs(preX - ShrinkX) > 15 && i <= (halfPosition + OwO * 0.2))
                {
                    if (Math.Abs(ShrinkX - pre2X) < 7)
                    {
                        RobotPosition.RemoveAt(RobotPosition.Count() - 1);
                        Robotabc.RemoveAt(Robotabc.Count() - 1);
                        ListY.RemoveAt(ListY.Count() - 1);
                    }
                    //preX = ShrinkX;
                    //preY = ShrinkY;
                    //RobotQ.RemoveAt(RobotQ.Count() - 1);
                    //RobotPosition.RemoveAt(RobotPosition.Count() - 1);
                }
                if (i < Allinf.Count() - 1)
                {
                    pre2X = preX;
                    pre2Y = preY;
                }
                if (Math.Sqrt(Math.Pow(preX - ShrinkX, 2) + Math.Pow(preY - ShrinkY, 2)) < 10)
                {
                    continue;
                }
                preX = ShrinkX;
                preY = ShrinkY;
                //Quaternion = quaternion.QuatMultiply(Quaternion, rotation);//路徑沿X軸轉180度
                //Quaternion = quaternion.QuatMultiply(Quaternion, rotation2);
                //FixSprayAngle(targetaxis, out angle,sign);
                //Quaternion = quaternion.QuatMultiply(Quaternion, rotation3);

                /////0506
                //Quaternion = quaternion.QuatMultiply(Quaternion, rotation);
                /////
                arrayPosition = new double[3];
                orient.ToEulerAngles(out Ex, out Ey, out Ez);
                arrayabc = new double[3] { Ez, Ey, Ex + 180 };
                arrayPosition[0] = ShrinkX;
                arrayPosition[1] = ShrinkY;
                arrayPosition[2] = Allinf[i][2] + ShareArea.shiftHeight;

                RobotPosition.Add(arrayPosition);
                Robotabc.Add(arrayabc);
                if (i == halfPosition)
                {
                    if (Math.Sqrt(Math.Pow(RobotPosition[1][0] - ShrinkX, 2) + Math.Pow(RobotPosition[1][1] - ShrinkY, 2)) < 10)
                    {
                        RobotPosition.RemoveAt(RobotPosition.Count() - 1);
                        Robotabc.RemoveAt(RobotPosition.Count() - 1);
                    }
                }
                ListY.Add(ShrinkY);
            }
            bool add = false;
            double halfValue, halfValue2;
            int endposition;
            halfValue2 = RobotPosition[half_path - 1][0];
            //補鞋頭的點
            if (Math.Sqrt(Math.Pow(RobotPosition[half_path - 1][0] - RobotPosition[half_path][0], 2) + Math.Pow(RobotPosition[half_path - 1][1] - RobotPosition[half_path][1], 2)) > 20)
            {

                arrayPosition = new double[3];
                arrayPosition[0] = (RobotPosition[half_path - 1][0] + RobotPosition[half_path][0]) / 2;
                arrayPosition[1] = (RobotPosition[half_path - 1][1] + RobotPosition[half_path][1]) / 2;
                arrayPosition[2] = (RobotPosition[half_path - 1][2] + RobotPosition[half_path][2]) / 2;
                RobotPosition.Insert(half_path, arrayPosition);
                arrayabc = new double[3] { (Robotabc[half_path - 1][0] + Robotabc[half_path][0]) / 2, (Robotabc[half_path - 1][1] + Robotabc[half_path][1]) / 2,
                    (Robotabc[half_path - 1][2] + Robotabc[half_path ][2]) / 2 };
                arrayabc = new double[3] { 90, -70, 100 };
                Robotabc.Insert(half_path, arrayabc);

                orient.FillFromEulerAngles(arrayabc[2], arrayabc[1], arrayabc[0]);
                Quaternion = new double[4] { orient.Q1, orient.Q2, orient.Q3, orient.Q4 };
                add = true;
                endposition = half_path;
            }
            else
            {
                Robotabc[half_path - 1][2] = Robotabc[half_path - 1][2];
                orient.FillFromEulerAngles(Robotabc[half_path - 1][2], Robotabc[half_path - 1][1], Robotabc[half_path - 1][0]);
                Quaternion = new double[4] { orient.Q1, orient.Q2, orient.Q3, orient.Q4 };
                endposition = half_path - 1;
            }
            halfValue = RobotPosition[half_path][0];
            finalPosition = ListY.IndexOf(ListY.Min());

            //補鞋根的點
            if (Math.Sqrt(Math.Pow(RobotPosition[1][0] - RobotPosition[RobotPosition.Count() - 1][0], 2) + Math.Pow(RobotPosition[1][1] - RobotPosition[RobotPosition.Count() - 1][1], 2)) > 10)
            {

                //////0506
                //Quaternion = quaternion.QuatMultiply(Quaternion, rotation);
                //////

                heelmid = new double[3] { (RobotPosition[1][0] + RobotPosition[RobotPosition.Count() - 1][0]) / 2, (RobotPosition[1][1] + RobotPosition[RobotPosition.Count() - 1][1]) / 2, (RobotPosition[1][2] + RobotPosition[RobotPosition.Count() - 1][2]) / 2 };
                RobotPosition.Add(heelmid);
                arrayabc = new double[3] { -90, -70, -120 };
                Robotabc.Add(arrayabc);
            }
            targetaxis = new double[3] { Allinf[0][3], Allinf[0][4], Allinf[0][5] };
            Quaternion = quaternion.calculateQuaternion(startaxis, targetaxis);
            //Quaternion = quaternion.QuatMultiply(Quaternion, rotation);
            //FixSprayAngle(targetaxis, out angle, sign);

            //Quaternion = quaternion.QuatMultiply(Quaternion, rotation2);
            //FixSprayAngle(targetaxis, out angle,sign);
            //Quaternion = quaternion.QuatMultiply(Quaternion, rotation3);
            ///0506
            //Quaternion = quaternion.QuatMultiply(Quaternion, rotation2);
            ///

            //讓路徑繞回去第一點
            RobotPosition.Add(RobotPosition[1]);
            orient.Q1 = Quaternion[0];
            orient.Q2 = Quaternion[1];
            orient.Q3 = Quaternion[2];
            orient.Q4 = Quaternion[3];
            orient.ToEulerAngles(out Ex, out Ey, out Ez);
            arrayabc = new double[3] { Ez, Ey, Ex - 180 };
            Robotabc.Add(arrayabc);
            //0512
            //讓噴槍變換角度噴中間
            arrayPosition = new double[3];
            arrayPosition[0] = (RobotPosition[1][0] + RobotPosition[endposition][0]) / 2;
            arrayPosition[1] = (RobotPosition[1][1] + RobotPosition[endposition][1]) / 2;
            arrayPosition[2] = (RobotPosition[1][2] + RobotPosition[endposition][2]) / 2;
            RobotPosition.Add(arrayPosition);
            orient.Q1 = Quaternion[0];
            orient.Q2 = Quaternion[1];
            orient.Q3 = Quaternion[2];
            orient.Q4 = Quaternion[3];
            orient.ToEulerAngles(out Ex, out Ey, out Ez);
            arrayabc = new double[3] { -180, -90, 0 };
            Robotabc.Add(arrayabc);


            //0506 讓噴槍從鞋根噴到鞋頭
            RobotPosition.Add(RobotPosition[endposition]);
            Robotabc.Add(arrayabc);

            var b = RobotPosition.Select((c, index) => new { index, c }).OrderBy(n => n.c[1]).ToArray();
            Miny = b[b.Count() - 1].c[1];
            length = Math.Abs(b[0].c[1] - Miny);
            bool state1, state2;
            int counter_x = 0;

            //檢查歪太多的點
            for (int i = 2; i < RobotPosition.Count() - 2; i++)
            {
                //if (i >= half_path)

                //{
                //    ShareArea.Robotabc[i][2] = ShareArea.Robotabc[i][2] + 90;
                //    orient.FillFromEulerAngles(ShareArea.Robotabc[i][2], ShareArea.Robotabc[i][1], ShareArea.Robotabc[i][0]);
                //    Quaternion = new double[4] { orient.Q1, orient.Q2, orient.Q3, orient.Q4 };
                //    strQ = Quaternion[0].ToString() + "," + Quaternion[1].ToString() + "," + Quaternion[2].ToString() + "," + Quaternion[3].ToString();
                //    RobotQ.Insert(i, strQ);
                //    RobotQ.RemoveAt(i + 1);
                //}
                if (Math.Abs(RobotPosition[i][1] - Miny) <= length * 0.2 || Math.Abs(RobotPosition[i][1] - Miny) >= length * 0.8)
                {
                    state1 = RobotPosition[i][0] - RobotPosition[i - 1][0] >= 0 ? true : false;
                    state2 = RobotPosition[i][0] - RobotPosition[i + 1][0] < 0 ? false : true;
                    if (state1 == state2)
                    {
                        if (Math.Abs(RobotPosition[i][0] - RobotPosition[i - 1][0]) <= 1 || Math.Abs(RobotPosition[i][0] - RobotPosition[i + 1][0]) <= 1)
                            continue;
                        else
                        {
                            counter_x += 1;
                            if (counter_x == 2)
                            {
                                RobotPosition[i][0] = (RobotPosition[i + 1][0] + RobotPosition[i - 1][0]) / 2;
                                RobotPosition[i][1] = (RobotPosition[i + 1][1] + RobotPosition[i - 1][1]) / 2;
                            }

                        }
                    }
                    else
                        counter_x = 0;
                }
            }
            //檢查歪太多的點
            for (int i = 2; i < RobotPosition.Count() - 2; i++)
            {
                if (Math.Sqrt(Math.Pow(RobotPosition[i][0] - RobotPosition[i + 1][0], 2) + Math.Pow(RobotPosition[i][1] - RobotPosition[i + 1][1], 2)) < 10)
                {
                    RobotPosition.RemoveAt(i);
                    Robotabc.RemoveAt(i);
                }
                if (Math.Abs(RobotPosition[i][1] - Miny) <= length * 0.2 || Math.Abs(RobotPosition[i][1] - Miny) >= length * 0.8)
                {
                    state1 = RobotPosition[i][0] - RobotPosition[i - 1][0] >= 0 ? true : false;
                    state2 = RobotPosition[i][0] - RobotPosition[i + 1][0] < 0 ? false : true;
                    if (state1 == state2)
                    {
                        if (Math.Abs(RobotPosition[i][0] - RobotPosition[i - 1][0]) <= 1 || Math.Abs(RobotPosition[i][0] - RobotPosition[i + 1][0]) <= 1)
                            continue;
                        else
                        {
                            counter_x += 1;
                            if (counter_x == 2)
                            {
                                RobotPosition[i][0] = (RobotPosition[i + 1][0] + RobotPosition[i - 1][0]) / 2;
                                RobotPosition[i][1] = (RobotPosition[i + 1][1] + RobotPosition[i - 1][1]) / 2;
                            }
                        }
                    }
                    else
                        counter_x = 0;
                }
            }
            for (int i = 2; i < RobotPosition.Count() - 3; i++)
            {
                if (Math.Sqrt(Math.Pow(RobotPosition[i][0] - RobotPosition[i + 1][0], 2) + Math.Pow(RobotPosition[i][1] - RobotPosition[i + 1][1], 2)) < 10)
                {
                    //ShareArea.RobotPositionD.RemoveAt(i);
                    //ShareArea.Robotabc.RemoveAt(i);
                    RobotPosition[i][0] = (RobotPosition[i + 1][0] + RobotPosition[i - 1][0]) / 2;
                    RobotPosition[i][1] = (RobotPosition[i + 1][1] + RobotPosition[i - 1][1]) / 2;
                }

            }
            List<double[]> _robotposition = new List<double[]>();
            List<double[]> _robotabc = new List<double[]>();
            _robotposition = Clone<double[]>(RobotPosition);
            _robotabc = Clone<double[]>(Robotabc);
            Robotabc.Clear();
            RobotPosition.Clear();
            int robc = 0;
            for (int i = 0; i < _robotposition.Count(); i++)
            {
                if (Math.Abs(_robotposition[i][1] - Miny) >= length * 0.1 && Math.Abs(_robotposition[i][1] - Miny) <= length * 0.9)
                {
                    if (Math.Sqrt(Math.Pow(_robotposition[i][0] - RobotPosition[robc - 1][0], 2) + Math.Pow(_robotposition[i][1] - RobotPosition[robc - 1][1], 2)) < 20)
                    {
                        continue;
                    }
                    else
                    {
                        RobotPosition.Add(_robotposition[i]);
                        Robotabc.Add(_robotabc[i]);
                        robc++;
                    }
                }
                else
                {
                    RobotPosition.Add(_robotposition[i]);
                    Robotabc.Add(_robotabc[i]);
                    robc++;
                }
                if (halfpath[0] == _robotposition[i][0] && halfpath[1] == _robotposition[i][1] && !halfFind)
                {
                    halfFind = true;
                    ShareArea.half = RobotPosition.Count();
                }
            }
            int p;
            if (add)
            {
                try
                {
                    p = RobotPosition.FindIndex(x => x[0] == halfValue) + 1;
                }
                catch
                {
                    p = RobotPosition.FindIndex(x => x[0] == halfValue2) + 1;
                }
            }
            else
                p = ShareArea.half ;

            string modName = "Bottom";
            string[] Title = { "MODULE " + modName, "PROC " + "broutine()", "ConfJ \\off;", "!ConfL \\off;" };
            System.IO.File.WriteAllLines(startpath + "\\" + modName + ".mod", Title);
            using (StreamWriter strW = new StreamWriter(startpath + "\\" + modName + ".mod", true))
            {
                for (int i = 0; i < RobotPosition.Count(); i++)
                {
                    orient.FillFromEulerAngles(Robotabc[i][2], Robotabc[i][1], Robotabc[i][0]);
                    Quaternion = new double[4] { orient.Q1, orient.Q2, orient.Q3, orient.Q4 };
                    string t;
                    t = "MoveL" + " " + "[[" + RobotPosition[i][0] + "," + RobotPosition[i][1] + "," + RobotPosition[i][2] + "]," + "[" + Quaternion[0] + "," + Quaternion[1] + "," + Quaternion[2] + "," + Quaternion[3] + "],[0,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,255]], v400, z50, tool1,\\WObj:=wobjtracktest1;";
                    strW.WriteLine(t);
                }
                string End = "EndPROC \nEndModule";
                strW.WriteLine(End);
            }
            using (StreamWriter strw = new StreamWriter(startpath + $@"\MovePath\{DateTime.Now.ToString("yyyy-MM-dd")}\MovePath{DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss")}.txt", false))  
            {
                for (int i = 0; i < RobotPosition.Count(); i++)
                {
                    if (i == 0 || i == RobotPosition.Count - 1 || i == RobotPosition.Count - 2)
                        Robotabc[i][1] += 0;
                    else
                        Robotabc[i][1] += ShareArea.angle;
                    strw.Write(RobotPosition[i][0] + " " + RobotPosition[i][1] + " " + RobotPosition[i][2] + " ");
                    strw.WriteLine(Robotabc[i][0] + " " + Robotabc[i][1] + " " + Robotabc[i][2]);
                }
            }
            if (message == "sample")
            {
                using (StreamWriter strw = new StreamWriter(startpath + @"\path\" + ShareArea.name + "path.txt", false))
                {
                    for (int i = 0; i < RobotPosition.Count(); i++)
                    {
                        if (i == 0 || i == RobotPosition.Count - 1 || i == RobotPosition.Count - 2)
                            Robotabc[i][1] += 0;
                        else
                            Robotabc[i][1] += ShareArea.angle;
                        strw.Write(RobotPosition[i][0] + " " + RobotPosition[i][1] + " " + RobotPosition[i][2] + " ");
                        strw.WriteLine(Robotabc[i][0] + " " + Robotabc[i][1] + " " + Robotabc[i][2]);
                    }
                }
            }
            else if (checkBox_PathCorrection.Checked) 
            {
                pathshiftv2(p, length, Miny, RobotPosition, Robotabc, dir,size);
            }
            ShareArea.XYZ_Queue.Enqueue(RobotPosition);
            ShareArea.ABC_Queue.Enqueue(Robotabc);

            sw2.Stop();
        }

        #region support       
        private void Interpolation(Mat imageInput, List<List<double>> height, out Mat Interpolated)
        {
            Mat thresholdimg = new Mat();
            thresholdimg = imageInput.Clone();
            Cv2.Threshold(imageInput, thresholdimg, 0, 255, ThresholdTypes.Binary);
            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(5, 5), new OpenCvSharp.Point(-1, -1));
            Cv2.MorphologyEx(thresholdimg, thresholdimg, MorphTypes.Close, kernel, new OpenCvSharp.Point(-1, -1));
            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchies;
            Cv2.FindContours(thresholdimg, out contours, out hierarchies, RetrievalModes.Tree, ContourApproximationModes.ApproxNone);
            Mat contimg = Mat.Zeros(imageInput.Size(), imageInput.Type());
            Rect BoundingRectangle = new Rect();

            for (int i = 0; i < contours.Length; i++)
            {
                if (contours[i].Length > 800)
                {
                    Cv2.DrawContours(contimg, contours, i, new Scalar(255), 1);
                    BoundingRectangle = Cv2.BoundingRect(contours[i]);
                }
            }

            contimg.SaveImage(startpath + @"\Save\2.5D\contour1111111.bmp");
            OpenCvSharp.Point point = new OpenCvSharp.Point();
            double Gray_val = 0, Find_R = 0, Find_L = 0, left = 0, right = 0, Find_T = 0, Find_B = 0;
            double incontour = 0;
            Interpolated = imageInput.Clone();

            if (contours != null)
            {
                for (int i = BoundingRectangle.Top; i < BoundingRectangle.Bottom; i++)
                {
                    for (int j = BoundingRectangle.Left; j < BoundingRectangle.Right; j++)
                    {
                        Find_R = 0;
                        Find_L = 0;
                        Find_B = 0;
                        Find_T = 0;
                        point.X = j;
                        point.Y = i;
                        incontour = Cv2.PointPolygonTest(contours[0], point, false);
                        if (incontour > 0)
                        {
                            if (height[i][j] == 0)
                            {
                                for (int x = j - 1; x > BoundingRectangle.Left; x--)
                                {
                                    if (height[i][x] > 5)
                                    {
                                        Find_L = height[i][x];
                                        left = x;
                                        break;
                                    }
                                }
                                for (int x = j + 1; x < BoundingRectangle.Right; x++)
                                {
                                    if (height[i][x] > 5)
                                    {
                                        Find_R = height[i][x];
                                        right = x;
                                        break;
                                    }
                                }
                                if (Math.Abs(Find_R - Find_L) > 5 && Find_R != 0 && Find_L != 0)
                                {
                                    Gray_val = interpoVal(Find_L, Find_R, left, right, j);
                                    height[i][j] = Gray_val;
                                    Interpolated.Set(i, j, (int)Gray_val);
                                }
                                else
                                {
                                    for (int y = i - 1; y > BoundingRectangle.Top; y--)
                                    {
                                        if (height[y][j] > 5)
                                        {
                                            Find_T = height[y][j];
                                            left = y;
                                            break;
                                        }
                                    }
                                    for (int y = i + 1; y < BoundingRectangle.Bottom; y++)
                                    {
                                        if (height[y][j] > 5)
                                        {
                                            Find_B = height[y][j];
                                            right = y;
                                            break;
                                        }
                                    }
                                    if (Find_T != 0 && Find_B != 0)
                                    {
                                        Gray_val = interpoVal(Find_T, Find_B, left, right, i);
                                        height[i][j] = Gray_val;
                                        Interpolated.Set(i, j, (int)Gray_val);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private double interpoVal(double L_gray, double R_gray, double dis_L, double dis_R, double present)
        {
            double grayVal = 0, diff = 0;
            diff = dis_R - dis_L;
            grayVal = (((dis_R - present) * L_gray + (present - dis_L) * R_gray) / diff);
            return grayVal;
        }
        private void ShrinkEdge(double[] Euler, List<double> Allinf, int shrinkdis, out double Nx, out double Ny)
        {
            double sin = Math.Sin(Euler[2]);
            double cos = Math.Cos(Euler[2]);
            double ErrorX = (Allinf[0] * cos - Allinf[1] * sin) - Allinf[0];
            double ErrorY = (Allinf[0] * sin + Allinf[1] * cos) - Allinf[1];
            Nx = ((Allinf[0] + shrinkdis) * cos - (Allinf[1]) * sin) - ErrorX;
            Ny = ((Allinf[0] + shrinkdis) * sin + (Allinf[1]) * cos) - ErrorY;
        }
        private void pathshiftv2(int p, double length, double Miny, List<double[]> RobotPosition, List<double[]> Robotabc, string dir,double size)
        {
            var data = ShareArea.shiftdatas.Find(n => n.name == $"{size}{dir}.txt");
            Console.WriteLine(size + dir);
            if (data.name == null)
            {
                Console.WriteLine("查無此偏移路徑");
                return;
            }
            int PointNum = Convert.ToInt16(data.shift[0][0]);
            int half = Convert.ToInt16(data.shift[0][1]);
            int lastindex = -1;
            double[] shift;
            double[] LocationInfA = new double[3];
            List<double[]> LocationInfL = new List<double[]>();
            double percent = (double)(p + 1) / (half + 1);
            int CurrentIndex, NextCount = 999;
            string[] Title = { "" };
            System.IO.File.WriteAllLines(startpath + @"\ini\VerifyPath.ini", Title);
            for (int i = 0; i < data.shift.Count(); i++)
            {
                if (data.shift[i][2] > data.shift[i][1])
                {
                    NextCount = i;
                    break;
                }

                shift = new double[6];
                CurrentIndex = (int)Math.Round(data.shift[i][2] * percent) - 1;
                LocationInfL.Clear();

                shift[0] = data.shift[i][3];
                shift[1] = data.shift[i][4];
                shift[2] = data.shift[i][5];
                shift[3] = data.shift[i][6];
                shift[4] = data.shift[i][7];
                shift[5] = data.shift[i][8];
                if (-RobotPosition[CurrentIndex][1] + Miny > length * 0.2 && -RobotPosition[CurrentIndex][1] + Miny < length * 0.8)
                {
                    for (int j = CurrentIndex - 3 < 0 ? 0 : CurrentIndex - 3; j <= CurrentIndex + 3; j++)
                    {
                        if (-RobotPosition[j][1] + Miny > length * 0.2 && -RobotPosition[j][1] + Miny < length * 0.8)
                        {
                            LocationInfA = new double[3];
                            LocationInfA[0] = j;//index
                            LocationInfA[1] = Math.Abs(shift[1] - RobotPosition[j][1]);//Y distance
                            LocationInfA[2] = Math.Sqrt(Math.Pow(shift[0] - RobotPosition[j][0], 2) + Math.Pow(shift[1] - RobotPosition[j][1], 2));//two point distance
                            LocationInfL.Add(LocationInfA);
                        }
                    }
                    var a = LocationInfL.Select((c) => new { c }).OrderBy(n => n.c[1]).Take(2).ToArray();
                    var b = LocationInfL.Select((c) => new { c }).OrderBy(n => n.c[2]).Take(2).ToArray();
                    int index;
                    index = (int)a[0].c[0];
                    if (a[0].c[0] != b[0].c[0])
                        index = a[0].c[1] + a[0].c[2] < b[0].c[1] + b[0].c[2] ? (int)a[0].c[0] : (int)b[0].c[0];
                    if (lastindex == index)
                        continue;
                    RobotPosition[index][0] = RobotPosition[index][0] + shift[2];
                    RobotPosition[index][1] = RobotPosition[index][1] + shift[3];
                    RobotPosition[index][2] = RobotPosition[index][2] + shift[4];
                    Robotabc[index][1] = Robotabc[index][1] + shift[5];
                    ini.IniWriteValue("Point" + index, "VerifyX", shift[2].ToString(), "VerifyPath.ini");
                    ini.IniWriteValue("Point" + index, "VerifyY", shift[3].ToString(), "VerifyPath.ini");
                    ini.IniWriteValue("Point" + index, "VerifyZ", shift[4].ToString(), "VerifyPath.ini");
                    ini.IniWriteValue("Point" + index, "VerifyB", shift[5].ToString(), "VerifyPath.ini");
                    lastindex = index;
                    continue;
                }
                else
                {
                    if (lastindex == CurrentIndex)
                        continue;
                    RobotPosition[CurrentIndex][0] = RobotPosition[CurrentIndex][0] + shift[2];
                    RobotPosition[CurrentIndex][1] = RobotPosition[CurrentIndex][1] + shift[3];
                    RobotPosition[CurrentIndex][2] = RobotPosition[CurrentIndex][2] + shift[4];
                    Robotabc[CurrentIndex][1] = Robotabc[CurrentIndex][1] + shift[5];
                    ini.IniWriteValue("Point" + CurrentIndex, "VerifyX", shift[2].ToString(), "VerifyPath.ini");
                    ini.IniWriteValue("Point" + CurrentIndex, "VerifyY", shift[3].ToString(), "VerifyPath.ini");
                    ini.IniWriteValue("Point" + CurrentIndex, "VerifyZ", shift[4].ToString(), "VerifyPath.ini");
                    ini.IniWriteValue("Point" + CurrentIndex, "VerifyB", shift[5].ToString(), "VerifyPath.ini");
                    lastindex = CurrentIndex;
                }

            }
            percent = (double)(RobotPosition.Count() - p - 1) / (PointNum - half - 1);
            lastindex = -1;
            for (int i = NextCount; i < data.shift.Count(); i++)
            {
                shift = new double[6];
                CurrentIndex = (int)Math.Round((data.shift[i][2] - half - 1) * percent) + p;
                int jend = CurrentIndex + 3 >= RobotPosition.Count() ? RobotPosition.Count() - 1 : CurrentIndex + 3;
                LocationInfL.Clear();

                shift[0] = data.shift[i][3];
                shift[1] = data.shift[i][4];
                shift[2] = data.shift[i][5];
                shift[3] = data.shift[i][6];
                shift[4] = data.shift[i][7];
                shift[5] = data.shift[i][8];
                if (-RobotPosition[CurrentIndex][1] + Miny > length * 0.2 && -RobotPosition[CurrentIndex][1] + Miny < length * 0.8)
                {
                    for (int j = CurrentIndex - 3 < half + 1 ? CurrentIndex : CurrentIndex - 3; j <= jend; j++)
                    {
                        if (-RobotPosition[j][1] + Miny > length * 0.2 && -RobotPosition[j][1] + Miny < length * 0.8)
                        {
                            LocationInfA = new double[3];
                            LocationInfA[0] = j;//index
                            LocationInfA[1] = Math.Abs(shift[1] - RobotPosition[j][1]);//Y distance
                            LocationInfA[2] = Math.Sqrt(Math.Pow(shift[0] - RobotPosition[j][0], 2) + Math.Pow(shift[1] - RobotPosition[j][1], 2));//two point distance
                            LocationInfL.Add(LocationInfA);
                        }
                    }
                    var a = LocationInfL.Select((c) => new { c }).OrderBy(n => n.c[1]).Take(2).ToArray();
                    var b = LocationInfL.Select((c) => new { c }).OrderBy(n => n.c[2]).Take(2).ToArray();
                    int index;
                    index = (int)a[0].c[0];
                    if (a[0].c[0] != b[0].c[0])
                        index = a[0].c[1] + a[0].c[2] < b[0].c[1] + b[0].c[2] ? (int)a[0].c[0] : (int)b[0].c[0];
                    if (lastindex == index)
                        continue;
                    RobotPosition[index][0] = RobotPosition[index][0] + shift[2];
                    RobotPosition[index][1] = RobotPosition[index][1] + shift[3];
                    RobotPosition[index][2] = RobotPosition[index][2] + shift[4];
                    Robotabc[index][1] = Robotabc[index][1] + shift[5];
                    ini.IniWriteValue("Point" + index, "VerifyX", shift[2].ToString(), "VerifyPath.ini");
                    ini.IniWriteValue("Point" + index, "VerifyY", shift[3].ToString(), "VerifyPath.ini");
                    ini.IniWriteValue("Point" + index, "VerifyZ", shift[4].ToString(), "VerifyPath.ini");
                    ini.IniWriteValue("Point" + index, "VerifyB", shift[5].ToString(), "VerifyPath.ini");
                    lastindex = index;
                    continue;
                }
                else
                {
                    if (lastindex == CurrentIndex)
                        continue;
                    RobotPosition[CurrentIndex][0] = RobotPosition[CurrentIndex][0] + shift[2];
                    RobotPosition[CurrentIndex][1] = RobotPosition[CurrentIndex][1] + shift[3];
                    RobotPosition[CurrentIndex][2] = RobotPosition[CurrentIndex][2] + shift[4];
                    Robotabc[CurrentIndex][1] = Robotabc[CurrentIndex][1] + shift[5];
                    ini.IniWriteValue("Point" + CurrentIndex, "VerifyX", shift[2].ToString(), "VerifyPath.ini");
                    ini.IniWriteValue("Point" + CurrentIndex, "VerifyY", shift[3].ToString(), "VerifyPath.ini");
                    ini.IniWriteValue("Point" + CurrentIndex, "VerifyZ", shift[4].ToString(), "VerifyPath.ini");
                    ini.IniWriteValue("Point" + CurrentIndex, "VerifyB", shift[5].ToString(), "VerifyPath.ini");
                    lastindex = CurrentIndex;
                }

            }

            //for (int i = 0; i <= half; i++)
            //{
            //    shift = new double[6];
            //    CurrentIndex = (int)Math.Round(i * percent);
            //    LocationInfL.Clear();
            //    if (ShareArea.shiftList[i][1] != 0)
            //    {
            //        shift[0] = ShareArea.shiftList[i][0];
            //        shift[1] = ShareArea.shiftList[i][1];
            //        shift[2] = ShareArea.shiftList[i][2];
            //        shift[3] = ShareArea.shiftList[i][3];
            //        shift[4] = ShareArea.shiftList[i][4];
            //        shift[5] = ShareArea.shiftList[i][5];
            //        if (-RobotPosition[CurrentIndex][1] + Miny > length * 0.2 && -RobotPosition[CurrentIndex][1] + Miny < length * 0.8)
            //        {
            //            for (int j = CurrentIndex - 3 < 0 ? 0 : CurrentIndex - 3; j <= CurrentIndex + 3; j++)
            //            {
            //                if (-RobotPosition[j][1] + Miny > length * 0.2 && -RobotPosition[j][1] + Miny < length * 0.8)
            //                {
            //                    LocationInfA = new double[3];
            //                    LocationInfA[0] = j;//index
            //                    LocationInfA[1] = Math.Abs(shift[1] - RobotPosition[j][1]);//Y distance
            //                    LocationInfA[2] = Math.Sqrt(Math.Pow(shift[0] - RobotPosition[j][0], 2) + Math.Pow(shift[1] - RobotPosition[j][1], 2));//two point distance
            //                    LocationInfL.Add(LocationInfA);
            //                }
            //            }
            //            var a = LocationInfL.Select((c) => new { c }).OrderBy(n => n.c[1]).Take(2).ToArray();
            //            var b = LocationInfL.Select((c) => new { c }).OrderBy(n => n.c[2]).Take(2).ToArray();
            //            int index;
            //            index = (int)a[0].c[0];
            //            if (a[0].c[0] != b[0].c[0])
            //                index = a[0].c[1] + a[0].c[2] < b[0].c[1] + b[0].c[2] ? (int)a[0].c[0] : (int)b[0].c[0];
            //            if (lastindex == index)
            //                continue;
            //            RobotPosition[index][0] = RobotPosition[index][0] + shift[2];
            //            RobotPosition[index][1] = RobotPosition[index][1] + shift[3];
            //            RobotPosition[index][2] = RobotPosition[index][2] + shift[4];
            //            Robotabc[index][1] = Robotabc[index][1] + shift[5];
            //            ini.IniWriteValue("Point" + index, "VerifyX", shift[2].ToString(), "VerifyPath.ini");
            //            ini.IniWriteValue("Point" + index, "VerifyY", shift[3].ToString(), "VerifyPath.ini");
            //            ini.IniWriteValue("Point" + index, "VerifyZ", shift[4].ToString(), "VerifyPath.ini");
            //            ini.IniWriteValue("Point" + index, "VerifyB", shift[5].ToString(), "VerifyPath.ini");
            //            lastindex = index;
            //            continue;
            //        }
            //        else
            //        {
            //            if (lastindex == CurrentIndex)
            //                continue;
            //            RobotPosition[CurrentIndex][0] = RobotPosition[CurrentIndex][0] + shift[2];
            //            RobotPosition[CurrentIndex][1] = RobotPosition[CurrentIndex][1] + shift[3];
            //            RobotPosition[CurrentIndex][2] = RobotPosition[CurrentIndex][2] + shift[4];
            //            Robotabc[CurrentIndex][1] = Robotabc[CurrentIndex][1] + shift[5];
            //            ini.IniWriteValue("Point" + CurrentIndex, "VerifyX", shift[2].ToString(), "VerifyPath.ini");
            //            ini.IniWriteValue("Point" + CurrentIndex, "VerifyY", shift[3].ToString(), "VerifyPath.ini");
            //            ini.IniWriteValue("Point" + CurrentIndex, "VerifyZ", shift[4].ToString(), "VerifyPath.ini");
            //            ini.IniWriteValue("Point" + CurrentIndex, "VerifyB", shift[5].ToString(), "VerifyPath.ini");
            //            lastindex = CurrentIndex;
            //        }
            //    }                
            //}
            //percent = (double)(RobotPosition.Count() - p - 1) / (PointNum - half - 1);
            //lastindex = -1;
            //for (int i = half + 1; i < PointNum; i++)
            //{
            //    shift = new double[6];
            //    CurrentIndex = (int)Math.Round((i - half - 1) * percent) + p + 1;
            //    int jend = CurrentIndex + 3 >= RobotPosition.Count() ? RobotPosition.Count() - 1 : CurrentIndex + 3;
            //    LocationInfL.Clear();
            //    if (ShareArea.shiftList[i][1] != 0)
            //    {
            //        shift[0] = ShareArea.shiftList[i][0];
            //        shift[1] = ShareArea.shiftList[i][1];
            //        shift[2] = ShareArea.shiftList[i][2];
            //        shift[3] = ShareArea.shiftList[i][3];
            //        shift[4] = ShareArea.shiftList[i][4];
            //        shift[5] = ShareArea.shiftList[i][5];
            //        if (-RobotPosition[CurrentIndex][1] + Miny > length * 0.2 && -RobotPosition[CurrentIndex][1] + Miny < length * 0.8)
            //        {
            //            for (int j = CurrentIndex - 3 < half + 1 ? CurrentIndex : CurrentIndex - 3; j <= jend; j++)
            //            {
            //                if (-RobotPosition[j][1] + Miny > length * 0.2 && -RobotPosition[j][1] + Miny < length * 0.8)
            //                {
            //                    LocationInfA = new double[3];
            //                    LocationInfA[0] = j;//index
            //                    LocationInfA[1] = Math.Abs(shift[1] - RobotPosition[j][1]);//Y distance
            //                    LocationInfA[2] = Math.Sqrt(Math.Pow(shift[0] - RobotPosition[j][0], 2) + Math.Pow(shift[1] - RobotPosition[j][1], 2));//two point distance
            //                    LocationInfL.Add(LocationInfA);
            //                }
            //            }
            //            var a = LocationInfL.Select((c) => new { c }).OrderBy(n => n.c[1]).Take(2).ToArray();
            //            var b = LocationInfL.Select((c) => new { c }).OrderBy(n => n.c[2]).Take(2).ToArray();
            //            int index;
            //            index = (int)a[0].c[0];
            //            if (a[0].c[0] != b[0].c[0])
            //                index = a[0].c[1] + a[0].c[2] < b[0].c[1] + b[0].c[2] ? (int)a[0].c[0] : (int)b[0].c[0];
            //            if (lastindex == index)
            //                continue;
            //            RobotPosition[index][0] = RobotPosition[index][0] + shift[2];
            //            RobotPosition[index][1] = RobotPosition[index][1] + shift[3];
            //            RobotPosition[index][2] = RobotPosition[index][2] + shift[4];
            //            Robotabc[index][1] = Robotabc[index][1] + shift[5];
            //            ini.IniWriteValue("Point" + index, "VerifyX", shift[2].ToString(), "VerifyPath.ini");
            //            ini.IniWriteValue("Point" + index, "VerifyY", shift[3].ToString(), "VerifyPath.ini");
            //            ini.IniWriteValue("Point" + index, "VerifyZ", shift[4].ToString(), "VerifyPath.ini");
            //            ini.IniWriteValue("Point" + index, "VerifyB", shift[5].ToString(), "VerifyPath.ini");
            //            lastindex = index;
            //            continue;
            //        }
            //        else
            //        {
            //            if (lastindex == CurrentIndex)
            //                continue;
            //            RobotPosition[CurrentIndex][0] = RobotPosition[CurrentIndex][0] + shift[2];
            //            RobotPosition[CurrentIndex][1] = RobotPosition[CurrentIndex][1] + shift[3];
            //            RobotPosition[CurrentIndex][2] = RobotPosition[CurrentIndex][2] + shift[4];
            //            Robotabc[CurrentIndex][1] = Robotabc[CurrentIndex][1] + shift[5];
            //            ini.IniWriteValue("Point" + CurrentIndex, "VerifyX", shift[2].ToString(), "VerifyPath.ini");
            //            ini.IniWriteValue("Point" + CurrentIndex, "VerifyY", shift[3].ToString(), "VerifyPath.ini");
            //            ini.IniWriteValue("Point" + CurrentIndex, "VerifyZ", shift[4].ToString(), "VerifyPath.ini");
            //            ini.IniWriteValue("Point" + CurrentIndex, "VerifyB", shift[5].ToString(), "VerifyPath.ini");
            //            lastindex = CurrentIndex;
            //        }
            //    }
            //}
        }
        private void UploadtoFTP()
        {
            try
            {
                FtpWebRequest request;
                byte[] fileContents;
                bool exist = CheckFTPFileExist(comboBox_model.Text, "ftp://192.168.10.100/GoldSample/Bottom/");
                if (!exist)
                {
                    request = (FtpWebRequest)WebRequest.Create("ftp://192.168.10.100/GoldSample/Bottom/" + comboBox_model.Text + "/" + comboBox_size.Text);
                    request.Credentials = new NetworkCredential("beckhoff", "12345");
                    request.KeepAlive = false;
                    request.Method = WebRequestMethods.Ftp.MakeDirectory;
                    using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                    {
                        Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
                    }
                }
                else
                {
                    exist = CheckFTPFileExist(comboBox_size.Text, "ftp://192.168.10.100/GoldSample/Bottom/" + comboBox_model.Text + "/");
                    if (!exist)
                    {
                        request = (FtpWebRequest)WebRequest.Create("ftp://192.168.10.100/GoldSample/Bottom/" + comboBox_model.Text + "/" + comboBox_size.Text);
                        request.Credentials = new NetworkCredential("beckhoff", "12345");
                        request.KeepAlive = false;
                        request.Method = WebRequestMethods.Ftp.MakeDirectory;
                        using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                        {
                            Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
                        }
                    }
                }

                request = (FtpWebRequest)WebRequest.Create("ftp://192.168.10.100/GoldSample/Bottom/" + comboBox_model.Text + "/" + comboBox_size.Text + "/" + comboBox_direction.Text + "LowPointHeight.pcd");
                request.Credentials = new NetworkCredential("beckhoff", "12345");
                request.KeepAlive = false;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                using (StreamReader source = new StreamReader(startpath + "\\PCD\\LowPointHeight.pcd"))
                {
                    fileContents = Encoding.UTF8.GetBytes(source.ReadToEnd());
                }
                request.ContentLength = fileContents.Length;
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(fileContents, 0, fileContents.Length);
                }
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
                }
                GC.Collect();
            }
            catch(Exception ex)
            {
                Console.WriteLine("創建失敗:" + ex.Message);
            }
            GC.Collect();
        }      
        private static List<T> Clone<T>(object List)
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, List);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as List<T>;
            }
        }
        #endregion
    }
}
