using System;
using BasicBodyMassIndexCalculator.Common.Interfaces;

namespace BasicBodyMassIndexCalculator.Common.Classes
{
    public class Person : IPerson
    {
        private string _name { get; set; }
        public string Name
        {
            get { return _name;  }
            set { _name = value; }
        }

        private int _weight { get; set; }
        public int Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        private double _length { get; set; }
        public double Length
        {
            get { return _length; }
            set { _length = value; }
        }
    }
}
