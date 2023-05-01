using EntekhabSalary.WebApi.Contracts.Requests;
using EntekhabSalary.WebApi.Contracts.Responses;
using EntekhabSalary.WebApi.Models;
using EntekhabSalary.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EntekhabSalary.WebApi.Controllers;

[ApiController]
[Route("{dataType?}/api/[controller]/[action]")]
public class EmployeeController : ControllerBase
{

    private readonly IEmployeeService _employeeService;
    private readonly IEmployeeSalaryService _employeeSalaryService;

    public EmployeeController(IEmployeeService employeeService, IEmployeeSalaryService employeeSalaryService)
    {
        _employeeService = employeeService;
        _employeeSalaryService = employeeSalaryService;
    }

    /// <summary>
    /// Adds a new employee.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /json/api/employee/add
    ///     {
    ///         "data": "Ali/Ahmadi/1200000/400000/350000/14010801",
    ///         "overTimeCalculator": "CalculatorA"
    ///     }
    ///
    /// The dataType parameter in the route can be "json", "xml", "cs", or "custom".
    /// </remarks>
    /// <param name="addEmployeeRequest">The employee data to be added.</param>
    /// <param name="dataType">The data type.</param>
    /// <returns>The newly added employee's ID.</returns>
    /// <response code="201">Returns the newly added employee's ID.</response>
    /// <response code="400">If the request is invalid.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(AddEmployeeRequest addEmployeeRequest, [FromRoute] string dataType)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseBase<string>.CreateErrorResponse("Invalid or incomplete request data!"));

        try
        {
            var result = await _employeeService.AddEmployee(addEmployeeRequest, dataType);
            return result is not null
                ? Ok(ResponseBase<int?>.CreateSuccessResponse(result))
                : StatusCode(500,
                    ResponseBase<string>.CreateErrorResponse("Failed to add employee."));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseBase<string>.CreateErrorResponse($"Error occurred while adding employee: {e.Message}"));
        }
    }
    
    /// <summary>
    /// Updates an existing employee.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /api/employees
    ///     {
    ///         "id": 1,
    ///         "name": "Jane Doe",
    ///         "position": "Senior Software Engineer"
    ///     }
    /// </remarks>
    /// <param name="updateEmployeeRequest">The employee data to be updated.</param>
    /// <returns>True if the update was successful, false otherwise.</returns>
    /// <response code="200">If the update was successful.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="404">If the employee is not found.</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(UpdateEmployeeRequest updateEmployeeRequest, string dataType = "json")
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ResponseBase<string>.CreateErrorResponse("Invalid or incomplete request data!"));
        }
        
        try
        {
            var result = await _employeeService.UpdateEmployee(updateEmployeeRequest);
            return !result
                ? StatusCode(500,
                    ResponseBase<string>.CreateErrorResponse($"Employee with id {updateEmployeeRequest.Employee.Id} does not exist!"))
                : Ok(ResponseBase<bool>.CreateSuccessResponse(result));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseBase<string>.CreateErrorResponse($"Error occurred while updating the employee :{e.Message}!"));
        }
    }

    /// <summary>
    /// Deletes an employee by ID.
    /// </summary>
    /// <param name="id">The ID of the employee to be deleted.</param>
    /// <returns>True if the deletion was successful, false otherwise.</returns>
    /// <response code="204">If the deletion was successful.</response>
    /// <response code="404">If the employee is not found.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, string dataType = "json")
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ResponseBase<string>.CreateErrorResponse("Invalid or incomplete request data!"));
        }

        try
        {
            var result = await _employeeService.DeleteEmployee(id);

            return !result
                ? StatusCode(500,
                    ResponseBase<string>.CreateErrorResponse($"Employee with id {id} does not exist!"))
                : Ok(ResponseBase<bool>.CreateSuccessResponse(result));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseBase<string>.CreateErrorResponse($"Error occurred while deleting the employee: {e.Message}!"));
        }
    }

    /// <summary>
    /// Retrieves an employee by ID.
    /// </summary>
    /// <param name="id">The ID of the employee to be retrieved.</param>
    /// <returns>The employee data.</returns>
    /// <response code="200">Returns the employee data.</response>
    /// <response code="404">If the employee is not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id, string dataType = "json")
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ResponseBase<string>.CreateErrorResponse("Invalid or incomplete request data!"));
        }
        
        try
        {

            var result = await _employeeService.GetEmployee(id);
            return result is null
                ? StatusCode(500,
                    ResponseBase<string>.CreateErrorResponse($"Employee with id {id} does not exist!"))
                : Ok(ResponseBase<Employee>.CreateSuccessResponse(result));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseBase<string>.CreateErrorResponse($"Error occurred while getting the employee: {e.Message}!"));
        }
    }
    
    /// <summary>
    /// Retrieves a range of employee data for a specified period.
    /// </summary>
    /// <param name="id">The id of the employee to retrieve data for.</param>
    /// <param name="startDate">The start date of the range.</param>
    /// <param name="endDate">The end date of the range.</param>
    /// <returns>Returns a list of employee data for the specified period.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ResponseBase<List<Employee>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseBase<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetRange(int id, DateTime startDate, DateTime endDate, string dataType = "json")
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ResponseBase<string>.CreateErrorResponse("Invalid or incomplete request data!"));
        }
        
        try
        {
            var result = (await _employeeSalaryService.GetEmployeeSalariesInRangeAsync(id, startDate, endDate)).ToList();
            return !result.Any()
                ? StatusCode(500,
                    ResponseBase<string>.CreateErrorResponse($"Employee with id {id} does not exist!"))
                : Ok(ResponseBase<List<EmployeeSalary>>.CreateSuccessResponse(result));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseBase<string>.CreateErrorResponse($"Error occurred while getting the employee's info: {e.Message}!"));
        }
    }
}
