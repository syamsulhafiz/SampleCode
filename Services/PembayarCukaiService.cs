using ClosedXML.Excel;
using eFilingExcel.DTOs;

namespace eFilingExcel.Services
{
    public class PembayarCukaiService
    {
        public Task<List<PembayarCukai>> GetPembayarCukaiAsync(
            MemoryStream stream)
        {
            using var excel = new XLWorkbook(stream);

            var worksheet = excel.Worksheet(1);

            var senaraiPembayarCukai = new List<PembayarCukai>();

            // Skip 2 row (tajuk & header)
            var rows = worksheet.RowsUsed().Skip(2);

            foreach (var row in rows)
            {
                // Skip empty rows
                if (row.IsEmpty())
                    continue;

                var pembayarCukai = new PembayarCukai
                {
                    Nama = row.Cell(1).GetString(),
                    TIN = row.Cell(2).GetValue<long>(),
                    TarikhLahir = row.Cell(3).GetDateTime(),
                    JenisPengenalan = row.Cell(4).GetString(),
                    NoPengenalan = row.Cell(5).GetString()
                };

                senaraiPembayarCukai.Add(pembayarCukai);
            }

            return Task.FromResult(senaraiPembayarCukai);
        }
    }
}