#r @"FAKE\tools\FakeLib.dll"
open Fake
open Fake.AssemblyInfoFile

RestorePackages()

let buildDir = "./build"
let testDir = "./test"
let testAssemblies = !! (testDir + "/*.Tests.dll")
let version = 
    match buildServer with
        | TeamCity -> buildVersion
        | _ -> "1.5.0"

Target "Clean" (fun _ -> CleanDirs [buildDir; testDir])

Target "BuildLib" (fun _ -> 
    CreateCSharpAssemblyInfo "./NeverNull/Properties/AssemblyInfo.cs"
        [Attribute.Title "NeverNull"
         Attribute.Description "A Option type that allows readable and bloat free null handling."
         Attribute.Guid "1f08d66b-44d7-4616-a82e-250a3817adbd"
         Attribute.Product "NeverNull"
         Attribute.Version version
         Attribute.FileVersion version]

    !! "NeverNull/**/*.csproj"
    |> MSBuildRelease buildDir "Build"
    |> Log "Build output: "
)

Target "BuildTests" (fun _ -> 
    !! "NeverNull.Tests/**/*.csproj"
    |> MSBuildDebug testDir "Build"
    |> Log "Test build output: "
)

Target "Test" (fun _ ->
    testAssemblies
        |> MSpec (fun p -> {p with HtmlOutputDir = testDir})
)

"Clean"
    ==> "BuildLib"
    ==> "BuildTests"
    ==> "Test"

RunTargetOrDefault "Test"