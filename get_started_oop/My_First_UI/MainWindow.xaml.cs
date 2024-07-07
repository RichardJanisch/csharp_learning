using System;
using System.Windows;
using System.Threading.Tasks;

namespace MeinErstesProjekt
{
    public partial class MainWindow : Window
    {
        private LongRunningOperation _longRunningOperation;

        public MainWindow()
        {
            InitializeComponent();
            _longRunningOperation = new LongRunningOperation();
        }

        private async void PressButton_Click(object sender, RoutedEventArgs e)
        {
            int result = await _longRunningOperation.PerformCalculationAsync();
            MessageBox.Show($"Asynchrone Berechnung abgeschlossen! Ergebnis: {result}");
        }
    }
}
