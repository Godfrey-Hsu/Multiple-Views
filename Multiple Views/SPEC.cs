using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

//通知

namespace Multiple_Views
{
    public class SPEC : INotifyPropertyChanged
    {
        private string _Thresh, _spec2;
        public string Thresh
        {
            get { return _Thresh; }
            set
            {
                _Thresh = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Thresh"));
                }
            }
        }

        public string spec2
        {
            get { return _spec2; }
            set
            {
                _spec2 = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("spec2"));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
    
}
