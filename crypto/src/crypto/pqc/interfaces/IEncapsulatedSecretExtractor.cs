namespace crystals_csharp.crypto.pqc.interfaces
{
    public interface IEncapsulatedSecretExtractor
    {
        /// <summary>
        /// Generate an exchange pair based on the recipient public key.
        /// </summary>
        /// <param name="encapsulation"> the encapsulated secret.</param>
        byte[] ExtractSecret(byte[] encapsulation);
    }
}