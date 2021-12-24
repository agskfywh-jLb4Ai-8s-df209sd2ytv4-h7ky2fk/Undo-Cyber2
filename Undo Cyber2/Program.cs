using System;
using System.IO;
using System.Text;

namespace Undo_Cyber2 {
    class Program {
        private static string InitialWriting = "";
        static void Main() {
            InitialWriting = "Please enter the neccessary details below\nKey: ";

            Console.Write(InitialWriting);
            string key = Console.ReadLine().Trim();

            InitialWriting += key + "\n";
            HandleURI(key);
        }

        private static void HandleURI(string key) {
            string uri = "";
            Console.Clear();
            Console.WriteLine(InitialWriting + "\n");

            do {
                Console.Write("File Path: ");
                uri = Console.ReadLine();

                if (!File.Exists(uri)) {
                    Console.Clear();
                    Console.Write(InitialWriting);
                    Console.WriteLine("\nInvalid File path");
                }
            }
            while (!File.Exists(uri));

            byte[] file = File.ReadAllBytes(uri);

            UndoEncryption(file, key);

        }

        private static void UndoEncryption(byte[] file, string key) {
            Console.Clear();
            Console.WriteLine(Encoding.ASCII.GetString(BitFlip(XORWithKey(file, Encoding.ASCII.GetBytes(key)))));
            Console.ReadLine();
        }


        private static byte[] BitFlip(byte[] input) {
            for (int i = 0; i < input.Length; i++) {
                input[i] ^= 255;
            }
            return input;

        }
        private static byte[] XORWithKey(byte[] input, byte[] key) {
            for (int i = 0; i < input.Length; i++) {
                input[i] ^= key[i % key.Length];
            }
            return input;
        }
    }
}
