using System;

namespace RefugeAnimauxWPF.coucheMetier
{
    public class Vaccination
    {
        private DateTime date_vaccination;
        private Animal2? animal;
        private Vaccin? vaccin;

        public Vaccination() { }

        public Vaccination(Vaccination vaccination)
        {
            this.date_vaccination = vaccination.date_vaccination;
            this.animal = vaccination.animal != null ? new Animal2(vaccination.animal) : null;
            this.vaccin = vaccination.vaccin != null ? new Vaccin(vaccination.vaccin) : null;
        }

        public Vaccination(DateTime date_vaccination, Animal2? animal, Vaccin? vaccin)
        {
            this.date_vaccination = date_vaccination;
            this.animal = animal;
            this.vaccin = vaccin;
        }

        public DateTime Date_vaccination { get => date_vaccination; set => date_vaccination = value; }
        public Animal2? Animal { get => animal; set => animal = value; }
        public Vaccin? Vaccin { get => vaccin; set => vaccin = value; }

        public override string ToString()
        {
            return $"Vaccination: {vaccin?.Nom} - {date_vaccination:dd/MM/yyyy}";
        }
    }
}
