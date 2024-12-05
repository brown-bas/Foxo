using Foxo;

namespace FoxoLib
{
    public class InputHelper
    {
        public static T GetValidatedInput<T>(string prompt, Func<string, bool> validationFunc)
        {
            T result = default!;
            bool validInput = false;

            while (!validInput)
            {
                Console.Write(prompt);
                string userInput = Console.ReadLine()!;

                if (validationFunc(userInput))
                {
                    try
                    {
                        result = (T)Convert.ChangeType(userInput, typeof(T));
                        validInput = true;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"Hiba: A bemenet nem alakítható át '{typeof(T)}' típusra.");
                    }
                }
                else
                {
                    Console.WriteLine("Nem megfelelő bemenet!\n");
                }
            }

            return result;
        }

        public static Func<string, bool> CreateValidationFuncForInt(int min, int max) {
            return input =>
            {
                if (int.TryParse(input, out int value))
                {
                    return value >= min && value <= max;
                }
                return false;
            };
        }
        public static Func<string, bool> CreateValidationFuncForStr(int min, int max) {
            return input =>
            {
                return input.Length > min && input.Length <= max;
            };
        }
        public static Func<string, bool> CreateValidationFuncForChar(char[] charList) {
            return input =>
            {
                if (char.TryParse(input, out char value))
                {
                    return charList.Contains(value);
                }
                return false;
            };
        }
    }
}
