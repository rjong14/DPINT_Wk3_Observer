using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPINT_Wk3_Observer.Model
{
    public class Aankomsthal : IObserver<Baggageband>
    {
        // TODO: Hier een ObservableCollection van maken, dan weten we wanneer er vluchten bij de wachtrij bij komen of afgaan.
        public ObservableCollection<Vlucht> WachtendeVluchten { get; private set; }
        public List<Baggageband> Baggagebanden { get; private set; }

        public Aankomsthal()
        {
            WachtendeVluchten = new ObservableCollection<Vlucht>();
            Baggagebanden = new List<Baggageband>();

            // TODO: Als baggageband Observable is, gaan we subscriben op band 1 zodat we updates binnenkrijgen.
            Baggagebanden.Add(new Baggageband("Band 1", 30));
            Baggagebanden [0].Subscribe (this);
            // TODO: Als baggageband Observable is, gaan we subscriben op band 2 zodat we updates binnenkrijgen.
            Baggagebanden.Add(new Baggageband("Band 2", 60));
            Baggagebanden [1].Subscribe (this);
            // TODO: Als baggageband Observable is, gaan we subscriben op band 3 zodat we updates binnenkrijgen.
            Baggagebanden.Add(new Baggageband("Band 3", 90));
            Baggagebanden [2].Subscribe (this);

        }

        public void NieuweInkomendeVlucht(string vertrokkenVanuit, int aantalKoffers)
        {
            // TODO: Het proces moet straks automatisch gaan, dus als er lege banden zijn moet de vlucht niet in de wachtrij.
            // Dan moet de vlucht meteen naar die band.

            if (Baggagebanden.Any (bb => bb.AantalKoffers==0)&& WachtendeVluchten.Count() < 1) {
                Baggageband legeBand = Baggagebanden.FirstOrDefault (b => b.AantalKoffers==0);
                legeBand.HandelNieuweVluchtAf (new Vlucht (vertrokkenVanuit, aantalKoffers));
            } else {
                WachtendeVluchten.Add (new Vlucht (vertrokkenVanuit, aantalKoffers));
            }
        }

        public void NaarBand (int i) {
            Baggageband legeBand = Baggagebanden[i];
            Vlucht volgendeVlucht = WachtendeVluchten.FirstOrDefault ();
            WachtendeVluchten.RemoveAt (0);

            legeBand.HandelNieuweVluchtAf (volgendeVlucht);
        }

        public void WachtendeVluchtenNaarBand()
        {
            if (WachtendeVluchten.Any()) {
                for (int i = 0;i<3;i++) {
                    if (Baggagebanden [i].AantalKoffers<1) { NaarBand (i); }
                }
            }
        }

        public void OnNext (Baggageband value) => WachtendeVluchtenNaarBand ();
        public void OnError (Exception error) => throw new NotImplementedException ();
        public void OnCompleted () => throw new NotImplementedException ();
    }
}
