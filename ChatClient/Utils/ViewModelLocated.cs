using ChatClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace ChatClient.Utils
{
    public class ViewModelLocated
    {
        private UnityContainer container;

        public ViewModelLocated()
        {
            container = new UnityContainer();
            container.RegisterType<IChatService, ChatService>();
            container.RegisterType<IDialogService, DialogService>();
        }
        public MainWindowViewModel MainWindow { get; }

    }
}
