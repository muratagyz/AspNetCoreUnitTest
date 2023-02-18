namespace JobApplicationLibrary.Services;

public class IdentityValidator:IIdentityValidator
{
    public bool IsValid(string identityNumber)
    {
        throw new NotImplementedException();
    }

    public bool CheckConnectionToRemoteServer()
    {
        throw new NotImplementedException();
    }

    public string Country { get; }
    public ICountryDataProvider CountryDataProvider { get; }
    public ValidationMode ValidationMode { get; set; }
}