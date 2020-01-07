using Calc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Calc.Models
{
    class WPFViewHandler : IViewHandler
    {
        public void ExitApplication() => Application.Current.Shutdown();

        public void Hide(IView view) => ((Window)view).Hide();

        public bool IsReady(IView view) => (view == null || !((Window)view).IsVisible);


        public void Show(IView view) => ((Window)view).Show();

        public void ShowDialog(IView view) => ((Window)view).ShowDialog();
    }
}
