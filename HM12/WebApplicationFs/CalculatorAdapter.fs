module WebApplication.CalculatorAdapter

open FSLibraryResult

let calculate val1 val2 operation =
    let val1 = ParserFs.parseDecimal val1
    let val2 = ParserFs.parseDecimal val2
    let operation =
        match operation with
        | "sum" -> "+"
        | "dif" -> "-"
        | "div" -> "/"
        | "mult" -> "*"
        | _ -> ""
    let operation = ParserFs.parseCalculatorOperation operation
    CalculatorFs.calculate val1 val2 operation
