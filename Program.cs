using System;

namespace ConvertToWords
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("enter an amount in two decimal places to change to words, within trillion range");
            var value = Console.ReadLine();
            // var value = "1000.11";
            var split = value.Split('.');
            
            if (split.Length == 1 || split.Length > 2 || split[1].Length == 1)
            {
                System.Console.WriteLine("Invalid Input");
                return;
            }

            foreach (var num in split)
            {
                var res = 0m;
                var good = decimal.TryParse(num, out res);
                if (!good)
                {
                    System.Console.WriteLine("Invalid Input");
                    return;
                }
            }
            var input = Convert.ToDecimal(split[0]);
            
            if (Convert.ToInt32(split[1]) > 99)
            {
                System.Console.WriteLine("Invalid number of decimal places");
                return;
            }

            if (input > 999999999999999.99m)
            {
                System.Console.WriteLine("Invalid input, out of range");
                return;
            }

            var index = 5;
            var n = 12;

            for (var i = 5; i > 0; i--)
            {
                if (((double)input / Math.Pow((double)10, (double)n)) >= 1)
                    continue;
                index--;
                n -= 3;
            }
            var figInWords = "";

            for (var j = index; j > 0; j--)
            {
                var figure = Convert.ToInt32(Math.Floor((decimal)((double)input / Math.Pow((double)10, (double)n))));
                if (figure > 0)
                {
                    if (j == 1 && ((input/100) < 1))
                        figInWords += ("and " + HundredsInWords(figure));
                    else
                        figInWords += (HundredsInWords(figure) + words3[j]);
                }
                input -= (decimal)(figure * Math.Pow((double)10, (double)n));
                n -= 3;
            }

            var figAfterDecimalPoint = DecimalFigureInWords(Convert.ToInt32(split[1]));

            if (figInWords != "")
                System.Console.WriteLine(figInWords + " Naira");
            if (figAfterDecimalPoint != "")
                System.Console.WriteLine(figAfterDecimalPoint + " Kobo");
        }
        static string []words1 = {
            "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"
        };
        static string []words2 = {
            "", "", "twenty", "thirty", "fourty", "fifty", "sixty", "seventy", "eighty", "ninety"
        };
        static string []words3 = {
            "", "", " Thousand ", " Million ", " Billion ", " Trillion "
        };

        static string HundredsInWords(int fig)
        {
            var str = "";
            var hundreds = fig/100;
            if (hundreds > 0)
                str += (words1[hundreds] + " hundred");
            var fig1 = fig % 100; //for figures after "hundreds"
            var fig2 = fig1 / 10; //for "tens" starting from twenty
            var fig3 = fig1 % 10; //for "units" and teens
            if (fig1 > 0)
            {
                if (fig1 > 19)
                {
                    if (hundreds > 0)
                    {
                        if (fig3 > 0)
                            str += (" and " + words2[fig2] + " " + words1[fig3]);
                        else
                            str += (" and " + words2[fig2]);
                    }
                    else
                    {
                        if (fig3 > 0)
                            str += (words2[fig2] + " " + words1[fig3]);
                        else
                            str += (words2[fig2]);
                    }
                }

                if (fig1 < 10)
                {
                    if (hundreds > 0)
                        str += (" and " + words1[fig3]);
                    else
                        str += (words1[fig3]);
                }
                if (fig1 > 9 && fig1 < 20)
                {
                    if (hundreds > 0)
                        str += (" and " + words1[fig1]);
                    else
                        str += (words1[fig1]);
                }
            }
            return str;
        }
        static string DecimalFigureInWords(int fig)
        {
            var str = "";
            var tens = fig / 10;
            var fig1 = fig % 10;
            if (fig < 20)
                str += words1[fig];
            else
                str += (words2[tens] + " " + words1[fig1]);

            return str;
        }
    }
}
