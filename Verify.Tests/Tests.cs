using System;
using System.Threading.Tasks;
using NUnit.Framework;
using VerifyNUnit;

namespace Verify.Tests;

public sealed class Tests
{
   
    [Test]
    public Task Test1()
    {
        var person = ClassBeingTested.FindPerson();
        return Verifier.Verify(person);
    }

    [Test]
    public Task Test2()
    {
        var values = new[] {"1", "2", "3", "4"};
        return Verifier.Verify(values);
    }
}

public static class ClassBeingTested
{
    public static Person FindPerson()
    {
        return new Person
        {
            Id = new Guid("ebced679-45d3-4653-8791-3d969c4a986c"),
            GivenNames = "John",
            FamilyName = "Smith",
            Spouse = "Jill",
        };
    }
}

public sealed class Person
{
    public Guid Id { get; set; }
    public string GivenNames { get; set; }
    public string FamilyName { get; set; }
    public string Spouse { get; set; }
}