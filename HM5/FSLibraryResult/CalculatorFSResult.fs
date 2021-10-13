namespace FSLibraryResult

type ResultBuilder(errorMessage: string) =
    member b.Zero() = Error errorMessage

    member b.Bind(x, f) =
        match x with
        | Ok x -> f x
        | Error _ -> x

    member b.Return x = Ok x
    member b.Combine(x, f) = f x

module CalculatorFs =
    let defaultResult = ResultBuilder("unknown error")

    let DevByZero = "val2 was 0"

    type Operation =
        | Plus
        | Minus
        | Divide
        | Multiply

    let inline Calculate
        (val1: Result<'T, string> when 'T: (static member (+) : 'T * 'T -> 'T))
        (val2: Result<'T, string>)
        (operation: Operation)
        =
        ResultBuilder(DevByZero) {
            let! val11 = val1
            let! val22 = val2
            match operation with
            | Plus ->
                return val11 + val22
            | Divide ->
                if val22 <> new 'T() then
                    return val11 / val22
            | Minus ->
                return val11 - val22
            | Multiply ->
                return val11 * val22
        }
