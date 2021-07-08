namespace IBANTest.Models
{
    public class Iban4NetModel
    {
        public string CountryCode { get; set; }
        public string CountryCodeAndCheckDigit { get; set; }
        public string IsFromEurozoneSEPACountry { get; set; }
        public string IsFromSEPACountry { get; set; }
        public string IsSupportedCountry { get; set; }
        public string BankCode { get; set; }
        public string BranchCode { get; set; }
        public string AccountNumber { get; set; }
        public string AccountNumberPrefix { get; set; }
        public string AccountType { get; set; }
        public string BalanceAccountNumber { get; set; }
        public string OwnerAccountType { get; set; }
        public string CheckDigit { get; set; }
        public string BBan { get; set; }
        public string IdentificationNumber { get; set; }
        public string NationalCheckDigit { get; set; }
    }
}
