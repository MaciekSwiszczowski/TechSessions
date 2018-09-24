using System.Collections.Generic;

namespace NUnitAndMoq
{
    public interface IData<T>
    {
        List<T> Values { get; }
    }
    
    
    public class IntData : IData<int>
    {
        public IntData()
        {
            Values = new List<int> { -1, 0, 1};
        }
        public List<int> Values { get; }
    }

    public class DoubleData : IData<double>
    {
        public DoubleData()
        {
            Values = new List<double> { -2.0, 0.0, 3.0};
        }
        public List<double> Values { get; }
    }
}