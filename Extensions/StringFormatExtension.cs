using System.Text.RegularExpressions;

namespace Extensions;

public static class StringFormatExtensions
{
    public static string Format(this string @string, object parameters)
    {
        return parameters
            .GetType()
            .GetProperties()
            .Aggregate(
                @string,
                (accumulator, property) =>
                    Regex.Replace(
                        accumulator,
                        $"{{{property.Name}}}",
                        property.GetValue(parameters)?.ToString() ?? ""
                    )
            );
    }
}
