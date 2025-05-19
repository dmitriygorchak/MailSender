﻿using Microsoft.Win32;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;

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

        private void Attach_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            model.AddAttach(dialog.FileName);
        }

        private void Send_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate email addresses
                new MailAddress(model.From);
                new MailAddress(model.To);

                MailMessage message = new MailMessage(model.From, model.To, model.Theme, model.Body);
                foreach (var item in model.Attachments)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        message.Attachments.Add(new Attachment(item));
                    }
                }
                if (important)
                    message.Priority = MailPriority.High;

                SmtpClient client = new SmtpClient(server, port);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(user_name, user_pass);
                client.SendCompleted += Client_SendCompleted;

                client.SendAsync(message, message);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Invalid email address format. Please check From and To fields.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void Client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            var state = (MailMessage)e.UserState;
            MessageBox.Show($"Message was sent! Subject: {state.Subject}!");
        }

        private void Mark_Important_Button_Click(object sender, RoutedEventArgs e)
        {
            important = true;
        }
    }
}