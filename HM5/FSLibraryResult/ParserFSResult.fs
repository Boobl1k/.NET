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

    let numberErrorMessage = "value is not int"
    let resultNumber = ResultBuilder(numberErrorMessage)

    //надо эти 4 метода собрать в 1, вызывать в них T.TryParse. хз как это делать
    let ParseInt (str: string) =
        resultNumber {
            let success, result = Int32.TryParse str
            if success then return result
        }

    let ParseDouble (str: string) =
        resultNumber {
            let success, result = Double.TryParse str
            if success then return result
        }

    let ParseFloat (str: string) =
        resultNumber {
            let success, result = Single.TryParse str
            if success then return result
        }

    let ParseDecimal (str: string) =
        resultNumber {
            let success, result = Decimal.TryParse str
            if success then return result
        }
