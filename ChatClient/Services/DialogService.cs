using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChatClient.Services
{
    public class DialogService : IDialogService
    {
        public string OpenFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter= "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            fileDialog.CheckFileExists = true;
            fileDialog.CheckPathExists = true;
            fileDialog.RestoreDirectory = true;

            if(fileDialog.ShowDialog().Value)
            {
                return fileDialog.FileName;
            }
            return string.Empty;
        }

        public bool ShowConfirmationRequest(string message)
        {
            var result = MessageBox.Show(message);
            return result.HasFlag(MessageBoxResult.OK);
        }

        public void ShowNotify(string message)
        {
            MessageBox.Show(message);
        }
    }
}
