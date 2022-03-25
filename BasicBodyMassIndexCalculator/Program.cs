using System;
using System.Text.RegularExpressions;
using BasicBodyMassIndexCalculator.Common.Classes;

namespace BasicBodyMassIndexCalculator
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Boy/Kilo Endeksi Programı");

            Console.Write("Lütfen adınızı giriniz:");
            string personName = Convert.ToString(Console.ReadLine());

            if (!Regex.IsMatch(personName, "^[A-Za-z]+[A-Za-z ]*$"))
            {
                ValidationError("İsminiz geçersiz karakterler içeriyor. Lütfen geçerli bir isim giriniz.", personName, "Enes Bayraktar");
                return;
            }

            
            Console.Write("Lütfen kilonuzu giriniz:");
            string personWeight = Convert.ToString(Console.ReadLine());

            if (! Int32.TryParse(personWeight, out int numericValue))
            {
                ValidationError("Kilonuz sayılardan oluşmalıdır. Lütfen geçerli bir kilo giriniz.", personWeight, "110");
                return;
            }

            Console.Write("Lütfen boyunuzu metre cinsinden giriniz:");
            string personLength = Convert.ToString(Console.ReadLine());

            if (personLength.IndexOf(".") == -1 | !Double.TryParse(personLength, out double doubleValue))
            {
                ValidationError("Lütfen boyunuzu santimetre cinsinden giriniz.", personLength, "1.89");
                return;
            }

            if (Convert.ToInt32(personLength.Substring(0, 1)) > 2)
            {
                ValidationError("Lütfen boyunuzu doğru giriniz.", personLength, "1.70");
                return;
            }

            Person person = new Person(
                personName,
                Convert.ToInt32(personWeight),
                Convert.ToDouble(personLength)
            );

            int personMassIndex = person.CalculateMassIndex();

            if (-10 <= personMassIndex && personMassIndex <= 10)
            {
                Console.WriteLine("Kilonuz normal");
            } else if (10 <= personMassIndex)
            {
                Console.WriteLine("Kilonuz referans değerlerinin altında, kilo almalısınız.");
            } else
            {
                Console.WriteLine("Kilonuz referans değerlerin üstünde, kilo vermelisiniz.");
            }

        }

        public static void ValidationError(string errorMessage, string errorValue, string defaultValue)
        {
            Console.WriteLine($"\nHata: {errorMessage}\nGirilen değer:{errorValue}\nÖrnek değer:{defaultValue}");
        }
    }
}
