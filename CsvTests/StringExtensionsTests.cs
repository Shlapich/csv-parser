using CsvParser;

namespace CsvTests;

public class StringExtensionsTests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase("1,2,3", new[] {"1", "2", "3"})]
    [TestCase("1,2,", new[] {"1", "2", ""})]
    [TestCase("1,,3", new[] {"1", "", "3"})]
    [TestCase(",2,3", new[] {"", "2", "3"})]
    [TestCase("\"1\",\"2\",\"3\"", new[] {"1", "2", "3"})]
    [TestCase("\"1\",\"2\",\"\"", new[] {"1", "2", ""})]
    [TestCase("\"1\",\"\",\"3\"", new[] {"1", "", "3"})]
    [TestCase("\"\",\"2\",\"3\"", new[] {"", "2", "3"})]
    [TestCase("1,2,3", new[] {"1", "2", "3"})]
    [TestCase("1,\"2 with, commas\",3", new[] { "1", "2 with, commas", "3" })]
    [TestCase("1,2,3 ", new[] {"1", "2", "3 "})]
    [TestCase("1 ,2,3", new[] {"1 ", "2", "3"})]
    public void TestRowCount(string row, params string[] expected)
    {
        var actual = row.SplitRow();
        
        Assert.AreEqual(expected, actual);
    }
}