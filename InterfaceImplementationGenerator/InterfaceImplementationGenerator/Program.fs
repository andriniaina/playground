open System.Reflection
open System
open System.Collections
 
let String_Join separator (values:string []) =
    String.Join(separator, values)
 
let rec TypeToString (stringStrategy:Type -> string) (t:Type) = 
    let _TypeToString (genericType:Type) =
        let TypeDefinitionName = stringStrategy (t.GetGenericTypeDefinition())
        let genericArgs = genericType.GetGenericArguments() |> Array.map (TypeToString stringStrategy)
        TypeDefinitionName.Substring(0, TypeDefinitionName.IndexOf('`')) + "<" + String.Join(", ", genericArgs :> string Generic.IEnumerable) + ">"
 
    if not(t.IsGenericType) then stringStrategy t
    else _TypeToString t
 
let processType (t:System.Type) =
 
    let methods = t.GetMethods()
 
    let hasGenerics (parametersArray:System.Type list) =
        parametersArray |> List.exists (fun p ->
                if p=null || p.FullName=null 
                then false
                else (p.IsGenericType)
            )
 
    let generateWrapper (m:System.Reflection.MethodInfo) =
        let parameters_declarations =
            m.GetParameters()
                |> Seq.map (fun p -> sprintf "(%s:%s)" p.Name p.ParameterType.FullName)
                |> Seq.toArray
                |> String_Join " "
        printfn "        member me.%s %s : %s = raise (NotImplementedException()) // TODO"  m.Name parameters_declarations (TypeToString (fun t -> t.FullName) m.ReturnType )
 
    printfn "type Implementation%s() ="  (t.Name)
    printfn "    interface %s with"  (TypeToString (fun t -> t.FullName) t)
    methods
        |> Array.iter generateWrapper
        
processType typeof<System.IEquatable<int>>

type ImplementationIEquatable() =
    interface System.IEquatable<System.Int32> with
        member me.Equals (other:System.Int32) : System.Boolean = raise (NotImplementedException()) // TODO

// END

