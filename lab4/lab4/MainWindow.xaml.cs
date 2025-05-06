using lab4.Entities;
using lab4.Helpers;
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

namespace lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ADOHelper _helper;

        public MainWindow()
        {
            _helper = new ADOHelper();
            InitializeComponent();
        }

        private void Update_Lbox()
        {
            list.SelectedIndex = 0;
            list.Focus();
            list.ItemsSource = _helper.LoadTable().DefaultView;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Update_Lbox();
        }

        private void btn_Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = 1;
                var view = _helper.LoadTable().DefaultView;
                if (view != null && view.Count > 0)
                {
                    id = Convert.ToInt32(view[view.Count-1]["Id"]);
                    id++;
                }

                _helper.CreateBook(id, tbox_ISBN.Text, tbox_Name.Text, tbox_Authors.Text, tbox_Publisher.Text, int.Parse(tbox_PubYear.Text));
                Update_Lbox();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating book: " + ex.Message);
            }
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _helper.UpdateBook(int.Parse(tbox_ID.Text), tbox_ISBN.Text, tbox_Name.Text, tbox_Authors.Text, tbox_Publisher.Text, int.Parse(tbox_PubYear.Text));
                Update_Lbox();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating book: " + ex.Message);
            }
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _helper.DeleteBook(int.Parse(tbox_ID.Text));
                Update_Lbox();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting book: " + ex.Message);
            }
        }
    }
}