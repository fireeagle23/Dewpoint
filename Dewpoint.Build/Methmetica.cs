using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class Methmetica
    {
                public double tdb = 0, t = 0,pv=0;
        public double Tdb
        {
            get
            {
                return tdb;
            }
            set
            {
                tdb = value;
            }
        }
        public double T
        {
            get
            {
                return t;
            }
            set
            {
                t = value;
            }
        }
        public double Pv
        {
            get
            {
                return pv;
            }
            set
            {
                pv = value;
            }
        }
        public double PCount()
        {
            
            if (tdb >= -100 && tdb < 0)
                return Math.Exp(pdbcounta(T));
            else
                return Math.Pow(10, pdbcountb(tdb, T)) * Math.Pow(10, 5);
        }

        static double pdbcounta(double y)
        {
            return 6.3925274 - (5674.5359 / y) + 4.1635019 * Math.Log(y) - 0.9677843 * Math.Pow(10, -2) * y
                + 0.62215701 * Math.Pow(10, -6) * Math.Pow(y, 2) + 0.20747825 * Math.Pow(10, -8) * Math.Pow(y, 3)
                - 0.9484024 * Math.Pow(10, -12) * Math.Pow(y, 4);
        }

        static double pdbcountb(double tdb, double T)
        {
            double[] a = new double[] { 5.40221, 5.20389, 5.07680 };
            double[] b = new double[] { 1838.675, 1733.926, 1659.793 };
            double[] c = new double[] { -31.737, -39.485, -45.854 };
            int[] index = new int[] { 0, 30, 60, 90 };
            double ans = 0;
            int j = 0;
            while (j <= 2)
            {
                if (tdb <= index[j + 1])
                {
                    ans = (a[j] - b[j] / (T + c[j]));
                    break;
                }
                else
                    j++;
            }
            return ans;
        }

        public double Tfcount()
        {
            double Tftmp= 212.57196 + 7.3532296 * Math.Log(pv) + 0.25212096 * Math.Pow(Math.Log(pv), 2) + 0.87627568 * Math.Pow(10, -2) * Math.Pow(Math.Log(pv), 3)
                + 0.33933834 * Math.Pow(10, -3) * Math.Pow(Math.Log(pv), 4) + 0.12651455 * Math.Pow(10, -4) * Math.Pow(Math.Log(pv), 5);
            return Tftmp - 273.15;
        }

    }
}
