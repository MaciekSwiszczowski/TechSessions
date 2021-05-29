using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BenchmarkDotNet.Attributes;

namespace Benchmarks.MulticastDelegate
{
    [MemoryDiagnoser]
    public class EventVsCustomImplementation
    {
        private ClassWithEvent _classWithEvent;
        private List<SimpleClass> _simpleClasses;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _classWithEvent = new ClassWithEvent();
            _simpleClasses =
                new List<SimpleClass>(Enumerable.Range(0, 100_000).Select(_ => new SimpleClass(_classWithEvent)));
        }

        [Benchmark]
        public int Invoke()
        {
            _classWithEvent.NotifyValueChanged();
            return SimpleClass.counter;
        }
    }

    public class ClassWithEvent
    {
        public event Action ValueChanged;

        public virtual void NotifyValueChanged() => ValueChanged?.Invoke();
    }
    
    public class SimpleClass
    {
        private readonly ClassWithEvent _classWithEvent;
        public static int counter;

        public SimpleClass(ClassWithEvent classWithEvent)
        {
            _classWithEvent = classWithEvent;
            classWithEvent.ValueChanged += OnValueChanged;
        }

        //private void OnValueChanged() => Thread.Sleep(2);
        private void OnValueChanged()
        {
            //Thread.Sleep(1);
            //Thread.SpinWait(1000);
            Interlocked.Increment(ref counter);
            _classWithEvent.ValueChanged -= OnValueChanged;
        }
    }
}
