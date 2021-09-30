namespace FSLibrary

module CalculatorFs =
    let DevByZero =
        "val2 was 0"
    let WrongOperation =
        "Wrong operation"
    let OutOfRange =
        "Operation was out of range"
    
    type Operation =
    | Unassigned
    | Plus
    | Minus
    | Divide
    | Multiply
    
    let Calculate (val1:int) (val2:int) operation =
        match operation with
        | Unassigned -> failwith WrongOperation
        | Plus -> val1 + val2
        | Minus -> val1 - val2
        | Divide ->
            try
                val1 / val2
            with
            | :? System.DivideByZeroException -> failwith DevByZero
        | Multiply -> val1 * val2
    