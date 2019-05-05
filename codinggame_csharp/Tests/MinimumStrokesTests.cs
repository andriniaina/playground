using System;
using Xunit;

namespace Tests
{
    public class MinimumStrokesTests
    {
        [Fact]
        public void test()
        {
            MinimumStrokes.strokesRequired(new System.Collections.Generic.List<string>{
                "aabba",
                "aabba",
                "aaaca",
            });
        }
    }
}