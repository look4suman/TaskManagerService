if not exists (select 1 from sys.databases where name = 'LocalDB')
begin
	create database LocalDB
end
