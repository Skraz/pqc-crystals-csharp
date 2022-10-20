using System;
using Org.BouncyCastle.Utilities.Encoders;

namespace crystals_csharp_test.src.util.test
{
    /**
     * A fixed secure random designed to return data for someone needing random bytes.
     */
    public class TestRandomData
        : FixedSecureRandom
    {
        /**
         * Constructor from a Hex encoding of the data.
         *
         * @param encoding a Hex encoding of the data to be returned.
         */
        public TestRandomData(string encoding)
            : this(Hex.Decode(encoding))
        {
        }

        /**
         * Constructor from an array of bytes.
         *
         * @param encoding a byte array representing the data to be returned.
         */
        public TestRandomData(byte[] encoding)
            : base(new Source[] { new Data(encoding) })
        {
        }
    }
}
