using Org.BouncyCastle.Utilities;

namespace crystals_csharp.crypto.pqc.dilithium
{
    public class DilithiumPrivateKeyParameters
        : DilithiumKeyParameters
    {
        private byte[] privateKey;

        public DilithiumPrivateKeyParameters(DilithiumParameters parameters, byte[] skEncoded)
            : base(true, parameters)
        {
            privateKey = Arrays.Clone(skEncoded);
        }

        public byte[] GetEncoded()
        {
            return Arrays.Clone(privateKey);
        }
    }
}
