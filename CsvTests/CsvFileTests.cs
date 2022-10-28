using CsvParser;

namespace CsvTests;

public class CsvFileTests
{
    private CsvFile _csvFile;
    [SetUp]
    public void Setup()
    {
        _csvFile = new CsvFile("Files/test1.csv");
    }

    [Test]
    public void TestRowCount()
    {
        Assert.That(_csvFile.RowsCount, Is.EqualTo(3));
    }
    
    [Test]
    public void TestColumns()
    {
        Assert.That(_csvFile.Columns, Is.EqualTo(new List<string>{"col1","col2","col3"}));
    }
    
    [TestCase(1,"col1", "1")]
    [TestCase(1,"col2", "2 with, commas")]
    [TestCase(1,"col3", "3")]
    [TestCase(2,"col1", "4")]
    [TestCase(2,"col2", "5")]
    [TestCase(2,"col3", "")]
    [TestCase(3,"col1", "7")]
    [TestCase(3,"col2", "8")]
    [TestCase(3,"col3", "9")]

    public void TestIndexer(int row, string column, string result)
    {
        Assert.That(_csvFile[row, column], Is.EqualTo(result));
    }
}