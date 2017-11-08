[Code]
procedure UnInstallPrevision(CurStep: TSetupStep);
var 
  ResultCode: Integer; 
  Uninstall: String; 
  sPrevID: String;
begin 
  sPrevID := '{A88C7794-4D6C-4561-B6C4-093861E85DAE}';
  if (CurStep = ssInstall) then begin 
    if RegQueryStringValue(HKLM, 'Software\Microsoft\Windows\CurrentVersion\Uninstall\' + sPrevID + '_is1', 'UninstallString', Uninstall) then begin
      if MsgBox( ExpandConstant('{cm:UninstallPrevision}'), mbInformation, MB_YESNO) = IDYES then
        begin
          Exec(RemoveQuotes(Uninstall), ' /SILENT', '', SW_SHOWNORMAL, ewWaitUntilTerminated, ResultCode); 
        end
      else
        begin
          WizardForm.Close;
        end;
    end; 
  end; 
end;