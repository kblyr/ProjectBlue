using System.Text;

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

sealed class UserFullNameBuilder : IUserFullNameBuilder
{
    public string Build(IUserFullNameSource source)
    {
        if (source is null)
        {
            return "";
        }

        var firstName = source.FirstName.Trim();
        var lastName = source.LastName.Trim();
        var hasFirstName = !string.IsNullOrWhiteSpace(firstName);
        var hasLastName = !string.IsNullOrWhiteSpace(lastName);
        var builder = new StringBuilder();

        if (hasFirstName)
        {
            builder.Append(firstName);

            if (hasLastName)
            {
                builder.Append(" ");
            }
        }

        if (hasLastName)
        {
            builder.Append(lastName);
        }

        return builder.ToString();
    }
}
