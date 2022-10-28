using System.Text.RegularExpressions;

namespace CsvParser;

public static class StringExtensions
{
    private static readonly Regex QuotedValue = new(",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)", RegexOptions.Compiled);
    
    public static string[] SplitRow(this string row)
    {
        var res = QuotedValue.Split(row);
        for (var i = 0; i < res.Length; i++)
        {
            res[i] = res[i].Trim('"');
        }

        return res;
    }
}