#r @"FAKE\tools\FakeLib.dll"
open Fake
open Fake.AssemblyInfoFile

RestorePackages()

let buildDir = "./build"
let net451Dir = buildDir + "/net451"
let net45Dir = buildDir + "/net45"
let net40Dir = buildDir + "/net40"
let net35Dir = buildDir + "/net35"

let testDir = "./test"
let packagingDir = "./package"
let testAssemblies = !! (testDir + "/*.Tests.dll")
let version = 
    match buildServer with
        | TeamCity -> buildVersion
        | _ -> "1.6.0"

Target "Clean" (fun _ -> CleanDirs [buildDir; testDir; packagingDir])

Target "BuildLib" (fun _ -> 
    CreateCSharpAssemblyInfo "./NeverNull/Properties/AssemblyInfo.cs"
        [Attribute.Title "NeverNull"
         Attribute.Description "A Option type that allows readable and bloat free null handling."
         Attribute.Guid "1f08d66b-44d7-4616-a82e-250a3817adbd"
         Attribute.Product "NeverNull"
         Attribute.Version version
         Attribute.FileVersion version]

    !! "NeverNull/**/*.csproj"
    |> MSBuild net451Dir "Build" ["Configuration","Net451"]
    |> Log "Build output: "

    !! "NeverNull/**/*.csproj"
    |> MSBuild net45Dir "Build" ["Configuration","Net45"]
    |> Log "Build output: "

    !! "NeverNull/**/*.csproj"
    |> MSBuild net40Dir "Build" ["Configuration","Net40"]
    |> Log "Build output: "

    !! "NeverNull/**/*.csproj"
    |> MSBuild net35Dir "Build" ["Configuration","Net35"]
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

Target "CreatePackage" (fun _ ->
  CreateDir "package/lib/net451"
  CreateDir "package/lib/net45"
  CreateDir "package/lib/net40"
  CreateDir "package/lib/net35"

  CopyFile "package/lib/net451/NeverNull.dll" "build/net451/NeverNull.dll"
  CopyFile "package/lib/net45/NeverNull.dll" "build/net45/NeverNull.dll"
  CopyFile "package/lib/net40/NeverNull.dll" "build/net40/NeverNull.dll"
  CopyFile "package/lib/net35/NeverNull.dll" "build/net35/NeverNull.dll"

  NuGet (fun p ->
    {p with
        WorkingDir = packagingDir
        OutputPath = packagingDir
        Version = version
        Publish = false
            })
            "NeverNull.nuspec"
)

"Clean"
    ==> "BuildLib"
    ==> "BuildTests"
    ==> "Test"
    ==> "CreatePackage"

RunTargetOrDefault "CreatePackage"