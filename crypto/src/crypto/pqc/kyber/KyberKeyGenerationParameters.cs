using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;

namespace crystals_csharp.crypto.pqc.kyber
{
    public class KyberKeyGenerationParameters
        : KeyGenerationParameters
    {
        private KyberParameters parameters;

        public KyberKeyGenerationParameters(
            SecureRandom random,
            KyberParameters KyberParameters)
            : base(random, 256)
        {
            parameters = KyberParameters;
        }

        public KyberParameters Parameters => parameters;
    }
}