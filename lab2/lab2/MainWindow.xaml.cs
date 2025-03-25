using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string v_SaveFolder = "D:\\KPI\\ГрафІнтерфейси\\GraphicInterfaces\\lab2\\lab2\\text.txt";

        public MainWindow()
        {
            InitializeComponent();
            var saveCommand = new CommandBinding(ApplicationCommands.Save, execute_Save, canExecute_Save);
            var clearCommand = new CommandBinding(ApplicationCommands.Delete, execute_Clear, canExecute_Clear);
            var openCommand = new CommandBinding(ApplicationCommands.Open, execute_Open, canExecute_Open);

            CommandBindings.Add(saveCommand);
            CommandBindings.Add(clearCommand);
            CommandBindings.Add(openCommand);
        }

        private void fontSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TBox.FontSize = fontSlider.Value;
        }

        private void canExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            if (TBox.Text.Trim().Length > 0) 
                e.CanExecute = true; 
            else e.CanExecute = false;
        }
        
        private void execute_Save(object sender, ExecutedRoutedEventArgs e)
        {
            File.WriteAllText(v_SaveFolder, TBox.Text);
            MessageBox.Show("The file was saved");
        }

        private void canExecute_Clear(object sender, CanExecuteRoutedEventArgs e)
        {
            if (TBox.Text.Trim().Length > 0)
                e.CanExecute = true;
            else e.CanExecute = false;
        }

        private void execute_Clear(object sender, ExecutedRoutedEventArgs e)
        {
            TBox.Clear();
        }

        private void canExecute_Open(object sender, CanExecuteRoutedEventArgs e)
        {
            if (File.Exists(v_SaveFolder))
                e.CanExecute = true;
            else e.CanExecute = false;
        }

        private void execute_Open(object sender, ExecutedRoutedEventArgs e)
        {
            TBox.Text = File.ReadAllText(v_SaveFolder);
        }
    }
}