using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MaHoa
{
    public static class Cryption
    {
        private static int Mod_26(int key)
        {
            return (key % 26 + 26) % 26;
        }
        private static int Mod(int a, int b)
        {
            return (a % b + b) % b;
        }

        private static void Swap(ref int x, ref int y)
        {
            int tempswap = x;
            x = y;
            y = tempswap;
        }
        public static class Base64
        {
            public static string Encode(string plainText)
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                return System.Convert.ToBase64String(plainTextBytes);
            }

            public static string Decode(string cipher)
            {
                var base64EncodedBytes = System.Convert.FromBase64String(cipher);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
        }

        public static class MD5
        {
            public static string Encode(string input)
            {
                StringBuilder hash = new StringBuilder();
                MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
                byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

                for (int i = 0; i < bytes.Length; i++)
                {
                    hash.Append(bytes[i].ToString("x2"));
                }
                return hash.ToString();
            }
        }
        public static class Affine
        {
            public static string Encode(string plainText, int a, int b)
            {
                string cipherText = "";
                a = Mod_26(a);
                b = Mod_26(b);

                // Put Plain Text (all capitals) into Character Array
                char[] chars = plainText.ToCharArray();

                // Compute e(x) = (ax + b)(mod m) for every character in the Plain Text
                foreach (char c in chars)
                {
                    char d = char.IsUpper(c) ? 'A' : 'a';
                    int x = Convert.ToInt32(c - d);
                    cipherText += Convert.ToChar(Mod_26(a * x + b) + d);
                }

                return cipherText;
            }


            ///
            /// This function takes cipher text and decrypts it using the Affine Cipher
            /// d(x) = aInverse * (e(x)  b)(mod m).
            ///
            public static string Decode(string cipherText, int a, int b)
            {
                string plainText = "";
                a = Mod_26(a);
                b = Mod_26(b);
                // Get Multiplicative Inverse of a
                int aInverse = MultiplicativeInverse(a);

                // Put Cipher Text (all capitals) into Character Array
                char[] chars = cipherText.ToCharArray();

                // Computer d(x) = aInverse * (e(x)  b)(mod m)
                foreach (char c in chars)
                {
                    char d = char.IsUpper(c) ? 'A' : 'a';
                    int x = Convert.ToInt32(c - d);
                    plainText += Convert.ToChar(Mod_26(aInverse * (x - b)) + d);
                }

                return plainText;
            }

            ///
            /// This functions returns the multiplicative inverse of integer a mod 26.
            ///
            public static int MultiplicativeInverse(int a)
            {
                for (int x = 1; x < 27; x++)
                {
                    if ((a * x) % 26 == 1)
                        return x;
                }

                throw new Exception("No multiplicative inverse found!");
            }
        }

        public static class Ceasar
        {
            public static char cipher(char ch, int key)
            {
                if (!char.IsLetter(ch))
                {
                    return ch;
                }

                key = Mod_26(key);
                
                char d = char.IsUpper(ch) ? 'A' : 'a';
                return (char)(((ch + key - d) % 26) + d);
            }
            public static string Encode(string input, int key)
            {
                string output = string.Empty;
                foreach (char ch in input)
                    output += cipher(ch, key);
                return output;
            }

            public static string Decode(string input, int key)
            {
                return Encode(input, 26 - key);
            }

        }

        public static class Vigenere
        {
            private static string cipher(string input, string key, bool encipher)
            {
                for (int i = 0; i < key.Length; ++i)
                    if (!char.IsLetter(key[i]))
                        return null; // Error

                string output = string.Empty;
                int nonAlphaCharCount = 0;

                for (int i = 0; i < input.Length; ++i)
                {
                    if (char.IsLetter(input[i]))
                    {
                        bool cIsUpper = char.IsUpper(input[i]);
                        char offset = cIsUpper ? 'A' : 'a';
                        int keyIndex = (i - nonAlphaCharCount) % key.Length;
                        int k = (cIsUpper ? char.ToUpper(key[keyIndex]) : char.ToLower(key[keyIndex])) - offset;
                        k = encipher ? k : -k;
                        char ch = (char)((Mod_26(((input[i] + k) - offset))) + offset);
                        output += ch;
                    }
                    else
                    {
                        output += input[i];
                        ++nonAlphaCharCount;
                    }
                }

                return output;
            }

            public static string Encode(string input, string key)
            {
                return cipher(input, key, true);
            }

            public static string Decode(string input, string key)
            {
                return cipher(input, key, false);
            }
        }

        public static class Hill
        {
            //public static void getKeyMatrix(String key,
            //             int[,] keyMatrix)
            //{
            //    int k = 0;
            //    for (int i = 0; i < 3; i++)
            //    {
            //        for (int j = 0; j < 3; j++)
            //        {
            //            keyMatrix[i, j] = (key[k]) % 65;
            //            k++;
            //        }
            //    }
            //}

            //// Following function encrypts the message
            //public static void Encode(int[,] cipherMatrix, int[,] keyMatrix, int[,] messageVector)
            //{
            //    int x, i, j;
            //    for (i = 0; i < 3; i++)
            //    {
            //        for (j = 0; j < 1; j++)
            //        {
            //            cipherMatrix[i, j] = 0;

            //            for (x = 0; x < 3; x++)
            //            {
            //                cipherMatrix[i, j] += keyMatrix[i, x] * messageVector[x, j];
            //            }

            //            cipherMatrix[i, j] = cipherMatrix[i, j] % 26;
            //        }
            //    }
            //}

            //// Function to implement Hill Cipher
            //public static string HillCipher(String message, String key)
            //{

            //    // Get key matrix from the key string
            //    int[,] keyMatrix = new int[3, 3];
            //    getKeyMatrix(key, keyMatrix);

            //    int[,] messageVector = new int[3, 1];

            //    // Generate vector for the message
            //    for (int i = 0; i < 3; i++)
            //        messageVector[i, 0] = (message[i]) % 65;

            //    int[,] cipherMatrix = new int[3, 1];

            //    // Following function generates
            //    // the encrypted vector
            //    Encode(cipherMatrix, keyMatrix, messageVector);

            //    String CipherText = "";

            //    // Generate the encrypted text from
            //    // the encrypted vector
            //    for (int i = 0; i < 3; i++)
            //        CipherText += (char)(cipherMatrix[i, 0] + 65);

            //    // Finally print the ciphertext
            //    return CipherText;
            //}


            public static string Encode(string plaintText, string key)
            {
                // message to uppercase & removing white space from plaintText
                plaintText = plaintText.Trim();

                // if plaintText.length %2 != 0 perform padding
                int lenChk = 0;
                if (plaintText.Length % 2 != 0)
                {
                    plaintText += "0";
                    lenChk = 1;
                }

                // message to 2x plaintText.length/2 matrix
                int[,] plaintText2D = new int[2,plaintText.Length/2];
                int itr1 = 0;
                int itr2 = 0;
                char[] d = new char[4];
                for (int i = 0; i < plaintText.Length; i++)
                {
                    d[i] = char.IsUpper(plaintText[i]) ? 'A' : 'a';
                    if (i % 2 == 0)
                    {
                        plaintText2D[0, itr1] = plaintText[i] - d[i];
                        itr1++;
                    }
                    else
                    {
                        plaintText2D[1, itr2] = plaintText[i] - d[i];
                        itr2++;
                    }
                }

                // key to uppercase & removing white space from key
                key = key.Trim();

                // key to 2x2 matrix
                int[,] key2D = new int[2,2];
                int itr3 = 0;
                char[] d2 = new char[4];
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        d2[itr3] = char.IsUpper(key[itr3]) ? 'A' : 'a';
                        key2D[i,j] = key[itr3] - d2[itr3];
                        itr3++;
                    }
                }

                // checking validity of the key
                // finding determinant
                int deter = key2D[0,0] * key2D[1,1] - key2D[0,1] * key2D[1,0];
                deter = Mod_26(deter);

                // finding multiplicative inverse
                int mulInv = -1;
                for (int i = 0; i < 26; i++)
                {
                    int tempInv = deter * i;
                    if (Mod_26(tempInv) == 1)
                    {
                        mulInv = i;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                } // for

                if (mulInv == -1)
                {
                    MessageBox.Show("Khoá không hợp lệ.");
                    return null;
                }

                string encrypText = "";
                int itrCount = plaintText.Length / 2;
                if (lenChk == 0)
                {
                    // if plaintText.length % 2 == 0
                    for (int i = 0; i < itrCount; i++)
                    {
                        int temp1 = plaintText2D[0,i] * key2D[0,0] + plaintText2D[1,i] * key2D[0,1];
                        encrypText += (char)(Mod_26(temp1) + d[i]);
                        int temp2 = plaintText2D[0,i] * key2D[1,0] + plaintText2D[1,i] * key2D[1,1];
                        encrypText += (char)(Mod_26(temp2) + d[i]);
                    }
                }
                else
                {
                    // if plaintText.length % 2 == 0
                    for (int i = 0; i < itrCount - 1; i++)
                    {
                        int temp1 = plaintText2D[0,i] * key2D[0,0] + plaintText2D[1,i] * key2D[0,1];
                        encrypText += (char)(Mod_26(temp1) + d[i]);
                        int temp2 = plaintText2D[0,i] * key2D[1,0] + plaintText2D[1,i] * key2D[1,1];
                        encrypText += (char)(Mod_26(temp2) + d[i]);
                    }
                }

                return encrypText;
            }
            public static string Decode(string cipher, string key)
            {
                // removing white space from cipher
                cipher = cipher.Trim();
                char[] d = new char[4];
                // if cipher.length %2 != 0 perform padding
                int lenChk = 0;
                if (cipher.Length % 2 != 0)
                {
                    cipher += "0";
                    lenChk = 1;
                }

                // message to 2x cipher.length/2 matrix
                int[,] cipher2D = new int[2, cipher.Length / 2];
                int itr1 = 0;
                int itr2 = 0;
                for (int i = 0; i < cipher.Length; i++)
                {
                    d[i] = char.IsUpper(cipher[i]) ? 'A' : 'a';
                    if (i % 2 == 0)
                    {
                        cipher2D[0, itr1] = cipher[i] - d[i];
                        itr1++;
                    }
                    else
                    {
                        cipher2D[1, itr2] = cipher[i] - d[i];
                        itr2++;
                    }
                }
                // removing white space from key
                key = key.Trim();

                // key to 2x2 matrix
                int[,] key2D = new int[2, 2];
                int itr3 = 0;
                char[] d2 = new char[4];
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        d2[itr3] = char.IsUpper(key[itr3]) ? 'A' : 'a';
                        key2D[i, j] = key[itr3] - d2[itr3];
                        itr3++;
                    }
                }

                // finding determinant
                int deter = key2D[0,0] * key2D[1,1] - key2D[0,1] * key2D[1,0];
                deter = Mod_26(deter);

                // finding multiplicative inverse
                int mulInv = -1;
                for (int i = 0; i < 26; i++)
                {
                    int tempInv = deter * i;
                    if (Mod_26(tempInv) == 1)
                    {
                        mulInv = i;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                } // for

                if (mulInv == -1)
                {
                    MessageBox.Show("Khoá không hợp lệ.");
                    return null;
                }

                // adjugate matrix
                //swapping
                Swap(ref key2D[0,0], ref key2D[1,1]);

                // changing signs
                key2D[0,1] *= -1;
                key2D[1,0] *= -1;

                key2D[0,1] = Mod_26(key2D[0,1]);
                key2D[1,0] = Mod_26(key2D[1,0]);

                // multiplying multiplicative inverse with adjugate matrix
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        key2D[i,j] *= mulInv;
                    }
                }
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        key2D[i,j] = Mod_26(key2D[i,j]);
                    }
                }

                // ciphertext to plain text
                string decrypText = "";
                int itrCount = cipher.Length / 2;
                if (lenChk == 0)
                {
                    // if cipher.length % 2 == 0
                    for (int i = 0; i < itrCount; i++)
                    {
                        int temp1 = cipher2D[0,i] * key2D[0,0] + cipher2D[1,i] * key2D[0,1];
                        decrypText += (char)(Mod_26(temp1) + d[i]);
                        int temp2 = cipher2D[0,i] * key2D[1,0] + cipher2D[1,i] * key2D[1,1];
                        decrypText += (char)(Mod_26(temp2) + d[i]);
                    }
                }
                else
                {
                    // if cipher.length % 2 == 0
                    for (int i = 0; i < itrCount - 1; i++)
                    {
                        int temp1 = cipher2D[0, i] * key2D[0, 0] + cipher2D[1, i] * key2D[0, 1];
                        decrypText += (char)(Mod_26(temp1) + d[i]);
                        int temp2 = cipher2D[0, i] * key2D[1, 0] + cipher2D[1, i] * key2D[1, 1];
                        decrypText += (char)(Mod_26(temp2) + d[i]);
                    }
                }

                return decrypText;
            }
        }

        public static class RailFence
        {
            //public new string AlgorithmName = "Rail Fence";
            public static string Decode(string ciphertext)
            {
                string plaintext = null;
                int j = 0, k = 0, mid;
                char[] ctca = ciphertext.ToCharArray();
                char[,] railarray = new char[2, 100];
                if (ctca.Length % 2 == 0) mid = ((ctca.Length) / 2) - 1;
                else mid = (ctca.Length) / 2;
                for (int i = 0; i < ctca.Length; ++i)
                {
                    if (i <= mid)
                    {
                        railarray[0, j] = ctca[i];
                        ++j;
                    }
                    else
                    {
                        railarray[1, k] = ctca[i];
                        ++k;
                    }
                }
                railarray[0, j] = '\0';
                railarray[1, k] = '\0';
                for (int x = 0; x <= mid; ++x)
                {
                    if (railarray[0, x] != '\0') plaintext += railarray[0, x];
                    if (railarray[1, x] != '\0') plaintext += railarray[1, x];
                }
                return plaintext;
            }
            public static string Encode(string plaintext)
            {
                string ciphertext = null;
                int j = 0, k = 0;
                char[] ptca = plaintext.ToCharArray();
                char[,] railarray = new char[2, 100];
                for (int i = 0; i < ptca.Length; ++i)
                {
                    if (i % 2 == 0)
                    {
                        railarray[0, j] = ptca[i];
                        ++j;
                    }
                    else
                    {
                        railarray[1, k] = ptca[i];
                        ++k;
                    }
                }
                railarray[0, j] = '\0';
                railarray[1, k] = '\0';
                for (int x = 0; x < 2; ++x)
                {
                    for (int y = 0; railarray[x, y] != '\0'; ++y) 
                        ciphertext += railarray[x, y];
                }
                return ciphertext;
            }
        }

        public static class PlayFair
        {
            private static List<int> FindAllOccurrences(string str, char value)
            {
                List<int> indexes = new List<int>();

                int index = 0;
                while ((index = str.IndexOf(value, index)) != -1)
                    indexes.Add(index++);

                return indexes;
            }

            private static string RemoveAllDuplicates(string str, List<int> indexes)
            {
                string retVal = str;

                for (int i = indexes.Count - 1; i >= 1; i--)
                    retVal = retVal.Remove(indexes[i], 1);

                return retVal;
            }

            private static char[,] GenerateKeySquare(string key)
            {
                char[,] keySquare = new char[5, 5];
                string defaultKeySquare = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
                string tempKey = string.IsNullOrEmpty(key) ? "CIPHER" : key.ToUpper();

                tempKey = tempKey.Replace("J", "");
                tempKey += defaultKeySquare;

                for (int i = 0; i < 25; ++i)
                {
                    List<int> indexes = FindAllOccurrences(tempKey, defaultKeySquare[i]);
                    tempKey = RemoveAllDuplicates(tempKey, indexes);
                }

                tempKey = tempKey.Substring(0, 25);

                for (int i = 0; i < 25; ++i)
                    keySquare[(i / 5), (i % 5)] = tempKey[i];

                return keySquare;
            }

            private static void GetPosition(ref char[,] keySquare, char ch, ref int row, ref int col)
            {
                if (ch == 'J')
                    GetPosition(ref keySquare, 'I', ref row, ref col);

                for (int i = 0; i < 5; ++i)
                    for (int j = 0; j < 5; ++j)
                        if (keySquare[i, j] == ch)
                        {
                            row = i;
                            col = j;
                        }
            }

            private static char[] SameRow(ref char[,] keySquare, int row, int col1, int col2, int encipher)
            {
                return new char[] { keySquare[row, Mod((col1 + encipher), 5)], keySquare[row, Mod((col2 + encipher), 5)] };
            }

            private static char[] SameColumn(ref char[,] keySquare, int col, int row1, int row2, int encipher)
            {
                return new char[] { keySquare[Mod((row1 + encipher), 5), col], keySquare[Mod((row2 + encipher), 5), col] };
            }

            private static char[] SameRowColumn(ref char[,] keySquare, int row, int col, int encipher)
            {
                return new char[] { keySquare[Mod((row + encipher), 5), Mod((col + encipher), 5)], keySquare[Mod((row + encipher), 5), Mod((col + encipher), 5)] };
            }

            private static char[] DifferentRowColumn(ref char[,] keySquare, int row1, int col1, int row2, int col2)
            {
                return new char[] { keySquare[row1, col2], keySquare[row2, col1] };
            }

            private static string RemoveOtherChars(string input)
            {
                string output = input;

                for (int i = 0; i < output.Length; ++i)
                    if (!char.IsLetter(output[i]))
                        output = output.Remove(i, 1);

                return output;
            }

            private static string AdjustOutput(string input, string output)
            {
                StringBuilder retVal = new StringBuilder(output);

                for (int i = 0; i < input.Length; ++i)
                {
                    if (!char.IsLetter(input[i]))
                        retVal = retVal.Insert(i, input[i].ToString());

                    if (char.IsLower(input[i]))
                        retVal[i] = char.ToLower(retVal[i]);
                }

                return retVal.ToString();
            }

            private static string Cipher(string input, string key, bool encipher)
            {
                string retVal = string.Empty;
                char[,] keySquare = GenerateKeySquare(key);
                string tempInput = RemoveOtherChars(input);
                int e = encipher ? 1 : -1;

                if ((tempInput.Length % 2) != 0)
                    tempInput += "X";

                for (int i = 0; i < tempInput.Length; i += 2)
                {
                    int row1 = 0;
                    int col1 = 0;
                    int row2 = 0;
                    int col2 = 0;

                    GetPosition(ref keySquare, char.ToUpper(tempInput[i]), ref row1, ref col1);
                    GetPosition(ref keySquare, char.ToUpper(tempInput[i + 1]), ref row2, ref col2);

                    if (row1 == row2 && col1 == col2)
                    {
                        retVal += new string(SameRowColumn(ref keySquare, row1, col1, e));
                    }
                    else if (row1 == row2)
                    {
                        retVal += new string(SameRow(ref keySquare, row1, col1, col2, e));
                    }
                    else if (col1 == col2)
                    {
                        retVal += new string(SameColumn(ref keySquare, col1, row1, row2, e));
                    }
                    else
                    {
                        retVal += new string(DifferentRowColumn(ref keySquare, row1, col1, row2, col2));
                    }
                }

                retVal = AdjustOutput(input, retVal);

                return retVal;
            }

            public static string Encipher(string input, string key)
            {
                return Cipher(input, key, true);
            }

            public static string Decipher(string input, string key)
            {
                return Cipher(input, key, false);
            }
        }
    }
}
