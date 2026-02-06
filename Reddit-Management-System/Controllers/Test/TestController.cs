using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniExcelLibs;
using Reddit_Management_System.Application.Features.Test.Queries;
using System.Data;
using System.Diagnostics;

namespace Reddit_Management_System.Controllers.Test
{
    public static class TestDataGenerator
    {
        public static DataTable GenerateTestData(int rowCount)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Description", typeof(string));

            for (int i = 1; i <= rowCount; i++)
            {
                table.Rows.Add(i, $"Test Name {i}", $"This is a description for test item {i}.");
            }
            return table;
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> TestApi(CancellationToken cancellationToken)
        {
            var query = new TestApiQuery();
            var data = await _mediator.Send(query, cancellationToken);
            return Ok(data);
        }

        [HttpGet("closedxml")]
        public IActionResult GetTestDataAsExcel()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            long startMemory = GC.GetTotalMemory(true);

            var testData = TestDataGenerator.GenerateTestData(50000);
            using var workbook = new ClosedXML.Excel.XLWorkbook();
            var worksheet = workbook.Worksheets.Add("TestData");
            worksheet.Cell(1, 1).InsertTable(testData);
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);

            long endMemory = GC.GetTotalMemory(true);
            double memoryUsedMB = (endMemory - startMemory) / (1024.0 * 1024.0);
            stopwatch.Stop();

            // Add execution time to header
            Response.Headers.Add("Execution-Time-ms", stopwatch.ElapsedMilliseconds.ToString());
            Response.Headers.Add("Memory-Used-MB", memoryUsedMB.ToString("F2"));

            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TestData.xlsx");
        }
        [HttpGet("miniexcel")]
        public IActionResult GetTestDataAsExcelMiniExcel()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            long startMemory = GC.GetTotalMemory(true);

            var testData = TestDataGenerator.GenerateTestData(50000);
            using var stream = new MemoryStream();
            // Corrected the usage of MiniExcel to directly use its SaveAs method
            MiniExcel.SaveAs(stream, testData, printHeader: true, sheetName: "TestData");

            long endMemory = GC.GetTotalMemory(true);
            double memoryUsedMB = (endMemory - startMemory) / (1024.0 * 1024.0);
            stopwatch.Stop();

            // Add execution time to header
            Response.Headers.Add("Execution-Time-ms", stopwatch.ElapsedMilliseconds.ToString());
            Response.Headers.Add("Memory-Used-MB", memoryUsedMB.ToString("F2"));
            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TestData_MiniExcel.xlsx");
        }
    }
}
