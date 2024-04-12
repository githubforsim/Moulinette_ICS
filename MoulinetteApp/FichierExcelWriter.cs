﻿using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;

namespace NMoulinetteApp
{
    internal class FichierExcelWriter
    {
        public static void Create(LineOfDatasForExcel[] datas)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook excelWB = excelApp.Workbooks.Add("");
            Excel._Worksheet excelWS = excelWB.ActiveSheet;

            int idColonne = 1;
            excelWS.Cells[1, idColonne++] = "Numéro d'écriture";
            excelWS.Cells[1, idColonne++] = "Compte";
            excelWS.Cells[1, idColonne++] = "Compte Libellé";
            excelWS.Cells[1, idColonne++] = "Date de pièce";
            excelWS.Cells[1, idColonne++] = "Journal";
            excelWS.Cells[1, idColonne++] = "Référence";
            excelWS.Cells[1, idColonne++] = "Ecriture libellé";
            excelWS.Cells[1, idColonne++] = "Débit";
            excelWS.Cells[1, idColonne++] = "Crédit";
            excelWS.Cells[1, idColonne++] = "Lettre";

            var columnHeadingsRange = excelWS.Range[excelWS.Cells[1, 1], excelWS.Cells[1, 10]];
            columnHeadingsRange.Interior.Color = XlRgbColor.rgbLightGray;
            columnHeadingsRange.Font.Bold = true;
            columnHeadingsRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            excelWS.Columns["A"].ColumnWidth = 20;
            excelWS.Columns["A"].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            excelWS.Columns["B"].ColumnWidth = 15;
            excelWS.Columns["B"].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            excelWS.Columns["D"].ColumnWidth = 15;
            excelWS.Columns["D"].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            excelWS.Columns["E"].ColumnWidth = 15;
            excelWS.Columns["E"].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            excelWS.Columns["F"].ColumnWidth = 15;
            excelWS.Columns["F"].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            excelWS.Columns["H"].ColumnWidth = 15;
            excelWS.Columns["I"].ColumnWidth = 15;
            excelWS.Columns["J"].ColumnWidth = 15;

            for (int i = 1; i < datas.Length; i++)
            {
                var datasOfLine = datas[i];

                idColonne = 1;
                excelWS.Cells[1 + i, idColonne++] = datasOfLine.numéroDEcriture;
                excelWS.Cells[1 + i, idColonne++] = datasOfLine.compte;
                excelWS.Cells[1 + i, idColonne++] = datasOfLine.compteLibellé;

                excelWS.Cells[1 + i, idColonne++] = datasOfLine.dateDePièce;
                //excelWS.Cells[1 + i, idColonne++].NumberFormat = "DD/MM/YYYY";
                excelWS.Cells[1 + i, idColonne++] = datasOfLine.journal;
                excelWS.Cells[1 + i, idColonne++] = datasOfLine.référence;
                excelWS.Cells[1 + i, idColonne++] = datasOfLine.ecritureLibellé;
                excelWS.Cells[1 + i, idColonne++] = datasOfLine.débit;
                excelWS.Cells[1 + i, idColonne++] = datasOfLine.crédit;
                excelWS.Cells[1 + i, idColonne++] = datasOfLine.lettre;
            }

            excelWS.Columns["C"].Autofit();
            excelWS.Columns["G"].Autofit();

            excelWB.SaveAs(@"writeDataFile.xlsx");
            excelWB.Close();
            excelApp.Quit();
        }
    }
}