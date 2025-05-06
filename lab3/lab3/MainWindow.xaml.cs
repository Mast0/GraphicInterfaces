using lab3.DTOs;
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

namespace lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<TextValueDTO> _NSystems;

        public MainWindow()
        {
            InitializeComponent();

            _NSystems = new List<TextValueDTO>();
            Btn_Convert.IsEnabled = false;

            FillNSystems();
            FillCBoxes();

            var clickCOmmand = new CommandBinding(ApplicationCommands.NotACommand, Btn_Convert_Click, canBtn_Convert_Click);
            CommandBindings.Add(clickCOmmand);
        }

        private void ThemeToggle_Checked(object sender, RoutedEventArgs e)
        {
            ChangeTheme("Themes/DarkTheme.xaml");
        }

        private void ThemeToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeTheme("Themes/LightTheme.xaml");
        }

        private void ChangeTheme(string themePath)
        {
            var dict = new ResourceDictionary() { Source = new System.Uri(themePath, System.UriKind.Relative) };

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dict);
        }

        private void FillNSystems()
        {
            _NSystems.Add(new TextValueDTO { Text = "Decimal", Value = 10 });
            _NSystems.Add(new TextValueDTO { Text = "Binary", Value = 2 });
            _NSystems.Add(new TextValueDTO { Text = "Octal", Value = 8 });
            _NSystems.Add(new TextValueDTO { Text = "Hexadecimal", Value = 16 });
        }

        private void FillCBoxes()
        {
            CBox_From.DisplayMemberPath = "Text";
            CBox_To.DisplayMemberPath = "Text";

            _NSystems.ForEach(x =>
            {
                CBox_From.Items.Add(x);
                CBox_To.Items.Add(x);
            });
        }

        private void Btn_Convert_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBox_Input.Text))
            {
                MessageBox.Show("Input can`t be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var from = CBox_From.SelectedItem as TextValueDTO;
            var to = CBox_To.SelectedItem as TextValueDTO;

            int decimalValue = Convert.ToInt32(TBox_Input.Text, from.Value);

            TBox_Output.Text = Convert.ToString(decimalValue, to.Value).ToUpper();
        }

        public void canBtn_Convert_Click(object sender, CanExecuteRoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBox_Input.Text))
            {
                e.CanExecute = false;
            }
            else e.CanExecute = true;
        }

        private void CBox_Validate(object sender, SelectionChangedEventArgs e)
        {
            if (CBox_From.SelectedValue != null &&
                CBox_To.SelectedValue != null)
            {
                Btn_Convert.IsEnabled = true;
            }
            else Btn_Convert.IsEnabled = false;
        }
    }
}