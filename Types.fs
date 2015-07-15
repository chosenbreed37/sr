module Types

type Params = {
    Term:string;
    Replacement:string;
    Dir:string;
    Pattern:string;    
}

type ErrorMessage =
    |InvalidSearchTerm
    |InvalidReplacementTerm
    |InvalidDirectory
    |InvalidPattern

    |DirectoryNotFound
    |IOError

type SuccessMessage =
    |Succeeded

type Result<'T> =
    |Success of 'T
    |Failure of ErrorMessage

let bind switchFunction twoTrackInput =
    match twoTrackInput with
    |Success x -> switchFunction x
    |Failure f -> Failure f

let (>>=) twoTrackInput switchFunction =
    bind switchFunction twoTrackInput

