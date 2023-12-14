using System;
using System.Collections.Generic;
using Map.MapGeneration;
using Map.MapGeneration.Entities;
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
            Map.Map myNewMap = MapGenerator.GenerateMap(5, 5, new RandomMapGenerationStrategy(), new List<IEntity> { new Wall(), new Wall(),new Wall(),new Wall(),new Wall(),new Wall(),new Wall() });
            //Assert
            Assert.AreEqual(expectedWidth, myNewMap.Width);
            Assert.AreEqual(expectedHeight, myNewMap.Height);
        
        }
    }
}
