using Org.BouncyCastle.Utilities;

namespace crystals_csharp.crypto.pqc.kyber
{
    public class KyberPublicKeyParameters
        : KyberKeyParameters
    {
        internal byte[] publicKey;

        public byte[] GetEncoded()
        {
            return Arrays.Clone(publicKey);
        }

        public KyberPublicKeyParameters(KyberParameters parameters, byte[] publicKey)
            : base(false, parameters)
        {
            this.publicKey = Arrays.Clone(publicKey);
        }
    }
}