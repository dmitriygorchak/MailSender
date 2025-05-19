using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailSender
{
    public class ViewModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Theme { get; set; }
        public string Body { get; set; }
        public ObservableCollection<string> Attachments { get; private set; } // Зроблено публічним для binding

        public ViewModel(string from)
        {
            From = from;
            Attachments = new ObservableCollection<string>(); // Ініціалізація колекції
        }

        public void AddAttach(string at)
        {
            if (!string.IsNullOrEmpty(at))
            {
                Attachments.Add(at);
            }
        }
    }
}