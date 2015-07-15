module sr

open Types

let message args =
    let exampleUsage = "(E.g.: sr -t foo -r bar -d c:\\temp\ -p *.txt)"
    let reportWithExample msg = msg + " " + exampleUsage
    match args with
    |Failure f -> 
        match f with
        |InvalidSearchTerm -> "Please specify the search term" |> reportWithExample
        |InvalidReplacementTerm -> "Please specify the replacement term" |> reportWithExample
        |InvalidDirectory-> "Please specify the search directory" |> reportWithExample
        |InvalidPattern -> "Please specify the file pattern" |> reportWithExample
        |DirectoryNotFound -> "The specified directory could not be found"
        |_ -> "An unexpected error has occured!"
    |Success s -> "Operation complete!"

[<EntryPoint>]
let main argv = 

    let result = 
        argv |> String.concat " " |> ParamsParser.Parse |> ParamsValidator.Validate >>= SearchReplace.Execute

    printfn "%A" (message result)
    0 // return an integer exit code
        