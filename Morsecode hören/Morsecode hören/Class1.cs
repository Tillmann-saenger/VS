using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace Morsecode_hören
{
    class Letter
    {
        private string name;
        private string morsecode;

        private static int frequenz = 800;
        private static int ditDuration = 50;
        private static int dahDuration = 200;

        private static SpeechSynthesizer speaker;

        public Letter(string name, string morsecode)
        {
            this.name = name;
            this.morsecode = morsecode;
        }

        /// plays the entire letter
        public void playLetterAsMorsecode()
        {
            foreach (char letter in morsecode)
            {               
                if (letter.ToString().Equals(".")){
                    playDit();
                    Console.Out.Write(letter);
                } else if (letter.ToString().Equals("-"))
                {
                    playDah();
                    Console.Out.Write(letter);
                } else {
                    //TODO:Eroor ausgeben
                }
            }
            Console.Out.WriteLine("");
        }

        public string getName()
        {
            return this.name;
        }

        public string getMorsecode()
        {
            return this.morsecode;
        }

        public static Boolean setFreqency(int frequenz)
        {
            try
            {
                Letter.frequenz = frequenz;
                return true;
            }
            catch (Exception e)
            {
                //TODO: throw exception
            }
            return false;
        }

        public static Boolean setDitDuration(int duration)
        {
            try{
                Letter.ditDuration = duration;
                return true;
            }catch (Exception e){
                //TODO: throw exception
            }
            return false;
        }

        public static Boolean setDahDuration(int duration)
        {
            try
            {
                Letter.dahDuration = duration;
                return true;
            }
            catch (Exception e)
            {
                //TODO: throw exception
            }
            return false;
        }


        private static void playDit()
        {
            Console.Beep(frequenz, ditDuration);
        }

        private static void playDah()
        {
            Console.Beep(frequenz, dahDuration);
        }
        public Boolean playLetterAsSound()
        {
            try{
                speaker = new SpeechSynthesizer();
                speaker.SetOutputToDefaultAudioDevice();
                speaker.Rate = 1;
                speaker.Volume = 100;
                speaker.SpeakAsync(this.getName());
                return true;
            }catch{
                //TODO: throw exception
            }
            return false;
        }
    }
}
