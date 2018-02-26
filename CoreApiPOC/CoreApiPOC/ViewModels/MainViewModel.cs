using CoreApiPOC.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApiPOC.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {

        }

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is int)
            {
                var dataParameter = (int)navigationData;
                
            }

            return base.InitializeAsync(navigationData);
        }
    }
}
