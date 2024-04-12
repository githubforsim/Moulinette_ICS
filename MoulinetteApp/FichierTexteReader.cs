using System.Collections.Generic;
using System.IO;

namespace NMoulinetteApp
{
    internal class FichierTexteReader
    {
        public static LineOfDatasForExcel[] Read(string textFilePath, string comptesFilePath)
        {
            // Récupérer les données du fichier texte décrivant les comptes
            var datasOfComptes = ReadDatasOfComptes(comptesFilePath);
 
            // Récupérer les lignes du fichier texte principal
            var linesFromTextFile = File.ReadAllLines(textFilePath, System.Text.Encoding.GetEncoding("windows-1252"));

            // Créer un tableau vide qui contiendra les futures lignes du fichier excel
            var linesForExcel = new List<LineOfDatasForExcel>();

            for (int i = 0; i < linesFromTextFile.Length; i++)
            {
                // Récupérer la ième ligne du fichier texte principal
                string lineFromTextFile = linesFromTextFile[i];

                // Récupérer dans un tableau les éléments de cette ligne, séparés par une virgule
                var components = lineFromTextFile.Split(',');
                if (components.Length < 2)
                {
                    // Si la ligne ne contient pas au moins 2 composants ET que celle ligne
                    // n'est pas la dernière, alors il y a un GROS SOUCI.
                    if (i != lineFromTextFile.Length - 1)
                    {
                        throw new System.NotImplementedException();
                    }
                    break;
                }

                var ligneBrute = new LigneBrute()
                {
                    dateDePièce = components[1],
                    compte = components[3],
                    ecritureLibellé = ToolForStrings.RemoveGuillemets(components[5]),
                    nmrFacture = ToolForStrings.RemoveGuillemets(components[6]),
                    montant = components[7],
                    debit_credit = components[8],
                };

                var dateReformatée = ligneBrute.dateDePièce.Substring(0, 2)
                    + "/" + ligneBrute.dateDePièce.Substring(2, 2)
                    + "/20" + ligneBrute.dateDePièce.Substring(4, 2)
                    ;

                var lineForExcel = new LineOfDatasForExcel()
                {
                    numéroDEcriture = ligneBrute.nmrFacture,
                    compte = ligneBrute.compte,
                    compteLibellé = GetLibelléOfCompte(ligneBrute.compte, datasOfComptes),
                    dateDePièce = dateReformatée,
                    référence = ligneBrute.nmrFacture,
                    ecritureLibellé = ligneBrute.ecritureLibellé,
                    débit = ligneBrute.debit_credit == "D" ? ligneBrute.montant : string.Empty,
                    crédit = ligneBrute.debit_credit == "C" ? ligneBrute.montant : string.Empty,
                };
                linesForExcel.Add(lineForExcel);
            }

            return linesForExcel.ToArray();
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
            var lines = File.ReadAllLines(compteFilePath, System.Text.Encoding.GetEncoding("windows-1252"));
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
                var libelléCompte = ToolForStrings.RemoveGuillemets(components[1]);

                output.Add(new DatasOfCompte(compte, libelléCompte));
            }

            return output;
        }
    }
}
