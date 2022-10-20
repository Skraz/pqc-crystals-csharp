using crystals_csharp.crypto.pqc.interfaces;

namespace crystals_csharp.crypto.pqc.kyber
{
    public class KyberKEMExtractor
        : IEncapsulatedSecretExtractor
    {
        private KyberEngine engine;

        private KyberKeyParameters key;

        public KyberKEMExtractor(KyberKeyParameters privParams)
        {
            key = privParams;
            InitCipher(key.Parameters);
        }

        private void InitCipher(KyberParameters param)
        {
            engine = param.GetEngine();
        }

        public byte[] ExtractSecret(byte[] encapsulation)
        {
            byte[] session_key = new byte[engine.CryptoBytes];
            engine.KemDecrypt(session_key, encapsulation, ((KyberPrivateKeyParameters)key).privateKey);
            return session_key;
        }

        public int InputSize => engine.CryptoCipherTextBytes;
    }
}