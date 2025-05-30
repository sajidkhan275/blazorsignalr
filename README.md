# blazorsignalr

git code ->
git init
git remote add origin https://github.com/sajidkhan275/blazorsignalr.git
dotnet new gitignore

pull -> git pull origin main --allow-unrelated-histories
git add .
git commit -m "Initial commit"
git push -u origin main
git push -f origin main
git push -u origin main --force

delete old-> rd /s /q .git
rename the branch -> git branch -M main
check branch -> git branch

git reset --soft HEAD~1: Moves your branch back by 1 commit, but keeps all file changes staged.
git reset: Unstages all the changes (files go back to the "Changes" tab in Git UI).

Remove the File from Git Index -> git rm --cached appsettings.json