using System;
using System.Threading;
using System.Windows;
using System.Windows.Documents;

namespace NMoulinetteApp
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // *** PUBLIC ********************************************

        public MainWindow()
        {
            InitializeComponent();

            _ecrituresFilePath = _configDatas._lastEcrituresFilePath;
            _comptesFilePath = _configDatas._lastComptesFilePath;
            _targetFilePath = _configDatas._lastTargetFilePath;

            EcrituresFilePath.Text = FormatPathForDisplay(_ecrituresFilePath);
            ComptesFilePath.Text = FormatPathForDisplay(_comptesFilePath);
            TargetFilePath.Text = FormatPathForDisplay(_targetFilePath);

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(OnTimer_250ms);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 250);
            dispatcherTimer.Start();
        }

        // *** RESTRICTED ****************************************

        private string _ecrituresFilePath;
        private string _comptesFilePath;
        private string _targetFilePath;
        private ConfigurationDatas _configDatas = new ConfigurationDatas();
        private string _log = string.Empty;
        private Thread _thread;
        private bool _isThreadWorking;
        private bool _isReadingError;
        private bool _isWritingError;

        private void OnTimer_250ms(object sender, EventArgs e)
        {
            LogText.Text = _log;

            if (_thread != null && false == _isThreadWorking)
            {
                _thread.Abort();
                _thread = null;
                OnThreadEnds();
            }
        }

        private void OnClick_ChangeEcrituresFile(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "ecritures"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                _ecrituresFilePath = dialog.FileName;
                EcrituresFilePath.Text = FormatPathForDisplay(_ecrituresFilePath);

                _configDatas._lastEcrituresFilePath = _ecrituresFilePath;
                _configDatas.SaveDatas();
            }
        }

        private void OnClick_ChangeComptesFile(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "comptes"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                _comptesFilePath = dialog.FileName;
                ComptesFilePath.Text = FormatPathForDisplay(_comptesFilePath);

                _configDatas._lastComptesFilePath = _comptesFilePath;
                _configDatas.SaveDatas();
            }
        }

        private void OnClick_ChangeTargetFile(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "comptes"; // Default file name
            dialog.DefaultExt = ".xlsx"; // Default file extension
            dialog.Filter = "Excel documents (.xlsx)|*.xlsx"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                _targetFilePath = dialog.FileName;
                TargetFilePath.Text = FormatPathForDisplay(_targetFilePath);

                _configDatas._lastTargetFilePath = _targetFilePath;
                _configDatas.SaveDatas();
            }
        }

        private void OnClick_Convert(object sender, RoutedEventArgs e)
        {
            ChangeEcrituresFilePathBtn.IsEnabled = false;
            ChangeComptesFilePathBtn.IsEnabled = false;
            ChangeTargetFilePathBtn.IsEnabled = false;
            ConvertBtn.IsEnabled = false;

            _thread = new Thread(new ThreadStart(ThreadLoop));
            _thread.Start();
        }

        private string FormatPathForDisplay(string filePath)
        {
            var length = filePath.Length;
            var maxLength = 50;
            if (length > maxLength) 
            { 
                return "..." + filePath.Substring(length - maxLength, maxLength);
            }
            else
            {
                return filePath;
            }
        }

        private void OnThreadEnds()
        {
            ChangeEcrituresFilePathBtn.IsEnabled = true;
            ChangeComptesFilePathBtn.IsEnabled = true;
            ChangeTargetFilePathBtn.IsEnabled = true;
            ConvertBtn.IsEnabled = true;

            _log = string.Empty;

            if (_isReadingError)
            {
                MessageBox.Show("Impossible de lire les données d'entrée.", "Erreur de lecture", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (_isWritingError)
            {
                MessageBox.Show("Impossible de créer le fichier. Est-il ouvert quelque part ?", "Erreur d'écriture", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ThreadLoop()
        {
            _isThreadWorking = true;
            _isWritingError = false;

            var lineOfDatasForExcel = FichierTexteReader.Read(_ecrituresFilePath, _comptesFilePath, out _isReadingError);
            if (false == _isReadingError)
            {
                FichierExcelWriter.Create(
                    lineOfDatasForExcel,
                    _targetFilePath,
                    s =>
                    {
                        lock (_log)
                        {
                            _log = s;
                        }
                    },
                    out _isWritingError
                    );
            }

            _isThreadWorking = false;
        }
    }
}
