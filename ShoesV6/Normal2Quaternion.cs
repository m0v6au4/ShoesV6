using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesV6
{
    class Normal2Quaternion
    {
        public double[] calculateQuaternion(double[] startaxis, double[] targetaxis)
        {
            double sita = calculateangle(startaxis, targetaxis);
            double[] cross = calculatecross(startaxis, targetaxis);
            double[] Quaternion = new double[4];
            Quaternion[0] = Math.Cos(sita / 2);
            Quaternion[1] = cross[0] * Math.Sin(sita / 2);
            Quaternion[2] = cross[1] * Math.Sin(sita / 2);
            Quaternion[3] = cross[2] * Math.Sin(sita / 2);
            return Quaternion;
        }
        private double calculateangle(double[] s, double[] t)
        {
            double sita;
            double dot;
            double norm;
            dot = s[0] * t[0] + s[1] * t[1] + s[2] * t[2];
            norm = Math.Sqrt(s[0] * s[0] + s[1] * s[1] + s[2] * s[2]) * Math.Sqrt(t[0] * t[0] + t[1] * t[1] + t[2] * t[2]);
            sita = Math.Acos(dot / norm);
            return sita;
        }
        public double[] calculatecross(double[] s, double[] t)
        {
            double[] cross = new double[3];
            cross[0] = s[1] * t[2] - s[2] * t[1];
            cross[1] = s[2] * t[0] - s[0] * t[2];
            cross[2] = s[0] * t[1] - s[1] * t[0];
            double len = Math.Sqrt(Math.Pow(cross[0], 2) + Math.Pow(cross[1], 2) + Math.Pow(cross[2], 2));
            cross[0] /= len;
            cross[1] /= len;
            cross[2] /= len;
            return cross;
        }
        public double[] QuatMultiply(double[] QuatA, double[] QuatB)//將四元數相乘，代表將四元數旋轉 EX:A四元數乘以[0 0 0 1]，代表此四元數沿著Z軸旋轉，[0 0 0 1]為沿著Z軸旋轉180度的四元數
        {
            double[] Q = new double[4];
            Q[1] = QuatA[0] * QuatB[1] - QuatA[3] * QuatB[2] + QuatA[2] * QuatB[3] + QuatA[1] * QuatB[0];
            Q[2] = QuatA[3] * QuatB[1] + QuatA[0] * QuatB[2] - QuatA[1] * QuatB[3] + QuatA[2] * QuatB[0];
            Q[3] = -QuatA[2] * QuatB[1] + QuatA[1] * QuatB[2] + QuatA[0] * QuatB[3] + QuatA[3] * QuatB[0];
            Q[0] = -QuatA[1] * QuatB[1] - QuatA[2] * QuatB[2] - QuatA[3] * QuatB[3] + QuatA[0] * QuatB[0];

            return Q;
        }
    }
}
