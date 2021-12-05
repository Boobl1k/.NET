using System.ComponentModel.DataAnnotations.Schema;
using WebApplication.Models;

namespace WebApplication.DB;

[Table($"{nameof(ComputedExpression)}s")]
// ReSharper disable once ClassNeverInstantiated.Global
public class ComputedExpression
{
    public int Id { get; set; }
    public decimal V1 { get; init; }
    public decimal V2 { get; init; }
    public Operation Op { get; init; }
    public decimal? Res { get; set; }
}
