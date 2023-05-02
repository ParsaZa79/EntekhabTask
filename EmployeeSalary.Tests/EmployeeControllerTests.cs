using System.Text.Json;
using EntekhabSalary.WebApi.Contracts.Requests;
using EntekhabSalary.WebApi.Contracts.Responses;
using EntekhabSalary.WebApi.Controllers;
using EntekhabSalary.WebApi.Models;
using EntekhabSalary.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeSalary.Tests;

public class EmployeeControllerTests
{
    [Fact]
    public async Task AddEmployee_ValidRequest_AddsEmployeeSuccessfully()
    {
        // Arrange
        var employeeServiceMock = new Mock<IEmployeeService>();
        var employeeSalaryServiceMock = new Mock<IEmployeeSalaryService>();
        var addEmployeeRequest = new AddEmployeeRequest
        {
            Data = "Ali/Ahmadi/1200000/400000/350000/14010801",
            OverTimeCalculator = "CalculatorA"
        };
        employeeServiceMock.Setup(es => es.AddEmployee(addEmployeeRequest, "custom"))
            .ReturnsAsync(1);

        var controller = new EmployeeController(employeeServiceMock.Object, employeeSalaryServiceMock.Object);

        // Act
        var result = await controller.Add(addEmployeeRequest, "json");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ResponseBase<int?>>(okResult.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(1, response.Data);
    }

    [Fact]
    public async Task AddEmployee_InvalidRequest_ReturnsBadRequest()
    {
        // Arrange
        var employeeServiceMock = new Mock<IEmployeeService>();
        var employeeSalaryServiceMock = new Mock<IEmployeeSalaryService>();
        var addEmployeeRequest = new AddEmployeeRequest();
        var controller = new EmployeeController(employeeServiceMock.Object, employeeSalaryServiceMock.Object);
        controller.ModelState.AddModelError("Data", "Data is required");

        // Act
        var result = await controller.Add(addEmployeeRequest, "json");

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task AddEmployee_CustomRequest_AddsEmployeeSuccessfully()
    {
        // Arrange
        var employeeServiceMock = new Mock<IEmployeeService>();
        var employeeSalaryServiceMock = new Mock<IEmployeeSalaryService>();
        var addEmployeeRequest = new AddEmployeeRequest
        {
            Data = "Ali/Ahmadi/1200000/400000/350000/14010801",
            OverTimeCalculator = "CalculatorA"
        };
        employeeServiceMock.Setup(es => es.AddEmployee(addEmployeeRequest, "json"))
            .ThrowsAsync(new Exception("Error adding employee"));

        var controller = new EmployeeController(employeeServiceMock.Object, employeeSalaryServiceMock.Object);

        // Act
        var result = await controller.Add(addEmployeeRequest, "json");

        // Assert
        var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }
    
    [Fact]
    public async Task AddEmployee_JsonRequest_AddsEmployeeSuccessfully()
    {
        // Arrange
        var employeeServiceMock = new Mock<IEmployeeService>();
        var employeeSalaryServiceMock = new Mock<IEmployeeSalaryService>();
        var data = """
                    {
                        "FirstName": "Ali",
                        "LastName": "Ahmadi",
                        "BaseSalary":1200000,
                        "Allowance": 400000,
                        "Transportation": 350000,
                        "Date": "14010801"
                    }
                    """;
        var addEmployeeRequest = new AddEmployeeRequest
        {
            Data = data,
            OverTimeCalculator = "CalculatorA"
        };
        employeeServiceMock.Setup(es => es.AddEmployee(addEmployeeRequest, "json"))
            .ReturnsAsync(1);

        var controller = new EmployeeController(employeeServiceMock.Object, employeeSalaryServiceMock.Object);

        // Act
        var result = await controller.Add(addEmployeeRequest, "json");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ResponseBase<int?>>(okResult.Value);
        Assert.True(response.IsSuccess);
        Assert.NotNull(response.Data);
    }

    [Fact]
    public async Task AddEmployee_XmlRequest_AddsEmployeeSuccessfully()
    {
        // Arrange
        var employeeServiceMock = new Mock<IEmployeeService>();
        var employeeSalaryServiceMock = new Mock<IEmployeeSalaryService>();
        var addEmployeeRequest = new AddEmployeeRequest
        {
            Data = "<Employee><FirstName>Ali</FirstName><LastName>Ahmadi</LastName><BaseSalary>1200000</BaseSalary><Benefits>400000</Benefits><Overtime>350000</Overtime><EmployeeId>14010801</EmployeeId></Employee>",
            OverTimeCalculator = "CalculatorA"
        };
        employeeServiceMock.Setup(es => es.AddEmployee(addEmployeeRequest, "xml"))
            .ReturnsAsync(1);

        var controller = new EmployeeController(employeeServiceMock.Object, employeeSalaryServiceMock.Object);

        // Act
        var result = await controller.Add(addEmployeeRequest, "xml");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ResponseBase<int?>>(okResult.Value);
        Assert.True(response.IsSuccess);
        Assert.NotNull(response.Data);
    }

    [Fact]
    public async Task AddEmployee_CsRequest_AddsEmployeeSuccessfully()
    {
        // Arrange
        var employeeServiceMock = new Mock<IEmployeeService>();
        var employeeSalaryServiceMock = new Mock<IEmployeeSalaryService>();
        var addEmployeeRequest = new AddEmployeeRequest
        {
            Data = "Ali,Ahmadi,1200000,400000,350000,14010801",
            OverTimeCalculator = "CalculatorA"
        };
        employeeServiceMock.Setup(es => es.AddEmployee(addEmployeeRequest, "csv"))
            .ReturnsAsync(1);

        var controller = new EmployeeController(employeeServiceMock.Object, employeeSalaryServiceMock.Object);

        // Act
        var result = await controller.Add(addEmployeeRequest, "csv");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ResponseBase<int?>>(okResult.Value);
        Assert.True(response.IsSuccess);
        Assert.NotNull(response.Data);
    }

    
    [Fact]
    public async Task UpdateEmployee_InvalidRequest_ReturnsBadRequest()
    {
        // Arrange
        var employeeServiceMock = new Mock<IEmployeeService>();
        var employeeSalaryServiceMock = new Mock<IEmployeeSalaryService>();
        var updateEmployeeRequest = new UpdateEmployeeRequest();
        var controller = new EmployeeController(employeeServiceMock.Object, employeeSalaryServiceMock.Object);
        controller.ModelState.AddModelError("Employee", "Employee data is required");

        // Act
        var result = await controller.Update(updateEmployeeRequest);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
    
    [Fact]
    public async Task UpdateEmployee_EmployeeNotFound_ReturnsNotFound()
    {
        // Arrange
        var employeeServiceMock = new Mock<IEmployeeService>();
        var employeeSalaryServiceMock = new Mock<IEmployeeSalaryService>();
        var updateEmployeeRequest = new UpdateEmployeeRequest
        {
            Employee = new Employee
            {
                Id = 1,
                FirstName = "Jane",
                LastName = "Doe",
                BasicSalary = 2900000,
                Allowance = 30000,
                Transportation = 25000
            }
        };
        employeeServiceMock.Setup(es => es.UpdateEmployee(updateEmployeeRequest))
            .ReturnsAsync(false);

        var controller = new EmployeeController(employeeServiceMock.Object, employeeSalaryServiceMock.Object);

        // Act
        var result = await controller.Update(updateEmployeeRequest);

        // Assert
        var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task UpdateEmployee_ExceptionDuringUpdate_ReturnsInternalServerError()
    {
        // Arrange
        var employeeServiceMock = new Mock<IEmployeeService>();
        var employeeSalaryServiceMock = new Mock<IEmployeeSalaryService>();
        var updateEmployeeRequest = new UpdateEmployeeRequest
        {
            Employee = new Employee
            {
                Id = 1,
                FirstName = "Jane",
                LastName = "Doe",
                BasicSalary = 2900000,
                Allowance = 30000,
                Transportation = 25000
            }
        };
        employeeServiceMock.Setup(es => es.UpdateEmployee(updateEmployeeRequest))
            .ThrowsAsync(new Exception("Error updating employee"));

        var controller = new EmployeeController(employeeServiceMock.Object, employeeSalaryServiceMock.Object);

        // Act
        var result = await controller.Update(updateEmployeeRequest);

        // Assert
        var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }

    
    [Fact]
    public async Task UpdateEmployee_ValidRequest_UpdatesEmployeeSuccessfully()
    {
        // Arrange
        var employeeServiceMock = new Mock<IEmployeeService>();
        var employeeSalaryServiceMock = new Mock<IEmployeeSalaryService>();
        var updateEmployeeRequest = new UpdateEmployeeRequest
        {
            Employee = new Employee
            {
                Id = 1,
                FirstName = "Jane",
                LastName = "Doe",
                BasicSalary = 2900000,
                Allowance = 30000,
                Transportation = 25000
            }
        };
        employeeServiceMock.Setup(es => es.UpdateEmployee(updateEmployeeRequest))
            .ReturnsAsync(true);

        var controller = new EmployeeController(employeeServiceMock.Object, employeeSalaryServiceMock.Object);

        // Act
        var result = await controller.Update(updateEmployeeRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ResponseBase<bool>>(okResult.Value);
        Assert.True(response.IsSuccess);
        Assert.True(response.Data);
    }


}