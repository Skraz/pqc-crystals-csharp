using System;

namespace crystals_csharp_test.src.util.test
{
    public class TestFailedException
        : Exception
    {
        private readonly ITestResult m_result;

        public TestFailedException(ITestResult result)
            : base()
        {
            m_result = result;
        }

        public ITestResult Result
        {
            get { return m_result; }
        }
    }
}
