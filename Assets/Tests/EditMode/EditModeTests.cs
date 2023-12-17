using System;
using System.Collections.Generic;
using Map.MapGeneration;
using Map.MapGeneration.Entities;
using Map.MapGeneration.Entities.Concrete;
using Map.MapGeneration.Strategy;
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
            DataMap myNewDataMap = MapGenerator.GenerateDataMap(5, 5, new RandomMapGenerationStrategy(), new List<IEntity> { new LWallEntity(), new LWallEntity(),new LWallEntity(),new LWallEntity(),new LWallEntity(),new LWallEntity(),new LWallEntity() });
            //Assert
            Assert.AreEqual(expectedWidth, myNewDataMap.Width);
            Assert.AreEqual(expectedHeight, myNewDataMap.Height);
        
        }
    }
}
