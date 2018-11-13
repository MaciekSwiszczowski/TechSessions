using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ProfileOptimization
{

    public partial class MainWindow 
    {
        const int Size = 200;

        public MainWindow()
        {

            var array = new int[Size][];
            for (var i = 0; i < Size; i++)
            {
                array[i] = new int[Size];
                array[i][0] = i;
            }

            array[0] = Enumerable.Range(0, Size).ToArray();

            for (var i = 1; i < Size; i++)
                for (var j = 1; j < Size; j++)
                {
                    var min = Min(Concat(array, i, j));
                    array[i][j] = min;
                }

            DataContext = array;

            InitializeComponent();


            ContentRendered += (sender, args) =>
            {
                Console.WriteLine($@"Application start time: {DateTime.Now - App.Timer}");

                MessageBox.Show($@"Application start time: {DateTime.Now - App.Timer}");

                // Close();
            };
        }



        private static IEnumerable<int> Concat(int[][] array, int i, int j)
        {
            return array[i].Take(j).Concat(array.Take(i).Select(a => a[j]));
        }

        private static int Min(IEnumerable<int> values)
        {
            var copy = values.Distinct().ToList();
            copy.Sort();

            var toCompare = Enumerable.Range(0, copy.Count).ToArray();

            for (var i = 0; i < copy.Count; i++)
            {
                if (copy[i] != toCompare[i])
                    return i;
            }

            return copy.Count;
        }
    }
}
