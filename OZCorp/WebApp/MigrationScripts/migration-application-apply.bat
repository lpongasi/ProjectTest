echo '[Started] Apply Application Migration'
dotnet ef database update --context ApplicationDbContext
echo '[Done] Apply Application Migration'
