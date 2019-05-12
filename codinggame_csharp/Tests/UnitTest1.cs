using System;
using Xunit;

namespace Tests
{
    public class ArrayToolsTests
    {
        [Fact]
        public void Join()
        {
            var s = ArrayTools.join(new int[] { -1, 2, 3, 4, 5, 6 });
            Assert.Equal("-1,2,3,4,5,6", s);
        }
        [Fact(Timeout = 100)]
        public void FindIndexOf()
        {
            var s = ArrayTools.FindIndexOf(new int[] { -1, 2, 3, 4, 5, 6 }, 2);
            Assert.Equal(1, s);
            
            s = ArrayTools.FindIndexOf(new int[] { -1, 2, 3, 4, 5, 6 }, 0);
            Assert.Equal(0.5m, s);
            
            s = ArrayTools.FindIndexOf(new int[] { -1, 2, 3, 4, 5, 6 }, 100);
            Assert.Equal(5.5m, s);
            
            s = ArrayTools.FindIndexOf(new int[] { -1, 2, 3, 4, 5, 6 }, -100);
            Assert.Equal(-.5m, s);
        }
        [Fact(Timeout = 100)]
        public void FindLowerBound_middle()
        {
            var s = ArrayTools.FindIndexGreaterOrEqualThan(new int[] { -1, 2, 3, 4, 5, 6 }, 2);
            Assert.Equal(1, s);
        }
        [Fact(Timeout = 100)]
        public void FindUpperBound_notinarray()
        {
            var s = ArrayTools.FindIndexLesserOrEqualThan(new int[] { -1, 2, 4, 5, 6 }, 3);
            Assert.Equal(1, s);
        }
        [Fact(Timeout = 100)]
        public void FindLowerBound_notinarray()
        {
            var s = ArrayTools.FindIndexGreaterOrEqualThan(new int[] { -1, 2, 4, 5, 6 }, 3);
            Assert.Equal(2, s);
        }
        [Fact(Timeout = 100)]
        public void FindUpperBound_middle()
        {
            var s = ArrayTools.FindIndexLesserOrEqualThan(new int[] { -1, 2, 3, 4, 5, 6 }, 2);
            Assert.Equal(1, s);
        }
        [Fact(Timeout = 100)]
        public void FindLowerBound_allabove()
        {
            var s = ArrayTools.FindIndexGreaterOrEqualThan(new int[] { 1, 2, 3, 4, 5, 6 }, 0);
            Assert.Equal(0, s);
            s = ArrayTools.FindIndexGreaterOrEqualThan(new int[] { 1, 2, 3, 4, 5, 6 }, -1);
            Assert.Equal(0, s);
        }
        [Fact(Timeout = 100)]
        public void FindLowerBound_allbelow()
        {
            var s = ArrayTools.FindIndexGreaterOrEqualThan(new int[] { 1, 2, 3, 4, 5, 6 }, 100);
            Assert.Equal(-1, s);
        }
        [Fact(Timeout = 100)]
        public void FindUpperBound_allbelow()
        {
            var s = ArrayTools.FindIndexLesserOrEqualThan(new int[] { 0, 1, 2, 3, 4, 5 }, 5);
            Assert.Equal(5, s);
            s = ArrayTools.FindIndexLesserOrEqualThan(new int[] { 0, 1, 2, 3, 4, 5 }, 100);
            Assert.Equal(5, s);
            s = ArrayTools.FindIndexLesserOrEqualThan(new int[] { 0, 1, 2, 3, 4, 5 }, -1);
            Assert.Equal(-1, s);
            s = ArrayTools.FindIndexLesserOrEqualThan(new int[] { -100, 0, 1, 2, 3, 4, 5 }, -10000);
            Assert.Equal(-1, s);
        }
    }
}
