using CsvParser;

var path = "test.txt";

var reader = new CsvFile(path);

var rowCount = reader.RowsCount;
var columns = reader.Columns;

Console.WriteLine($"Rows count: {rowCount}");
Console.WriteLine($"Columns: {string.Join(", ", columns)}");
for (var i = 1; i < rowCount; i++)
{
    foreach (var column in columns)
    {
        Console.WriteLine($"Record: ({i+1}, {column}): {reader[i, column]} ");
    }
}


