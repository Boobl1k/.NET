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

    let ParseNumber str (tryParse : string*byref<'T> -> bool) : Result<'T, string> =
        ResultBuilder(numberError){
            let mutable parsed = new 'T()
            if tryParse(str,&parsed) then
                return parsed
        }
        
    let ParseInt str =
        ParseNumber str Int32.TryParse
    
    let ParseDouble str =
        ParseNumber str Double.TryParse
    
    let ParseDecimal str =
        ParseNumber str Decimal.TryParse
        
    let ParseFloat str =
        ParseNumber str Single.TryParse
        