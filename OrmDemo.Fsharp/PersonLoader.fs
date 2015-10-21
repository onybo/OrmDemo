module OrmDemo.Fs.PersonLoader

open FSharp.Data
open OrmDemo.Domain

[<Literal>]
let ConnectionString = 
    @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=OrmDemo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"

let LoadPersons (names:seq<string>) =
   let createPerson id name oId oName amount =
      let toString =
         function 
         | Some s -> s
         | None -> ""

      Person(id, (toString name), Organization(oId, (toString oName), amount))

   use cmd = new SqlCommandProvider<"PersonQuery.sql", ConnectionString, ResolutionFolder = "..\OrmDemo\sql">()
   
   seq{
      for r in cmd.Execute() do
         yield createPerson r.Id r.Name r.orgId r.oName r.Amount
   }
