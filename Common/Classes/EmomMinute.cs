using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Boost.Common.Classes
{
    public class EmomMinute 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Exercise> Exercises { get; set; } = new ObservableCollection<Exercise>();
    }
}
