using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;

namespace crystals_csharp.crypto.pqc.dilithium
{
    public class DilithiumKeyGenerationParameters
     : KeyGenerationParameters
    {
        private DilithiumParameters parameters;

        public DilithiumKeyGenerationParameters(SecureRandom random, DilithiumParameters parameters) : base(random, 255)
        {
            this.parameters = parameters;
        }

        public DilithiumParameters Parameters => parameters;
    }
}
