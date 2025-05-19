using Microsoft.Win32;
using System.Net;
using System.Net.Mail;
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

namespace MailSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string user_name;
        string user_pass;
        string server = "smtp.gmail.com";
        int port = 587;
        ViewModel model;
        bool important;

        //dimon4elogor@gmail.com
        //"kpjc jryl bizv hfmx"
        public MainWindow()
        {
            InitializeComponent();
            Login login = new Login();
            login.ShowDialog();
            user_name = login.loginTb.Text;
            user_pass = login.passTb.Password;
            model = new ViewModel(user_name);
            important = false;
            this.DataContext = model;
        }
    }
}