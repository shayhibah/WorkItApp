using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkItApp.Controller;

namespace WorkItApp.View
{
    public interface IView
    {
        void setController(IController controller);
    }
}
