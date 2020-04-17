using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Byteswap
{
    class Program
    {

        static void ParseFile(string SourceFile, string TargetFile)
        {
            FileStream fsInput = null;
            FileStream fsOutput = null;

            // open source file
            try
            {
                fsInput = new FileStream(SourceFile, FileMode.Open, FileAccess.Read);
                if (fsInput.Length % 2 == 1) {
                    Console.WriteLine("Sourcefile contains an odd number of bytes. Can't process file.");
                    Console.WriteLine("Processing failed");
                    Environment.Exit(13);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error opening input file.\r\n" + ex.Message);
                Console.WriteLine("Processing failed");
                Environment.Exit(ex.HResult);
            }

            // open target file
            try
            {
                fsOutput = new FileStream(TargetFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                fsOutput.SetLength(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine ("Error opening output file for writing.\r\n" + ex.Message);
                Console.WriteLine("Processing failed");
                Environment.Exit(ex.HResult);
            }


            // swap bytes and write to output file
            try
            {
                while (fsInput.Position < fsInput.Length)
                {
                    int byte1 = fsInput.ReadByte();
                    int byte2 = fsInput.ReadByte();

                    fsOutput.WriteByte(Convert.ToByte(byte2));
                    fsOutput.WriteByte(Convert.ToByte(byte1));
                }
                Console.WriteLine(fsOutput.Length + " bytes written to " + fsOutput.Name);
                Console.WriteLine("Completed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.HResult + " " + ex.Message);
                Console.WriteLine("Processing failed");
                Environment.Exit(ex.HResult);

            }

        }
        static void Main(string[] args)
        {
            
            Console.WriteLine();
            if (args.Length == 2)
            {
                string inFile = args[0];
                string outFile = args[1];
                ParseFile(inFile, outFile);
            }
            else
            {

                Console.WriteLine("Syntax: " + AppDomain.CurrentDomain.FriendlyName + " <inputfile> <outputfile>");
            }
        }
    }
}
