#define MyAppName "ArxBuh"
#define MyBuildDir "..\ArxBuh\Bin\Release"
#define MyBuildUpdateDir "..\ArxBuhUpdater\Bin\Release"

#define MyAppExeName "ArxBuh.exe"
#define MyUpdaterExeName "ArxBuhUpdater.exe"

#define use_dotnetfx40

;Get vesion
#dim Version[4]
#expr ParseVersion(MyBuildDir + "\" + MyAppExeName, Version[0], Version[1], Version[2], Version[3])
#define MyAppVersion Str(Version[0]) + "." + Str(Version[1]) + "." + Str(Version[2]) + "." + Str(Version[3])

[Setup]
AppId={{a88c7794-4d6c-4561-b6c4-093861e85dae}
AppName="{#MyAppName} - {cm:Product}"
AppVersion={#MyAppVersion}
AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher=Ivan "Arxont" Lezhnev
AppPublisherURL=https://twitter.com/arxont
DefaultGroupName={#MyAppName}
PrivilegesRequired=lowest
OutputBaseFilename=setup
Compression=lzma
SolidCompression=yes
DefaultDirName={userdocs}\ArxBuh

;Downloading and installing dependencies will only work if the memo/ready page is enabled (default behaviour)
DisableReadyPage=no
DisableReadyMemo=no

ShowLanguageDialog=no
AppContact=arxbuh@itchita.ru
MinVersion=0,5.01

[Languages]
Name: "en"; MessagesFile: "compiler:Default.isl"
Name: "de"; MessagesFile: "compiler:Languages\German.isl"
Name: "ru"; MessagesFile: "compiler:Languages\Russian.isl"

[CustomMessages]
#include '.\Language\en.iss'
#include '.\Language\ru.iss'

[Files]
Source: "{#MyBuildDir}\ArxBuh.exe"; DestDir: "{app}"; Flags: ignoreversion ; Permissions: everyone-full
Source: "{#MyBuildDir}\ArxBuhUpdater.exe"; DestDir: "{app}"; Flags: ignoreversion ; Permissions: everyone-full
Source: "{#MyBuildDir}\*.dll"; DestDir: "{app}"; Flags: ignoreversion ; Permissions: everyone-full

Source: "{#MyBuildDir}\*.config"; DestDir: "{app}"; Flags: ignoreversion ; Permissions: everyone-full

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{userdesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"

[Run]
Filename: "{app}\{#MyAppExeName}"; Flags: nowait postinstall skipifsilent; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"

[Code]
// shared code for installing the products
#include "scripts\products.iss"
// helper functions
#include "scripts\products\stringversion.iss"
#include "scripts\products\winversion.iss"
#include "scripts\products\fileversion.iss"
#include "scripts\products\dotnetfxversion.iss"      

// actual products
#ifdef use_dotnetfx40
#include "scripts\products\dotnetfx40client.iss"
#endif

function InitializeSetup(): boolean;
begin
	// initialize windows version
	initwinversion();
  
#ifdef use_dotnetfx40
	if (not netfxinstalled(NetFx40Client, '') and not netfxinstalled(NetFx40Full, '')) then
		dotnetfx40client();
#endif

	Result := true;
end;