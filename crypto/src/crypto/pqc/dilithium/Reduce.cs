using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crystals_csharp.crypto.pqc.dilithium
{
    internal class Reduce
    {
        public static int MontgomeryReduce(long a)
        {
            int t;
            t = (int)(a * DilithiumEngine.QInv);
            t = (int)(a - t * (long)DilithiumEngine.Q >> 32);
            //Console.Write("{0}, ", t);
            return t;
        }

        public static int Reduce32(int a)
        {
            int t;
            t = a + (1 << 22) >> 23;
            t = a - t * DilithiumEngine.Q;
            return t;
        }

        public static int ConditionalAddQ(int a)
        {
            a += a >> 31 & DilithiumEngine.Q;
            return a;
        }


    }
}
