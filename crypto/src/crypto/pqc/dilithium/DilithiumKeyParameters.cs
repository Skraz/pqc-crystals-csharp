using crystals_csharp.crypto.pqc.interfaces;
using Org.BouncyCastle.Crypto;

namespace crystals_csharp.crypto.pqc.dilithium
{
    public class DilithiumKeyParameters
        : AsymmetricKeyParameter
    {
        DilithiumParameters parameters;

        public DilithiumKeyParameters(bool isPrivate, DilithiumParameters parameters) : base(isPrivate)
        {
            this.parameters = parameters;
        }

        public DilithiumParameters Parameters => parameters;
    }
}
