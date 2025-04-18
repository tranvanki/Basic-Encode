using System;

namespace CipherCraft
{
    // Abstract class defining the blueprint for string processing
    abstract class AbstractStringProcessing
    {
        public abstract int[] InputCode();
        public abstract int[] OutputCode();
        public abstract string Encode();
        public abstract string Sort();
        public abstract string Print(); // should return the encoded string only
    }

    class StringProcessing : AbstractStringProcessing
    {
        public int N { get; private set; }
        public string S { get; private set; }

        // Constructor
        public StringProcessing(string inputString, int inputNumber)
        {
            if (!IsValidString(inputString))
                throw new ArgumentException("Only uppercase letters (A-Z) with max 40 characters are allowed.");

            if (inputNumber < -25 || inputNumber > 25)
                throw new ArgumentException("N must be between -25 and 25.");

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

        public override string Encode()
        {
            char[] charArray = S.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                char shifted = (char)(charArray[i] + N);
                if (shifted > 'Z')
                    shifted = (char)(shifted - 26);
                else if (shifted < 'A')
                    shifted = (char)(shifted + 26);

                charArray[i] = shifted;
            }
            return new string(charArray);
        }

        public override int[] InputCode()
        {
            int[] codes = new int[S.Length];
            for (int i = 0; i < S.Length; i++)
                codes[i] = (int)S[i];
            return codes;
        }

        public override int[] OutputCode()
        {
            string encoded = Encode();
            int[] codes = new int[encoded.Length];
            for (int i = 0; i < encoded.Length; i++)
                codes[i] = (int)encoded[i];
            return codes;
        }

        public override string Sort()
        {
            char[] chars = S.ToCharArray();
            Array.Sort(chars);
            return new string(chars);
        }

        // ✅ This method must only return the encoded string
        public override string Print()
        {
            return Encode();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a string (max 40 uppercase letters): ");
            string inputString = Console.ReadLine();

            Console.Write("Enter shift value (-25 to 25): ");
            int shift = ReadValidInteger();

            try
            {
                StringProcessing processor = new StringProcessing(inputString, shift);

                Console.WriteLine("\n--- OUTPUT ---");
                Console.WriteLine($"Original String: {inputString}");
                Console.WriteLine($"Encoded String: {processor.Print()}");
                Console.WriteLine("Input ASCII Codes: " + string.Join(", ", processor.InputCode()));
                Console.WriteLine("Output ASCII Codes: " + string.Join(", ", processor.OutputCode()));
                Console.WriteLine("Sorted Input String: " + processor.Sort());

                // Reversal Test
                StringProcessing reverseProcessor = new StringProcessing(processor.Encode(), -shift);
                Console.WriteLine("\n--- REVERSAL TEST ---");
                Console.WriteLine($"Decrypted String: {reverseProcessor.Print()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static int ReadValidInteger()
        {
            int result;
            while (!int.TryParse(Console.ReadLine(), out result) || result < -25 || result > 25)
            {
                Console.WriteLine("Invalid input! Please enter an integer in range [-25, 25].");
                Console.Write("Enter shift value: ");
            }
            return result;
        }
    }
}
