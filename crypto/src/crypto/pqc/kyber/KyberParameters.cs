
using System;
using crystals_csharp.crypto.pqc.interfaces;
using Org.BouncyCastle.Crypto;

namespace crystals_csharp.crypto.pqc.kyber
{
    public class KyberParameters
        : ICipherParameters
    {

        public static KyberParameters kyber512 = new KyberParameters("kyber512", 2);
        public static KyberParameters kyber768 = new KyberParameters("kyber768", 3);
        public static KyberParameters kyber1024 = new KyberParameters("kyber1024", 4);

        private string name;
        private int k;
        private KyberEngine engine;

        public KyberParameters(string name, int k)
        {
            this.name = name;
            this.k = k;
            engine = new KyberEngine(k);
        }

        public string Name => name;

        public int K => k;

        internal KyberEngine GetEngine()
        {
            return engine;
        }
    }
}