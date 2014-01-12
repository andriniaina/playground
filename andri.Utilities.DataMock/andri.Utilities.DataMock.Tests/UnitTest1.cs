using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using andri.TestUtilities.DataMocks;
using System.Collections.Generic;

namespace andri.Utilities.DataMock.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var reader = new MockDataReader(
                new string[] { "id", "col2", "col3", "col4" },
                new List<object[]>()
                {
                    new object[]{1,"abc",3,4},
                    new object[]{2,"2",null,4},
                    new object[]{3,"x2",3,4},
                    new object[]{4,"fdsf2",3,null},
                    new object[]{5,"2zefh",3,4},
                    new object[]{6,"2rtryu",null,4},
                    new object[]{7,"2'(udth,",3,null},
                }
                );

            while (reader.Read())
            {
                var id = reader["id"];
                var col2 = reader["col2"];
                var col3 = reader["col3"];
                var col4 = reader["col4"];
            }
        }
    }
}
