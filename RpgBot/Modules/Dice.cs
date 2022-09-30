using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgBot.Modules
{
    public class Dice
    {
        string lastNumberChar = "";

        public int DiceRoll(string arg)
        {
            int value = 0;
            char n;
            int lastChar = 0;
            char lastCharType = 'x';
            int nDices = 0;
            int finalNumber;
            Random numAleatorio = new Random();

            for (int cont = 0; cont < arg.Length; cont++)
            {
                int lastNumber = 0;
                lastNumberChar = "";
                n = arg[cont];
                if (n=='d'||n=='+'||n=='-')
                {
                    switch (arg[cont])
                    {
                        case 'd':
                            for (int contN =  lastChar+1; contN < cont; contN++)
                            {
                                lastNumberChar = (string)(lastNumberChar + arg[contN]);
                            }
                            int.TryParse(lastNumberChar, out nDices);
                            lastCharType = 'd';
                            break;
                        case '+':
                            for (int contN = lastChar + 1; contN < cont; contN++)
                            {
                                lastNumberChar = (string)(lastNumberChar + arg[contN]);
                            }
                            int.TryParse(lastNumberChar, out lastNumber);
                            if (lastCharType == 'd')
                            {
                                value = nDices * numAleatorio.Next(1, 21);
                            }
                            else
                            {
                                if(lastCharType == '-')
                                {
                                    value =  value - lastNumber;
                                }
                                else
                                {
                                    value = -10000;
                                }
                            }
                            lastCharType = '+';
                            break;
                        case '-':
                            for (int contN = lastChar + 1; contN < cont; contN++)
                            {
                                lastNumberChar = (string)(lastNumberChar + arg[contN]);
                            }
                            int.TryParse(lastNumberChar, out lastNumber);
                            if (lastCharType == 'd')
                            {
                                value = nDices * numAleatorio.Next(1, 21);
                            }
                            else
                            {
                                if (lastCharType == '+')
                                {
                                    value = value + lastNumber;
                                }
                                else
                                {
                                    value = -10000;
                                }
                            }
                            lastCharType = '-';
                            break;
                        default:
                            value = -999999;
                            break;
                    }
                    lastChar = cont;
                }
            }

            switch (lastCharType)
            {
                case 'd':
                    for (int contN = lastChar + 1; contN < arg.Length; contN++)
                    {
                        lastNumberChar = (string)(lastNumberChar + arg[contN]);
                    }
                    int.TryParse(lastNumberChar, out finalNumber);
                    value = nDices * numAleatorio.Next(1, 21);
                    break;
                case '+':
                    for (int contN = lastChar + 1; contN < arg.Length; contN++)
                    {
                        lastNumberChar = (string)(lastNumberChar + arg[contN]);
                    }
                    int.TryParse(lastNumberChar, out finalNumber);
                    value = value + finalNumber;
                    break;
                case '-':
                    for (int contN = lastChar + 1; contN < arg.Length; contN++)
                    {
                        lastNumberChar = (string)(lastNumberChar + arg[contN]);
                    }
                    int.TryParse(lastNumberChar, out finalNumber);
                    value = value - finalNumber;
                    break;
                default:
                    value = -999999;
                    break;
            }
            return value;
        }
    }
}
