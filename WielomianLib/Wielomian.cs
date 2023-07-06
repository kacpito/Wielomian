using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace MyMath
{
    public sealed class Wielomian
    {
        private int[] _wspolczynniki;
        public int Stopien { get => _wspolczynniki.Length - 1; }

        public Wielomian() 
        {
            _wspolczynniki = new int[1] {0};
        }

        public Wielomian(params int[] wspolczynniki)
        {
            if (wspolczynniki.Length == 0) throw new ArgumentException("wielomian nie może być pusty");
            if (wspolczynniki is null) throw new NullReferenceException();

            List<int> list = wspolczynniki.ToList();

            while (list.Count > 1 && list[0] == 0)
            {
                list.RemoveAt(0);
            }

            _wspolczynniki = list.ToArray();
        }

        public override string ToString()
        {
            if (_wspolczynniki.Length == 1)
                return _wspolczynniki[Stopien].ToString();

            int i = 0;
            string result = $"{_wspolczynniki[i]}x^{Stopien}";
            
            for (i = 1; i < _wspolczynniki.Length; i++)
            {
                string sign = _wspolczynniki[i] < 0 ? "-" : "+";

                if (i == _wspolczynniki.Length - 1)
                    result += $" {sign} {Math.Abs(_wspolczynniki[i])}";
                else
                    result += $" {sign} {Math.Abs(_wspolczynniki[i])}x^{Stopien - i}";
            }

            return result;
        }
    }
}