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

    let ParseNumber str parser =
        try
            Ok (parser str)
        with
        | _ -> Error numberError
        
    let ParseInt str =
        ParseNumber str Int32.Parse
    
    let ParseDouble str =
        ParseNumber str Double.Parse
    
    let ParseDecimal str =
        ParseNumber str Decimal.Parse
        
    let ParseFloat str =
        ParseNumber str Single.Parse