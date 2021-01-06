using System;
using System.Threading.Tasks;
using NUnit.Framework;
using VerifyNUnit;

namespace Verify.Tests
{
    public class Tests
    {
   
        [Test]
        public Task Test1()
        {
            var person = ClassBeingTested.FindPerson();
            return Verifier.Verify(person);
        }
    }

    public static class ClassBeingTested
    {
        public static Person FindPerson()
        {
            return new Person()
            {
                Id = new Guid("ebced679-45d3-4653-8791-3d969c4a986c"),
                GivenNames = "John",
                FamilyName = "Smith",
                Spouse = "Jill",
            };
        }
    }

    public class Person
    {
        public Guid Id { get; set; }
        public string GivenNames { get; set; }
        public string FamilyName { get; set; }
        public string Spouse { get; set; }
    }
}