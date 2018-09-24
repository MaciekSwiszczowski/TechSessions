using System.Threading;
using System.Windows;
using NUnit.Framework;

namespace NUnitAndMoq
{
    [TestFixture]
    public class Untestable
    {

        [Test, Apartment(ApartmentState.STA)]
        public void Trouble()
        {
            MessageBox.Show("Test", "Test");
            
            Application.Current.Dispatcher.Invoke(() => {});
        }
        
    }
}
