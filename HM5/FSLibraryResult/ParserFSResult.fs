namespace FSLibraryResult

open System

module ParserFs =
    let wrongOperation = "Wrong operation"

    let parseCalculatorOperation arg =
        match arg with
        | "+" -> Ok CalculatorFs.Operation.Plus
        | "-" -> Ok CalculatorFs.Operation.Minus
        | "*" -> Ok CalculatorFs.Operation.Multiply
        | "/" -> Ok CalculatorFs.Operation.Divide
        | _ -> Error wrongOperation

    let numberErrorMessage = "value is not int"
    let private resultNumber = ResultBuilder(numberErrorMessage)

    //надо эти 4 метода собрать в 1, вызывать в них T.TryParse. хз как это делать
    let parseInt (str: string) =
        resultNumber {
            let success, result = Int32.TryParse str
            if success then return result
        }

    let parseDouble (str: string) =
        resultNumber {
            let success, result = Double.TryParse str
            if success then return result
        }

    let parseFloat (str: string) =
        resultNumber {
            let success, result = Single.TryParse str
            if success then return result
        }

    let parseDecimal (str: string) =
        resultNumber {
            let success, result = Decimal.TryParse str
            if success then return result
        }
