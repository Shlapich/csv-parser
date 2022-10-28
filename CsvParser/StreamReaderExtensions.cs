using System.Reflection;

namespace CsvParser;

public static class StreamReaderExtensions
{
    private static readonly FieldInfo CharPosField = typeof(StreamReader).GetField("_charPos",
        BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);

    private static readonly FieldInfo ByteLenField = typeof(StreamReader).GetField("_byteLen",
        BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);

    private static readonly FieldInfo CharBufferField = typeof(StreamReader).GetField("_charBuffer",
        BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);

    public static long GetPosition(this StreamReader reader)
    {

        var byteLen = (int)ByteLenField.GetValue(reader)!;

        // BaseStream has a buffer size of 1024 bytes
        // And on first read we have start position is 1*buffer size(byte length) so we need to subtract byteLen to start from zero
        var position = reader.BaseStream.Position - byteLen;
        
        var charPos = (int)CharPosField.GetValue(reader)!;
        
        if (charPos <= 0) return position;
        
        var charBuffer = (char[])CharBufferField.GetValue(reader)!;
        var encoding = reader.CurrentEncoding;
        var bytesConsumed = encoding.GetBytes(charBuffer, 0, charPos).Length;
        position += bytesConsumed;

        return position;
    }

    public static void SetPosition(this StreamReader reader, long position)
    {
        reader.DiscardBufferedData();
        reader.BaseStream.Seek(position, SeekOrigin.Begin);
    }
}