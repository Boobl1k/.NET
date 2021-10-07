namespace FSLibraryResult

module CalculatorFs =
    let DevByZero =
        "val2 was 0"
    let WrongOperation =
        "Wrong operation"
    
    type Operation =
    | Unassigned
    | Plus
    | Minus
    | Divide
    | Multiply
    
    let Calculate (val1:int) (val2:int) operation =
        match operation with
        | Unassigned -> Error WrongOperation
        | Plus -> Ok(val1 + val2)
        | Minus -> Ok(val1 - val2)
        | Divide ->
            try
                Ok(val1 / val2)
            with
            | :? System.DivideByZeroException -> Error DevByZero
        | Multiply -> Ok(val1 * val2)