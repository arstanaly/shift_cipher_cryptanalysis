using System;
using System.Text;


namespace ConsoleApp1
{
    class Program
    {
        static char[] alphabet= "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        static int Mod(int a, int n)
        {
            return a - (int)Math.Floor((double)a / n) * n;
        }

        static int get_key_for_known_plaintext(char plaintext_letter, char encryption_letter)
        {
            return Mod((Array.IndexOf(alphabet, encryption_letter) - Array.IndexOf(alphabet, plaintext_letter)), (alphabet.Length - 1));
        }

        static string[] exhaustive_search(string encrypted_text)
        {
            string[] possible_answers = new string[alphabet.Length];

            for(int i = 0; i < alphabet.Length; i++)
            {
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < encrypted_text.Length; ++j)
                {
                    if (Char.IsLetter(encrypted_text[j]))
                    {
                        int val = Mod((Array.IndexOf(alphabet, encrypted_text[j]) - i), (alphabet.Length - 1));
                        sb.Append(alphabet[val]);
                    }
                    else
                        sb.Append(encrypted_text[j]);
                }
                possible_answers[i] = sb.ToString();
            }
      
            return possible_answers;
        }

        static void known_plaintext_analysis() {
            Console.Write("Enter plaintext letter: ");
            char plainttext_letter = Convert.ToChar(Console.ReadLine());
            Console.Write("Enter its encryption: ");
            char encrypted_letter = Convert.ToChar(Console.ReadLine());
            int key = get_key_for_known_plaintext(plainttext_letter, encrypted_letter);
            Console.WriteLine("Enter your encrypted text: ");
            string encrypted_text = Console.ReadLine().ToLower();

            char[] decrypted_text = new char[encrypted_text.Length];
            for (int i = 0; i < encrypted_text.Length; ++i)
            {
                if (Char.IsLetter(encrypted_text[i]))
                    decrypted_text[i] = alphabet[Mod((Array.IndexOf(alphabet, encrypted_text[i]) - key),(alphabet.Length - 1))];
                else decrypted_text[i] = encrypted_text[i];
            }
            Console.WriteLine(decrypted_text);
            Console.WriteLine("Press any key to close programm.");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to a tool for cryptanalysis of shift cipher");
            Console.WriteLine("If you know just one letter of the plaintext and its encryption, press 'y'." +
                "Else press 'n'.");
            string answer_for_known_plaintext = Console.ReadLine();

            if (String.Equals(answer_for_known_plaintext, "y", StringComparison.Ordinal))
            {
                known_plaintext_analysis();
            }
            else
            {
                Console.WriteLine("Enter your encrypted text: ");
                string encrypted_text = Console.ReadLine().ToLower();
                string[] answers = exhaustive_search(encrypted_text);
                for(int i = 0; i < answers.Length; ++i)
                {
                    Console.WriteLine("Key is " + i + ". Decrypted text = " + answers[i]);
                    Console.WriteLine("--------------------------------------------------");
                }
                Console.WriteLine("Press any key to close programm.");
                Console.ReadKey();
            }
        }
    }
}
