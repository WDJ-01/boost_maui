using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Boost.Common.Classes
{
    public class CalenderDay : INotifyPropertyChanged
    {
        public DateTime date {  get; set; }
        public int dayDate { get; set; }
        //public string selectedColor { get; set; }
        //public string selectedTextColor { get; set; }
        public string initial {  get; set; }
        private Color _selectedColor = Colors.Transparent;
        public Color selectedColor
        {
            get => _selectedColor;
            set
            {
                _selectedColor = value;
                OnPropertyChanged();
            }
        }

        private Color _selectedTextColor = Colors.Gray;
        public Color selectedTextColor
        {
            get => _selectedTextColor;
            set
            {
                _selectedTextColor = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
