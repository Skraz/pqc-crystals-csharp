using System;

namespace crystals_csharp_test.src.util.test;

public interface ITestResult
{
    bool IsSuccessful();

    Exception GetException();

    string ToString();
}
