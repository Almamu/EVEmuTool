using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Common.Logging.Streams;

namespace Editor.LogServer
{
    public class FileParser
    {
        public static void DumpFileContents(string filename)
        {
            FileStream stream = File.Open(filename, FileMode.Open);
            BinaryReader reader = new BinaryReader(stream);

            while (stream.Position < stream.Length)
                DumpDataType(reader);
        }

        private static void DumpDataType(BinaryReader reader)
        {
            byte type = reader.ReadByte();

            switch (type)
            {
                case 0x02:
                    Console.WriteLine($"Byte: {reader.ReadByte()}");
                    break;
                case 0x03:
                    Console.WriteLine($"Short: {reader.ReadInt16()}");
                    break;
                case 0x04:
                    Console.WriteLine($"Integer: {reader.ReadInt32()}");
                    break;
                case 0x11:
                    Console.WriteLine($"LongInteger: {reader.ReadInt64()}");
                    break;
                case 0x06:
                {
                    byte length = reader.ReadByte();
                    string value = Encoding.ASCII.GetString(reader.ReadBytes(length));
                    Console.WriteLine($"String ({length}): \"{value}\"");
                }
                    break;
                
                default:
                    reader.BaseStream.Seek(-1, SeekOrigin.Current);
                    Console.WriteLine($"HANDLING AS NORMAL INT64 VALUE: {reader.ReadInt64()}");
                    break;
            }
        }
    }
}