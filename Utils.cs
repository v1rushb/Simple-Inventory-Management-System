using System;
using System.ComponentModel;
using System.Threading;
using System.Xml;
using System.Xml.Schema;

namespace InventoryApplication {
    class MenuUtils {

        private readonly List<string> validResponses = new List<string> {"yes", "no", "y", "n"};
        public int GetValidatedInput() {
            string input;
            int choice;
            while(true) {
                input = Console.ReadLine();
                if(!IsValidInput(input, out choice)) {
                    ShowErrorMessage(input);
                } else {
                    return choice;
                }
            }
        }

        private bool IsValidInput(string input, out int choice) {
            if(int.TryParse(input, out choice)) {
                return (choice >= 1 && choice <= 6);
            }
            return false;
        }

        private void ShowErrorMessage(string input) {
            if(!IsNumeric(input)) {
                Console.WriteLine("Invalid input type. Make sure to enter a numeric value.");
                return;
            }
            Console.WriteLine("Invalid input, Enter a number between 1 and 6.");
        }

        private bool IsNumeric(string str) {
            return int.TryParse(str, out _); // check notes
        }

        public string GetValidatedStringInput() {
            string? input = Console.ReadLine();
            while(!IsValidString(input)) {
                // Console.Clear();
                Console.WriteLine(input);
                Console.WriteLine("Please try again.");
            }
            return input;
        }

        private bool IsValidString(string? str) {
            return !string.IsNullOrWhiteSpace(str);
        }


        public decimal GetValidatedDecimalInput() {
            string? input = Console.ReadLine();
            decimal result;
            while(!IsValidDecimalInput(input, out result)) {
                Console.WriteLine("Invalid input, please try again: ");
                input = Console.ReadLine();
            }
            return result;
        }

        private bool IsValidDecimalInput(string? input, out decimal result) {
            return decimal.TryParse(input, out result);
        }

        public int GetValidatedIntInput() {
            string? input = Console.ReadLine();
            int res;
            while(!IsValidInt(input, out res)) {
                Console.WriteLine("Invalid input, please try again: ");
                input = Console.ReadLine();
            }
            return res;
        }

        private bool IsValidInt(string? input, out int res) {
            return int.TryParse(input, out res);
        }

        public void Delay(int second = 2) {
            int ms = second*1000;
            Thread.Sleep(ms);
        }

        public string GetValidetdYesOrNoString() {
            string? input = Console.ReadLine();
            while(!IsValidYesOrNoString(input)) {
                Console.WriteLine("Invalid input, try again.");
                input = Console.ReadLine();
            }
            return input.ToLower();
        }

        private bool IsValidYesOrNoString(string? input) {
            if(string.IsNullOrWhiteSpace(input)) // hmmm, not needed??
                return false;
            return validResponses.Find(el => el.Equals(input, StringComparison.OrdinalIgnoreCase)) != null;
        }

        public bool getDecision(string userRespone) {
            return userRespone.Equals("y") || userRespone.Equals("yes");
        }
    }
}