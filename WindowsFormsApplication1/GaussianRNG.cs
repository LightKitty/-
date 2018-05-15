using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    // 
    // Gaussian Random Number Generator class
    // ref. ``Numerical Recipes in C++ 2/e'', p.293 ~ p.294
    //
    class GaussianRNG
    {
        int iset;
        double gset;
        Random r1, r2;

        public GaussianRNG()
        {
            r1 = new Random(unchecked((int)DateTime.Now.Ticks));
            r2 = new Random(~unchecked((int)DateTime.Now.Ticks));
            iset = 0;
        }

        public double Next()
        {
            double fac, rsq, v1, v2;
            if (iset == 0)
            {
                do
                {
                    v1 = 2.0 * r1.NextDouble() - 1.0;
                    v2 = 2.0 * r2.NextDouble() - 1.0;
                    rsq = v1 * v1 + v2 * v2;
                } while (rsq >= 1.0 || rsq == 0.0);

                fac = Math.Sqrt(-2.0 * Math.Log(rsq) / rsq);
                gset = v1 * fac;
                iset = 1;
                return v2 * fac;
            }
            else
            {
                iset = 0;
                return gset;
            }
        }
    }
}
