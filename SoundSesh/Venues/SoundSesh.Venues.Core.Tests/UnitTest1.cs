using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace SoundSesh.Venues.Core.Tests
{
    [TestClass]
    public class StudioUnitTests
    {


        //private IStudio _studio;

        [TestInitialize]
        public void Initialize()
        {
            //_studio = new Studio();
        }

        [TestMethod]
        public async Task Create_Success()
        {
            //Arrange
           
                //SETUP
                //var options = SqliteInMemory.CreateOptions<StudioContext>();
                //using (var context = new StudioContext(options))
                //{
                //    context.Database.EnsureCreated();
                //    var service = new CreateStudio(context);

                //    //ATTEMPT
                //    var dto = new CreateTodoDto
                //    {
                //        Name = "Test",
                //        Difficulty = 3
                //    };
                //    var result = service.BizAction(dto);
                //    context.SaveChanges();

                //    //VERIFY
                //    service.HasErrors.ShouldBeFalse(service.GetAllErrors());
                //    context.TodoItems.Single().Name.ShouldEqual("Test");
                //}


                /*
                 * 
                 * 
                 * * var studio = new Create.StudioDTO()
            {
                State = "WA"
            };

            _mockCreateStudio.Setup(f => f.BizActionAsync(studio)).ReturnsAsync(studio);

            //Act
            var actual = await _studio.BizActionAsync(studio);

            //assert
            Assert.AreEqual(actual., 1);
            */

        }

        [TestMethod]
        public async Task Create_Failure_BusinessRuleViolation()
        {
            ////Arrange
            //_mockCreateStudio.Setup(f => f.BizActionAsync(new Entities.DTOs.Create.StudioDTO
            //{

            //})).ReturnsAsync(new Entities.DTOs.Create.StudioDTO());

            ////Act
            //var actual = await _studio.BizActionAsync(new Entities.DTOs.Create.StudioDTO
            //{
                
            //});

            ////assert
            //Assert.AreNotEqual(actual.Id, 1);
        }


    }
}
