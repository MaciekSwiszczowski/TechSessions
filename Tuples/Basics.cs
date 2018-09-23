using System;
using System.IO;
using System.Runtime.Serialization.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace Tuples
{
    // C# 7 - Jon Skeet
    // https://www.youtube.com/watch?v=yj9GKRxFxVU&t=2200s

    [TestClass]
    public class Basics
    {

        [TestMethod]
        public void TestMethod1()
        {
            var tuple1 = (1, 2);
            Console.WriteLine(tuple1.Item1);
            Console.WriteLine(tuple1.Item2);

            var tuple2 = new ValueTuple<int, long>(1, 2); // <== long!
            Console.WriteLine(tuple2.Item1);
            Console.WriteLine(tuple2.Item2);

            Assert.AreEqual(tuple1, tuple2);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var tuple1 = (a: 1, b: 2);
            Console.WriteLine(tuple1.a);
            Console.WriteLine(tuple1.b);
            Console.WriteLine(tuple1);

            (long a, int b) tuple2 = (1, 2);
            Console.WriteLine(tuple2.a);
            Console.WriteLine(tuple2.b);
            Console.WriteLine(tuple2);

            Assert.AreEqual(tuple1, tuple2);
            
            //Assert.That(tuple1 == tuple2); // a good reason not to expose tuples outside your code

            //tuple1.ShouldBeSameAs(tuple2);
            //Assert.IsTrue(tuple1.Equals(tuple2));
            //tuple1.ShouldBe(tuple2); // this doesn't work too (in C# 7.2)
        }

        [TestMethod]
        public void SerializingWithMicrosoftJsonSerializer()
        {
            var tuple = (a: 1, b: 2);
            Console.WriteLine(tuple);

            using (var stream1 = new MemoryStream())
            {
                var ser = new DataContractJsonSerializer(tuple.GetType());
                ser.WriteObject(stream1, tuple);

                var t = ser.ReadObject(stream1);
                Console.WriteLine(t);
            }
        }

        [TestMethod]
        public void SerializingWithNewtonsoftJsonSerializer()
        {
            var tuple = (a: 1, b: 2);
            Console.WriteLine(tuple);

       
            var json = JsonConvert.SerializeObject(tuple, Formatting.Indented);
            Console.WriteLine($"serialized: {json}");

            var @object = JsonConvert.DeserializeObject(json);
            Console.WriteLine($"deserialized {@object}");
            Console.WriteLine($"deserialized type: {@object.GetType().Name}");
        }

        [TestMethod]
        public void Names()
        {
            var tuple1 = (a: 1, b: 2);
            var tuple2 = (min: 101, max: 202);

            tuple1 = tuple2;
            Console.WriteLine(tuple1.a);
        }

        [TestMethod]
        public void MinMax()
        {
            var values = new int[] { 1, 4, -6, 19, 4, -10};
            var minMax = MinMax(values);
            var (min, max) = MinMax(values);
            Console.WriteLine(minMax);
        }

        private static (int min, int max) MinMax(int[] values)
        {
            if (values == null || values.Length == 0)
                throw new ArgumentException();

            (int min, int max) minMax = (values[0], values[0]);

            foreach (var value in values)
            {
                minMax.min = Math.Min(minMax.min, value);
                minMax.max = Math.Max(minMax.max, value);
            }

            return minMax;
        }
    }
}
