using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Boost.Common.Classes
{
    public class Workout 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public List<Exercise> ExerciseList { get; set; } = new List<Exercise>();
    }

    public class Exercise : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public List<string> UnitOfMeaasurements { get; set; }
        public List<string> MuscleGroup { get; set; }
        public bool IsWeightliting { get; set; } = false;
        private bool _use1RM;
        public bool use1RM
        {
            get { return _use1RM; }
            set
            {
                if (_use1RM != value)
                {
                    _use1RM = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsCalisthenics { get; set; } = false;
        public bool IsRest { get; set; } = false;
        public bool IsOther { get; set; } = false;
        public string Description { get; set; }
        public List<Sets> Sets { get; set; } = new List<Sets>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public class Sets
    {
        public int Id { get; set; }
        public int Weight { get; set; }
        public int Reps { get; set; }
    }
}
