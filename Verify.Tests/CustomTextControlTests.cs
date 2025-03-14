using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using NUnit.Framework;
using VerifyNUnit;

namespace Verify.Tests;

public class CustomTextControlTests
{
    [Test, Apartment(ApartmentState.STA)]
    public async Task SnapshotTest_json()
    {
        var control = new CustomTextControl();
        await Verifier.Verify(control);
    }
}

public class CustomTextControl : UserControl
{
    public CustomTextControl()
    {
        var textBox = new TextBox { Text = "Hello, WPF" };
        Content = textBox;
    }
}