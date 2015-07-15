module ParamsParser

open System.Text.RegularExpressions
open Types

let (|Parameter|_|) (prefix:string) (text:string) =
    if text.StartsWith(prefix) then
        Some(text.Substring(prefix.Length).Trim())
    else
        None

let Split args =
    Regex.Matches(args, @"-(?'option'.)\s+(?'value'[^\s]*)")
    |> Seq.cast
    |> Seq.map (fun (x: Match) -> x.Value.Trim())
    |> Seq.toList

let Parse args =
    let args' = Split args
    let rec Parse' p args' =
        match args' with
        |[] -> p
        |h::t ->
            match h with
            |Parameter "-t" v -> Parse' { p with Term = v } t
            |Parameter "-r" v -> Parse' { p with Replacement = v } t
            |Parameter "-d" v -> Parse' { p with Dir = v.Trim() } t
            |Parameter "-p" v -> Parse' { p with Pattern = v.Trim() } t
            |_ -> Parse' p t
    Parse' { Term = ""; Replacement = ""; Dir = ""; Pattern = "" } args'
