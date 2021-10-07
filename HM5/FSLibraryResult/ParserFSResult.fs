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

    let TryParsOrQuit (str: string) (result: outref<int>) =
        if Int32.TryParse(str, &result) then
            true
        else
            Console.WriteLine($"value is not int. The value was {str}")
            false
