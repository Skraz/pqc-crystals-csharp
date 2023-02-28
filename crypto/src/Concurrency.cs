using crystals_csharp.crypto.pqc.interfaces;
using crystals_csharp.crypto.pqc.kyber;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Security;
using System.Reflection;
using Org.BouncyCastle.Pqc.Crypto;
using System.Diagnostics;
using crystals_csharp.crypto.pqc.dilithium;

namespace crystals_csharp
{
    class Concurrency
    {
        static void Main()
        {
            //CheckKyber();
            CheckDilithium();
        }

        static void CheckDilithium()
        {
            DilithiumParameters[] parameters =
            {
                DilithiumParameters.Dilithium2 ,
                DilithiumParameters.Dilithium3 ,
                DilithiumParameters.Dilithium5
            };


            string[] files =
            {
                "PQCsignKAT_Dilithium2.rsp",
                "PQCsignKAT_Dilithium3.rsp",
                "PQCsignKAT_Dilithium5.rsp"
            };

            string[] outFiles =
            {
                "Dilithium2-concurrency.csv",
                "Dilithium3-concurrency.csv",
                "Dilithium5-concurrency.csv"
            };


            for (int fileIndex = 0; fileIndex != files.Length; fileIndex++)
            {
                string name = files[fileIndex];
                string outName = "C:\\Users\\ladmin\\Documents\\University\\Year 4\\crystals-csharp\\crypto\\src\\crypto\\" + outFiles[fileIndex];
                File.WriteAllText(outName, String.Empty);
                Assembly assembly = Assembly.GetExecutingAssembly();
                StreamReader src = new StreamReader(assembly.GetManifestResourceStream("crystals_csharp.data.crypto.pqc.crystals.dilithium." + name));

                StreamWriter sw = new StreamWriter(outName);

                Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
                long nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;



                string line = null;
                Dictionary<string, string> buf = new Dictionary<string, string>();
                while ((line = src.ReadLine()) != null)
                {
                    line = line.Trim();

                    if (line.StartsWith("#"))
                    {
                        continue;
                    }

                    if (line.Length == 0)
                    {
                        if (buf.Count > 0)
                        {
                            string count = buf["count"];
                            byte[] seed = Hex.Decode(buf["seed"]);      // seed for Dilithium secure random
                            //int mlen = int.Parse(buf["mlen"]);          // message length
                            byte[] msg = Hex.Decode(buf["msg"]);        // message
                            //byte[] pk = Hex.Decode(buf["pk"]);          // public key
                            //byte[] sk = Hex.Decode(buf["sk"]);          // private key
                            //int smlen = int.Parse(buf["smlen"]);        // signature length
                            //byte[] sigExpected = Hex.Decode(buf["sm"]); // signature

                            SecureRandom random = new SecureRandom();
                            random.SetSeed(seed);
                            DilithiumParameters dilithiumparameters = parameters[fileIndex];

                            DilithiumKeyPairGenerator kpGen = new DilithiumKeyPairGenerator();
                            DilithiumKeyGenerationParameters genParams = new DilithiumKeyGenerationParameters(random, dilithiumparameters);

                            //
                            // Generate keys and test.
                            //
                            kpGen.Init(genParams);

                            GC.Collect();
                            watch.Restart();

                            AsymmetricCipherKeyPair kp = kpGen.GenerateKeyPair();

                            long keypairMemory = GC.GetTotalMemory(true);

                            watch.Stop();
                            long keypairSeconds = watch.ElapsedTicks * nanosecPerTick;


                            DilithiumPublicKeyParameters pubParams = (DilithiumPublicKeyParameters)kp.Public;
                            DilithiumPrivateKeyParameters privParams = (DilithiumPrivateKeyParameters)kp.Private;

                            //Console.WriteLine(string.Format("{0} Expected pk       = {1}", pk.Length, Convert.ToHexString(pk)));
                            //Console.WriteLine(String.Format("{0} Actual Public key = {1}", pubParams.GetEncoded().Length, Convert.ToHexString(pubParams.GetEncoded())));


                            //
                            // Signature test
                            //
                            DilithiumSigner signer = new DilithiumSigner(random);

                            signer.Init(true, privParams);

                            GC.Collect();
                            watch.Restart();

                            byte[] sigGenerated = signer.GenerateSignature(msg);

                            long sigGenMemory = GC.GetTotalMemory(true);

                            watch.Stop();
                            long sigGenSeconds = watch.ElapsedTicks * nanosecPerTick;

                            byte[] attachedSig = Arrays.ConcatenateAll(sigGenerated, msg);


                            //Console.WriteLine(string.Format("{0} Expected sig       = {1}", sigExpected.Length, Convert.ToHexString(sigExpected)));
                            //Console.WriteLine(String.Format("{0} Actual Signature   = {1}", attachedSig.Length, Convert.ToHexString(attachedSig)));


                            byte[] msg1 = new byte[msg.Length];

                            signer.Init(false, pubParams);
                            GC.Collect();
                            watch.Restart();

                            signer.VerifySignature(msg1, attachedSig);

                            long sigVerifyMemory = GC.GetTotalMemory(true);

                            watch.Stop();
                            long sigVerifySeconds = watch.ElapsedTicks * nanosecPerTick;

                            String outString = String.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}", count, keypairSeconds.ToString(), keypairMemory.ToString(), sigGenSeconds.ToString(), sigGenMemory.ToString(), sigVerifySeconds.ToString(), sigVerifyMemory.ToString());

                            sw.WriteLine(outString);
                        }

                        buf.Clear();
                        //break;
                        continue;
                    }

                    int a = line.IndexOf("=");
                    if (a > -1)
                    {
                        buf[line.Substring(0, a).Trim()] = line.Substring(a + 1).Trim();
                    }
                }
                sw.Close();
            }
        }

        static void CheckKyber() {
            KyberParameters[] KyberParams =
                       {
                KyberParameters.kyber512,
                KyberParameters.kyber768,
                KyberParameters.kyber1024,
            };
            string[] files =
            {
                "kyber512.rsp",
                "kyber768.rsp",
                "kyber1024.rsp"
            };

            string[] outFiles =
            {
                "kyber512-concurrency.csv",
                "kyber768-concurrency.csv",
                "kyber1024-concurrency.csv"
            };

            for (int fileIndex = 0; fileIndex != files.Length; fileIndex++)
            {
                string name = files[fileIndex];
                string outName = "C:\\Users\\ladmin\\Documents\\University\\Year 4\\crystals-csharp\\crypto\\src\\crypto\\" + outFiles[fileIndex];
                File.WriteAllText(outName, String.Empty);
                Assembly assembly = Assembly.GetExecutingAssembly();
                StreamReader src = new StreamReader(assembly.GetManifestResourceStream("crystals_csharp.data.crypto.pqc.crystals.kyber." + name));

                StreamWriter sw = new StreamWriter(outName);

                Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
                long nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;



                string line = null;
                Dictionary<string, string> buf = new Dictionary<string, string>();
                while ((line = src.ReadLine()) != null)
                {
                    line = line.Trim();

                    if (line.StartsWith("#"))
                    {
                        continue;
                    }

                    if (line.Length == 0)
                    {
                        if (buf.Count > 0)
                        {
                            string count = buf["count"];

                            byte[] seed = Hex.Decode(buf["seed"]); // seed for Kyber secure random
                            //byte[] pk = Hex.Decode(buf["pk"]); // public key
                            //byte[] sk = Hex.Decode(buf["sk"]); // private key
                            //byte[] ct = Hex.Decode(buf["ct"]); // ciphertext
                            //byte[] ss = Hex.Decode(buf["ss"]); // session key

                            SecureRandom random = new SecureRandom();
                            random.SetSeed(seed);
                            KyberParameters parameters = KyberParams[fileIndex];

                            KyberKeyPairGenerator kpGen = new KyberKeyPairGenerator();
                            KyberKeyGenerationParameters
                                genParam = new KyberKeyGenerationParameters(random, parameters);

                            //Console.WriteLine(string.Format("seed = {0}", Hex.ToHexString(seed)));

                            //
                            // Generate keys and test.
                            //

                            kpGen.Init(genParam);

                            GC.Collect();
                            watch.Restart();
                            
                            AsymmetricCipherKeyPair kp = kpGen.GenerateKeyPair();

                            long keypairMemory = GC.GetTotalMemory(true);

                            watch.Stop();
                            long keypairSeconds = watch.ElapsedTicks * nanosecPerTick;

 



                            KyberPublicKeyParameters pubParams = (KyberPublicKeyParameters)kp.Public;
                            KyberPrivateKeyParameters privParams = (KyberPrivateKeyParameters)kp.Private;

                            //Console.WriteLine(string.Format("sk = {0}", Hex.ToHexString(sk)));
                            //Console.WriteLine(string.Format("sk bytes = {0}", sk.Length));
                            //Console.WriteLine(string.Format("Secret key = {0}", Hex.ToHexString(privParams.GetEncoded())));
                            //Console.WriteLine(string.Format("secret key bytes = {0}", privParams.GetEncoded().Length));

                            // KEM Enc
                            KyberKEMGenerator KyberEncCipher = new KyberKEMGenerator(random);

                            GC.Collect();

                            watch.Restart();

                            ISecretWithEncapsulation secWenc = KyberEncCipher.GenerateEncapsulated(pubParams);

                            long encryptMemory = GC.GetTotalMemory(true);

                            watch.Stop();
                            long encryptSeconds = watch.ElapsedTicks * nanosecPerTick;


                            byte[] generated_cipher_text = secWenc.GetEncapsulation();


                            //Console.WriteLine(string.Format("ct = {0}", Convert.ToHexString(ct)));
                            //Console.WriteLine(String.Format("ct bytes = {0}", ct.Length));
                            //Console.WriteLine(String.Format("Cipher Text = {0}", Convert.ToHexString(generated_cipher_text)));
                            //Console.WriteLine(String.Format("Cipher Text bytes = {0}", generated_cipher_text.Length));

                            //Console.WriteLine(string.Format("ss = {0}", Convert.ToHexString(ss)));
                            //Console.WriteLine(String.Format("ss bytes = {0}", ss.Length));
                            //Console.WriteLine(String.Format("Shared Secret = {0}", Convert.ToHexString(secWenc.GetSecret())));
                            //Console.WriteLine(String.Format("Shared Secret bytes = {0}", secWenc.GetSecret().Length));

                            byte[] secret = secWenc.GetSecret();

                            // KEM Dec
                            KyberKEMExtractor KyberDecCipher = new KyberKEMExtractor(privParams);

                            GC.Collect();

                            watch.Restart();

                            byte[] dec_key = KyberDecCipher.ExtractSecret(generated_cipher_text);

                            long decryptMemory = GC.GetTotalMemory(true);

                            watch.Stop();
                            long decryptSeconds = watch.ElapsedTicks * nanosecPerTick;

                            String outString = String.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}", count, keypairSeconds.ToString(), keypairMemory.ToString(), encryptSeconds.ToString(), encryptMemory.ToString(), decryptSeconds.ToString(), decryptMemory.ToString());

                            sw.WriteLine(outString);
                        }

                        buf.Clear();

                        //break;
                        continue;
                    }

                    int a = line.IndexOf("=");
                    if (a > -1)
                    {
                        buf[line.Substring(0, a).Trim()] = line.Substring(a + 1).Trim();
                    }
                }
                sw.Close();
            }

        }
    }
}
