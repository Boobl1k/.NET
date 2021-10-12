namespace FSLibraryResult

type ResultBuilder(errorMessage : string) =
        member b.Zero() = Error errorMessage
        member b.Bind(x, f) =
            match x with
            | Ok x -> f x
            | Error _ -> x
        member b.Return x = Ok x
        member b.Combine(x,f) = f x
        
module CalculatorFs =
    let defaultResult = ResultBuilder("unknown error")
    
    let DevByZero = "val2 was 0"

    type Operation =
        | Plus
        | Minus
        | Divide
        | Multiply

    let inline Calculate (val1: Result<'T, string> when 'T : (static member (+) : 'T * 'T -> 'T)) (val2 : Result<'T, string>) (operation : Operation) : Result<'T, string> =
        match operation with
        | Plus ->
            defaultResult{
                let! val11 = val1
                let! val22 = val2
                return val11 + val22
                }
        | Divide ->
            ResultBuilder(DevByZero){
                let! val11 = val1
                let! val22 = val2
                let zero = val22 - val22
                if val22 <> zero then
                     return val11/val22
            }
        | Minus ->
            defaultResult{
                let! val11 = val1
                let! val22 = val2
                return val11 - val22
            }
        | Multiply ->
            defaultResult{
                let! val11 = val1
                let! val22 = val2
                return val11 * val22
            }
