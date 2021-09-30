namespace FSLibrary

    open System
    
    module ParserFs =
        let ParseCalculatorOperation arg =
            match arg with
            | "+" -> CalculatorFs.Operation.Plus
            | "-" -> CalculatorFs.Operation.Minus
            | "*" -> CalculatorFs.Operation.Multiply
            | "/" -> CalculatorFs.Operation.Divide
            | _ -> CalculatorFs.Operation.Unassigned
            
        let TryParsOrQuit (str:string) (result:outref<int>) =
            let valueRef = ref 0;  
            if Int32.TryParse(str, valueRef) then
                result <- !valueRef
                true
            else
                Console.WriteLine($"value is not int. The value was {str}");
                false