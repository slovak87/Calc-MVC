using Calc.Models;
using Ninject;
//using Calc.Views;
using System;
//using System.Windows;

namespace Calc.Controllers
{
    class CalcController : ICalcController
    {
        IKernel container;
        IViewHandler viewHandler;
        IModelFacade model;

        public IView MainView { get; set; }
        public IView LogView { get; set; }
        public IView ErrorView { get; set; }
        public string Error { get; private set; }

        public CalcController(IKernel container, IViewHandler viewHandler)
        {
            this.container = container;
            this.model = container.Get<IModelFacade>();
            this.viewHandler = viewHandler;
        }

        private void calculate(Action<double> operation, string x)
        {
            var dx = 0d;
            try
            {
                dx = double.Parse(x);
                Error = "";
            }
            catch (FormatException)
            {
                Error = "Use numbers!";
            }
            catch (Exception ex)
            {
                Error = $"Unexpeced error: {ex.Message}";
            }
            if (Error == "")
            {
                operation(dx);
                MainView.UpdateView();
                if (viewHandler.IsReady(LogView))//if (LogView != null && ((Window)LogView).IsVisible)
                    LogView.UpdateView();

            }
            else
            {
                //if (ErrorView == null || !((Window)ErrorView).IsVisible)
                if(viewHandler.IsReady(ErrorView))
                    ErrorView = container.Get<IView>("CalcError"); //new Views.ErrorWindow<ICalcController>(model, this);
                ErrorView.UpdateView();
                //((Window)ErrorView).ShowDialog();
                viewHandler.ShowDialog(ErrorView);
            }
        }

        public void MinusAction(string x) => calculate(model.Minus, x);
        public void PlusAction(string x) => calculate(model.Plus, x);

        public void ShowLogAction()
        {
            //if (LogView == null || !(.IsVisible)
            if (viewHandler.IsReady(LogView))
                LogView = container.Get<IView>("Log");
            viewHandler.Show(LogView);
            LogView.UpdateView();
        }

        public void ExitAppAction()
        {
            viewHandler.ExitApplication();
        }
    }
}
