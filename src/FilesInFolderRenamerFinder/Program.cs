using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilesInFolderRenamerFinder
{
    public class Program
    {
        //[STAThread]
        //public static void Main(string[] args)
        //{
        //    int numberofprefixdigits = 3;
        //    string startfolder = FileRenamer.GetInputFolder();
        //    string resultfolder = FileRenamer.GetOutputFolder();
        //    DirectoryInfo d = new DirectoryInfo(resultfolder);
        //    foreach (DirectoryInfo dir in d.GetDirectories())
        //    {
        //        string fakturanummerfile = dir.FullName + "\\fakturor.txt";
        //        List<FileNameInfo> order = FileRenamer.GetFileOrder(FileRenamer.ReadFakturorFile(fakturanummerfile), 2);

        //        FileRenamer.RenameFiles(startfolder, dir.FullName, order, numberofprefixdigits);
        //    }
        //}


        // Skapa dictionary över alla 
        public static void Main(string[] args)
        {
            string FilesFolder = "C:\\Users\\Jan Nordberg\\Desktop\\BoJobb\\410xxxxxPer902xxxxx\\";
            InvoiceMatcher invoiceMatcher = new InvoiceMatcher();
            invoiceMatcher.BuildDictionary(FilesFolder);
            string ColumnsFile = "C:\\Users\\Jan Nordberg\\Desktop\\BoJobb\\20161128 13297 Fortums bilaga till S1 11-1, till excel av BN, sorterad på verifikationsnummer endast kolumn med underfakturenummer.txt";
            string Outputfile = "C:\\Users\\Jan Nordberg\\Desktop\\BoJobb\\20161128 13297 Fortums bilaga till S1 11-1, till excel av BN, sorterad på verifikationsnummer endast kolumn med underfakturenummer matchad.csv";
            invoiceMatcher.MatchAndWriteFile(ColumnsFile, Outputfile);
        }
    }
}
