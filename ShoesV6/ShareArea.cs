using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.Concurrent;
using System.IO.Ports;
using TwinCAT.Ads;

namespace ShoesV6
{
    public static class ShareArea
    {
        public static SerialPort SerialPort = new SerialPort();
        public static CamClass cameraSetting = new CamClass();
        public static int[] inputState = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        public static bool[] input = new bool[2] { false, false };
        public static double[,] abc = new double[2, 3];
        public static KUKA_EKI KUKA_EKI = new KUKA_EKI();
        public static bool IOstate = false;
        //public static object _IOLock = new object();
        public static bool[] Connectstate = new bool[4];//0 Eki 1 Cam 2 Ads 3 IO
        public static bool isscan, isshifpath;
        public static ConcurrentQueue<Bitmap> bitmaps = new ConcurrentQueue<Bitmap>();
        public static ConcurrentQueue<List<double[]>> XYZ_Queue = new ConcurrentQueue<List<double[]>>();
        public static ConcurrentQueue<List<double[]>> ABC_Queue = new ConcurrentQueue<List<double[]>>();
        public static int Threshold, Dilate, Erode, shiftY, shiftX, shrink, shiftHeight, angle, Collinear, half, PointNum;
        public static string name;
        public static double par_y, Exposure, size;
        public static List<double[]> shiftList = new List<double[]>();
        public static AdsClient AdsClient = new AdsClient();
        public static txtLog txtLog = new txtLog();
        public static List<shiftdata> shiftdatas = new List<shiftdata>();
        public struct shiftdata
        {
            public string name;
            public List<double[]> shift;
        }

    }
}
