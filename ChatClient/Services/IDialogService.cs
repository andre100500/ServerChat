using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Services
{
    public interface IDialogService
    {
        void ShowNotify(string message);
        bool ShowConfirmationRequest(string message);
        string OpenFile();
    }
}
