using System;
using System.Collections;
using System.Linq;
using Xunit;

namespace Tests
{
    public class TriangleTests
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal((int)Triangle.TriangleType.INVALID_OR_NOTTRIANGLE, Triangle.triangle(100,90,200));

            Assert.Equal((int)Triangle.TriangleType.EQUILATERAL, Triangle.triangle(3, 3, 3));
            Assert.Equal((int)Triangle.TriangleType.INVALID_OR_NOTTRIANGLE, Triangle.triangle(0,0,0));
            Assert.Equal((int)Triangle.TriangleType.INVALID_OR_NOTTRIANGLE, Triangle.triangle(-1,5,3));
            Assert.Equal((int)Triangle.TriangleType.INVALID_OR_NOTTRIANGLE, Triangle.triangle(3,-1,5));
            Assert.Equal((int)Triangle.TriangleType.INVALID_OR_NOTTRIANGLE, Triangle.triangle(3,5,-1));
            Assert.Equal((int)Triangle.TriangleType.SIMPLE, Triangle.triangle(3,5,4));
            Assert.Equal((int)Triangle.TriangleType.SIMPLE, Triangle.triangle(3,4,5));
            Assert.Equal((int)Triangle.TriangleType.SIMPLE, Triangle.triangle(5,4,3));
            Assert.Equal((int)Triangle.TriangleType.INVALID_OR_NOTTRIANGLE, Triangle.triangle(7,4,3));
        }
    }
}