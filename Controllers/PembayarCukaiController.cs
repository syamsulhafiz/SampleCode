using eFilingExcel.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PembayarCukaiController : ControllerBase
{
    private readonly PembayarCukaiService _pembayarCukaiService;

    public PembayarCukaiController(
        PembayarCukaiService pembayarCukaiService)
    {
        _pembayarCukaiService = pembayarCukaiService;
    }

    /// <summary>
    /// Untuk muat naik excel senarai pembayar cukai. Hanya fail .xlsx sahaja yang diterima. Sila pastikan format fail excel adalah betul.
    /// </summary>
    /// <param name="file">Sample file ada dalam folder code nih</param>
    /// <param name="batchId"></param>
    /// <param name="uploadedBy"></param>
    /// <returns></returns>
    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadExcel(IFormFile file, [FromForm] int batchId,[FromForm] string uploadedBy)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Please upload an Excel file.");
        }

        if (!string.Equals(
            Path.GetExtension(file.FileName),
            ".xlsx",
            StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest("Only .xlsx files are supported.");
        }

        try
        {
            using var stream = new MemoryStream();

            await file.CopyToAsync(stream);

            // Reset stream position before reading
            stream.Position = 0;

            // Call existing service
            var result = await _pembayarCukaiService
                .GetPembayarCukaiAsync(stream);

            return Ok(result);
        }
        catch (ClosedXML.Excel.Exceptions.ClosedXMLException)
        {
            return BadRequest("Invalid Excel file.");
        }
        catch (FormatException)
        {
            return BadRequest("Invalid Excel data format.");
        }
    }
}