namespace JIL.Accounts.Utilities;

public interface IUserFullNameBuilder
{
    string Build(IUserFullNameSource source);
}

public interface IUserFullNameSource
{
    string FirstName { get; }
    string LastName { get; }
}