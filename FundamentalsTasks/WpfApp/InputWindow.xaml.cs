using StandardLibraryConcat;
using System;
using System.Linq;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for InputWindow.xaml
    /// </summary>
    public partial class InputWindow : Window
    {
        public InputWindow()
        {
            InitializeComponent();
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            MainWindow username = new MainWindow();
            username.TextBlockName.Text = textBox1.Text;
            ConcatLogic concatenationLogic = new ConcatLogic();
            string result = concatenationLogic.Concat(Convert.ToString(username.TextBlockName.Text));
            username.TextBlockName.Text = result;
            username.Show();
            Close();
        }
    }
}
