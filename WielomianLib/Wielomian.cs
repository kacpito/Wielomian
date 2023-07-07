using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Xml;
using static System.Formats.Asn1.AsnWriter;

namespace MyMath
{
    public sealed class Wielomian : IEquatable<Wielomian>
    {
        public int[] _wspolczynniki { get; }
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

        public bool Equals(object? other)
        {
            if (other is null) return false;
            if (other is not Wielomian) return false;

            return Equals((Wielomian)other);
        }

        public bool Equals(Wielomian? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (other.Stopien != Stopien)
                return false;
            else
            {
                for (int i = 0; i <= Stopien; i++)
                {
                    if (_wspolczynniki[i] != other._wspolczynniki[i]) 
                        return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Wielomian a, Wielomian b) => a.Equals(b);
        public static bool operator !=(Wielomian a, Wielomian b) => !(a == b);

        public static Wielomian operator +(Wielomian a, Wielomian b)
        {
            int stop;
            int[] tmp;

            if (a.Stopien < b.Stopien)
            {
                tmp = (int[])b._wspolczynniki.Clone();
                stop = a.Stopien;


                for (int i = stop, y = b.Stopien; i >= 0; i--, y--)
                {
                    tmp[y] += a._wspolczynniki[i];
                }
            }
            else
            {
                tmp = (int[])a._wspolczynniki.Clone();
                stop = b.Stopien;



                for (int i = stop, y = a.Stopien; i >= 0; i--, y--)
                {
                    tmp[y] += b._wspolczynniki[i];
                }
            }

            return new Wielomian(tmp);
        }

        //public static Wielomian operator -(Wielomian a)
        //{
        //    for (int i = 0; i <= a.Stopien; i++)
        //    {
        //        a._wspolczynniki[i] *= -1;
        //    }

        //    Console.WriteLine(new Wielomian(a._wspolczynniki));

        //    return new Wielomian(a._wspolczynniki);
        //}

        public static Wielomian operator -(Wielomian a, Wielomian b)
        {
            int stop;
            int[] tmp;

            if (a.Stopien < b.Stopien)
            {
                tmp = (int[])b._wspolczynniki.Clone();

                for (int i = 0; i <= b.Stopien; i++)
                {
                    tmp[i] *= -1;
                }

                stop = a.Stopien;

                for (int i = stop, y = b.Stopien; i >= 0; i--, y--)
                {
                    tmp[y] += a._wspolczynniki[i];
                }
            }
            else
            {
                tmp = (int[])a._wspolczynniki.Clone();
                stop = b.Stopien;

                for (int i = stop, y = a.Stopien; i >= 0; i--, y--)
                {
                    tmp[y] -= b._wspolczynniki[i];
                }
            }

            return new Wielomian(tmp);
        }
    }
}