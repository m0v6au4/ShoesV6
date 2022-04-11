using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ShoesV6
{
    public partial class Form1 : Form
    {
        int bbabc;
        List<int> vs = new List<int>();
        private void scantest()
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
                //if ((string)message == "sample")
                //    sampleBool = false;
                MinpointHeightL.Clear();
                MinpointHeightR.Clear();
                newHeight.Clear();
                linecompose.Clear();
                widthL.Clear();
                widthR.Clear();
                vs.Clear();
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
                Thread_bw = new Thread(bw123);
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
                //fixedgeAndFindheight(bottom_spliced, linecompose, out newHeight, out MinpointHeightL, out MinpointHeightR, out widthL, out widthR, out dir);
                for (int y = 0; y < newHeight.Count(); y++)
                {
                    for (int x = 0; x < newHeight[0].Count(); x++)
                    {
                        image_linecompose.Set(y, x, (int)newHeight[y][x]);
                    }
                }
                vs = vs.OrderBy(c => c).ToList();
                double[] save = new double[3];
                for (int kk = linecompose[0].Count() - 1; kk >= 0; kk--)
                {
                    if (linecompose[vs[0]][kk] != 0)
                    {
                        save[0] = kk;
                        save[1] = vs[0];
                        save[2] = linecompose[vs[0]][kk];
                    }
                }
                movepath123(save);
                image_linecompose.SaveImage(startpath + @"\Save\2.5D\2.bmp");
                Console.WriteLine("取像張數:" + count_photo);
                sw2.Restart();
                makepcd(image_linecompose, linecompose);
                //edgeHeight_pcd(MinpointHeightL, MinpointHeightR, widthL, widthR, (string)message);
                sw2.Stop();
                sw2.Restart();
                //Normal();
                sw2.Stop();
                //MovePath(dir, (string)message, size);
                ShareArea.isshifpath = false;
                sw.Stop();
                Console.WriteLine("取像與處理時間" + sw.ElapsedMilliseconds + "ms");
                sw.Reset();
            }
        }
        private void bw123()
        {
            while (!ShareArea.input[1] || ShareArea.bitmaps.Count() > 0)
            {
                if (ShareArea.bitmaps.Count() > 0)
                {
                    Bitmap c;
                    ShareArea.bitmaps.TryDequeue(out c);
                    imagesource = BitmapConverter.ToMat(c);
                    ProcessImage123();
                }
            }
        }
        private void ProcessImage123()
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
                bbabc = 0;
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
                    if (tmpHeight <= 10 || tmpHeight > 75)
                    {
                        tmpHeight = 0;
                    }
                    if (tmp25D[Centroid[j].X] != 0)
                    {
                        tmp25D[Centroid[j].X] = tmpHeight > tmp25D[Centroid[j].X] ? tmpHeight : tmp25D[Centroid[j].X];
                        bbabc++;
                    }
                    else
                        tmp25D[Centroid[j].X] = tmpHeight;
                }
                if (bbabc > 20)
                    vs.Add(count_photo);
            }
            else
            {
                for (int j = 0; j < tmp25D.Count(); j++)
                    tmp25D[j] = 0;
            }
            linecompose.Add(tmp25D.ToList<double>());
        }
        private void movepath123(double[] save)
        {
            using (StreamWriter strw = new StreamWriter(startpath + $@"\MovePath.txt", false))
            {

                double y = -(save[1] * ShareArea.par_y);
                double z = save[2] + 10;  //實際高度值
                double x = save[0] * (Math.Pow(z, 2) * ShareArea.abc[1, 0] + z * ShareArea.abc[1, 1] + ShareArea.abc[1, 2]);
                string pcldata = x.ToString("0.000") + " " + y.ToString("0.000") + " " + z.ToString("0.000");

                strw.Write(x.ToString() + " " + y.ToString() + " " + z.ToString() + " " + "-180" + " " + "-90" + " " + "0");
            }
        }
    }
}
