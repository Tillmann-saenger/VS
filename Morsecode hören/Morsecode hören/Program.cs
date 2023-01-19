using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Morsecode_hören
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private static string[,] alphabet = new string[,]    {  { "A", ".-" },
                                                                { "B", "-..." },
                                                                { "C", "-.-." },
                                                                { "D", "-.." },
                                                                { "E", "." },
                                                                { "F", "..-." },
                                                                { "G", "--." },
                                                                { "H", "...." },
                                                                { "I", ".." },
                                                                { "J", ".---" },
                                                                { "K", "-.-" },
                                                                { "L", ".-.." },
                                                                { "M", "--" },
                                                                { "N", "-." },
                                                                { "O", "---" },
                                                                { "P", ".--." },
                                                                { "Q", "--.-" },
                                                                { "R", ".-." },
                                                                { "S", "..." },
                                                                { "T", "-" },
                                                                { "U", "..-" },
                                                                { "V", "...-" },
                                                                { "W", ".--" },
                                                                { "X", "-..-" },
                                                                { "Y", "-.--" },
                                                                { "Z", "--.." } };

     static void Main(string[] args)
        {
            Random rnd = new Random();
            Letter letter;

            int argLength = 2;
            int timBetweenLetters = 120000;
            IntPtr h = Process.GetCurrentProcess().MainWindowHandle;

            foreach (string arg in args)
            {
                string argumentPrefix = arg.Substring(0, argLength);
                string argument = arg.Substring(argLength, arg.Length - argLength);

                switch (argumentPrefix)
                {
                    case "-h":
                        Console.Out.WriteLine("-h --> help");
                        Console.Out.WriteLine("-. --> sets playtime for . duration");
                        Console.Out.WriteLine("-- --> sets playtime for - duration");
                        Console.Out.WriteLine("-f --> sets frequency fore played tone");
                        Console.Out.WriteLine("-t --> sets time between letters");
                        Console.Out.WriteLine("-v --> invisibility");
                        break;
                    case "-.":
                        Letter.setDitDuration(int.Parse(argument));
                        Console.Out.WriteLine("sets dit duration to: " + argument);
                        break;
                    case "--":
                        Letter.setDahDuration(int.Parse(argument));
                        Console.Out.WriteLine("sets dah duration to: " + argument);
                        break;
                    case "-f":
                        Letter.setFreqency(int.Parse(argument));
                        Console.Out.WriteLine("sets tone frequency to: " + argument);
                        break;
                    case "-t":
                        timBetweenLetters = int.Parse(argument);
                        Console.Out.WriteLine("sets time between letters to: " + argument);
                        break;
                    case "-v":
                        Console.Out.WriteLine("sets invisibility");
                        ShowWindow(h, 0);
                        break;
                    default:
                        break;
                }
            }

            while (true)
            {
                int randomNuber = rnd.Next(0, 26);
                letter = new Letter(alphabet[randomNuber,0], alphabet[randomNuber,1]);
                letter.playLetterAsMorsecode();
                System.Threading.Thread.Sleep(5000);
                letter.playLetterAsSound();
                System.Threading.Thread.Sleep(timBetweenLetters);
            }
        }
    }
}
