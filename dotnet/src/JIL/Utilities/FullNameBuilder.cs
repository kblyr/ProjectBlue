namespace JIL.Utilities;

public interface IFullNameBuilder
{
    string Build(string firstName, string middleName, string lastName, string nameSuffix);
}