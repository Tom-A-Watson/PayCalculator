using Microsoft.AspNetCore.Mvc;
using PayCalculatorAPI.Controllers;
using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;
using PayCalculatorLibrary.Services;

namespace PayCalculatorTest
{
    public class PermEmployeeControllerTest
    {
#nullable disable
        // Arrange
        PermanentEmployeeController controller;
        PermanentEmployeeRepository repository;
        PermanentPayCalculator calculator;
        PermanentEmployee employee;
        IActionResult result;
#nullable enable

        [SetUp]
        public void SetupTests()
        {
            // Arrange
            repository = new();
            calculator = new();
            controller = new PermanentEmployeeController(repository, calculator);
        }

        [Test]
        public void TestGetEmployeeWorks()
        {
            // Act
            result = controller.GetEmployee(1);
            // Assert
            Assert.That(result, Equals());
        }
    }
}
