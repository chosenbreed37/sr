module ParamsValidator

open Types

let validateSearchTerm input =
    if input.Term = "" then
        Failure InvalidSearchTerm
    else
        Success input

let validateReplacementTerm input =
    if input.Replacement = "" then
        Failure InvalidReplacementTerm
    else
        Success input

let validateDirectory input =
    if input.Dir = "" then
        Failure InvalidDirectory
    else
        Success input

let validatePattern input =
    if input.Pattern = "" then
        Failure InvalidPattern
    else
        Success input

let Validate input =
    input 
    |> validateSearchTerm 
    >>= validateReplacementTerm
    >>= validateDirectory
    >>= validatePattern
    
