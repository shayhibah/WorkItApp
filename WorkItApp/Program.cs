using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkItApp.Controller;
using WorkItApp.Model;
using WorkItApp.View;

namespace WorkItApp
{
    class Program
    {
        [STAThread]
        public static void Main()
        {
            IController controller = new MyController();
            IModel model = new MyModel(controller);
            controller.setModel(model);
            IView view = new MainWindow();
            view.setController(controller);
            controller.setView(view);
            App app = new App();
            app.Run((MainWindow)view);
     //       int x = 10;
        }
    }
}
