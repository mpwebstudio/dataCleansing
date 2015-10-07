namespace data_validation.net.Web.Controllers.Helpers
{
    using System.Text.RegularExpressions;
    
    public class ConversionController
    {
        public static string FranceToDigit(Match match)
        {
            string number = string.Empty;
            switch (match.ToString())
            {
                case "A": number = "1"; break;
                case "B": number = "2"; break;
                case "C": number = "3"; break;
                case "D": number = "4"; break;
                case "E": number = "5"; break;
                case "F": number = "6"; break;
                case "G": number = "7"; break;
                case "H": number = "8"; break;
                case "I": number = "9"; break;
                case "J": number = "1"; break;
                case "K": number = "2"; break;
                case "L": number = "3"; break;
                case "M": number = "4"; break;
                case "N": number = "5"; break;
                case "O": number = "6"; break;
                case "P": number = "7"; break;
                case "Q": number = "8"; break;
                case "R": number = "9"; break;
                case "S": number = "2"; break;
                case "T": number = "3"; break;
                case "U": number = "4"; break;
                case "V": number = "5"; break;
                case "W": number = "6"; break;
                case "X": number = "7"; break;
                case "Y": number = "8"; break;
                case "Z": number = "9"; break;

                default: number = string.Empty; break;
            }

            return number;
        }

        public static int EvenNumber(char match)
        {
            int number = 0;

            switch (match.ToString())
            {
                case "A": number = 0; break;
                case "B": number = 1; break;
                case "C": number = 2; break;
                case "D": number = 3; break;
                case "E": number = 4; break;
                case "F": number = 5; break;
                case "G": number = 6; break;
                case "H": number = 7; break;
                case "I": number = 8; break;
                case "J": number = 9; break;
                case "K": number = 10; break;
                case "L": number = 11; break;
                case "M": number = 12; break;
                case "N": number = 13; break;
                case "O": number = 14; break;
                case "P": number = 15; break;
                case "Q": number = 16; break;
                case "R": number = 17; break;
                case "S": number = 18; break;
                case "T": number = 19; break;
                case "U": number = 20; break;
                case "V": number = 21; break;
                case "W": number = 22; break;
                case "X": number = 23; break;
                case "Y": number = 24; break;
                case "Z": number = 25; break;
                case "-": number = 26; break;
                case ".": number = 27; break;
                case "␢": number = 28; break;
                case "0": number = 0; break;
                case "1": number = 1; break;
                case "2": number = 2; break;
                case "3": number = 3; break;
                case "4": number = 4; break;
                case "5": number = 5; break;
                case "6": number = 6; break;
                case "7": number = 7; break;
                case "8": number = 8; break;
                case "9": number = 9; break;
            }

            return number;
        }

        public static int OddNumber(char match)
        {
            int number = 0;

            switch (match.ToString())
            {
                case "A": number = 1; break;
                case "B": number = 0; break;
                case "C": number = 5; break;
                case "D": number = 7; break;
                case "E": number = 9; break;
                case "F": number = 13; break;
                case "G": number = 15; break;
                case "H": number = 17; break;
                case "I": number = 19; break;
                case "J": number = 21; break;
                case "K": number = 2; break;
                case "L": number = 4; break;
                case "M": number = 18; break;
                case "N": number = 20; break;
                case "O": number = 11; break;
                case "P": number = 3; break;
                case "Q": number = 6; break;
                case "R": number = 8; break;
                case "S": number = 12; break;
                case "T": number = 14; break;
                case "U": number = 16; break;
                case "V": number = 10; break;
                case "W": number = 22; break;
                case "X": number = 25; break;
                case "Y": number = 24; break;
                case "Z": number = 23; break;
                case "-": number = 27; break;
                case ".": number = 28; break;
                case "␢": number = 26; break;
                case "0": number = 1; break;
                case "1": number = 0; break;
                case "2": number = 5; break;
                case "3": number = 7; break;
                case "4": number = 9; break;
                case "5": number = 13; break;
                case "6": number = 15; break;
                case "7": number = 17; break;
                case "8": number = 19; break;
                case "9": number = 21; break;
            }

            return number;
        }

        public static string CinToChar(int match)
        {
            string cin = string.Empty;

            switch (match)
            {
                case 0: cin = "A"; break;
                case 1: cin = "B"; break;
                case 2: cin = "C"; break;
                case 3: cin = "D"; break;
                case 4: cin = "E"; break;
                case 5: cin = "F"; break;
                case 6: cin = "G"; break;
                case 7: cin = "H"; break;
                case 8: cin = "I"; break;
                case 9: cin = "J"; break;
                case 10: cin = "K"; break;
                case 11: cin = "L"; break;
                case 12: cin = "M"; break;
                case 13: cin = "N"; break;
                case 14: cin = "O"; break;
                case 15: cin = "P"; break;
                case 16: cin = "Q"; break;
                case 17: cin = "R"; break;
                case 18: cin = "S"; break;
                case 19: cin = "T"; break;
                case 20: cin = "U"; break;
                case 21: cin = "V"; break;
                case 22: cin = "W"; break;
                case 23: cin = "X"; break;
                case 24: cin = "Y"; break;
                case 25: cin = "Z"; break;
            }

            return cin;
        }
    }
}