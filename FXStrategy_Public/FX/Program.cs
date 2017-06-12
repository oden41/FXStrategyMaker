using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FX
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var mainForm = new MainForm();
            mainForm.ShowDialog();
            mainForm.Activate();
        }
    }
}
