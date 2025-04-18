using System;
using System.Text;

namespace CipherCraft
{
    // Abstract class defining the blueprint for string processing
    abstract class AbstractStringProcessing
    {
        public abstract int[] InputCode();
        public abstract int[] OutputCode();
        public abstract string Encode();
        public abstract string Sort();
    }

    class StringProcessing : AbstractStringProcessing
    {
        public int N { get; private set; }
        public string S { get; private set; }

        // Constructor
        public StringProcessing(string inputString, int inputNumber)
        {
            if (!IsValidString(inputString))
                throw new Exception("An error occurred! Only uppercase letters (A-Z) with max 40 characters are allowed");

            if (inputNumber < -25 || inputNumber > 25)
                throw new Exception("An error occurred! N must be between -25 and 25.");

            N = inputNumber;
            S = inputString;
        }

        private static bool IsValidString(string str)
        {
            if (string.IsNullOrEmpty(str) || str.Length > 40)
                return false;

            foreach (char c in str)
            {
                if (c < 'A' || c > 'Z')
                    return false;
            }

            return true;
        }

        // Encode method: convert string
        public override string Encode()
        {
            char[] charArray = S.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                charArray[i] = (char)(charArray[i] + N);
                if (charArray[i] > 'Z')
                {
                    charArray[i] = (char)(charArray[i] - 26);
                }
                else if (charArray[i] < 'A')
                {
                    charArray[i] = (char)(charArray[i] + 26);
                }
            }
            return new string(charArray);
        }
        public override int[] InputCode()
        {
            int[] asciiValues = new int[S.Length];
            for (int i = 0; i < S.Length; i++)
            {
                asciiValues[i] = (int)S[i];
            }
            return asciiValues;
        }

        public override int[] OutputCode()
        {
            string encodedStr = Encode();
            int[] asciiValues = new int[encodedStr.Length];
            for (int i = 0; i < encodedStr.Length; i++)
            {
                asciiValues[i] = (int)encodedStr[i];
            }
            return asciiValues;
        }

        // Sort in ascending order
        public override string Sort()
        {
            char[] chars = S.ToCharArray();
            Array.Sort(chars);
            return new string(chars);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a string (max 40 uppercase letters): ");
            string inputString = Console.ReadLine();
            Console.Write("Enter shift value (-25 to 25): ");
            int inputNumber = ReadValidInteger();
            StringProcessing processor = new StringProcessing(inputString, inputNumber);
            string encodedString = processor.Encode();

            // Display results
            Console.WriteLine("\n--- OUTPUT ---");
            Console.WriteLine($"Original String: {inputString}");
            Console.WriteLine($"Encoded String: {encodedString}");
            Console.WriteLine("Input ASCII Codes: " + string.Join(", ", processor.InputCode()));
            Console.WriteLine("Output ASCII Codes: " + string.Join(", ", processor.OutputCode()));
            Console.WriteLine("Sorted Input String: " + processor.Sort());

            // Reversal check
            StringProcessing reverseProcessor = new StringProcessing(encodedString, -inputNumber);
            string decryptedString = reverseProcessor.Encode();
            Console.WriteLine("\n--- REVERSAL TEST ---");
            Console.WriteLine($"Decrypted String : {decryptedString}");
        }

        // Get valid integer input
        public static int ReadValidInteger()
        {
            int result;
            while (!int.TryParse(Console.ReadLine(), out result) || result < -25 || result > 25)
            {
                Console.WriteLine("Invalid input! Please enter an integer in range [-25 , 25].");
                Console.Write("Enter shift value: ");
            }
            return result;
        }
    }
}