using System.Runtime.CompilerServices;
using DiffEngine;
using NUnit.Framework;
using VerifyTests;

namespace Verify.Tests;


[SetUpFixture]
public class TestSetup
{
    [ModuleInitializer]
    public static void Init()
    {
        VerifyXaml.Initialize();
        DiffTools.UseOrder(DiffTool.VisualStudio, DiffTool.VisualStudioCode, DiffTool.Rider);
    }
}