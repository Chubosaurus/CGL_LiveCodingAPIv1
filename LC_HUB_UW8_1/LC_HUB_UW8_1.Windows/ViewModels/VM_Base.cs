using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;

using CGL;
using CGL.LC_Models;


namespace LC_HUB_UW8_1.ViewModels
{
    /// <summary>
    /// A basic ViewModel with a DataSource.
    /// </summary>
    /// <typeparam name="T">The Type of DataSource for the basic VM.</typeparam>
    public class VM_Base<T>
    {
        private ObservableCollection<T> _datasource;

        public ObservableCollection<T> DataSource
        {
            get { return _datasource; }
            set
            {
                if (_datasource != value)
                {
                    _datasource = value;
                }
            }
        }
    }
}
