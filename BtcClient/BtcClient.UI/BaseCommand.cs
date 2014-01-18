using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BtcClient.UI
{
    public class BaseCommand : System.Windows.Input.ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
        public event EventHandler OnExecute;

        public void Execute(object parameter)
        {
            OnExecute(this, new EventArgs());
        }
    }
}
