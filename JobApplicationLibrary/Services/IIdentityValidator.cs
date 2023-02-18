using JobApplicationLibrary.Models;

namespace JobApplicationLibrary.Services;

public interface IIdentityValidator
{
    bool IsValid(string identityNumber);

    bool CheckConnectionToRemoteServer();

    string Country { get; }

    ICountryDataProvider CountryDataProvider { get; }

    public ValidationMode ValidationMode { get; set; }
}

public interface ICountryData
{
    string Country { get; }
}

public interface ICountryDataProvider
{
    ICountryData countryData { get; }
}

public enum ValidationMode
{
    Detailed,
    Quick
}