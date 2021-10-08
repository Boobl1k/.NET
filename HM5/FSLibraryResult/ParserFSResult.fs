namespace FSLibraryResult

open System

type ParserFs<'T when 'T : (static member TryParse : string * outref<'T> -> bool)> = class
        member b.WrongOperation = "Wrong operation"

        member b.ParseCalculatorOperation arg =
            match arg with
            | "+" -> Ok CalculatorFs.Operation.Plus
            | "-" -> Ok CalculatorFs.Operation.Minus
            | "*" -> Ok CalculatorFs.Operation.Multiply
            | "/" -> Ok CalculatorFs.Operation.Divide
            | _ -> Error b.WrongOperation

        member b.numberError = "value is not int"

        member inline b.ParseNumber (str:string) (result : outref< Result<'T, string > >) =
            let res = ref (new 'T())
            if T.TryParse(str, res) then
                result = Ok(!res)
            else
                result = Error b.numberError
end
