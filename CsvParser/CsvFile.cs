namespace CsvParser;

public class CsvFile : ICsvFile, IDisposable
{
    
    private bool _disposed;
    private readonly StreamReader _reader;
    private Dictionary<int, long> _rowMaps;
    private string[] _columns;

    public CsvFile(string fileName)
    {
        _reader = new StreamReader(fileName);
        ReadColumns();
        Enumerate();
    }
    
    public int RowsCount => _rowMaps.Count;
       
    public IEnumerable<string> Columns => _columns;
    public string this[int rowIndex, string column] => GetCell(rowIndex, column);

    private string GetCell(int rowIndex, string column)
    {
        _reader.SetPosition(_rowMaps[rowIndex]);

        var line = GetCurrentLine();

        if (line == null)
        {
            throw new Exception("Row not found");
        }
        var cells = line.SplitRow();
        var columnIndex = Array.IndexOf(_columns, column);
        return cells[columnIndex];
    }
    
    private void Enumerate()
    {
        _rowMaps = new Dictionary<int, long>();
        var index = 1;
        while (!_reader.EndOfStream)
        {
            var position = _reader.GetPosition();
            var line = GetCurrentLine();
            
            if (string.IsNullOrWhiteSpace(line)) continue;
            
            _rowMaps.Add(index, position);
            index++;
        }
    }

    private void ReadColumns()
    {
        var line = _reader.ReadLine();
        if (string.IsNullOrEmpty(line))
        {
            throw new Exception("File is empty");
        }
        
        _columns = line.SplitRow();
    }

    private string GetCurrentLine()
    {
        var line = _reader.ReadLine();
        var isRecordFinished = line.Count(c => c == '"') % 2 == 0;
        
        while (!isRecordFinished)
        {
            line += _reader.ReadLine();
            isRecordFinished = line.Count(c => c == '"') % 2 == 0;
        }

        return line;
    }
    
    
    private void ReleaseUnmanagedResources()
    {
        if (_disposed)
        {
            return;
        }
        
        _reader.Dispose();
        
        _disposed = true;
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~CsvFile()
    {
        ReleaseUnmanagedResources();
    }
}