
using System;
using crystals_csharp.crypto.pqc.interfaces;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace crystals_csharp.crypto.pqc.kyber
{
    public class KyberKEMGenerator
        : IEncapsulatedSecretGenerator
    {
        // the source of randomness
        private SecureRandom sr;

        public KyberKEMGenerator(SecureRandom random)
        {
            sr = random;
        }

        public ISecretWithEncapsulation GenerateEncapsulated(AsymmetricKeyParameter recipientKey)
        {
            KyberPublicKeyParameters key = (KyberPublicKeyParameters)recipientKey;
            KyberEngine engine = key.Parameters.GetEngine();
            engine.Init(sr);
            byte[] CipherText = new byte[engine.CryptoCipherTextBytes];
            byte[] SessionKey = new byte[engine.CryptoBytes];
            engine.KemEncrypt(CipherText, SessionKey, key.publicKey);
            return new SecretWithEncapsulationImpl(SessionKey, CipherText);
        }

        private class SecretWithEncapsulationImpl
            : ISecretWithEncapsulation
        {
            private volatile bool hasBeenDestroyed = false;

            private byte[] SessionKey;
            private byte[] CipherText;

            public SecretWithEncapsulationImpl(byte[] sessionKey, byte[] cipher_text)
            {
                SessionKey = sessionKey;
                CipherText = cipher_text;
            }

            public byte[] GetSecret()
            {
                CheckDestroyed();

                return Arrays.Clone(SessionKey);
            }

            public byte[] GetEncapsulation()
            {
                CheckDestroyed();

                return Arrays.Clone(CipherText);
            }

            public void Dispose()
            {
                if (!hasBeenDestroyed)
                {
                    hasBeenDestroyed = true;
                    Arrays.Clear(SessionKey);
                    Arrays.Clear(CipherText);
                }
            }

            public bool IsDestroyed()
            {
                return hasBeenDestroyed;
            }

            void CheckDestroyed()
            {
                if (IsDestroyed())
                {
                    throw new ArgumentException("data has been destroyed");
                }
            }
        }
    }
}