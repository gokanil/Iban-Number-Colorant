using IBANTest.Models;
using SinKien.IBAN4Net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IBANTest
{
    public static class Iban4NetTest
    {
        public static Dictionary<string, ValueModel> ValueModels { get; set; }

        static Dictionary<string, ValueModel> _valueModels;
        public static Iban4NetModel IbanModel;
        public static string ElectronicIban;
        public static bool IbanIsValid;
        public static ConsoleColor IbanIsValidColor
        {
            get => IbanIsValid ? ConsoleColor.Green : ConsoleColor.Red;
        }

        public static void Iban4NetFill(string iban)
        {
            IbanModel = new Iban4NetModel();
            ValueModels = new Dictionary<string, ValueModel>();
            _valueModels = new Dictionary<string, ValueModel>();

            ElectronicIban = iban;
            CheckAndFillIbanModel();
            FillValueModelAndColor();

            if (IbanIsValid)
                IbanSorter(string.Empty);
            else
                ValueModels = _valueModels;
        }

        static void CheckAndFillIbanModel()
        {
            IbanIsValid = IbanUtils.IsValid(ElectronicIban, out IbanFormatViolation validationResult);

            if (IbanIsValid)
            {
                IbanModel.BBan = IbanUtils.GetBBan(ElectronicIban);

                //Country
                IbanModel.CountryCode = IbanUtils.GetCountryCode(ElectronicIban);
                IbanModel.CountryCodeAndCheckDigit = IbanUtils.GetCountryCodeAndCheckDigit(ElectronicIban);
                IbanModel.IsFromEurozoneSEPACountry = IbanUtils.IsFromEurozoneSEPACountry(ElectronicIban).ToString();
                IbanModel.IsFromSEPACountry = IbanUtils.IsFromSEPACountry(ElectronicIban).ToString();
                IbanModel.IsSupportedCountry = IbanUtils.IsSupportedCountry(ElectronicIban).ToString();

                //Bank
                IbanModel.BankCode = IbanUtils.GetBankCode(ElectronicIban);
                IbanModel.BranchCode = IbanUtils.GetBranchCode(ElectronicIban);

                //Account
                IbanModel.AccountNumber = IbanUtils.GetAccountNumber(ElectronicIban);
                IbanModel.AccountNumberPrefix = IbanUtils.GetAccountNumberPrefix(ElectronicIban);
                IbanModel.AccountType = IbanUtils.GetAccountType(ElectronicIban);
                IbanModel.BalanceAccountNumber = IbanUtils.GetBalanceAccountNumber(ElectronicIban);
                IbanModel.OwnerAccountType = IbanUtils.GetOwnerAccountType(ElectronicIban);

                //Others
                IbanModel.CheckDigit = IbanUtils.GetCheckDigit(ElectronicIban);
                IbanModel.IdentificationNumber = IbanUtils.GetIdentificationNumber(ElectronicIban);
                IbanModel.NationalCheckDigit = IbanUtils.GetNationalCheckDigit(ElectronicIban);
            }
        }

        static void FillValueModelAndColor()
        {
            var defaultBackground = ConsoleColor.Black;

            _valueModels.Add(nameof(IbanModel.CountryCode), new ValueModel { Value = IbanModel.CountryCode, ForegroundColor = ConsoleColor.Red, BackgroundColor = defaultBackground });
            _valueModels.Add(nameof(IbanModel.CheckDigit), new ValueModel { Value = IbanModel.CheckDigit, ForegroundColor = ConsoleColor.Blue, BackgroundColor = defaultBackground });
            _valueModels.Add(nameof(IbanModel.BankCode), new ValueModel { Value = IbanModel.BankCode, ForegroundColor = ConsoleColor.Magenta, BackgroundColor = defaultBackground });
            _valueModels.Add(nameof(IbanModel.BranchCode), new ValueModel { Value = IbanModel.BranchCode, ForegroundColor = ConsoleColor.DarkGreen, BackgroundColor = defaultBackground });
            _valueModels.Add(nameof(IbanModel.AccountNumber), new ValueModel { Value = IbanModel.AccountNumber, ForegroundColor = ConsoleColor.DarkYellow, BackgroundColor = defaultBackground });
            _valueModels.Add(nameof(IbanModel.AccountNumberPrefix), new ValueModel { Value = IbanModel.AccountNumberPrefix, ForegroundColor = ConsoleColor.White, BackgroundColor = defaultBackground });
            _valueModels.Add(nameof(IbanModel.AccountType), new ValueModel { Value = IbanModel.AccountType, ForegroundColor = ConsoleColor.DarkGray, BackgroundColor = defaultBackground });
            _valueModels.Add(nameof(IbanModel.BalanceAccountNumber), new ValueModel { Value = IbanModel.BalanceAccountNumber, ForegroundColor = ConsoleColor.White, BackgroundColor = defaultBackground });
            _valueModels.Add(nameof(IbanModel.OwnerAccountType), new ValueModel { Value = IbanModel.OwnerAccountType, ForegroundColor = ConsoleColor.DarkBlue, BackgroundColor = defaultBackground });
            _valueModels.Add(nameof(IbanModel.IdentificationNumber), new ValueModel { Value = IbanModel.IdentificationNumber, ForegroundColor = ConsoleColor.DarkRed, BackgroundColor = defaultBackground });
            _valueModels.Add(nameof(IbanModel.NationalCheckDigit), new ValueModel { Value = IbanModel.NationalCheckDigit, ForegroundColor = ConsoleColor.Yellow, BackgroundColor = defaultBackground });
        }

        static bool IbanSorter(string value)
        {
            if (_valueModels.Count == 0)
                return true;

            foreach (var valueModel in _valueModels.ToList())
            {
                string newValue = string.Concat(value, valueModel.Value.Value);

                if (ElectronicIban.LastIndexOf(newValue) == -1 || ElectronicIban.Substring(0, newValue.Length) != newValue)
                    continue;

                KeyValuePair<string, ValueModel> deletedValueModel = valueModel;
                _valueModels.Remove(valueModel.Key);

                if (IbanSorter(newValue))
                {
                    ValueModels.Add(deletedValueModel.Key, deletedValueModel.Value);
                    return true;
                }

                _valueModels.Add(deletedValueModel.Key, deletedValueModel.Value);
                return false;
            }
            return false;
        }
    }
}
