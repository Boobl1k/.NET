namespace FSLibraryResult

open System

module ParserFs =
    let ParseCalculatorOperation arg =
        match arg with
        | "+" -> CalculatorFs.Operation.Plus
        | "-" -> CalculatorFs.Operation.Minus
        | "*" -> CalculatorFs.Operation.Multiply
        | "/" -> CalculatorFs.Operation.Divide
        | _ -> CalculatorFs.Operation.Unassigned

    let ParseNumber (str: string) =
        let result = ref 0
        if Int32.TryParse(str, result) then
            Ok(!result)
        else
            Error $"value is not int. The value was {str}"