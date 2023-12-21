using Map.MapGeneration;
using NUnit.Framework;

namespace Tests.EditMode
{
    public class EditModeTests
    {
        [Test]
        public void TestMapGeneration()
        {
            //Arrange
            int expectedHeight = 5;
            int expectedWidth = 5;
            
            //Act
            DataMap myNewDataMap = new DataMap();
            
            myNewDataMap.InitializeTiles(5,5);
            
            //Assert
            Assert.AreEqual(expectedWidth, myNewDataMap.Width);
            Assert.AreEqual(expectedHeight, myNewDataMap.Height);
        }
    }
}
