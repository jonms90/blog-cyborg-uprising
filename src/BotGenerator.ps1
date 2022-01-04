# Combine the source code files to a single file that can be used for bot programming in CodinGame
# See https://stackoverflow.com/questions/61697680/how-to-combine-simple-cs-files-into-one-cs-file-with-all-usings-at-the-top-so
$usings, $rest = (Get-Content .\BotProgramming.CyborgUprising\*.cs).Where({ $_ -match '^\s*using\s' }, 'Split')
Set-Content bot.cs -Value (@($usings | Select-Object -Unique) + $rest)