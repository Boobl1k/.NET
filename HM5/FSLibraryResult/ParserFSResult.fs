namespace FSLibraryResult

open System

module ParserFs =
    let WrongOperation = "Wrong operation"

    let ParseCalculatorOperation arg =
        match arg with
        | "+" -> Ok CalculatorFs.Operation.Plus
        | "-" -> Ok CalculatorFs.Operation.Minus
        | "*" -> Ok CalculatorFs.Operation.Multiply
        | "/" -> Ok CalculatorFs.Operation.Divide
        | _ -> Error WrongOperation

    let numberError = "value is not int"

    let ParseNumber (str: string) =
        let result = ref 0

        if Int32.TryParse(str, result) then
            Ok(!result)
        else
            Error numberError
