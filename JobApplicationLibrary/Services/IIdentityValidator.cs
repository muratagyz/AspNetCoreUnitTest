namespace JobApplicationLibrary.Services;

public interface IIdentityValidator
{
    bool IsValid(string identityNumber);

    bool CheckConnectionToRemoteServer();

    string Country { get; }

    ICountryDataProvider CountryDataProvider { get; }
}

public interface ICountryData
{
    string Country { get; }
}

public interface ICountryDataProvider
{
    ICountryData countryData { get; }
}