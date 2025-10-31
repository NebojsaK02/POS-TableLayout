using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace KaficDomaciApp
{
    public class Sto : INotifyPropertyChanged
    {
        private int broj;

        public int Broj
        {
            get => broj;
            set
            {
                broj = value;
                Javi(nameof(Broj));
            }
        }

        private double x, y, w, h;

        
        public double X
        {
            get => x;
            set { x = value; Javi(nameof(X)); Javi(nameof(Lokacija)); }
        }

        public double Y
        {
            get => y;
            set { y = value; Javi(nameof(Y)); Javi(nameof(Lokacija)); }
        }

        public Point Lokacija
        {
            get => new Point(x, y);
        }

        public double W
        {
            get => w;
            set { w = value; Javi(nameof(W)); }
        }

        public double H
        {
            get => h;
            set { h = value; Javi(nameof(H)); }
        }

        private Color boja;
        public Color Boja
        {
            get => boja;
            set { boja = value; Javi(nameof(Boja)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void Javi(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return string.Format("Broj {0}, ({1},{2}), Sirina: {3}, Visina: {4}, Boja: {5}", broj, x, y, w, h, boja);
        }
    }
}
