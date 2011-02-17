using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace soapbar
{
    public interface Service
    {
        void showInput(PropertyGrid grid);
        bool doQuery(PropertyGrid grid);
    };
}
