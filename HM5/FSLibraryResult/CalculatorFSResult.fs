namespace FSLibraryResult

module CalculatorFs =
    let DevByZero = "val2 was 0"

    type Operation =
        | Plus
        | Minus
        | Divide
        | Multiply

    let Calculate (val1: int) (val2: int) operation =
        match operation with
        | Plus -> Ok(val1 + val2)
        | Minus -> Ok(val1 - val2)
        | Divide ->
            try
                Ok(val1 / val2)
            with
            | :? System.DivideByZeroException -> Error DevByZero
        | Multiply -> Ok(val1 * val2)
