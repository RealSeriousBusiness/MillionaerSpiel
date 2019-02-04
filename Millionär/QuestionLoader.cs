using System;
using System.Collections.Generic;
using System.Text;

namespace Millionär
{
    class QuestionLoader
    {
        byte question;
        String[] lines;

        public QuestionLoader(byte question, String[] lines)
        {
            this.question = question;
            this.lines = lines;
        }

        public string getQuestion()
        {
            //durchläuft alle zeilen
            foreach (string line in lines)
            {
                //wenn gewünschte frage erreicht ist....
                if (line.Contains(question + "=") && line.Contains("|"))
                {
                    //wird diese zwischen "=" und "|" extrahiert und zurückgegeben
                    return line.Split('=')[1].Split('|')[0];
                }
            }
            return "ERROR";
        }

        public char getAnswer()
        {
            //durchläuft alle zeilen
            foreach (string line in lines)
            {
                //wenn gewünschte frage erreicht ist....
                if (line.Contains(question + "=") && line.Contains("|"))
                {
                    //string nach "|" auslesen, in großbuchstaben umwandeln und das erste zeichen dieser zeichenkette zurückgeben
                    return line.Split('|')[1].ToUpper()[0];
                }
            }
            return 'E';
        }

        public string getPosAnswer(char index)
        {
            //durchläuft alle zeilen
            for (byte line = 0; line < lines.Length; line++)
            {
                //wenn gewünschte frage erreicht ist....
                if (lines[line].Contains(question + "=") && lines[line].Contains("|"))
                {
                    byte questionline = line;
                    do
                    {
                        //in nächste zeile springen
                        line++;
                        //wenn diese nächste zeile die gewünschte antwort enthält dann diese zeile zurückgeben
                        if (lines[line].Contains("|" + index + "="))
                        {
                            return lines[line].Split('=')[1];
                        }
                    }
                    while (line < questionline + 4);
                }
            }
            return "ERROR"; 
        }
    }
}
