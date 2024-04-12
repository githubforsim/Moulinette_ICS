using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace NMoulinetteApp
{

    internal class FichierTexteReader
    {
        public static LineOfDatas[] Read(string textFilePath, string compteFilePath)
        {
            var datasOfComptes = ReadDatasOfComptes(compteFilePath);

            var linesForExcel = new List<LineOfDatas>();
            var linesFromTextFile = File.ReadAllLines(textFilePath);
            for (int i = 0; i < linesFromTextFile.Length; i++)
            {
                string lineFromTextFile = linesFromTextFile[i];
                var components = lineFromTextFile.Split(',');
                if (components.Length < 2)
                {
                    if (i != lineFromTextFile.Length - 1)
                    {
                        throw new System.NotImplementedException();
                    }
                    break;
                }


            }
        }

        private static string GetLibelléOfCompte(string compte, List<DatasOfCompte> datasOfComptes)
        {
            foreach(var datas in datasOfComptes)
            {
                if (datas.compte == compte)
                {
                    return datas.compteLibellé;
                }
            }

            throw new System.NotImplementedException();
        }

        private static List<DatasOfCompte> ReadDatasOfComptes(string compteFilePath) 
        {
            var output = new List<DatasOfCompte>();
            var lines = File.ReadAllLines(compteFilePath);
            for (int i = 0; i < lines.Length; i++) 
            {
                string line = lines[i];
                var components = line.Split(',');
                if ( components.Length < 2) 
                {
                    if (i != lines.Length - 1)
                    {
                        throw new System.NotImplementedException();
                    }
                    break;
                }
                var compte = components[0];
                var libelléCompte = components[1];

                output.Add(new DatasOfCompte(compte, libelléCompte));
            }

            return output;
        }
    }
}
