using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPINT_Wk3_Observer.Model {
    public abstract class Observable<T> : IObservable<T>, IDisposable {

        private List<IObserver<T>> observers;

        public Observable () {
            observers=new List<IObserver<T>> ();
        }

        private struct Unsubscriber : IDisposable {
            private Action unsubscribe;
            public Unsubscriber (Action unsubscribe) { this.unsubscribe=unsubscribe; }
            public void Dispose () { unsubscribe (); }
        }
        public IDisposable Subscribe (IObserver<T> observer) {
            observers.Add (observer);
            return new Unsubscriber (() => observers.Remove (observer));
        }

        protected void Notify (T subject) {
            foreach (var observer in observers) {
                observer.OnNext (subject);
            }
        }
        public void Dispose () {
            foreach (var observer in observers) {
                observer.OnCompleted ();
            }
        }
    }
}
