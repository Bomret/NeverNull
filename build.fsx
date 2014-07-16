#r @"FAKE\tools\FakeLib.dll"
open Fake
open Fake.AssemblyInfoFile

RestorePackages()

let name ="NeverNull"
let description = "A Option type that allows readable and bloat free null handling. Provides a growing number of combinators for functional composition."
let id = "1f08d66b-44d7-4616-a82e-250a3817adbd"
let authors = ["Stefan Reichel"]
let tags = "null, functional, NullReferenceException, option, optional"

let solution = name + ".sln"
let builtAssembly = name + ".dll"
let publishDir = "./publish"
let buildDir = "./build"
let testDir = "./test"

let version =
    match buildServer with
        | TeamCity -> buildVersion
        | _ -> "0.5.0"

Target "Clean" (fun _ -> CleanDirs [buildDir; testDir; publishDir])

Target "BuildLibrary" (fun _ ->
    CreateCSharpAssemblyInfo "./NeverNull/Properties/AssemblyInfo.cs"
        [Attribute.Title name
         Attribute.Description description
         Attribute.Guid id
         Attribute.Product name
         Attribute.Version version
         Attribute.FileVersion version]

    MSBuildRelease buildDir "Build" [solution]
    |> Log "Building app: "
)

Target "BuildTests" (fun _ ->
    !! "*.Tests/**/*.csproj"
    |> MSBuildDebug testDir "Build"
    |> Log "Building tests: "
)

Target "Test" (fun _ ->
    !! (testDir @@ "*.Tests.dll")
    |> MSpec (fun p -> {p with HtmlOutputDir = testDir})
)

Target "Package" (fun _ ->
    CopyFiles publishDir !! (buildDir @@ builtAssembly)

    NuGet (fun p ->
        {p with
            Project = name
            Authors = authors
            Description = description
            Tags = tags
            Version = version
            OutputPath = publishDir
            WorkingDir = publishDir
            Files = [builtAssembly, Some "lib/portable-net40+sl50+win+wpa81+wp80", None] })
            "package.nuspec"
)

"Clean"
    ==> "BuildLibrary"
    ==> "BuildTests"
    ==> "Test"
    ==> "Package"

RunTargetOrDefault "Test"
