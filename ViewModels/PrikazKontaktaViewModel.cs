using Imenik.Model;
using Imenik.PomocneKlase;
using System.Windows;
using System.Windows.Input;

namespace Imenik.ViewModels
{
    public class PrikazKontaktaViewModel : ObservableObject
    {
        public Kontakt IzabraniKontakt { get; set; }

        private readonly IImenikViewModel _sender;

        public ICommand IzmeniKomanda { get; private set; }
        public ICommand OmiljeniKomanda { get; private set; }
        public ICommand ObrisiKomanda { get; private set; }


        public PrikazKontaktaViewModel(Kontakt izabraniKontakt, IImenikViewModel sender)
        {
            IzabraniKontakt = izabraniKontakt;

            _sender = sender;

            OmiljeniKomanda = new RelayCommand(Omiljeni); // ove komande su povezane sa sa korisnickim interfejsom
            IzmeniKomanda = new RelayCommand(Izmeni);
            ObrisiKomanda = new RelayCommand(Obrisi);
        }

        // Biramo da li je izabrani kontakt omiljen ili ne 
        private void Omiljeni()
        {
            IzabraniKontakt.JeOmiljeni = !IzabraniKontakt.JeOmiljeni;
            _sender.SacuvajKontakt(IzabraniKontakt);
        }

        // Metoda koja poziva metodu u ImenikViewModel za postavljanje view-a IzmeniKontaktView 
        private void Izmeni()
        {
            _sender.IzmeniKontakt();
        }
        // Uput i brisanje izabranog kontakta
        // poziva metodu iz ImenikViewModel-a
        private void Obrisi()
        {
            if (IzabraniKontakt != null)
            {
                MessageBoxResult result = MessageBox.Show($"Da li ste sigurni da zelite obrisati kontakt {IzabraniKontakt.PunoIme}?",
                    "Potvrdite", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    _sender.ObrisiIzabraniKontakt();
                }
            }


        }
    }
}
