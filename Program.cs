using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SevenBridgesTree
{
    class Program
    {
        public static Queue<int> TextToQueue(string text)
        {
            var queue = new Queue<int>();
            foreach (var s in text.Split(' '))
            {
                queue.Enqueue(int.Parse(s));
            }
            return queue;
        }

        public static string CreateTree(string preorderStr, string inorderStr)
        {
            var preorder = TextToQueue(preorderStr);
            var inorder = TextToQueue(inorderStr);

            var leaves = new StringBuilder();

            Node n = null;
            int i = inorder.Dequeue();
            while (preorder.Count > 0)
            {
                n = new Node(preorder.Dequeue(), n);
                if (n.Value == i) // Is a leaf.
                {
                    leaves.Append(n.Value).Append(' ');

                    if (inorder.Count == 0) // The end.
                    {
                        break;
                    }

                    var parent = n;
                    while (parent != null && parent.Value == i)
                    {
                        n = parent;
                        parent = n.Parent;
                        while (parent != null && parent.IsFull)
                        {
                            parent = parent.Parent;
                        }
                        i = inorder.Dequeue();
                    }
                }
            }

            return leaves.ToString();
        }

        public static string CalculateMd5Hash(string input)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input.Replace(" ", ""));
            var hash = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (var b in hash)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString().ToLower();
        }

        [STAThread]
        static void Main(string[] args)
        {
            var preorder = "2 1 0 11 7 13 10 12 3 9 1 8 4";
            var inorder = "11 0 1 13 7 12 10 2 9 3 8 1 4";

            // Change the paths below to match the folder on your computer
            // or comment it out to use test values (given in README.txt).
            preorder = File.ReadAllText(@"C:\Users\Velja\Downloads\d62e96d2\preorder.txt");
            inorder = File.ReadAllText(@"C:\Users\Velja\Downloads\d62e96d2\inorder.txt");

            var leaves = CreateTree(preorder, inorder);
            var hash = CalculateMd5Hash(leaves);
            Console.WriteLine("Leaves: " + leaves);
            Console.WriteLine("MD5: " + hash);

            Clipboard.SetText(hash);
            Console.WriteLine("\nHash copied to clipboard.\nPress ENTER to continue...");
            Console.ReadLine();
        }
    }
}
