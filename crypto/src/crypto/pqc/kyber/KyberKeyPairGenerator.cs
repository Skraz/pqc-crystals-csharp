using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;

namespace crystals_csharp.crypto.pqc.kyber
{
    public class KyberKeyPairGenerator
        : IAsymmetricCipherKeyPairGenerator
    {
        private KyberKeyGenerationParameters KyberParams;

        private int k;

        private SecureRandom random;

        private void Initialize(
            KeyGenerationParameters param)
        {
            KyberParams = (KyberKeyGenerationParameters)param;
            random = param.Random;

            k = KyberParams.Parameters.K;
        }

        private AsymmetricCipherKeyPair GenKeyPair()
        {
            KyberEngine engine = KyberParams.Parameters.GetEngine();
            engine.Init(random);
            byte[] sk = new byte[engine.CryptoSecretKeyBytes];
            byte[] pk = new byte[engine.CryptoPublicKeyBytes];
            engine.GenerateKemKeyPair(pk, sk);

            KyberPublicKeyParameters pubKey = new KyberPublicKeyParameters(KyberParams.Parameters, pk);
            KyberPrivateKeyParameters privKey = new KyberPrivateKeyParameters(KyberParams.Parameters, sk);
            return new AsymmetricCipherKeyPair(pubKey, privKey);
        }

        public void Init(KeyGenerationParameters param)
        {
            Initialize(param);
        }

        public AsymmetricCipherKeyPair GenerateKeyPair()
        {
            return GenKeyPair();
        }
    }
}