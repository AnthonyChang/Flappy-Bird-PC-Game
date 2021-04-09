using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyChangFinalProject
{
    class Score
    {
        public int HighScore { get; private set; }
        public int Scores { get; private set; }

        //Read Highscore from text file on instantiate
        public Score()
        {
            using (StreamReader readtext = new StreamReader("HighScore.txt"))
            {
                this.HighScore = Convert.ToInt32(readtext.ReadLine());
            }
        }

        public void ResetScore()
        {
            Scores = 0;
        }

        public void AddScore()
        {
            Scores++;

            //if player's score is higher than highscore, save score as highscore in text file
            if(Scores > HighScore)
            {
                HighScore = Scores;
                using (StreamWriter writetext = new StreamWriter("HighScore.txt"))
                {
                    writetext.WriteLine(Convert.ToString(HighScore));
                }
            }
        }
    }
}
