using Org.BouncyCastle.Utilities;

namespace crystals_csharp.crypto.pqc.kyber
{
    public class KyberPrivateKeyParameters
        : KyberKeyParameters
    {
        internal byte[] privateKey;

        public KyberPrivateKeyParameters(KyberParameters parameters, byte[] privateKey)
            : base(true, parameters)
        {
            this.privateKey = Arrays.Clone(privateKey);
        }

        public byte[] GetEncoded()
        {
            return Arrays.Clone(privateKey);
        }
    }
}