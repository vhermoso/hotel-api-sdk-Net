@echo Off
set config=%1
if "%config%" == "" (
   set config=Debug
)
 
set version=1.0.0
if not "%PackageVersion%" == "" (
   set version=%PackageVersion%
)

set nuget=
if "%nuget%" == "" (
	set nuget=nuget
)

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild src\hotel-api-sdk.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false

mkdir Build
mkdir Build\lib
mkdir Build\lib\net40

%nuget% pack "HotelBedsSDK.nuspec" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"
