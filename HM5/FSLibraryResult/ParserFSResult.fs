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
            let res = ref (Int32())

            if Int32.TryParse(str, res) = true then
                return !res
        }

    let ParseDouble (str: string) =
        resultNumber {
            let res = ref (Double())

            if Double.TryParse(str, res) = true then
                return !res
        }

    let ParseFloat (str: string) =
        resultNumber {
            let res = ref (Single())

            if Single.TryParse(str, res) = true then
                return !res
        }

    let ParseDecimal (str: string) =
        resultNumber {
            let res = ref (Decimal())

            if Decimal.TryParse(str, res) = true then
                return !res
        }
