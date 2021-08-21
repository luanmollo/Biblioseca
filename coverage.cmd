@echo off

rem Coverage
rem 	1. Instalar en Biblioseca.Test
rem 		OpenCover
rem 		ReportGenerator
rem 		NUnit
rem 		NUnit.ConsoleRunner
rem 		NUnit3TestAdapter
rem
rem   2. MSTest por NUnit
rem      TestClass -> TestFixture
rem      TestMethod -> Test
rem      TestInitialize -> SetUp
rem      TestCleanUp -> TearDown
rem 		
rem 	3. Ejecutar coverage.cmd
rem 	
rem 	4. Abrir TestResults\index.html 


cmd /c "nuget restore Biblioseca.sln"
cmd /c "msbuild Biblioseca.sln /m /nr:false /t:Rebuild /p:Configuration=Debug /p:Platform="Any CPU" /verbosity:minimal /p:CreateHardLinksForCopyLocalIfPossible=true" /clp:Summary;ShowCommandLine;ErrorsOnly

cmd /c "cd /d Biblioseca.Test && msbuild /m /nr:false /t:Rebuild /p:Configuration=Debug /verbosity:minimal /clp:Summary;ShowCommandLine;ErrorsOnly


mkdir TestResults

packages\OpenCover.4.7.1221\tools\OpenCover.Console.exe -register:user -target:"packages\NUnit.ConsoleRunner.3.12.0\tools\nunit3-console.exe" -targetargs:"Biblioseca.Test\bin\Debug\Biblioseca.Test.dll" -output:TestResults\opencover.xml -register:user -filter:"+[*]* -[Moq*]*-[Biblioseca.Test*]*"

packages\ReportGenerator.4.8.12\tools\net47\ReportGenerator.exe  -reports:TestResults\opencover.xml -targetdir:TestResults
