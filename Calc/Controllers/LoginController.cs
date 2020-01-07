using Calc.Models;
using Ninject;

namespace Calc.Controllers
{
    public class LoginController : ILoginController
    {
        IKernel container;
        IModelFacade model;
        IViewHandler viewHandler;
        
        public LoginController(IKernel container)
        {
            this.container = container;
            model = container.Get<IModelFacade>();
        }

        public IView LoginView { get; set; }
        public IView MainView { get; set; }
        public IView ErrorView { get; set; }

        public string Error { get; private set; }

        public void LogInAction(string user, string pwd)
        {
            if(model.Login(user, pwd))
            {
                //((Window)LoginView).Hide();
                viewHandler.Hide(LoginView);
                if (MainView == null) MainView = container.Get<IView>("Main");
                //((Window)MainView).Show();
                viewHandler.Show(LoginView);
                Error = "";
            }
            else
            {
                //if (ErrorView == null || !((Window)ErrorView).IsVisible)
                if (viewHandler.IsReady(ErrorView))
                    ErrorView = container.Get<IView>("LoginError");
                Error = "Invalid login";
                ErrorView.UpdateView();
                viewHandler.ShowDialog(ErrorView);
            }
        }
    }
}
