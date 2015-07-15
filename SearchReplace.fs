module SearchReplace

open System
open System.IO

open Types

let getFiles args =
    try
        let files = Directory.EnumerateFiles(args.Dir, args.Pattern, SearchOption.AllDirectories) |> Seq.toList
        Success (args, files)
    with
        | :? DirectoryNotFoundException -> Failure DirectoryNotFound
        | :? IOException -> Failure IOError
        | _ -> Failure IOError

let replace (s:string) (r:string) filename =
    try
        let text = File.ReadAllText(filename)
        Success (text.Replace(s, r))
    with
        | :? IOException -> Failure IOError
        | _ -> Failure IOError

let save filename text =
    try
        File.WriteAllText(filename, text)
        Success Succeeded
    with
        | :? IOException -> Failure IOError
        | _ -> Failure IOError

let apply args =
    try
        let params' = 
            match args with
            |(t1, _) -> t1
        let files = 
            match args with
            |(_, t2) -> t2
        List.map (fun file -> replace params'.Term params'.Replacement file >>= save file) files |> ignore
        Success Succeeded
    with
        | :? IOException -> Failure IOError
        | _ -> Failure IOError

let Execute args = args |> getFiles >>= apply