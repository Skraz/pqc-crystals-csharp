using System;

using NUnit.Framework;

/*
 Basic test interface
 */
namespace crystals_csharp_test.src.util.test
{
    public interface ITest
    {
        string Name { get; }

        [Test]
        ITestResult Perform();
    }
}
