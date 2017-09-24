echo '[Started] Apply Commerce Migration'
dotnet ef database update --context CommerceContext
echo '[Done] Apply Commerce Migration'
