using System.Windows;

namespace NMoulinetteApp
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var lineOfDatasForExcel = FichierTexteReader.Read("ecritures.txt", "comptes.txt");
            FichierExcelWriter.Create(lineOfDatasForExcel);
        }
    }
}
