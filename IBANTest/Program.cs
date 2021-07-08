using System;
using System.Linq;
using System.Text.RegularExpressions;
using static IBANTest.Iban4NetTest;

namespace IBANTest
{
    class Program
    {
        static bool invertColors = false;

        static void Main(string[] args)
        {
            IbanTest("TR330006100519786457841326");

            while (true)
            {
                Console.WriteLine();
                string iban = Console.ReadLine();
                Console.Clear();
                IbanTest(iban);
            }
        }

        static void IbanTest(string iban)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{0,}", options);
            iban = regex.Replace(iban, string.Empty);

            Iban4NetFill(iban);

            //IBAN
            ConsoleWriteInColor(IbanIsValid.ToString(), " :İban Doğruluğu(isValid)", IbanIsValidColor, ConsoleColor.Black);
            ConsoleWriteInLine(IbanModel.BBan?.PadLeft(ElectronicIban.Length, 'X'), " :BBAN");
            ConsoleWriteInLine(ElectronicIban, " :IBAN");
            Console.WriteLine();

            foreach (var valueModel in ValueModels.Values.Reverse())
                ConsoleWriteInColor(valueModel.Value, "", valueModel.ForegroundColor, valueModel.BackgroundColor, false, false);
            Console.WriteLine();

            //Country
            ConsoleWriteInLine("-----------Country(Ülke)--------------");
            ConsoleWriteInColor(IbanModel.CountryCode, " :Ülke Kodu(country Code)", nameof(IbanModel.CountryCode));
            ConsoleWriteInLine(IbanModel.CountryCodeAndCheckDigit, " :Ülke Kodu ve Kontrol Basamağı(Country Code And Check Digit)");
            ConsoleWriteInLine(IbanModel.IsFromEurozoneSEPACountry, " :Euro Bölgesindeki SEPA Ülkesinden mi?(Is From Eurozone SEPA Country)");
            ConsoleWriteInLine(IbanModel.IsFromSEPACountry, " :SEPA Ülkesinden mi?(Is From SEPA Country)");
            ConsoleWriteInLine(IbanModel.IsSupportedCountry, " :Desteklenen Ülke mi?(Is Supported Country)");

            //Bank
            Console.WriteLine();
            ConsoleWriteInLine("-----------Bank(Banka)--------------");
            ConsoleWriteInColor(IbanModel.BankCode, " :Banka Kodu(Bank Code)", nameof(IbanModel.BankCode));
            ConsoleWriteInColor(IbanModel.BranchCode, " :Şube Kodu(Branch Code)", nameof(IbanModel.BranchCode));

            //Account
            Console.WriteLine();
            ConsoleWriteInLine("-----------Account(Hesap)--------------");
            ConsoleWriteInColor(IbanModel.AccountNumber, " :Hesap Numarası(Account Number)", nameof(IbanModel.AccountNumber));
            ConsoleWriteInColor(IbanModel.AccountNumberPrefix, " :Hesap Numarası Prefix(Account Number Prefix)", nameof(IbanModel.AccountNumberPrefix));
            ConsoleWriteInColor(IbanModel.BalanceAccountNumber, " :Balance Hesap Numarası(Balance Account Number)", nameof(IbanModel.BalanceAccountNumber));
            ConsoleWriteInColor(IbanModel.AccountType, " :Hesap Türü(Account Type)", nameof(IbanModel.AccountType));
            ConsoleWriteInColor(IbanModel.OwnerAccountType, " :Owner Hesap Türü(Owner Account Type)", nameof(IbanModel.OwnerAccountType));

            //Others
            Console.WriteLine();
            ConsoleWriteInLine("-----------Others(Diğer İşlemler)--------------");
            ConsoleWriteInColor(IbanModel.CheckDigit, " :Kontrol Basamağı(Calculate Check Digit)", nameof(IbanModel.CheckDigit));
            ConsoleWriteInColor(IbanModel.NationalCheckDigit, " :Ulusal kontrol hanesi(National Check Digit)", nameof(IbanModel.NationalCheckDigit));
            ConsoleWriteInColor(IbanModel.IdentificationNumber, " :Kimlik Numarası(Identification Number)", nameof(IbanModel.IdentificationNumber));
        }

        static void ConsoleWriteInColor(string value, string value2, string valueModelKey, bool WriteLine = true, bool NullCheck = true)
        {
            ConsoleWriteInColor(value, value2, ValueModels[valueModelKey].ForegroundColor, ValueModels[valueModelKey].BackgroundColor, WriteLine, NullCheck);
        }

        static void ConsoleWriteInColor(string value, string value2, ConsoleColor ForegroundColor, ConsoleColor BackgroundColor, bool WriteLine = true, bool NullCheck = true)
        {
            if (NullCheck && string.IsNullOrWhiteSpace(value))
                value = "-";
            else
            {
                if (invertColors)
                {
                    Console.BackgroundColor = ForegroundColor;
                    Console.ForegroundColor = BackgroundColor;
                }
                else
                {
                    Console.BackgroundColor = BackgroundColor;
                    Console.ForegroundColor = ForegroundColor;
                }
            }

            Console.Write(value);
            Console.ResetColor();
            Console.Write(value2);

            if (WriteLine)
                Console.WriteLine();

        }

        static void ConsoleWriteInLine(string value = null, string value2 = null)
        {
            if (string.IsNullOrWhiteSpace(value))
                value = "-";
            Console.WriteLine(value + value2);

        }
    }
}
