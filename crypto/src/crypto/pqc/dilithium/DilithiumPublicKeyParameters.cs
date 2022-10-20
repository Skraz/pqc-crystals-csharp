using Org.BouncyCastle.Utilities;

namespace crystals_csharp.crypto.pqc.dilithium
{
    public class DilithiumPublicKeyParameters
        : DilithiumKeyParameters
    {

        private byte[] publicKey;

        public DilithiumPublicKeyParameters(DilithiumParameters parameters, byte[] pkEncoded)
            : base(false, parameters)
        {
            publicKey = Arrays.Clone(pkEncoded);
        }

        public byte[] GetEncoded()
        {
            return Arrays.Clone(publicKey);
        }

    }
}
