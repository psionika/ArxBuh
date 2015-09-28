#define MyAppName "ArxBuh - Домашняя бухгалтерия"
#define MyAppVersion "0.0.1.30"
#define MyAppExeName "ArxBuhUpdater.exe"

#define use_dotnetfx40

[Setup]
AppId={{a88c7794-4d6c-4561-b6c4-093861e85dae}
AppName={#MyAppName}
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
DisableStartupPrompt=False
ShowLanguageDialog=no
AppContact=arxbuh@itchita.ru
MinVersion=0,5.01

[Languages]
Name: "en"; MessagesFile: "compiler:Default.isl"
Name: "de"; MessagesFile: "compiler:Languages\German.isl"
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"

[Files]
;Файлы самой библиотеки
Source: "C:\Users\Пользователь\Dropbox\Progect\ArxBuh\ArxBuh\ArxBuh\bin\Release\ArxBuh.exe"; DestDir: "{app}"; Flags: ignoreversion ; Permissions: everyone-full
Source: "C:\Users\Пользователь\Dropbox\Progect\ArxBuh\ArxBuh\ArxBuh\bin\Release\Microsoft.ReportViewer.Common.dll"; DestDir: "{app}"; Flags: ignoreversion ; Permissions: everyone-full
Source: "C:\Users\Пользователь\Dropbox\Progect\ArxBuh\ArxBuh\ArxBuh\bin\Release\Microsoft.ReportViewer.DataVisualization.dll"; DestDir: "{app}"; Flags: ignoreversion ; Permissions: everyone-full
Source: "C:\Users\Пользователь\Dropbox\Progect\ArxBuh\ArxBuh\ArxBuh\bin\Release\Microsoft.ReportViewer.ProcessingObjectModel.dll"; DestDir: "{app}"; Flags: ignoreversion ; Permissions: everyone-full
Source: "C:\Users\Пользователь\Dropbox\Progect\ArxBuh\ArxBuh\ArxBuh\bin\Release\Microsoft.ReportViewer.WinForms.dll"; DestDir: "{app}"; Flags: ignoreversion ; Permissions: everyone-full
;Файлы необходимые для Updater`a
Source: "C:\Users\Пользователь\Dropbox\Progect\ArxBuh\ArxBuh\ArxBuhUpdater\bin\Release\ArxBuhUpdater.exe"; DestDir: "{app}"; Flags: ignoreversion ; Permissions: everyone-full

; NOTE: Don't use "Flags: ignoreversion" on any shared system files

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