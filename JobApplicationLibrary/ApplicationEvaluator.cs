using JobApplicationLibrary.Models;
using JobApplicationLibrary.Services;

namespace JobApplicationLibrary;

public class ApplicationEvaluator
{
    private const int miniAge = 18;
    private const int autoAcceptedYearsOfExperience = 15;
    private List<string> techSatacList = new() { "C#", "RabbitMQ", "Microservice", "Visual Studio" };
    private IdentityValidator IdentityValidator;

    public ApplicationEvaluator()
    {
        IdentityValidator = new IdentityValidator();
    }

    public ApplicationResult Evaluate(JobApplication form)
    {
        if (form.Applicant.Age < miniAge)
            return ApplicationResult.AutoRejected;

        var validIdentity = IdentityValidator.IsValid(form.Applicant.IdentityNumber);

        if (!validIdentity)
            return ApplicationResult.TransferredToHR;

        var sr = GetTechStackSimilarityRate(form.TechStackList);

        if (sr < 25)
            return ApplicationResult.AutoRejected;

        if (sr > 75 && form.YearsOfExperience > autoAcceptedYearsOfExperience)
            return ApplicationResult.AutoAccepted;

        return ApplicationResult.AutoAccepted;
    }

    private int GetTechStackSimilarityRate(List<string> techStacks)
    {
        var matchedCount = techStacks.Where(i => techSatacList.Contains(i, StringComparer.OrdinalIgnoreCase)).Count();

        return (int)((double)matchedCount / techSatacList.Count()) * 100;
    }
}

public enum ApplicationResult
{
    AutoRejected,
    TransferredToHR,
    TransferredToLead,
    TransferredToCTO,
    AutoAccepted
}