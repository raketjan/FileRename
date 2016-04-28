using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FilesInFolderRenamerFinder
{
    public static class FileRenamer
    {
        public static string GetInputFolder()
        {
            string folder = "C:\\Users\\JanNordberg\\Desktop\\Besab städad";
            return folder;
        }

        public static string GetOutputFolder()
        {
            string folder = "C:\\Users\\JanNordberg\\Desktop\\Besab städad";
            return folder;
        }

        public static List<FileNameInfo> GetFileOrder(List<string> fakturanummer, int startrownumber = 1)
        {
            List<FileNameInfo> fileinfos = new List<FileNameInfo>();
            int i = startrownumber;
            foreach (string str in fakturanummer)
            {
                fileinfos.Add(new FileNameInfo() { filename = "", fakturanr = str, order = i++ });
            }         
            List<FileNameInfo> filteredfileinfos = fileinfos
                                                    .GroupBy(finf => finf.fakturanr)
                                                    .Select(fninfo => fninfo.First())
                                                    .ToList();
            
            return filteredfileinfos;
        }

        public static int RenameFiles(string inputfolder, string outputfolder, List<FileNameInfo> order, int prefixdigits)
        {
            List<string> log = new List<string>();
            List<string> foundfakturor = new List<string>();
            List<string> notfoundfakturor = new List<string>();
            List<string> foundfilenames = new List<string>();
            log.Add(outputfolder);

            DirectoryInfo d = new DirectoryInfo(inputfolder);
            FileInfo[] Files = d.GetFiles();

            foreach (FileNameInfo fnr in order)
            {
                FileInfo[] foundfiles = Array.FindAll(Files, s => s.Name.Contains(fnr.fakturanr));
                if (foundfiles.Length == 0)
                {
                    notfoundfakturor.Add(fnr.fakturanr);
                    continue;
                }
                else if (foundfiles.Length > 1)
                {
                    log.Add("More than one file with fakturanummer " + fnr.fakturanr + ", taking the first one");
                }
                foundfakturor.Add(fnr.fakturanr);
                if (File.Exists(foundfiles[0].FullName))
                {
                    string name = foundfiles[0].Name;
                    string directory = foundfiles[0].DirectoryName;
                    string newname = fnr.order.ToString().PadLeft(prefixdigits, '0') + "_" + foundfiles[0].Name;
                    string newfullname = outputfolder + "\\" + newname;
                    File.Copy(foundfiles[0].FullName, newfullname, true);
                    foundfilenames.Add(newname);                
                }     
            }
            log.Add("Found files: " + foundfakturor.Count);
            log.Add("Not found files:" + notfoundfakturor.Count);


            File.WriteAllLines(outputfolder + "\\log.txt", log, Encoding.UTF8);
            File.WriteAllLines(outputfolder + "\\found.txt", foundfakturor, Encoding.UTF8);
            File.WriteAllLines(outputfolder + "\\notfound.txt", notfoundfakturor, Encoding.UTF8);
            File.WriteAllLines(outputfolder + "\\filenames.txt", foundfilenames, Encoding.UTF8);

            using (StreamWriter sw = File.AppendText(GetOutputFolder() + "\\log.txt"))
            {
                foreach (string str in log)
                    sw.WriteLine(str);
                sw.WriteLine();
            }
            using (StreamWriter sw = File.AppendText(GetOutputFolder() + "\\Ej funna fakturor.txt"))
            {
                foreach (string str in notfoundfakturor)
                    sw.WriteLine(str);
            }
            return 0;
        }

        private static void WriteLogToFile(string path, List<string> log)
        {
            File.WriteAllLines(path, log, Encoding.UTF8);
        }

        private static List<string> testfakturor = new List<string>() {
"59093422729",
"59093422729",
"59093422828",
"59093422927",
"59172980019",
"59173187713",
"59173187713",
"59173233210",
"59173233210",
"59173310810",
"59173315611",
"59173315611",
"59173351111",
"59173351111",
"59173351111",
"59173351111",
"59173351111",
"59173351111",
"59173678422",
"59173743424",
"59173743424",
"59173746922",
"59174192126",
"59174459129",
"59174500021",
"59174626422",
"59174824926",
"59174861621",
"59174901229",
"59174901229",
"59174937629",
"59174937629",
"59174937629",
"59174945721",
"59174945721",
"59174948923",
"59200003321",
"59200003321",
"59200007520",
"59200007520",
"59200007520",
"59200007520",
"59200007520",
"59200007520",
"59200007520",
"59200007520",
"59200034326",
"59200037121",
"59200040828",
"59200044127",
"59200075220",
"59200075220",
"59200080121",
"59200080220",
"59200080220",
"59200086326",
"59200116628",
"80192061929",
"59173194511",
"59173229911",
"59093422422",
"59093422620" };

        public static List<string> ReadFakturorFile(string filepath)
        {
            List<string> fakturor = File.ReadLines(filepath).ToList();
            return fakturor;
        }
    }
}
