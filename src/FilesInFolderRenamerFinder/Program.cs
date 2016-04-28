using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilesInFolderRenamerFinder
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            int numberofprefixdigits = 3;
            string startfolder = FileRenamer.GetInputFolder();
            string resultfolder = FileRenamer.GetOutputFolder();
            DirectoryInfo d = new DirectoryInfo(resultfolder);
            foreach (DirectoryInfo dir in d.GetDirectories())
            {
                string fakturanummerfile = dir.FullName + "\\fakturor.txt";
                List<FileNameInfo> order = FileRenamer.GetFileOrder(FileRenamer.ReadFakturorFile(fakturanummerfile), 2);

                FileRenamer.RenameFiles(startfolder, dir.FullName, order, numberofprefixdigits);
            }
        }
    }
}
