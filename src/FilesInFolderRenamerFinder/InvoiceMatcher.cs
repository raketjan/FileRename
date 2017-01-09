using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FilesInFolderRenamerFinder
{
    public class InvoiceMatcher
    {
        private Dictionary<string, string> DelFakturaTillUnderFaktura = new Dictionary<string, string>();



        public void BuildDictionary(string filesFolder)
        {
            DirectoryInfo d = new DirectoryInfo(filesFolder);
            FileInfo[] Files = d.GetFiles();

            foreach (var file in Files)
            {
                string SamlingsFaktura = file.Name.Split('.')[0];
                using (StreamReader sr = file.OpenText())
                {
                    string UnderFaktura = "";
                    while ((UnderFaktura = sr.ReadLine()) != null)
                    {
                        AddToDictionary(UnderFaktura, SamlingsFaktura);
                    }
                }

            }
        }

        private void AddToDictionary(string underFaktura, string samlingsFaktura)
        {
            if (!DelFakturaTillUnderFaktura.ContainsKey(underFaktura))
                DelFakturaTillUnderFaktura.Add(underFaktura, samlingsFaktura);
            else if (DelFakturaTillUnderFaktura[underFaktura].Contains(samlingsFaktura))
                ;       // duplicate in file
            else
            {
                DelFakturaTillUnderFaktura[underFaktura] += ", " + samlingsFaktura;
            }
        }

        public void MatchAndWriteFile(string columnsFile, string outputFile)
        {
            List<string> matchings = new List<string>();
            int notfoundCount = 0;

            foreach (string underFaktura in File.ReadAllLines(columnsFile))
            {
                string matchning = "";
                if (DelFakturaTillUnderFaktura.ContainsKey(underFaktura))
                    matchning = DelFakturaTillUnderFaktura[underFaktura];
                else
                {
                    notfoundCount++;
                }
                matchings.Add(underFaktura + ";" + matchning);
            }

            File.WriteAllLines(outputFile, matchings);
        }
    }
}
