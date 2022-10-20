using Org.BouncyCastle.Utilities;
using System;

using M = Org.BouncyCastle.Math;

namespace crystals_csharp_test.src.util.test
{
    /**
     * A fixed secure random designed to return data for someone needing to create a single BigInteger.
     */
    public class TestRandomBigInteger
        : FixedSecureRandom
    {
        /**
         * Constructor from a base 10 represention of a BigInteger.
         *
         * @param encoding a base 10 represention of a BigInteger.
         */
        public TestRandomBigInteger(string encoding)
            : this(encoding, 10)
        {
        }

        /**
         * Constructor from a base radix represention of a BigInteger.
         *
         * @param encoding a String BigInteger of base radix.
         * @param radix the radix to use.
         */
        public TestRandomBigInteger(string encoding, int radix)
            : base(new Source[] { new BigInteger(BigIntegers.AsUnsignedByteArray(new M.BigInteger(encoding, radix))) })
        {
        }

        /**
         * Constructor based on a byte array.
         *
         * @param encoding a 2's complement representation of the BigInteger.
         */
        public TestRandomBigInteger(byte[] encoding)
            : base(new Source[] { new BigInteger(encoding) })
        {
        }

        /**
         * Constructor which ensures encoding will produce a BigInteger from a request from the passed in bitLength.
         *
         * @param bitLength bit length for the BigInteger data request.
         * @param encoding bytes making up the encoding.
         */
        public TestRandomBigInteger(int bitLength, byte[] encoding)
            : base(new Source[] { new BigInteger(bitLength, encoding) })
        {
        }
    }
}
