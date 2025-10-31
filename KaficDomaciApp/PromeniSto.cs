using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace KaficDomaciApp
{
    public partial class PromeniSto:INotifyPropertyChanged
    {
        private Sto selectedTable;

        public Sto SelectedTable
        {
            get => selectedTable;
            set
            {
                selectedTable = value; Javi(nameof(SelectedTable));
            }
        }

        public Color Boja
        {
            get => SelectedTable?.Boja ?? Colors.White;
            set
            {
                if (SelectedTable != null)
                {
                    SelectedTable.Boja = value; Javi(nameof(Boja));
                }
            }
        }

        public int Broj
        {
            get => SelectedTable?.Broj ?? 0;
            set
            {
                if (SelectedTable != null)
                {
                    SelectedTable.Broj = value;
                    Javi(nameof(Broj));
                }
            }
        }

        public double X
        {
            get => SelectedTable?.X ?? 0.0;
            set
            {
                if (SelectedTable != null)
                {
                    SelectedTable.X = value;
                    Javi(nameof(X));
                    Javi(nameof(Lokacija));
                }
            }
        }

        public double Y
        {
            get => SelectedTable?.Y ?? 0.0;
            set
            {
                if (SelectedTable != null)
                {
                    SelectedTable.Y = value;
                    Javi(nameof(Y));
                    Javi(nameof(Lokacija));
                }
            }
        }

        public double W
        {
            get => SelectedTable?.W ?? 0.0;
            set
            {
                if (SelectedTable != null)
                {
                    SelectedTable.W = value;
                    Javi(nameof(W));
                }
            }
        }

        public double H
        {
            get => SelectedTable?.H ?? 0.0;
            set
            {
                if (SelectedTable != null)
                {
                    SelectedTable.H = value;
                    Javi(nameof(H));
                }
            }
        }

        public Point Lokacija => new Point(X, Y);

        public event PropertyChangedEventHandler? PropertyChanged;
        private void Javi(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
