using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesV6
{
    class Quaternion2Euler
    {
        public string[] calculateEuler(string[] Q)
        {
            string[] Euler = new string[3];
            double W, X, Y, Z;
            double Yaw, Pitch, Roll;
            W = Convert.ToDouble(Q[0]);
            X = Convert.ToDouble(Q[1]);
            Y = Convert.ToDouble(Q[2]);
            Z = Convert.ToDouble(Q[3]);
            Yaw = Math.Atan2(2 * (W * X + Y * Z), 1 - 2 * (X * X + Y * Y));
            Pitch = Math.Asin(2 * (W * Y - Z * X));
            Roll = Math.Atan2(2 * (W * Z + X * Y), 1 - 2 * (Y * Y + Z * Z));
            Yaw = Yaw * 180 / Math.PI;
            Pitch = Pitch * 180 / Math.PI;
            Roll = Roll * 180 / Math.PI;
            Euler[0] = Yaw.ToString();
            Euler[1] = Pitch.ToString();
            Euler[2] = Roll.ToString();
            return Euler;
        }
        public double[] calculateEulerD(double[] Q)
        {
            double[] Euler = new double[3];
            double W, X, Y, Z;
            double Yaw, Pitch, Roll;
            W = Convert.ToDouble(Q[0]);
            X = Convert.ToDouble(Q[1]);
            Y = Convert.ToDouble(Q[2]);
            Z = Convert.ToDouble(Q[3]);
            Yaw = Math.Atan2(2 * (W * X + Y * Z), 1 - 2 * (X * X + Y * Y));
            Pitch = Math.Asin(2 * (W * Y - Z * X));
            Roll = Math.Atan2(2 * (W * Z + X * Y), 1 - 2 * (Y * Y + Z * Z));

            Euler[0] = Yaw;
            Euler[1] = Pitch;
            Euler[2] = Roll;
            return Euler;
        }
    }
}
