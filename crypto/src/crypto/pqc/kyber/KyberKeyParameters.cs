using crystals_csharp.crypto.pqc.interfaces;
using Org.BouncyCastle.Crypto;

namespace crystals_csharp.crypto.pqc.kyber
{
    public class KyberKeyParameters
        : AsymmetricKeyParameter
    {
        private KyberParameters parameters;

        public KyberKeyParameters(
            bool isPrivate,
            KyberParameters parameters)
            : base(isPrivate)
        {
            this.parameters = parameters;
        }

        public KyberParameters Parameters => parameters;
    }
}