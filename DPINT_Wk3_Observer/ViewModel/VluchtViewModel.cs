﻿using DPINT_Wk3_Observer.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPINT_Wk3_Observer.ViewModel
{
    public class VluchtViewModel : ViewModelBase, IObserver<Vlucht>
    {
        private string _vertrokkenVanuit;
        public string VertrokkenVanuit
        {
            get { return _vertrokkenVanuit; }
            set { _vertrokkenVanuit = value; RaisePropertyChanged ("VertrokkenVanuit"); }
        }

        private int _aantalKoffers;
        public int AantalKoffers
        {
            get { return _aantalKoffers; }
            set { _aantalKoffers = value; RaisePropertyChanged ("AantalKoffers"); }
        }

        public VluchtViewModel(Vlucht vlucht)
        {
            Update (vlucht);
            vlucht.Subscribe (this);
        }

        public void Update(Vlucht vlucht)
        {
            // TODO: Dit mag natuurlijk naar de OnNext methode toe.
            VertrokkenVanuit = vlucht.VertrokkenVanuit;
            AantalKoffers = vlucht.AantalKoffers;
        }

        public void OnNext (Vlucht value) => Update (value);
        public void OnError (Exception error) => throw new NotImplementedException ();
        public void OnCompleted () => throw new NotImplementedException ();
    }
}
